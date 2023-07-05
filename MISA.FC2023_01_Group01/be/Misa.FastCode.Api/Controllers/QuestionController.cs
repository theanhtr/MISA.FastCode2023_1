using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Mvc;
using Misa.FastCode.Bl.Service;
using Misa.FastCode.Bl.Service.Question;

namespace Misa.FastCode.Api.Controllers
{
    public class QuestionController : CRUDBaseController<QuestionDto, QuestionUpdateDto, QuestionCreateDto>
    {
        private readonly IQuestionService _questionService;
        public QuestionController(IQuestionService questionService) : base(questionService)
        {
            _questionService= questionService;
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterAsync(int pageSize, int currentPage, Guid? subjectId, string? textSearch)
        {
            var result = await _questionService.FilterAsync(pageSize, currentPage, subjectId, textSearch);
            return Ok(result);
        }
    }
}
