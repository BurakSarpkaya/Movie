using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utility.Security.Jwt
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(Users user);
    }
}
