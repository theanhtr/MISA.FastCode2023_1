using Misa.FastCode.Common.Error;
using Misa.FastCode.Common.Resource;
using Misa.FastCode.Bl.ValidateDto.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.FastCode.Bl.ValidateDto.Decorators
{
    /// <summary>
    /// validate nhỏ hơn giá trị max
    /// created by: nqhuy(21/05/2023)
    /// </summary>
    public class LowerDecorator : BaseDecorator
    {
        /// <summary>
        /// logic validate
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        /// <returns></returns>
        protected override ValidateError? Handle()
        {
            var value = (double)PropValue;
            var attr = (Lower)Attr;
            var max = attr.Max;
            if (value > max)
            {
                return new ValidateError()
                {
                    FieldNameError = this.FieldNameError,
                    Message = string.Format(ErrorMessage.LowerError, Name, max)

                };
            }
            else
                return null;
        }
       
    }
}
