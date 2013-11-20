using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;
using System.Security.Cryptography;
using System.Web.WebPages;
using HomeDoctor.Models;
using HomeDoctor.Models.Database;
using HomeDoctor.Models.Database.Tables;
using Microsoft.Internal.Web.Utils;

namespace HomeDoctor.Providers
{
    public class CustomMembershipProvider : MembershipProvider
    {
        public override bool ValidateUser(string username, string password)
        {
            bool isValid = false;

            using (UserContext _db = new UserContext())
            {
                try
                {
                    User user = (from u in _db.Users
                                 where u.UserName == username
                                 select u).FirstOrDefault();

                    if (user != null && Crypto.VerifyHashedPassword(user.Password, password))
                    {
                        isValid = true;
                    }
                }
                catch
                {
                    isValid = false;
                }
            }
            return isValid;
        }

        public MembershipUser CreateUser(RegisterViewModel registerVm)
        {
            MembershipUser membershipUser = GetUser(registerVm.UserName, false);

            if (membershipUser == null)
            {
                try
                {
                    using (UserContext _db = new UserContext())
                    {
                        var user = new User()
                        {
                            UserName = registerVm.UserName,
                            Password = Crypto.HashPassword(registerVm.Password),
                            CreationDate = DateTime.Now
                        };

                        Profile profile = new Profile()
                        {
                            SkypeLogin = registerVm.SkypeLogin,
                            Name = registerVm.Name,
                            Surname = registerVm.Surname,
                            Age = registerVm.Age,
                            User = user
                        };
                        if (_db.Roles.Find(2) != null)
                        {
                            user.RoleId = 2;
                        }

                        _db.Users.Add(user);
                        _db.Profiles.Add(profile);
                        _db.SaveChanges();
                        membershipUser = GetUser(registerVm.UserName, false);
                        return membershipUser;
                    }
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            try
            {
                using (UserContext _db = new UserContext())
                {
                    var users = from u in _db.Users
                                where u.UserName == username
                                select u;
                    if (users.Any())
                    {
                        User user = users.First();
                        MembershipUser memberUser = new MembershipUser("MyMembershipProvider", user.UserName, null, null, null, null,
                            false, false, user.CreationDate, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
                        return memberUser;
                    }
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            using (UserContext _db = new UserContext())
            {
                var users = from u in _db.Users
                    where u.UserName == username
                    select u;
                User user = users.First();
                if (user == null || !Crypto.VerifyHashedPassword(user.Password, oldPassword))
                    return false;
                user.Password = Crypto.HashPassword(newPassword);
                _db.SaveChanges();
            }
            return true;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }
        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            var userCollection = new MembershipUserCollection();
            using (var _db = new UserContext())
            {
                IQueryable<User> users = from u in _db.Users
                    where u.UserName == usernameToMatch
                    select u;

                foreach (User user in users)
                {
                    var memberUser = new MembershipUser("MyMembershipProvider", user.UserName, null, null, null, null,
                        false, false, user.CreationDate, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue,
                        DateTime.MinValue);
                    userCollection.Add(memberUser);
                }
            }
            totalRecords = userCollection.Count;
            return userCollection;
        }
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }
        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }
        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }
        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }
        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }
        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }
        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }
        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }
        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }
        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }
        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }
        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }
        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }
    }
}