using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManualAction.BusinessLayer.Managers;
namespace ManualAction.PresentationLayer.Controllers
{
    [Authorize(Roles = "A,U")]
    public class DepartmantController : Controller
    {
        DepartmantManager manager = new DepartmantManager();
        public JsonResult GetManager()
        {
            var _list = manager.GetAllManager();
            var jsonResult = Json(_list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public JsonResult Details(int id)
        {
            var _listManager = manager.GetManagerById(id);
            return Json(_listManager, JsonRequestBehavior.AllowGet);
        }
    }
}