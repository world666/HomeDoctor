using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataRepository.Model;
using System.Web.Helpers;

namespace DataRepository.DataAccess
{
    public class HomeDoctorDbInitializer : DropCreateDatabaseIfModelChanges<UserContext>
    {
        protected override void Seed(UserContext context)
        {
            if (context == null)
                context = new UserContext();
            var roles = new List<Role>()
            {
                new Role() {Name = "Admin"},
                new Role() {Name = "User"},
                new Role() {Name = "AdvUser"}
            };
            roles.ForEach(r=>context.Roles.Add(r));
            context.SaveChanges();

            var profile = new Profile()
            {
                Name = "Andrey",
                Surname = "Kirillov",
                Age = 22,
                SkypeLogin = "andrey7620",
                Money = 0m
            };
            context.Profiles.Add(profile);

            var user = new User()
            {
                CreationDate = DateTime.Now,
                Password = Crypto.HashPassword("andrey3828016"),
                UserName = "world666",
                RoleId = 1,
                Profile = profile
            };
            context.Users.Add(user);

            context.SaveChanges();
        }
    }
}
