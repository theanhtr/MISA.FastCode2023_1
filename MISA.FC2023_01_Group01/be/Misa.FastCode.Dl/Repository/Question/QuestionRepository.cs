using Dapper;
using Misa.FastCode.Common.Const;
using Misa.FastCode.Dl.Entity;
using Misa.FastCode.Dl.unitOfWork;
using Misa.FastCode.DL.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.FastCode.Dl.Repository.Question
{
    public class QuestionRepository : BaseRepository<QuestionEntity>, IQuestionRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public QuestionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<QuestionEntity>> FilterAsync(int pageSize, int currentPage, Guid? subjectId, string? textSearch)
        {
            var connection = await GetOpenConnectionAsync();
            var sql = ProcedureName.Filter_Question;
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("page_size", pageSize);
            dynamicParams.Add("current_page", currentPage);
            dynamicParams.Add("subject_id", subjectId == null? "" : subjectId);
            dynamicParams.Add("text_search", textSearch);
            
            var listQuestion = await connection.QueryAsync<QuestionEntity>(sql, dynamicParams, commandType: CommandType.StoredProcedure);
            return listQuestion;
        }

        public override string GetTableName()
        {
            return "question";
        }
    }
}
