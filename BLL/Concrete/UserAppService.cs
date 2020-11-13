using AutoMapper;
using BLL.Abstract;
using BLL.Constants;
using Core.Aspect.Autofac.Performance;
using Core.Models;
using Core.Utility.Results;
using Core.Utility.Security.Hashing;
using DAL.Abstract;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BLL.Concrete
{
    public class UserAppService : IUserAppService
    {
        private IUserDal _userDal;
        private readonly IMapper _mapper;

        public UserAppService(IUserDal userDal, IMapper mapper)
        {
            _userDal = userDal;
            _mapper = mapper;
        }

        [PerformanceAspect(5)]
        public IResult Add(UserForRegisterDto user)
        {
            Thread.Sleep(5000);
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

        public IDataResult<List<UserDto>> GetList()
        {
            try
            {              
                var entity = _userDal.GetList().ToList();
                var dtos = _mapper.Map<List<UserDto>>(entity).OrderBy(o => o.FirsName.ToLower()).ToList();

                return new SuccessDataResult<List<UserDto>>(dtos);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<UserDto>>(ex.Message);
            }
        }
    }
}
