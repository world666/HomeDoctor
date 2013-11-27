using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using DataRepository.DataAccess;
using DataRepository.Model;
using HomeDoctor.Models;
using WebGrease.Css.Extensions;

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
        public List<UserControlViewModel> GetUserControlVm()
        {
            var profileViewModel = new List<UserControlViewModel>();
            using (var _db = new UserContext())
            {
                _db.Users.ForEach(user => profileViewModel.Add(new UserControlViewModel
                {
                    UserName = user.UserName,
                    SkypeLogin = user.Profile.SkypeLogin,
                    Name = user.Profile.Name,
                    Surname = user.Profile.Surname,
                    Age = user.Profile.Age,
                    UserRole = user.Role.Name,
                    Money = user.Profile.Money
                }));
            }
            return profileViewModel;
        }
        public List<UserControlViewModel> GetUserControlVm(string findType, string value)
        {
            var profileViewModel = new List<UserControlViewModel>();
            using (var _db = new UserContext())
            {
                IEnumerable<User> users = null;
                switch (findType)
                {
                    case "Login":
                        users = _db.Users.Where(u => u.UserName == value);
                        break;
                    case "SkypeLogin":
                        users = _db.Users.Where(u => u.Profile.SkypeLogin == value);
                        break;
                    case "Name":
                        users = _db.Users.Where(u => u.Profile.Name == value);
                        break;
                    case "Surname":
                        users = _db.Users.Where(u => u.Profile.Surname == value);
                        break;
                }
                users.ForEach(user => profileViewModel.Add(new UserControlViewModel
                {
                    UserName = user.UserName,
                    SkypeLogin = user.Profile.SkypeLogin,
                    Name = user.Profile.Name,
                    Surname = user.Profile.Surname,
                    Age = user.Profile.Age,
                    UserRole = user.Role.Name,
                    Money = user.Profile.Money
                }));
            }
            return profileViewModel;
        }
        public  bool UpdateUserControlVm(List<UserControlViewModel> userControlViewModels)
        {
            try
            {
                using (var _db = new UserContext())
                {
                    foreach (var userControlViewModel in userControlViewModels)
                    {
                        var user = _db.Users.FirstOrDefault(u => u.UserName == userControlViewModel.UserName);
                        user.RoleId = GetRolesList().FindIndex(r => r == userControlViewModel.UserRole) + 1;
                        user.Profile.Money = userControlViewModel.Money;
                    }
                    _db.SaveChanges();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public bool DelteUsers(List<UserControlViewModel> userControlViewModels)
        {
            try
            {
                using (var _db = new UserContext())
                {
                    foreach (var userControlViewModel in userControlViewModels)
                    {
                        if(userControlViewModel.UserName=="world666")
                            continue;
                        var user = _db.Users.FirstOrDefault(u => u.UserName == userControlViewModel.UserName);
                        _db.Users.Remove(user);
                    }
                    _db.SaveChanges();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public List<string> GetRolesList()
        {
            var roles = new List<string>();
            using (var _db = new UserContext())
            {
                _db.Roles.ForEach(r => roles.Add(r.Name));
            }
            return roles;
        }

        public List<RecordViewModel> GetRecords(string userName)
        {
            var recordViewModels = new List<RecordViewModel>();
            using (var _db = new UserContext())
            {
                var user = _db.Users.FirstOrDefault(u => u.UserName == userName);
                user.UserRecords.ForEach(r=>recordViewModels.Add(new RecordViewModel
                {
                    Date = r.CreationDate,
                    Message = r.Message,
                    Title = r.Title,
                    Answered = r.Answered,
                    RecordId = r.Id
                }));
            }
            return recordViewModels;
        }
        public List<RecordViewModel> GetRecords(string findType, string value)
        {
            var recordViewModels = new List<RecordViewModel>();
            using (var _db = new UserContext())
            {
                IEnumerable<User> users = null;
                switch (findType)
                {
                    case "Login":
                        users = _db.Users.Where(u => u.UserName == value);
                        break;
                    case "SkypeLogin":
                        users = _db.Users.Where(u => u.Profile.SkypeLogin == value);
                        break;
                    case "Name":
                        users = _db.Users.Where(u => u.Profile.Name == value);
                        break;
                    case "Surname":
                        users = _db.Users.Where(u => u.Profile.Surname == value);
                        break;
                }
                users.ForEach(u=>u.UserRecords.ForEach(r=> recordViewModels.Add(new RecordViewModel
                {
                    Date = r.CreationDate,
                    Message = r.Message,
                    Title = r.Title,
                    Answered = r.Answered,
                    UserName = r.User.UserName,
                    RecordId = r.Id
                })));
            }
            return recordViewModels;
        }
        public List<RecordViewModel> GetRecords()
        {
            var recordViewModels = new List<RecordViewModel>();
            using (var _db = new UserContext())
            {
                _db.UserRecords.ForEach(r => recordViewModels.Add(new RecordViewModel
                {
                    Date = r.CreationDate,
                    Message = r.Message,
                    Title = r.Title,
                    Answered = r.Answered,
                    UserName = r.User.UserName,
                    RecordId = r.Id
                }));
            }
            return recordViewModels;
        }
        public bool SaveRecord(string userName,RecordViewModel recordViewModel)
        {
            try
            {
                using (var _db = new UserContext())
                {
           
                    var user = _db.Users.FirstOrDefault(u=>u.UserName == userName);
                    user.UserRecords.Add(new UserRecord
                    {
                        CreationDate = DateTime.Now,
                        User = user,
                        Title = recordViewModel.Title,
                        Message = recordViewModel.Message
                    });
                    _db.SaveChanges();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool UpdateRecords(List<RecordViewModel> recordsViewModel)
        {
            try
            {
                using (var _db = new UserContext())
                {
                    foreach (var update in recordsViewModel)
                    {
                        var record = _db.UserRecords.Find(update.RecordId);
                        if (record.Message != update.Message)
                        {
                            record.Message = update.Message;
                            record.Answered = true;
                        }
                    }
                    _db.SaveChanges();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }

}