using Autofac;
using BLL.Abstract;
using BLL.Concrete;
using Core.Utility.Security.Jwt;
using DAL.Abstract;
using DAL.Concrete;

namespace BLL.DependecyResolvers
{
    public class BusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserDal>().As<IUserDal>();
            builder.RegisterType<UserAppService>().As<IUserAppService>();
            builder.RegisterType<AuthAppService>().As<IAuthAppService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
            builder.RegisterType<MovieAppService>().As<IMovieAppService>();
        }
    }
}
