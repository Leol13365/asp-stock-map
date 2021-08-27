using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StockMap.Models;

namespace StockMap.Controllers
{
    public class AuthController : Controller
    {
        private StockMapDbContext db = new StockMapDbContext();

        // GET: Auth/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Auth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Account,Password")] User data)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Where(m => m.Account == data.Account).FirstOrDefault();
                if (user == null)
                {
                    ModelState.AddModelError(nameof(data.Account), "此帳號不存在，請先註冊");
                    return View(data);
                }
                if (data.Password != user.Password)
                {
                    ModelState.AddModelError(nameof(data.Password), "密碼錯誤");
                    return View(data);
                }
                Session["account"] = data.Account;
                return RedirectToAction("Index", "Favorites");
            }

            return View(data);
        }

        // GET: Auth/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        // GET: Auth/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Auth/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Account,Password,ConfirmedPassword")] RegisterUser data)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Where(m => m.Account == data.Account).FirstOrDefault();
                if (user != null)
                {
                    ModelState.AddModelError(nameof(data.Account), "此帳號已有人使用");
                    return View(data);
                }
                db.Users.Add(data);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(data);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
