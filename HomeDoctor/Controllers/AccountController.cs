using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataRepository.Model;
using HomeDoctor.Providers;
using HomeDoctor.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using HomeDoctor.Models;

namespace HomeDoctor.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный пароль или логин");
                }
            }
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipUser membershipUser = ((CustomMembershipProvider)Membership.Provider).CreateUser(model);
                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким именем уже существует.");
                }
            }
            return View(model);
        }
        //
        // POST: /Account/Disassociate
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }*/

        //
        //GET: /Account/Manage
        [Authorize]
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Ваш пароль был изменен."
                : message == ManageMessageId.Error ? "Неправильный текущий пароль."
                : "";
 
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(ManageUserViewModel model)
        {
            ViewBag.ReturnUrl = Url.Action("Manage");
            bool isSucceeded = Membership.Provider.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);

            return isSucceeded
                ? RedirectToAction("Manage", new {Message = ManageMessageId.ChangePasswordSuccess})
                : RedirectToAction("Manage", new {Message = ManageMessageId.Error});
        }


        //
        //GET: /Account/Profile
        [Authorize]
        public ActionResult Profile(ProfileMessageId? message)
        {
            
            if(IoC.Resolve<RoleProvider>().IsUserInRole(User.Identity.GetUserName(),"Admin"))
               return RedirectToAction("Administration", "Admin");
            ViewBag.StatusMessage =
                message == ProfileMessageId.ChangeProfileSuccess ? "Ваш профиль был изменен."
                : message == ProfileMessageId.Error ? "Ошибка."
                : "";
            ViewBag.ReturnUrl = Url.Action("Profile");
            return View(IoC.Resolve<DataBaseService>().GetProfile(User.Identity.Name));
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        [ValidateAntiForgeryToken]
        public ActionResult Profile(ProfileViewModel profileViewModel)
        {
            ViewBag.ReturnUrl = Url.Action("Profile");
            bool isSucceeded = IoC.Resolve<DataBaseService>().UpdateProfile(profileViewModel, User.Identity.Name);
            return isSucceeded
                ? RedirectToAction("Profile", new { Message = ProfileMessageId.ChangeProfileSuccess })
                : RedirectToAction("Profile", new { Message = ProfileMessageId.Error });
        }

        
        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            
            return RedirectToAction("Login", "Account");
        }

        

        #region Helpers
        

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        #endregion
    }
}