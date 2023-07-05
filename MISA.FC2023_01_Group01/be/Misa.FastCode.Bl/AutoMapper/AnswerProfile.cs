using AutoMapper;
using Misa.FastCode.Bl.Service.Answer;
using Misa.FastCode.Bl.Service.Question;
using Misa.FastCode.Dl.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.FastCode.Bl.AutoMapper
{
    public class AnswerProfile : Profile
    {

        /// <summary>
        /// phương thức khởi tạo
        /// created by: nqhuy(27/06/2023)
        /// </summary>
        public AnswerProfile()
        {
            CreateMap<AnswerEntity, AnswerDto>();
            CreateMap<AnswerCreateDto, AnswerEntity>();
            CreateMap<AnswerUpdateDto, AnswerEntity>();
        }
    }
}
