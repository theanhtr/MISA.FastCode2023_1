using Misa.Web202303.QLTS.BL.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.FastCode.Bl.Service
{
    /// <summary>
    /// định nghĩa các phương thức chung của service
    /// created by: nqhuy(21/05/2023)
    /// </summary>
    /// <typeparam name="TEntityDto">dto để lấy dữ liệu</typeparam>
    /// <typeparam name="TEntityUpdateDto">dto để update dữ liệu</typeparam>
    /// <typeparam name="TEntityCreateDto">dto để thêm dữ liệu</typeparam>
    public interface ICRUDBaseService<TEntityDto, TEntityUpdateDto, TEntityCreateDto> : IReadBaseService<TEntityDto>
    {
        /// <summary>
        /// thêm 1 bản ghi
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        /// <param name="entityDto">dữ liệu tài nguyên thêm mới</param>
        /// <returns></returns>
        Task InsertAsync(TEntityCreateDto entityDto);


        /// <summary>
        /// phương thức update 1 bản ghi
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        /// /// <param name="entityId">id tài nguyên cần sửa</param>
        /// <param name="entityDto">dữ liệu tài nguyên cần sửa</param>
        /// <returns></returns>
        Task UpdateAsync(Guid entityId, TEntityUpdateDto entityDto);


        /// <summary>
        /// xóa nhiều bản ghi cùng lúc dựa vào danh sách id
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        /// <param name="listId">danh sách id tài nguyên cần xóa</param>
        /// <returns></returns>
        Task DeleteListAsync(IEnumerable<Guid> listId);
    }
}
