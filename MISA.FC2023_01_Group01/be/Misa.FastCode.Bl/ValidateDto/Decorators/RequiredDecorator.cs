using Misa.FastCode.Bl.ValidateDto.Attributes;
using Misa.FastCode.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misa.FastCode.Common.Resource;
using Misa.FastCode.Common.Emum;
using Misa.FastCode.Common.Error;

namespace Misa.FastCode.Bl.ValidateDto.Decorators
{
    /// <summary>
    /// kiểm tra bắt buộc nhập
    /// created by: nqhuy(21/05/2023)
    /// </summary>
    public class RequiredDecorator : BaseDecorator
    {

        /// <summary>
        /// hàm thực hiện logic validate
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        /// <exception cref="ValidateException"></exception>
        protected override ValidateError? Handle()
        {
            if (PropValue == null || string.IsNullOrEmpty(PropValue))
            {
                return new ValidateError()
                {
                   FieldNameError = this.FieldNameError,
                   Message = string.Format(ErrorMessage.RequiredError, Name),
                };
            }
            else return null;
        }
    }
}
