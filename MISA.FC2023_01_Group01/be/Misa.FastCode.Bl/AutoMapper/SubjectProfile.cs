using AutoMapper;
using Misa.FastCode.Bl.Service.Question;
using Misa.FastCode.Bl.Service.Subject;
using Misa.FastCode.Dl.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.FastCode.Bl.AutoMapper
{
    public class SubjectProfile : Profile
    {
        /// <summary>
        /// phương thức khởi tạo
        /// created by: nqhuy(27/06/2023)
        /// </summary>
        public SubjectProfile()
        {
            CreateMap<SubjectEntity, SubjectDto>();
        }
    }
}
