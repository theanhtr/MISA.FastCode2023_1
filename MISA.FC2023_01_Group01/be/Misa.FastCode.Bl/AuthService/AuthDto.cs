using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misa.FastCode.Bl.ValidateDto.Attributes;
using Misa.FastCode.Common.Const;

namespace Misa.FastCode.Bl.AuthService
{
    public class AuthDto
    {
        /// <summary>
        /// email
        /// </summary>
        [Required, Name(FieldName.Email)]
        public string email { get; set; }

        /// <summary>
        /// mật khẩu
        /// </summary>
        [Required, Name(FieldName.Password)]
        public string password { get; set; }
    }
}
