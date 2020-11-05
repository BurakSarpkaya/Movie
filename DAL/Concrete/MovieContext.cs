using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Concrete
{
    public class MovieContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-F4TG7E6\SQLEXPRESS;Database=MovieDB;Trusted_Connection=true");
        }

        public virtual DbSet<Users> Users { get; set; }
    }
}
