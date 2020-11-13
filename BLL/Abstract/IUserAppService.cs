using Core.Models;
using Core.Utility.Results;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Abstract
{
    public interface IUserAppService
    {
        IResult Add(UserForRegisterDto user);
        Users GetByMail(string email);
       IDataResult<List<UserDto>> GetList();
    }
}
