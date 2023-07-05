using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.FastCode.Bl.ValidateDto.Attributes
{
    /// <summary>
    /// đánh dấu giá trị lớn nhất
    /// </summary>
    public class Lower:BaseAttribute
    {
        /// <summary>
        /// giá trị lớn nhất
        /// </summary>
        public double Max { get; set; }

        public Lower(double max) { Max = max; }
    }
}
