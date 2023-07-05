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

namespace Misa.FastCode.Bl.ValidateDto.Decorators
{
    /// <summary>
    /// validate năm trùng năm hiện tại
    /// created by: nqhuy(21/05/2023)
    /// </summary>
    public class CurrentYearDecorator : BaseDecorator
    {
        /// <summary>
        /// logic valdiate
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        /// <exception cref="ValidateException"></exception>
        protected override ValidateError? Handle()
        {
          
            var value = (int)PropValue;
            var currentYear = DateTime.Now.Year;
            if(value != currentYear)
            {
                return new ValidateError()
                {
                    FieldNameError = this.FieldNameError,
                    Message = string.Format(ErrorMessage.EqualError, Name, DateTime.Now.Year)

                };
            } else
                return null;
        }
    }
}
