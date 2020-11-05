using Core.Models;
using Core.Utility.Results;
using Core.Utility.Security.Jwt;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Abstract
{
    public interface IAuthAppService
    {
        IDataResult<Users> Register(UserForRegisterDto userForRegisterDto);

        IDataResult<Users> Login(UserForLoginDto userForLoginDto);

        IDataResult<AccessToken> CreateAccessToken(Users user);

    }
}
