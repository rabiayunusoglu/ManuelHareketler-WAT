using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManualAction.BusinessLayer.Managers;
using ManualAction.BusinessLayer.DTO;
namespace ManualAction.PresentationLayer.Controllers
{
    [Authorize(Roles = "A")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManualAction()
        {

            return View();
        }
        public ActionResult Report()
        {

            return View();
        }
        public ActionResult ManualActionReason()
        {

            return View();
        }
        public ActionResult ManualActionExcel()
        {

            return View();
        }
    }
}