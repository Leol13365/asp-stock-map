using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using Newtonsoft.Json;
using StockMap.Models;

namespace StockMap.Controllers
{
    [SessionAuth]
    public class FavoritesController : Controller
    {
        private StockMapDbContext db = new StockMapDbContext();

        // GET: Favorites
        public async Task<ActionResult> Index()
        {
            var userAccount = Session["account"].ToString();
            DateTime localDate = DateTime.UtcNow;

            List<Stock> stocks = db.Favorites.Include(f => f.Stock).Where(m => m.UserAccount == userAccount).Select(m => m.Stock).ToList();
            if (localDate.Hour > 1 && localDate.Hour < 6)
            {
                string[] favoriteStock = db.Favorites.Where(m => m.UserAccount == userAccount).Select(m => m.StockId).ToList().ToArray();
                DateTime[] stockUpdate = db.Favorites.Include(f => f.Stock).Where(m => m.UserAccount == userAccount).Select(m => m.Stock.UpdateTime).ToList().ToArray();
                List<string> searchStockId = new List<string>();
                string searchId = "";

                for (int i = 0; i < favoriteStock.Length; i++)
                {
                    TimeSpan between = localDate.Subtract(stockUpdate[i]);
                    if (between.TotalMinutes > 1) { searchStockId.Add(favoriteStock[i]); }
                }
                for (int i = 0; i < searchStockId.Count; i++)
                {
                    searchId += (i == searchStockId.Count - 1) ? searchStockId.ElementAt(i) : searchStockId.ElementAt(i) + "&stock_id=";
                }

                try
                {
                    string targetURL = "http://localhost:5000/api/v1/stock?stock_id=" + searchId;
                    HttpClient client = new HttpClient();
                    client.MaxResponseContentBufferSize = Int32.MaxValue;
                    var response = await client.GetStringAsync(targetURL);
                    stocks = JsonConvert.DeserializeObject<IEnumerable<Stock>>(response).ToList();
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            var favorites = db.Favorites
                .Include(f => f.Stock)
                .Where(m => m.UserAccount == userAccount)
                .ToList();
            foreach(var favorite in favorites) { favorite.Stock = stocks.Find(m => m.StockId == favorite.StockId); }
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
