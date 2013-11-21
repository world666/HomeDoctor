using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using DataRepository.DataAccess;
using DataRepository.Model;
using HomeDoctor.Models;

namespace HomeDoctor.Services
{
    public class DataBaseService
    {
        public ProfileViewModel GetProfile(string userName)
        {
            ProfileViewModel profileViewModel;
            using (var _db = new UserContext())
            {
                User user = (from u in _db.Users
                             where u.UserName == userName
                             select u).FirstOrDefault();
                profileViewModel = new ProfileViewModel()
                {
                    SkypeLogin = user.Profile.SkypeLogin,
                    Name = user.Profile.Name,
                    Surname = user.Profile.Surname,
                    Age = user.Profile.Age,
                    Money = user.Profile.Money
                };
            }
            return profileViewModel;
        }

        public bool UpdateProfile(ProfileViewModel profileViewModel, string userName)
        {
            try
            {
                using (var _db = new UserContext())
                {
                    User user = (from u in _db.Users
                        where u.UserName == userName
                        select u).FirstOrDefault();
                    user.Profile.SkypeLogin = profileViewModel.SkypeLogin;
                    user.Profile.Name = profileViewModel.Name;
                    user.Profile.Surname = profileViewModel.Surname;
                    user.Profile.Age = profileViewModel.Age;
                    user.Profile.Money = profileViewModel.Money;
                    _db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}