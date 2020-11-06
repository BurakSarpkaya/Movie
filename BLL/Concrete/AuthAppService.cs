using BLL.Abstract;
using BLL.Constants;
using Core.Models;
using Core.Utility.Results;
using Core.Utility.Security.Hashing;
using Core.Utility.Security.Jwt;
using DAL.Abstract;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Concrete
{
    public class AuthAppService : IAuthAppService
    {

        private IUserAppService _userAppService;
        private IUserDal _userDal;
        private ITokenHelper _tokenHelper;

        public AuthAppService(IUserAppService userAppService, IUserDal userDal, ITokenHelper tokenHelper)
        {
            _userAppService = userAppService;
            _userDal = userDal;
            _tokenHelper = tokenHelper;
        }
        public IDataResult<AccessToken> CreateAccessToken(Users user)
        {
            var accessToken = _tokenHelper.CreateToken(user);

            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public IDataResult<Users> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userAppService.GetByMail(userForLoginDto.Email);

            if (userToCheck == null)
                return new ErrorDataResult<Users>(Messages.UserNotFound);

            var verifyPassword = HashingHelper.VerifyPasswordHash(userForLoginDto.PassWord, userToCheck.PassWordHash, userToCheck.PassWordSalt);

            if (verifyPassword == false)
                return new ErrorDataResult<Users>(Messages.PasswordError);

            return new SuccessDataResult<Users>(userToCheck, Messages.SuccessfulLogin);
        }

        public IDataResult<Users> Register(UserForRegisterDto userForRegisterDto)
        {
            var emailCheck = UserExist(userForRegisterDto.Email);
            if (!emailCheck.Success)
                return new ErrorDataResult<Users>(emailCheck.Message);

            byte[] passwordHash, passwordSalt;

            HashingHelper.CreatePasswordHash(userForRegisterDto.PassWord, out passwordHash, out passwordSalt);

            Users newUser = new Users
            {
                FirsName = userForRegisterDto.FirsName,
                LastName = userForRegisterDto.LastName,
                Email = userForRegisterDto.Email,
                PassWordHash = passwordHash,
                PassWordSalt = passwordSalt,
            };
            _userAppService.Add(userForRegisterDto);

            return new SuccessDataResult<Users>(newUser, Messages.UserRegistered);
        }

        public IResult UserExist(string email)
        {
            var checkUser = _userAppService.GetByMail(email);

            if (checkUser != null)
                return new ErrorResult(Messages.EmailAlreadyExist);

            return new SuccessResult();
        }
    }
}
