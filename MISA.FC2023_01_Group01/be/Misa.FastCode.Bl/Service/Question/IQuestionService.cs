using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.FastCode.Bl.Service.Question
{
    public interface IQuestionService : ICRUDBaseService<QuestionDto, QuestionUpdateDto, QuestionCreateDto>
    {
        Task<IEnumerable<QuestionDto>> FilterAsync(int pageSize, int currentPage, Guid? subjectId, string? textSearch);
    }
}
