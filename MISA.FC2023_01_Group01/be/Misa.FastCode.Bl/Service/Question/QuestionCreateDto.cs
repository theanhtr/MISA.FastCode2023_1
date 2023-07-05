using Misa.FastCode.Bl.ValidateDto.Attributes;
using Misa.FastCode.Common.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.FastCode.Bl.Service.Question
{
    public class QuestionCreateDto
    {

        public Guid subject_id { get; set; }

        [Required,  Name(FieldName.QuestionContent)]
        public string question_content { get; set; }

        [Required, Name(FieldName.QuestionTitle)]
        public string question_title { get; set; }

        public string? onwer_name { get; set; }
    }
}
