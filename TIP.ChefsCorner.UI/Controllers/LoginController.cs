using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TIP.ChefsCorner.BL;

namespace TIP.ChefsCorner.UI.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(string returnurl)
        {
            Login login = new Login();
            ViewBag.ReturnUrl = returnurl;
            return View(login);
        }

        [HttpPost]
        public ActionResult Login(Login login, string returnurl)
        {
            try
            {
                if (returnurl == null)
                    returnurl = "~/";
                if(login.SignIn())
                {
                    ViewBag.Message = "Thank you for logging in.";
                    Session["user"] = login;
                    return Redirect(returnurl);
                } else
                {
                    ViewBag.Message = "Sorry, wrong username or password.";
                    return View(login);
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View(login);
            }
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            return View();
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            Login login = new BL.Login();
            return View(login);
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(Login login)
        {
            try
            {
                if (login.Password != null && login.ScreenName != null && login.Email != null)
                {
                    if (login.Email.Contains("@"))
                    {
                        if (login.Password.Length >= 8)
                        {
                            login.Insert();
                            return RedirectToAction("Login");
                        } else
                        {
                            ViewBag.Message = "Passwords must be at least 8 characters in length.";
                            return View(login);
                        }
                    } else
                    {
                        ViewBag.Message = "This does not appear to be a valid email address.";
                        return View(login);
                    }
                } else
                {
                    ViewBag.Message = "All fields must be filled.";
                    return View(login);
                }              
            }
            catch
            {
                return View(login);
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            Login login = new BL.Login();
            login = (Login)Session["user"];
            return View(login);
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Login login)
        {
            try
            {
                login.Update();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            Login login = new BL.Login();
            login = (Login)Session["user"];
            return View(login);
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Login login)
        {
            try
            {
                login.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(login);
            }
        }
    }
}
