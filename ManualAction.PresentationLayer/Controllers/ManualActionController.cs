using System;
using System.Web.Mvc;
using ManualAction.BusinessLayer.Managers;
using ManualAction.BusinessLayer.DTO;
using System.Web;
using System.Linq;
using System.Collections.Generic;

namespace ManualAction.PresentationLayer.Controllers
{
    [Authorize(Roles = "A,U")]
    public class ManualActionController : Controller
    {
        ManualActionManager manager = new ManualActionManager();

        public JsonResult GetManager()
        {
            var _list = manager.GetAllManager();
            var jsonResult = Json(_list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult GetManagerByRegNo()
        {
            SecurityController security = new SecurityController();
            string regNo = HttpContext.Session["registerNo"].ToString();
            if (regNo == null || regNo.Length == 0)
                return Json(null, JsonRequestBehavior.AllowGet);
            var _list = manager.GetManagerByRegNo(regNo);
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
        public JsonResult Create(ManualActionDTO managerDTO)
        {
            var user = manager.CreateManager(managerDTO);

            return Json(user, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Edit(ManualActionDTO managerDTO)
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
        [HttpPost]
        public JsonResult Upload(FormCollection formCollection)
        {
            try
            {
                if (Request != null)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if ((file != null) && (file.ContentLength != 0) && !string.IsNullOrEmpty(file.FileName))
                    {
                        string fileName = file.FileName;
                        string fileContentType = file.ContentType;
                        byte[] fileBytes = new byte[file.ContentLength];
                        var FileStream = file.InputStream;
                        bool fileData = manager.GetDataFromExcelFile(file.FileName, FileStream);
                        if (fileData)
                            return Json(true, JsonRequestBehavior.AllowGet);
                        else
                            return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);

            }
            return Json(false, JsonRequestBehavior.AllowGet);


        }

        public JsonResult GetCount()
        {
            List<int> countList = new List<int>();
            var _list = manager.GetAllManager();
            if (_list == null)
                countList.Add(0);
            //total
            countList.Add(_list.Count());
            var _listCount = manager.GetCount(1);
            //finished
            countList.Add(_listCount);
            _listCount = manager.GetCount(2);
            //noStarted
            countList.Add(_listCount);
            _listCount = manager.GetCount(3);
            //started
            countList.Add(_listCount);
            return Json(countList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCountByRegNo()
        {
            SecurityController security = new SecurityController();
            string regNo = HttpContext.Session["registerNo"].ToString();
            List<int> countList = new List<int>();
            if (regNo == null || regNo.Length == 0)
            {
                countList.Add(0);
                countList.Add(0);
                countList.Add(0);
                countList.Add(0);
                return Json(countList, JsonRequestBehavior.AllowGet);
            }
            var _list = manager.GetManagerByRegNo(regNo);
            if (_list == null)
                countList.Add(0);
            //total
            countList.Add(_list.Count());
            var _listCount = manager.GetCountByRegNo(regNo, 1);
            //noStarted
            countList.Add(_listCount);
            _listCount = manager.GetCountByRegNo(regNo, 2);
            //started
            countList.Add(_listCount);
            _listCount = manager.GetCountByRegNo(regNo, 3);
            //finished
            countList.Add(_listCount);
            return Json(countList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult RedirectItem(ManualActionDTO managerDTO)
        {
            bool value = false;
            if (HttpContext.Session["departmantType"] == null || HttpContext.Session["username"] == null)
                RedirectToAction("Index", "Security");
            if (managerDTO == null)
                return Json(false, JsonRequestBehavior.AllowGet);
            int depNo = Convert.ToInt32(HttpContext.Session["departmantType"].ToString());
            string username = HttpContext.Session["username"].ToString();
            value = manager.RedirectManager(managerDTO, depNo, username);
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public void Export()
        {
            try
            {

                byte[] temp = manager.ExporttoExcel();
                if (temp == null)
                    return;
                Response.ClearContent();
                Response.BinaryWrite(temp);
                Response.AddHeader("content-disposition", "attachment; filename=Rabia.xlsx");
                Response.ContentType = "application/vnd.ms-excel";
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                return;
            }

        }
        public void ExportByDate(DateTime startDate, DateTime endDate)
        {
            try
            {
                if (startDate == null || endDate == null)
                    return;
                byte[] temp = manager.ExporttoExcelByDate(startDate, endDate);
                if (temp == null)
                    return;
                Response.ClearContent();
                Response.BinaryWrite(temp);
                Response.AddHeader("content-disposition", "attachment; filename=Rabia.xlsx");
                Response.ContentType = "application/vnd.ms-excel";
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                return;
            }
        }
        public void ExportByRegNo()
        {
            try
            {
                SecurityController security = new SecurityController();
                string regNo = HttpContext.Session["registerNo"].ToString();
                if (regNo == null || regNo.Length == 0)
                    return;
                byte[] temp = manager.ExporttoExcelByRegNo(regNo);
                if (temp == null)
                    return;
                Response.ClearContent();
                Response.BinaryWrite(temp);
                Response.AddHeader("content-disposition", "attachment; filename=Rabia.xlsx");
                Response.ContentType = "application/vnd.ms-excel";
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                return;
            }

        }
        public void ExportByDateRegNo(DateTime startDate, DateTime endDate)
        {
            try
            {
                if (startDate == null || endDate == null)
                    return;
                SecurityController security = new SecurityController();
                string regNo = HttpContext.Session["registerNo"].ToString();
                if (regNo == null || regNo.Length == 0)
                    return;
                byte[] temp = manager.ExporttoExcelByDateRegNo(startDate, endDate, regNo);
                if (temp == null)
                    return;
                Response.ClearContent();
                Response.BinaryWrite(temp);
                Response.AddHeader("content-disposition", "attachment; filename=Rabia.xlsx");
                Response.ContentType = "application/vnd.ms-excel";
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public JsonResult GetByDate(DateTime startDate, DateTime endDate)
        {
            if (startDate == null || endDate == null)
                Json(null, JsonRequestBehavior.AllowGet);
            List<ManualActionDTO> list = manager.GetAllManager();
            if (list == null || list.Count == 0)
                Json(null, JsonRequestBehavior.AllowGet);
            List<ManualActionDTO> archiveByid = list.Where(x => Convert.ToDateTime(x.registerDate) <= endDate && Convert.ToDateTime(x.registerDate) >= startDate).ToList();
            if (archiveByid == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);

            }
            var jsonResult = Json(archiveByid, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public JsonResult GetByDateByRegNo(DateTime startDate, DateTime endDate)
        {
            SecurityController security = new SecurityController();
            if (HttpContext.Session["registerNo"] == null)
                RedirectToAction("Index", "Security");
            string regNo = HttpContext.Session["registerNo"].ToString();
            if (startDate == null || endDate == null)
                Json(null, JsonRequestBehavior.AllowGet);
            List<ManualActionDTO> list = manager.GetAllManager();
            if (list == null || list.Count == 0)
                Json(null, JsonRequestBehavior.AllowGet);
            List<ManualActionDTO> archiveByid = list.Where(x => Convert.ToDateTime(x.registerDate) <= endDate && Convert.ToDateTime(x.registerDate) >= startDate && x.userRegisterNo == regNo).ToList();
            if (archiveByid == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);

            }
            var jsonResult = Json(archiveByid, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }


        public JsonResult GetReason()
        {
            var _list = manager.GetReason();
            var jsonResult = Json(_list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult GetMonth()
        {
            var _list = manager.GetMonth();
            var jsonResult = Json(_list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult GetReasonByMonth()
        {
            var _list = manager.GetReasonByMonth();
            var jsonResult = Json(_list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult GetReport()
        {
            var _list = manager.GetReport();
            var jsonResult = Json(_list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult GetReportByDate(string sortDate)
        {
            var _list = manager.GetReportByDate(sortDate);
            var jsonResult = Json(_list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult DeleteByDate(string sortDate)
        {
            var _listManager = manager.DeleteManagerByDate(sortDate);
            return Json(_listManager, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetYear()
        {
            var _list = manager.GetYear();
            var jsonResult = Json(_list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}