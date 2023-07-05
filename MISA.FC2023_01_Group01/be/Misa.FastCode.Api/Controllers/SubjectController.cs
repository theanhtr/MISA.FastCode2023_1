using Misa.FastCode.Bl.Service.Subject;
using Misa.Web202303.QLTS.BL.Service;

namespace Misa.FastCode.Api.Controllers
{
    public class SubjectController : ReadBaseController<SubjectDto>
    {
        public SubjectController(ISubjectService subjectService) : base(subjectService)
        {
        }
    }
}
