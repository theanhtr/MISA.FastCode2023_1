﻿using Dapper;
using Microsoft.Extensions.Configuration;
using Misa.FastCode.Common.Const;
using Misa.FastCode.Dl.Repository;
using Misa.FastCode.Dl.unitOfWork;
using Misa.FastCode.Common.Const;
using Misa.FastCode.Dl.unitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Misa.FastCode.DL.Repository
{
    /// <summary>
    /// thực thi các phương thức của IBaseRepository
    /// Created by: NQ Huy(20/05/2023)
    /// </summary>
    /// <typeparam name="TEntity">entity để lấy dữ liệu từ database</typeparam>
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        #region
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region
        /// <summary>
        /// mở kết nối
        /// Created by: NQ Huy(20/05/2023)
        /// </summary>
        /// <returns>DbConnection</returns>
        protected async Task<DbConnection> GetOpenConnectionAsync()
        {
            var connection = await _unitOfWork.GetDbConnectionAsync();
            return connection;
        }

        /// <summary>
        /// lấy 1 bản ghi theo id
        /// Created by: NQ Huy(20/05/2023)
        /// </summary>
        /// <param name="id">id tài nguyên cần lấy</param>
        /// <returns>tài nguyên cần lấy</returns>
        public virtual async Task<TEntity?> GetAsync(Guid id)
        {
            var connection = await GetOpenConnectionAsync();
            var tableName = this.GetTableName();
            // tạo entity để lấy ra các trường cần thiết, tránh việc dùng select * 
            var entity = (TEntity)Activator.CreateInstance(typeof(TEntity));
            var props = entity.GetType().GetProperties();
            // tạo lệnh sql
            var sql = $"SELECT " + string.Join(", ", props.Select(prop => prop.Name)) + $" FROM {tableName} WHERE {tableName}_id = @id";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("id", id);

            var result = await connection.QueryFirstOrDefaultAsync<TEntity>(sql, dynamicParams);

            return result;
        }

        /// <summary>
        /// lấy tất cả bản ghi
        /// Created by: NQ Huy(20/05/2023)
        /// </summary>
        /// <returns>danh sách bản ghi</returns>
        public virtual async Task<IEnumerable<TEntity>> GetAsync()
        {
            var connection = await GetOpenConnectionAsync();
            // tạo entity để lấy ra các trường cần thiết, tránh việc dùng select * 
            var entity = (TEntity)Activator.CreateInstance(typeof(TEntity));
            var props = entity.GetType().GetProperties();
            // tạo lệnh sql
            var sql = $"SELECT " + string.Join(", ", props.Select(prop => prop.Name)) + $" FROM {this.GetTableName()}";

            var result = await connection.QueryAsync<TEntity>(sql);

            return result;
        }

        /// <summary>
        /// cập nhật bản ghi
        /// Created by: NQ Huy(20/05/2023)
        /// </summary>
        /// <param name="entity">dữ liệu bản ghi cập nhật</param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(Guid entityId, TEntity entity)
        {
            var connection = await GetOpenConnectionAsync();
            // tên của procedure
            //var sql = $"proc_update_{this.GetTableName()}";
            var tableName = this.GetTableName();
            // khởi tạo câu lệnh sql
            var sql = $"UPDATE {tableName} SET ";
            var notNullProps = entity.GetType().GetProperties().Where(p => p.GetValue(entity) != null);
            sql += string.Join(", ", notNullProps.Select(p => $"{p.Name} = @{p.Name}"));
            sql += $" WHERE {tableName}_id = @{tableName}_id";

            var dynamicParams = new DynamicParameters();

            // tên của param trong procedure giống với tên property
            foreach (var prop in notNullProps)
            {
                dynamicParams.Add(prop.Name, prop.GetValue(entity));
            }

            dynamicParams.Add($"{tableName}_id", entityId);

            var transaction = await _unitOfWork.GetTransactionAsync();

            await connection.ExecuteAsync(sql, dynamicParams, transaction);
        }


        /// <summary>
        /// thêm bản ghi
        /// Created by: NQ Huy(20/05/2023)
        /// </summary>
        /// <param name="entity">dữ liệu bản ghi thêm mới</param>
        /// <returns></returns>
        public virtual async Task InsertAsync(TEntity entity)
        {
            var connection = await GetOpenConnectionAsync();

            var tableName = this.GetTableName();
            // tạo lệnh sql

            var sql = $"INSERT INTO {tableName} (";

            var notNullProps = entity.GetType().GetProperties().Where(p => p.GetValue(entity) != null);
            sql += string.Join(", ", notNullProps.Select(p => p.Name));
            sql += ") VALUES (";
            sql += string.Join(", ", notNullProps.Select(p => $"@{p.Name}"));
            sql += ")";
            var dynamicParams = new DynamicParameters();
            // thêm các param (tên param giống với tên thuộc tính của FixedAsset)
            foreach (var prop in notNullProps)
            {
                dynamicParams.Add(prop.Name, prop.GetValue(entity));
            }
            dynamicParams.Add($"{tableName}_id", Guid.NewGuid());

            var transaction = await _unitOfWork.GetTransactionAsync();

            await connection.ExecuteAsync(sql, dynamicParams, transaction);
        }

        /// <summary>
        /// kiểm tra mã code đã tồn tại khi thêm hoạc sửa
        /// Created by: NQ Huy(20/05/2023)
        /// </summary>
        /// <param name="code">mã code</param>
        /// <param name="id">id (là rỗng trong trường hợp thêm mới)</param>
        /// <returns>false nếu tài nguyên không tồn tại, true nếu tồn tại</returns>
        public virtual async Task<bool> CheckCodeExistedAsync(string code, Guid? id)
        {
            var tableName = this.GetTableName();
            // tạo lệnh sql
            var sql = $"SELECT {tableName}_id FROM {tableName} WHERE {tableName}_code = @code AND (LENGTH(@id) = 0 OR {tableName}_id != @id)";

            var connection = await GetOpenConnectionAsync();
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("code", code);
            dynamicParams.Add("id", id == null ? "" : id);

            var entity = await connection.QueryFirstOrDefaultAsync(sql, dynamicParams);
            return entity != null;
        }

        /// <summary>
        /// xóa nhiều bản ghi dựa vào chuỗi danh sách id, tách nhau bởi dấu ","
        /// Created by: NQ Huy(20/05/2023)
        /// </summary>
        /// <param name="listId">danh sách id</param>
        /// <returns></returns>
        public virtual async Task DeleteListAsync(string listId)
        {
            var tableName = GetTableName();
            var connection = await GetOpenConnectionAsync();

            var sql = $"DELETE FROM {tableName} WHERE FIND_IN_SET({tableName}_id, @list_id)";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("list_id", listId);

            var transaction = await _unitOfWork.GetTransactionAsync();

            await connection.ExecuteAsync(sql, dynamicParams, transaction);
        }

        /// <summary>
        /// lấy ra tổng số bản ghi tồn tại trong danh sách chuỗi id
        /// Created by: NQ Huy(20/05/2023)
        /// </summary>
        /// <param name="listId">danh sách id</param>
        /// <returns>số bản ghi tồn tại trong danh sách chuỗi id</returns>
        public virtual async Task<int> GetSumExistedOfListAsync(string listId)
        {
            var tableName = GetTableName();
            var connection = await GetOpenConnectionAsync();
            var dynamicParams = new DynamicParameters();
            var sql = $"SELECT COUNT(*) FROM {tableName} WHERE FIND_IN_SET({tableName}_id, @list_id)";
            dynamicParams.Add("list_id", listId);
            var result = await connection.QueryFirstAsync<int>(sql, dynamicParams);
            return result;
        }

        /// <summary>
        /// thêm nhiều bản ghi cùng lúc, dùng cho khi import file
        /// Created by: NQ Huy(20/05/2023)
        /// </summary>
        /// <param name="listEntity">danh sách tài nguyên cần thêm</param>
        /// <returns></returns>
        public virtual async Task InsertListAsync(IEnumerable<TEntity> listEntity)
        {
            var tableName = GetTableName();
            var connection = await GetOpenConnectionAsync();
            var dynamicParams = new DynamicParameters();

            var sql = "";

            var index = 0;
            // tạo lệnh sql và add dynamic param
            foreach (var entity in listEntity)
            {
                var notNullProps = entity.GetType().GetProperties().Where(prop => prop.GetValue(entity) != null);
                sql += $"INSERT INTO {tableName} (";
                sql += string.Join(", ", notNullProps.Select(prop => prop.Name));
                sql += ") Values (";
                sql += string.Join(", ", notNullProps.Select(prop => $"@{prop.Name}_{index}"));
                sql += ");";

                foreach (var prop in notNullProps)
                {
                    dynamicParams.Add($"{prop.Name}_{index}", prop.GetValue(entity));
                }

                index++;
            }

            var transaction = await _unitOfWork.GetTransactionAsync();

            await connection.ExecuteAsync(sql, dynamicParams, transaction);



        }

        /// <summary>
        /// kiểm tra các mã code tồn tại trong listCode khi thêm nhiều tài sản
        /// Created by: NQ Huy(20/05/2023)
        /// </summary>
        /// <param name="listCode">danh sách mã tài sản</param>
        /// <returns>danh sánh mã tài sản tồn tại</returns>
        public virtual async Task<IEnumerable<string>> GetListExistedCodeAsync(string listCode)
        {
            var connection = await GetOpenConnectionAsync();
            var tableName = GetTableName();
            var dynamicParams = new DynamicParameters();
            var sql = $"SELECT {tableName}_code FROM {tableName} WHERE FIND_IN_SET({tableName}_code, @list_code)";
            dynamicParams.Add("@list_code", listCode);

            var result = await connection.QueryAsync<string>(sql, dynamicParams);
            return result;
        }

        /// <summary>
        /// lấy mã tài nguyên có cùng tiền tố với mã tài nguyên được thêm hoạc sửa gần nhất và có hậu tố lớn nhất
        /// Created by: NQ Huy(20/05/2023)
        /// </summary>
        /// <returns>danh sách chứa mã tài nguyên và tiền tố</returns>
        public virtual async Task<List<string>> GetRecommendCodeAsync()
        {
            var connection = await GetOpenConnectionAsync();
            var sql = ProcedureName.GET_MAX_FIXED_ASSET_CODE;
            var tableName = GetTableName();
            // thêm các param
            var dynamicParams = new DynamicParameters();
            // thêm param output để lấy ra tiền tố của mã tài sản, và tài sản có hậu tố lớn nhất
            dynamicParams.Add("prefix_code", dbType: DbType.String, direction: ParameterDirection.Output);
            dynamicParams.Add("max_code", dbType: DbType.String, direction: ParameterDirection.Output);
            dynamicParams.Add("tbn", tableName);
            await connection.ExecuteAsync(sql, dynamicParams, commandType: CommandType.StoredProcedure);

            var result = new List<string>();
            // lấy ra tiền tố
            var preFix = dynamicParams.Get<string>("prefix_code");
            // lấy ra mã tài sản lớn nhất
            var maxAssetCode = dynamicParams.Get<string>("max_code");

            result.Add(preFix);
            result.Add(maxAssetCode);


            return result;
        }



        #endregion

        #region
        /// <summary>
        /// lấy ra tên cụ thể của table ứng với repository
        /// Created by: NQ Huy(20/05/2023)
        /// </summary>
        /// <returns>tên của table ứng với repository</returns>
        public abstract string GetTableName();
        #endregion

    }
}
