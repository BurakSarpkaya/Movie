using AutoMapper;
using Core.Models;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Automapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Users, UserDto>();
        }
}
}
