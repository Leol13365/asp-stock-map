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
    public class StocksController : Controller
    {
        private StockMapDbContext db = new StockMapDbContext();

        // GET: Stocks
        public ActionResult Index()
        {
            return View();
        }

        // POST: Stocks/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string searchId)
        {
            ViewData["CurrentFilter"] = searchId;
            if (searchId == null){ ViewBag.ErroMsg = "請輸入股票代碼"; return View(); }
            var stockId = db.Stocks.Where(m => m.StockId == searchId).FirstOrDefault();
            if (stockId == null) { ViewBag.ErroMsg = "查無此股"; return View(); }
            Stock stock = db.Stocks.Find(searchId);
            ViewBag.CheckOk = true;
            return View(stock);
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
