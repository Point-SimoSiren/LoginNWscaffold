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



            return RedirectToAction("LoggedOut", "login");

            

        }

            public ActionResult LoggedOut()
            {
            ViewBag.LoggedOut = "You have logged out succesfully.";
            return View(); //We have a special wiev LoggedOut which is a copy of Index, but has additional viewbag message of succesfull logout.
            }



        }
    }
