using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.FastCode.Bl.AuthService
{
    public interface IAuthService
    {
        /// <summary>
        ///  kiểm tra tên đăng nhập và mật khẩu hợp lệ, nếu hợp lệ trả về token
        ///  created by: nq huy(20/06/2023)
        /// </summary>
        /// <param name="authDto">auth dto chứa thông tin tên đăng nhập và mật khẩu</param>
        /// <returns>json web token</returns>
        Task<string> GetAuthAsync(AuthDto authDto);
    }
}
