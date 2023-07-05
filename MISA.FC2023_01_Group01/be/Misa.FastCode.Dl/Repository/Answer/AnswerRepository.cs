using Dapper;
using Misa.FastCode.Dl.Entity;
using Misa.FastCode.Dl.unitOfWork;
using Misa.FastCode.DL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.FastCode.Dl.Repository.Answer
{
    public class AnswerRepository : BaseRepository<AnswerEntity>, IAnswerRepository
    {
        public AnswerRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<AnswerEntity>> GetListByQuestionIdAsync(Guid questionId)
        {
            var connection = await GetOpenConnectionAsync();
            var tableName = this.GetTableName();
            var sql = $"SELECT * FROM {tableName} WHERE question_id = @questionId";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("@questionId", questionId);

            var listEntity = await connection.QueryAsync<AnswerEntity>(sql, dynamicParams);
            return listEntity;
        }

        public override string GetTableName()
        {
            return "answer";
        }
    }
}
