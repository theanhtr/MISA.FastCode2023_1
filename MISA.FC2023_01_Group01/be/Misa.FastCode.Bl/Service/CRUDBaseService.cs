using AutoMapper;
using Misa.FastCode.Bl.ValidateDto;
using Misa.FastCode.Common.Const;
using Misa.FastCode.Common.Emum;
using Misa.FastCode.Common.Error;
using Misa.FastCode.Common.Exceptions;
using Misa.FastCode.Common.Resource;
using Misa.FastCode.Dl.Repository;
using Misa.FastCode.Dl.unitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.FastCode.Bl.Service
{
    public abstract class CRUDBaseService<TEntity, TEntityDto, TEntityUpdateDto, TEntityCreateDto> : ReadBaseService<TEntity, TEntityDto>, ICRUDBaseService<TEntityDto, TEntityUpdateDto, TEntityCreateDto>
    {
        protected readonly IBaseRepository<TEntity> _baseRepository;

        protected readonly IMapper _mapper;

        protected readonly IUnitOfWork _unitOfWork;

        public CRUDBaseService(IBaseRepository<TEntity> baseRepository, IUnitOfWork unitOfWork, IMapper mapper) : base(baseRepository, unitOfWork, mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// xóa nhiều bản ghi cùng lúc dựa vào danh sách id
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        /// <param name="listId">danh sách id tài nguyên cần xóa</param>
        /// <exception cref="ValidateException">throw exception khi không tồn tại tài nguyên</exception>
        /// <returns></returns>
        public async Task DeleteListAsync(IEnumerable<Guid> listId)
        {
            // nối danh sách id lại thành string cách nhau bởi dấu ,
            var listIdString = string.Join(",", listId);
            // kiểm tra có ít nhất bản ghi không tồn tại
            var sumOfExisted = await _baseRepository.GetSumExistedOfListAsync(listIdString);
            if (sumOfExisted != listId.Count())
            {
                throw new ValidateException()
                {
                    ErrorCode = ErrorCode.NotFound,
                    UserMessage = string.Format(ErrorMessage.NotFoundDeleteError, GetAssetName())
                };

            }
            await _baseRepository.DeleteListAsync(listIdString);

            await _unitOfWork.CommitAsync();
        }

        /// <summary>
        /// thêm 1 bản ghi
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        /// <param name="entityCreateDto">dữ liệu tài nguyên thêm mới</param>
        /// <exception cref="ValidateException">throw exception khi validate lỗi</exception>
        /// <returns></returns>
        public async Task InsertAsync(TEntityCreateDto entityCreateDto)
        {
            // validate Attr
            var attributeErrors = ValidateAttribute.Validate(entityCreateDto);
            // validate riêng
            var createValidateErrors = await CreateValidateAsync(entityCreateDto);

            var listError = Enumerable.Concat(attributeErrors, createValidateErrors).ToList();

            // nếu validate có lỗi thì throw exception
            if (listError.Count > 0)
            {
                throw new ValidateException()
                {
                    ErrorCode = ErrorCode.DataValidate,
                    Data = listError,
                    UserMessage = string.Join("", listError.Select(error => $"<span>{error.Message}</span>"))

                };
            }
            var entity = _mapper.Map<TEntity>(entityCreateDto);
            await _baseRepository.InsertAsync(entity);

            await _unitOfWork.CommitAsync();
        }

        /// <summary>
        /// phương thức update 1 bản ghi
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        /// <param name="entityId">id tài sản cần sửa</param>
        /// <param name="entityUpdateDto">dữ liệu tài sản cần sửa</param>
        /// <exception cref="ValidateException">throw exception khi validate lỗi</exception>
        /// <returns></returns>
        public async Task UpdateAsync(Guid entityId, TEntityUpdateDto entityUpdateDto)
        {
            // kiểm tra bản ghi không tồn tại
            var oldEntity = await _baseRepository.GetAsync(entityId);
            if (oldEntity == null)
            {
                throw new NotFoundException()
                {
                    ErrorCode = ErrorCode.NotFound,
                    UserMessage = string.Format(ErrorMessage.NotFoundUpdateError, GetAssetName())
                };
            }
            var attributeErrors = ValidateAttribute.Validate(entityUpdateDto);
            // validate riêng
            var updateValidateErrors = await UpdateValidateAsync(entityId, entityUpdateDto);

            var listError = Enumerable.Concat(attributeErrors, updateValidateErrors).ToList();

            if (listError.Count > 0)
            {
                throw new ValidateException()
                {
                    ErrorCode = ErrorCode.DataValidate,
                    Data = listError,
                    UserMessage = string.Join("", listError.Select(error => $"<span>{error.Message}</span>"))
                };
            }

            var entity = _mapper.Map<TEntity>(entityUpdateDto);
            await _baseRepository.UpdateAsync(entityId, entity);

            await _unitOfWork.CommitAsync();
        }


        /// <summary>
        /// validate khi update dữ liệu
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        /// <param name="id">id tài nguyên</param>
        /// <param name="entityUpdateDto">dữ liệu entity cần valdiate</param>
        /// <returns>danh sách lỗi</returns>
        protected virtual async Task<List<ValidateError>> UpdateValidateAsync(Guid id, TEntityUpdateDto entityUpdateDto)
        {
            var listError = new List<ValidateError>();

            return listError;
        }

        /// <summary>
        /// validate khi update dữ liệu
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        /// <param name="entityCreateDto">dữ liệu entity cần validate</param>
        /// <returns>danh sách lỗi</returns>
        protected virtual async Task<List<ValidateError>> CreateValidateAsync(TEntityCreateDto entityCreateDto)
        {
            var listError = new List<ValidateError>();

            return listError;
        }


        /// <summary>
        /// lấy ra tên tài nguyên
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        /// <returns>tên tài nguyên</returns>
        protected override abstract string GetAssetName();
    }
}
