using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web202303.QLTS.BL.Service
{
    /// <summary>
    /// định nghĩa các phương thức chung của service
    /// created by: nqhuy(21/05/2023)
    /// </summary>
    /// <typeparam name="TEntityDto">dto để lấy dữ liệu</typeparam>
    public interface IReadBaseService<TEntityDto>
    {
        /// <summary>
        /// lấy ra 1 bản thi theo id
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        /// <param name="id">id tài nguyên cần lấy</param>
        /// <returns></returns>
        Task<TEntityDto> GetAsync(Guid id);

        /// <summary>
        /// lấy ra tất cả bản ghi
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        /// <returns>tất cả tài nguyên trong 1 bảng</returns>
        Task<IEnumerable<TEntityDto>> GetAsync();

        /// <summary>
        /// kiểm tra mã code bị trùng khi thêm hoạc sửa
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        /// <param name="code">mã code</param>
        /// <param name="id">id (là rỗng trong trường hợp thêm mới)</param>
        /// <returns></returns>
        Task<bool> CheckCodeExisted(string code, Guid? id);
      
    }
}
