using Misa.FastCode.Dl.Entity;
using Misa.FastCode.Dl.unitOfWork;
using Misa.FastCode.DL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.FastCode.Dl.Repository.Subject
{
    public class SubjectRepository : BaseRepository<SubjectEntity>, ISubjectRepository
    {
        public SubjectRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override string GetTableName()
        {
            return "subject";
        }
    }
}
