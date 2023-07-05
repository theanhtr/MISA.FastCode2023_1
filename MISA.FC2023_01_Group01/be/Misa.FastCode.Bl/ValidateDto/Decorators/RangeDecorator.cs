using Misa.FastCode.Bl.ValidateDto.Attributes;
using Misa.FastCode.Common.Emum;
using Misa.FastCode.Common.Error;
using Misa.FastCode.Common.Exceptions;
using Misa.FastCode.Common.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RangeAttribute = Misa.FastCode.Bl.ValidateDto.Attributes.Range;

namespace Misa.FastCode.Bl.ValidateDto.Decorators
{
    /// <summary>
    /// kiểm tra giá trị nhằm trong khoảng
    /// created by: nqhuy(21/05/2023)
    /// </summary>
    public class RangeDecorator : BaseDecorator
    {
        /// <summary>
        /// logic validate
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        /// <exception cref="ValidateException"></exception>
        protected override ValidateError? Handle()
        {
            var value = (double)PropValue;
            var rangeAttribute = (RangeAttribute)Attr;
            if (value < rangeAttribute.Min || value > rangeAttribute.Max)
            {
                return new ValidateError()
                {
                    FieldNameError = this.FieldNameError,
                    Message = string.Format(ErrorMessage.RangeError, Name, rangeAttribute.Min, rangeAttribute.Max)
                };
            }
            else
                return null;
        }
    }
}
