using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Mvc;
using Misa.FastCode.Bl.Service;
using Misa.FastCode.Bl.Service.Answer;

namespace Misa.FastCode.Api.Controllers
{
    public class AnswerController : CRUDBaseController<AnswerDto, AnswerUpdateDto, AnswerCreateDto>
    {
        private readonly IAnswerService _answerService;
        public AnswerController(IAnswerService answerService) : base(answerService)
        {
           _answerService= answerService;
        }
        [HttpGet("byQuestionId")]
        public async Task<IActionResult> GetListByQuestionId(Guid questionId)
        {
            var result = await _answerService.GetListByQuestionIdAsync(questionId);
            return Ok(result);
        }
    }
}
