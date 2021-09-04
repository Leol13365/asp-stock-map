using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using StockMap.Models;

namespace StockMap.Controllers
{
    [SessionAuth]
    public class FavoritesController : Controller
    {
        private StockMapDbContext db = new StockMapDbContext();

        // GET: Favorites
        public ActionResult Index()
        {
            var userAccount = Session["account"].ToString();
            var favorites = db.Favorites
                .Include(f => f.Stock)
                .Include(f => f.User)
                .Where(m => m.UserAccount == userAccount)
                .ToList();
            var model = new Favorite() { FavoriteIndex = favorites };
            return View(model);
        }

        // POST: Favorites/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "StockId")] Favorite data)
        {
            if (ModelState.IsValid)
            {
                var userAccount = Session["account"].ToString();
                var stock = db.Stocks.Where(m => m.StockId == data.StockId).FirstOrDefault();
                if (stock == null)
                {
                    ModelState.AddModelError(nameof(data.StockId), "此股票不存在");
                    return View(data);
                }

                var favorite = db.Favorites.Where(m => m.StockId == data.StockId && m.UserAccount == userAccount).FirstOrDefault();
                if (favorite != null)
                {
                    ModelState.AddModelError(nameof(data.StockId), "此股票已在收藏內");
                    return View(data);
                }

                int max = db.Favorites.Count(m => m.UserAccount == userAccount);
                if (max >= 10)
                {
                    ModelState.AddModelError(nameof(data.StockId), "已達收藏上限");
                    return View(data);
                }

                db.Favorites.Add(new Favorite { UserAccount = userAccount, StockId = data.StockId });
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(data);
        }

        // GET: Favorites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorite favorite = db.Favorites.Find(id);
            if (favorite == null)
            {
                return HttpNotFound();
            }
            return View(favorite);
        }

        // GET: Favorites/Delete/5
        public ActionResult Delete(int? id)
        {
            Favorite favorite = db.Favorites.Find(id);
            db.Favorites.Remove(favorite);
            db.SaveChanges();
            return RedirectToAction("Index");
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
