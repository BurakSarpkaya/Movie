using BLL.Abstract;
using BLL.Constants;
using Core.Models;
using Core.Utility.Results;
using Core.Utility.Security.Hashing;
using DAL.Abstract;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Concrete
{
    public class UserAppService : IUserAppService
    {
        private IUserDal _userDal;

        public UserAppService(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(UserForRegisterDto user)
        {
            byte[] passwordHash, passwordSalt;

            HashingHelper.CreatePasswordHash(user.PassWord, out passwordHash, out passwordSalt);
            Users userEntity = new Users
            {
                FirsName = user.FirsName,
                LastName = user.LastName,
                Email = user.Email,
                PassWordHash = passwordHash,
                PassWordSalt = passwordSalt,
            };
            _userDal.Add(userEntity);

            return new SuccessResult(Messages.UserAdded);
        }

        public Users GetByMail(string email)
        {
            return _userDal.Get(x => x.Email == email);
        }
    }
}
