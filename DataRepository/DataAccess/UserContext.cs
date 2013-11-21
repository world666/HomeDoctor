using System.Data.Entity;
using DataRepository.Model;

namespace DataRepository.DataAccess
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        public UserContext() : base("DefaultConnection")
        {
            
        }
    }
}