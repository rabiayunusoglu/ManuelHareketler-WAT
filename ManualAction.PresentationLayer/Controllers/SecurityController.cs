using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManualAction.BusinessLayer.Managers;
using ManualAction.BusinessLayer.DTO;
using System.Web.Security;
using System.Threading.Tasks;

namespace ManualAction.PresentationLayer.Controllers
{
    public class SecurityController : Controller
    {
        UserListManager manager = new UserListManager();

        // GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (Session["usertype"] == null || Session["username"] == null || Session["registerNo"] == null || Session["departmantType"] == null)
                return View();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(UserListDTO user)
        {
            if (Session["usertype"] == null || Session["username"] == null || Session["registerNo"] == null || Session["departmantType"] == null)
            {
                var userInfo = manager.GetAllManager().Where(x => x.registerNo == user.registerNo && x.password.Trim() == user.password).ToList();
                Console.Write(userInfo);
                if (userInfo.Count != 0)
                {
                    FormsAuthentication.SetAuthCookie(userInfo[0].registerNo, true);
                    Session["registerNo"] = userInfo[0].registerNo;
                    Session["usertype"] = userInfo[0].userType;
                    Session["username"] = userInfo[0].username;
                    Session["departmantType"] = userInfo[0].departmantType;
                    var f = Session["registerNo"].ToString();
                    if (userInfo[0].userType == "A")
                        return RedirectToAction("Index", "Home");
                    else if (userInfo[0].userType == "U")
                        return RedirectToAction("Index", "HomeUser");
                    return RedirectToAction("Login");
                }
                else
                    return RedirectToAction("Login");
            }
            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (Session["usertype"] == null || Session["username"] == null || Session["registerNo"] == null || Session["departmantType"] == null)
            {
                return RedirectToAction("Login");
            }
            if (Session["usertype"].ToString() == "A")
                return RedirectToAction("Index", "Home");
            else if (Session["usertype"].ToString() == "U")
                return RedirectToAction("Index", "HomeUser");
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session["registerNo"] = null;
            Session["usertype"] = null;
            Session["username"] = null;
            Session["departmantType"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public UserListDTO GetUserInfo()
        {
            UserListDTO userInfo = new UserListDTO();
            userInfo.registerNo = Session["registerNo"].ToString();
            userInfo.userType = Session["usertype"].ToString();
            userInfo.username = Session["username"].ToString();
            userInfo.departmantType = Convert.ToInt32(Session["departmantType"]);
            return userInfo;
        }
        [HttpGet]
        public string GetUserRegNo()
        {
            return Session["registerNo"].ToString();
        }

    }
}