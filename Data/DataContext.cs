using Microsoft.EntityFrameworkCore;
using DatingApp.Models;

namespace DatingApp.Db
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}