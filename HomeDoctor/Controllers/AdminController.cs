using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeDoctor.Models;
using HomeDoctor.Services;

namespace HomeDoctor.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        //
        // GET: /Administration/
        [Authorize(Roles = "Admin")]
        public ActionResult Administration()
        {
            return View();
        }

        //
        // GET: /UserControl/
        [Authorize(Roles = "Admin")]
        public ActionResult UserControl(ProfileMessageId? message)
        {
            ViewBag.RolesList = IoC.Resolve<DataBaseService>().GetRolesList();
            ViewBag.StatusMessage =
               message == ProfileMessageId.ChangeProfileSuccess ? "Операция успешно выполнена."
               : message == ProfileMessageId.Error ? "Ошибка."
               : "";
            return View(IoC.Resolve<DataBaseService>().GetUserControlVm());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult UserControl(List<UserControlViewModel> profileViewModels, string buttonType)
        {
            bool isSucceeded;
            if(buttonType == "Edit")
                isSucceeded = IoC.Resolve<DataBaseService>().UpdateUserControlVm(profileViewModels);
            else
                isSucceeded = IoC.Resolve<DataBaseService>().DelteUsers(profileViewModels);
            return isSucceeded
                ? RedirectToAction("UserControl", new { Message = ProfileMessageId.ChangeProfileSuccess })
                : RedirectToAction("UserControl", new { Message = ProfileMessageId.Error });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult FindUsers(string type, string value)
        {
            ViewBag.RolesList = IoC.Resolve<DataBaseService>().GetRolesList();
            return View(IoC.Resolve<DataBaseService>().GetUserControlVm(type, value));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult RecordsControl(ProfileMessageId? message)
        {
            ViewBag.StatusMessage =
               message == ProfileMessageId.ChangeProfileSuccess ? "Операция успешно выполнена."
               : message == ProfileMessageId.Error ? "Ошибка."
               : "";
            return View(IoC.Resolve<DataBaseService>().GetRecords());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult RecordsControl(List<RecordViewModel> recordViewModels)
        {
            bool isSucceeded = IoC.Resolve<DataBaseService>().UpdateRecords(recordViewModels);
            return isSucceeded
                ? RedirectToAction("RecordsControl", new { Message = ProfileMessageId.ChangeProfileSuccess })
                : RedirectToAction("RecordsControl", new { Message = ProfileMessageId.Error });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult FindRecords(string type, string value)
        {
            return View(IoC.Resolve<DataBaseService>().GetRecords(type, value));
        }
	}
}