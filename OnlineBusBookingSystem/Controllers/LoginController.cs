using OnlineBusBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineBusBookingSystem.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        BusDBEntities db = new BusDBEntities();
        public ActionResult Register()
        {
            if (Session["Role"] == null)
            {
                return View();
            }
            else
            {
                TempData["Error"] = "Please logout first to access this page!";
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            try
            {
                if (db.Users.Any(u => u.UserName == model.UserName))
                {
                    ModelState.AddModelError("UserName", "This Username is already taken. Please choose a different one.");
                    return View(model);
                }
                User user = new User();
                user.Name = model.Name;
                user.UserName = model.UserName;
                user.Password = model.Password;
                user.Role = "user";
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Login()
        {
            if (Session["Role"] == null)
            {
                return View();
            }
            else
            {
                TempData["Error"] = "You are already logged in.";
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult Login(LoginModel user)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var obj = db.Users.Where(a => a.UserName.Equals(user.UserName) && a.Password.Equals(user.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["Id"] = obj.Id;
                        Session["Name"] = obj.Name;
                        Session["Role"] = obj.Role;
                        return RedirectToAction("Index", "Home");
                    }

                }

                ModelState.AddModelError("", "Invalid Username or Password!");
                return View(user);
            }
            catch (InvalidEmailOrPasswordException)
            {

                ViewBag.ErrorMessage = "Invalid email or password!";
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
                return View("Error");
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}