using Misa.FastCode.Dl.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.FastCode.Dl.Repository.Answer
{
    public interface IAnswerRepository : IBaseRepository<AnswerEntity>
    {
        Task<IEnumerable<AnswerEntity>> GetListByQuestionIdAsync(Guid questionId);
    }
}
