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
    public class StockTechesController : Controller
    {
        private StockMapDbContext db = new StockMapDbContext();

        // GET: StockTeches
        public ActionResult Index()
        {
            return View();
        }

        // POST: StockTeches/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string searchId)
        {
            ViewData["CurrentFilter"] = searchId;

            if (searchId == null) { ViewBag.ErroMsg = "請輸入股票代碼"; return View(); }
            var stockId = db.StockTeches.Include(s => s.Stock).Where(m => m.StockId == searchId).FirstOrDefault();
            if (stockId == null) { ViewBag.ErroMsg = "查無此股"; return View(); }
            var stockTeches = db.StockTeches.Find(searchId);

            var properties = stockTeches.GetType().GetProperties();
            var values = new double[30];
            for (int i = 0; i < 30; i++)
            {
                var a = values[i] = Convert.ToDouble(properties[i + 2].GetValue(stockTeches));
            }

            string[] dates = new string[10];
            for (int i = 1; i < 11; i++)
            {
                dates[i - 1] = DateTime.Now.AddDays(-i).ToString("MM/dd");
            }
            ViewBag.DateLabel = dates;

            ChartData chartData = new ChartData
            {
                labels = dates,
                datasets = new Dataset[]{
                    new Dataset
                    {
                        label = "ABC",
                        data = new double[] { 1.0, 2, 3, 4, 5 }
                    },
                    new Dataset
                    {
                        label = "BBC",
                        data = new double[] { 9, 4, 7, 5, 7 }
                    }
                }
            };

            stockTeches.ChartData = chartData;

            ViewBag.CheckOk = true;

            return View(stockTeches);
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
