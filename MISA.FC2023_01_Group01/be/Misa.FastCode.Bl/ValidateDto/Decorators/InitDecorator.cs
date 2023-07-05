using Misa.FastCode.Bl.ValidateDto.Attributes;
using Misa.FastCode.Common.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.FastCode.Bl.ValidateDto.Decorators
{
    /// <summary>
    /// khời tạo decorator
    /// created by: nqhuy(21/05/2023)
    /// </summary>
    public class InitDecorator : BaseDecorator
    {
        /// <summary>
        /// hàm khởi tạo nên không validate 
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        protected override ValidateError? Handle()
        {
            return null;
        }
    }
}
