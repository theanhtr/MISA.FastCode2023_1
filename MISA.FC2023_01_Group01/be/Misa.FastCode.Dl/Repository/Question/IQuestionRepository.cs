using Misa.FastCode.Dl.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.FastCode.Dl.Repository.Question
{
    public interface IQuestionRepository : IBaseRepository<QuestionEntity>
    {
        Task<IEnumerable<QuestionEntity>> FilterAsync(int pageSize, int currentPage, Guid? subjectId, string? textSearch);
       
    }


}
