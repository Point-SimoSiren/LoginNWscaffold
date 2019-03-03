using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using NWcompleteScaff.Models;

namespace NWcompleteScaff.Controllers
{
    public class LoginController : Controller
    {


        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(NWcompleteScaff.Models.User userModel)
        {
            using (LoginEntities db = new LoginEntities())
            {
                Console.WriteLine("autherize sent to controller");
                var userDetails = db.Users.Where
               (x => x.UserName == userModel.UserName && x.Password == userModel.Password)
               .FirstOrDefault();

                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong Username or password.";

                    return View("Index", userModel);
                }
                else
                {
                    Session["UserID"] = userDetails.UserID;
                    return RedirectToAction("Index", "Employees");
                }
            }


        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("index", "login");

        }
    }
}