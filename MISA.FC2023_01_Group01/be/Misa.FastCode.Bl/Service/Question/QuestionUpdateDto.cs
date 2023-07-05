using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.FastCode.Bl.Service.Question
{
    public class QuestionUpdateDto
    {
        public Guid subject_id { get; set; }

        public string question_content { get; set; }

        public string question_title { get; set; }

        public string? onwer_name { get; set; }
    }
}
