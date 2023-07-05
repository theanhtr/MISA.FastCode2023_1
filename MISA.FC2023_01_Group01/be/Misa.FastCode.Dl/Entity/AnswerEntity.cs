using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.FastCode.Dl.Entity
{
    public class AnswerEntity
    {
        public Guid answer_id { get; set; }

        public Guid question_id { get; set; }

        public string? onwer_name { get; set; }

        public string answer_content { get; set; }
    }
}
