using AutoMapper;
using Misa.FastCode.Common.Resource;
using Misa.FastCode.Dl.Entity;
using Misa.FastCode.Dl.Repository;
using Misa.FastCode.Dl.Repository.Subject;
using Misa.FastCode.Dl.unitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.FastCode.Bl.Service.Subject
{
    public class SubjectService : ReadBaseService<SubjectEntity, SubjectDto>, ISubjectService
    {
        public SubjectService(ISubjectRepository subjectRepository, IUnitOfWork unitOfWork, IMapper mapper) : base(subjectRepository, unitOfWork, mapper)
        {
        }

        protected override string GetAssetName()
        {
            return AssetName.Subject;
        }
    }
}
