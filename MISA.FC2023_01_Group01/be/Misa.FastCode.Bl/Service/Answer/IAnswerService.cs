using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.FastCode.Bl.Service.Answer
{
    public interface IAnswerService : ICRUDBaseService<AnswerDto, AnswerUpdateDto, AnswerCreateDto>
    {
        Task<IEnumerable<AnswerDto>> GetListByQuestionIdAsync(Guid questionId);
    }
}
