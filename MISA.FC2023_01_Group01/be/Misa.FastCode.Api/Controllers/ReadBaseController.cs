using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Misa.Web202303.QLTS.BL.Service;
using System.Net;

namespace Misa.FastCode.Api.Controllers
{

    /// <summary>
    /// controllerbase định nghĩa các phương thức chung, các controller khác kế thừa từ controllerbase
    /// created by: nqhuy(21/05/2023)
    /// </summary>
    /// <typeparam name="TEntityDto">entity để nhận dữ liệu</typeparam>
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ReadBaseController<TEntityDto> : ControllerBase
    {
        protected readonly IReadBaseService<TEntityDto> _readBaseService;

        public ReadBaseController(IReadBaseService<TEntityDto> readBaseService)
        {
            _readBaseService= readBaseService;
        }

        /// <summary>
        /// phương thức lấy ra 1 bản ghi theo id
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        /// <param name="id">id tài nguyên cần lấy</param>
        /// <returns>tài nguyên cần lấy</returns>
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetAsync(Guid id)
        {
            var entityDto = await _readBaseService.GetAsync(id);
            return Ok(entityDto);
        }

        /// <summary>
        /// phương thức lấy rất cả bản ghi
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        /// <returns>tất cả tài nguyên trong 1 bảng</returns>
        [HttpGet]
        public virtual async Task<IActionResult> GetAsync()
        {
            var result = await _readBaseService.GetAsync();
            return Ok(result);
        }
    }
}
