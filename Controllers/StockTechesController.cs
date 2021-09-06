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
            List<double> values = new List<double>();
            for (int i = 0; i < 30; i++)
            {
                values.Add(Convert.ToDouble(properties[i + 2].GetValue(stockTeches)));
            }

            List<string> dates = new List<string>();
            int countWeekend = 0;
            double[] fiveMA = new double[10];
            double[] tenMA = new double[10];
            double[] twentyMA = new double[10];

            for (int i = 0; i < 10; i++)
            {
                var date = DateTime.Today.AddDays(-(i + 1 + countWeekend));
                if(date.DayOfWeek == DayOfWeek.Sunday)
                {
                    date = date.AddDays(-2);
                    countWeekend += 2;
                }
                dates.Add(date.ToString());

                fiveMA[i] = values.GetRange(i , 5).Average();
                tenMA[i] = values.GetRange(i , 10).Average();
                twentyMA[i] = values.GetRange(i , 20).Average();
            }
            ViewBag.DateLabel = dates;

            ChartData chartData = new ChartData
            {
                labels = dates.ToArray(),
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
