using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HomeDoctor.Models;
using HomeDoctor.Services;
using Microsoft.AspNet.Identity;

namespace HomeDoctor.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Страница описания приложения.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Страница контактов.";

            return View();
        }

        [Authorize]
        public ActionResult Records()
        {
            if (IoC.Resolve<RoleProvider>().IsUserInRole(User.Identity.GetUserName(), "Admin"))
                return RedirectToAction("RecordsControl", "Admin");
            return View(IoC.Resolve<DataBaseService>().GetRecords(User.Identity.Name));
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        [ValidateAntiForgeryToken]
        public ActionResult Records(RecordViewModel recordViewModel)
        {
            IoC.Resolve<DataBaseService>().SaveRecord(User.Identity.Name, recordViewModel);
            return RedirectToAction("Records");
        }
    }
}