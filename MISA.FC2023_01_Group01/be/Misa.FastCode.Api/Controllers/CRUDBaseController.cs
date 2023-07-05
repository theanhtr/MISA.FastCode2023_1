using Microsoft.AspNetCore.Mvc;
using Misa.FastCode.Bl.Service;
using Misa.Web202303.QLTS.BL.Service;
using System.Net;

namespace Misa.FastCode.Api.Controllers
{
    public abstract class CRUDBaseController<TEntityDto, TEntityUpdateDto, TEntityCreateDto> : ReadBaseController<TEntityDto>
    {
        protected readonly ICRUDBaseService<TEntityDto, TEntityUpdateDto, TEntityCreateDto> _crudBaseService;
        protected CRUDBaseController(ICRUDBaseService<TEntityDto, TEntityUpdateDto, TEntityCreateDto> crudBaseService) : base(crudBaseService)
        {
            _crudBaseService = crudBaseService;
        }

        // <summary>
        /// sửa 1 bản ghi
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        /// <param name="id">id tài nguyên cần sửa</param>
        /// <param name="entityUpdateDto">dữ liệu tài nguyên cần sửa</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] TEntityUpdateDto entityUpdateDto)
        {
            await _crudBaseService.UpdateAsync(id, entityUpdateDto);
            return Ok();
        }

        /// <summary>
        /// thêm 1 bản ghi
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        /// <param name="entityCreateDto">dữ liệu tài nguyên cần thêm</param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] TEntityCreateDto entityCreateDto)
        {
            await _crudBaseService.InsertAsync(entityCreateDto);
            return StatusCode((int)HttpStatusCode.Created);
        }
    }
}
