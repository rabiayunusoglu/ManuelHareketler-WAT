using System;
using System.Web.Mvc;
using ManualAction.BusinessLayer.Managers;

namespace ManualAction.PresentationLayer.Controllers
{

    [Authorize(Roles = "A,U")]
    public class UserController : Controller
    {
        UserListManager manager = new UserListManager();
        public JsonResult GetManager()
        {
            var _list = manager.GetAllManager();
            var jsonResult = Json(_list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public JsonResult Details(string id)
        {
            var _listManager = manager.GetManagerById(id);
            return Json(_listManager, JsonRequestBehavior.AllowGet);
        }
    }
}