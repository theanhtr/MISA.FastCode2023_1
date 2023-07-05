﻿using Misa.FastCode.Common.Emum;
using Misa.FastCode.Common.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Misa.FastCode.Common.Exceptions
{ 
    /// <summary>
    /// exception lỗi validate dữ liệu
    /// created by: nqhuy(21/05/2023)
    /// </summary>
    public class ValidateException : BaseException
    {
        /// <summary>
        /// khởi tạo mã lỗi  mặc định
        /// created by: nqhuy(21/05/2023)
        /// </summary>
        public ValidateException()
        {
            HttpStatusCode = HttpStatusCode.BadRequest;
        }
    }
}
