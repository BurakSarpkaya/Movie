using Core.DataAccess.EntityFramework;
using Core.Models;
using DAL.Abstract;

namespace DAL.Concrete
{
    public class UserDal : EfEntityRepositroyBase<Users, MovieContext>, IUserDal
    {
    }
}
