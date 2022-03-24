using System;
using System.Web.Mvc;
using ManualAction.BusinessLayer.Managers;
using ManualAction.BusinessLayer.DTO;

namespace ManualAction.PresentationLayer.Controllers
{

    public class ManualActionReasonController : Controller
    {
        ManualActionReasonManager manager = new ManualActionReasonManager();
        [Authorize(Roles = "A,U")]
        public JsonResult GetManager()
        {
            var _list = manager.GetAllManager();
            var jsonResult = Json(_list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [Authorize(Roles = "A,U")]
        public JsonResult Details(Guid id)
        {
            var _listManager = manager.GetManagerById(id);
            return Json(_listManager, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize(Roles = "A")]
        public JsonResult Create(ManualActionReasonDTO managerDTO)
        {
            var user = manager.CreateManager(managerDTO);

            return Json(user, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize(Roles = "A")]
        public JsonResult Edit(ManualActionReasonDTO managerDTO)
        {
            var _listManager = manager.UpdateManager(managerDTO);
            return Json(_listManager, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles = "A")]
        public JsonResult Delete(Guid id)
        {
            var _listManager = manager.DeleteManager(id);
            return Json(_listManager, JsonRequestBehavior.AllowGet);
        }
    }
}