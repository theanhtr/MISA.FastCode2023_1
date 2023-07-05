using AutoMapper;
using Misa.FastCode.Common.Emum;
using Misa.FastCode.Common.Exceptions;
using Misa.FastCode.Common.Resource;
using Misa.FastCode.Dl.Repository;
using Misa.FastCode.Dl.unitOfWork;
using Misa.Web202303.QLTS.BL.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.FastCode.Bl.Service
{
    public abstract class ReadBaseService<TEntity, TEntityDto> : IReadBaseService<TEntityDto>
    {
        protected readonly IBaseRepository<TEntity> _baseRepository;

        protected readonly IMapper _mapper;

        protected readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// hàm khởi tạo
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        /// <param name="baseRepository">baseRepository</param>
        /// <param name="mapper">mapper</param>
        public ReadBaseService(IBaseRepository<TEntity> baseRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// kiểm tra mã code bị trùng khi thêm hoạc sửa
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        /// <param name="code">mã code</param>
        /// <param name="id">id (là rỗng trong trường hợp thêm mới)</param>
        /// <returns>false nếu không tồn tại, true nếu tồn tại</returns>
        public async Task<bool> CheckCodeExisted(string code, Guid? id)
        {
            var result = await _baseRepository.CheckCodeExistedAsync(code, id);

            await _unitOfWork.CommitAsync();


            return result;
        }

        /// <summary>
        /// lấy ra 1 bản thi theo id
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        /// <param name="id">id tài nguyên cần lấy</param>
        /// <exception cref="NotFoundException">exception trong trường hợp không tìm thấy tài nguyên</exception>
        public async Task<TEntityDto> GetAsync(Guid id)
        {
            var entity = await _baseRepository.GetAsync(id);
            //bản ghi không tồn tại throw ra excception
            if (entity == null)
            {
                throw new NotFoundException()
                {
                    ErrorCode = ErrorCode.NotFound,
                    UserMessage = string.Format(ErrorMessage.NotFoundError, GetAssetName())
                };
            }
            var entityDto = _mapper.Map<TEntityDto>(entity);

            await _unitOfWork.CommitAsync();

            return entityDto;
        }


        /// <summary>
        /// lấy ra tất cả bản ghi
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        /// <returns>tất cả tài nguyên trong 1 bảng</returns>
        public async Task<IEnumerable<TEntityDto>> GetAsync()
        {

            var listTEntity = await _baseRepository.GetAsync();

            var result = listTEntity.Select(entity => _mapper.Map<TEntityDto>(entity));

            await _unitOfWork.CommitAsync();

            return result;
        }


        /// <summary>
        /// lấy ra tên tài nguyên
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        /// <returns>tên tài nguyên</returns>
        protected abstract string GetAssetName();
    }
}
