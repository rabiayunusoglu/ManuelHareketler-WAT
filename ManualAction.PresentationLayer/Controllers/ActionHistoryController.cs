using System;
using System.Web.Mvc;
using ManualAction.BusinessLayer.Managers;
using ManualAction.BusinessLayer.DTO;
namespace ManualAction.PresentationLayer.Controllers
{
    [Authorize(Roles = "A,U")]
    public class ActionHistoryController : Controller
    {
        ActionHistoryManager manager = new ActionHistoryManager();

        public JsonResult GetManager()
        {
            var _list = manager.GetAllManager();
            var jsonResult = Json(_list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult GetManagerByManualID(string manualID)
        {
            var _list = manager.GetAllManagerByManualID(manualID);
            var jsonResult = Json(_list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult Details(Guid id)
        {
            var _listManager = manager.GetManagerById(id);
            return Json(_listManager, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Create(ActionHistoryDTO managerDTO)
        {
            var user = manager.CreateManager(managerDTO);

            return Json(user, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Edit(ActionHistoryDTO managerDTO)
        {
            var _listManager = manager.UpdateManager(managerDTO);
            return Json(_listManager, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(Guid id)
        {
            var _listManager = manager.DeleteManager(id);
            return Json(_listManager, JsonRequestBehavior.AllowGet);
        }
    }
}