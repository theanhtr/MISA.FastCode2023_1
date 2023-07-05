using Misa.FastCode.Dl.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.FastCode.Bl.JwtService
{
    public interface IJwtService
    {
        /// <summary>
        /// tạo token để xác thực người dùng
        /// </summary>
        /// <param name="user">thông tin người dùng</param>
        /// <returns>token</returns>
        string CreateToken(User user);
        
    }
}
