using Misa.FastCode.Bl.ValidateDto.Attributes;
using Misa.FastCode.Common.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.FastCode.Bl.Service.Answer
{
    public class AnswerCreateDto
    {
        public Guid question_id { get; set; }

        public string onwer_name { get; set; }

        [Required, Name(FieldName.AnswerContent)]
        public string answer_content { get; set; }
    }
}
