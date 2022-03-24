using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManualAction.PresentationLayer.Controllers
{
    [Authorize(Roles = "U")]
    public class HomeUserController : Controller
    {
        // GET: HomeUser
        public ActionResult Index()
        {
            return View();
        }
        // GET: HomeUser
        public ActionResult MyManualAction()
        {
            return View();
        }
    }
}