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
            int countNull = 0;
            for (int i = 0; i < 30; i++)
            {
                if (countNull >= 3)
                {
                    ViewBag.ErroMsg = "此股票資料遺失過多";
                    return View();
                }

                var value = Convert.ToDouble(properties[i + 2].GetValue(stockTeches));
                if (value == 0)
                {
                    countNull += 1;
                    continue;
                }
                values.Add(value);
            }

            if (countNull > 0)
            {
                int firstNull = values.FindIndex(x => x.Equals(0));
                if (countNull > 1)
                {
                    int lastNull = values.FindLastIndex(x => x.Equals(0));
                    if (firstNull + 1 == lastNull && (firstNull - 1 < 0 || lastNull + 1 > 29))
                    {
                        ViewBag.ErroMsg = "此股票資料遺失過多";
                        return View();
                    }
                    else if (firstNull + 1 == lastNull)
                    {
                        values[lastNull] = (values[firstNull - 1] + values[lastNull + 1]) / 2;
                        values[firstNull] = (values[firstNull - 1] + values[lastNull + 1]) / 2;
                    }
                }
                else { values[firstNull] = (firstNull > 0) ? 0 : (values[firstNull - 1] + values[firstNull + 1]) / 2; }
                
            }

            string[] dates = new string[10];
            int countWeekend = 0;
            double[] fiveMA = new double[10];
            double[] tenMA = new double[10];
            double[] twentyMA = new double[10];

            for (int i = 0; i < 10; i++)
            {
                var date = DateTime.Today.AddDays(-(i + 1 + countWeekend));
                if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    date = date.AddDays(-2);
                    countWeekend += 2;
                }
                dates[i] = date.ToString("MM/dd");

                fiveMA[i] = values.GetRange(i, 5).Average();
                tenMA[i] = values.GetRange(i, 10).Average();
                twentyMA[i] = values.GetRange(i, 20).Average();
            }

            Array.Reverse(dates);
            Array.Reverse(fiveMA);
            Array.Reverse(tenMA);
            Array.Reverse(twentyMA);
            ViewBag.Cross = "";
            for(int i = 1; i < fiveMA.Length;i++)
            {
                if (fiveMA[i - 1] > twentyMA[i - 1] && fiveMA[i] < twentyMA[i]) 
                { 
                    ViewBag.Cross += dates[i - 1] +"~" + dates[i] + "出現死亡交叉。"; 
                }
                if (fiveMA[i - 1] < twentyMA[i - 1] && fiveMA[i] > twentyMA[i]) 
                { 
                    ViewBag.Cross += dates[i - 1] + "~" + dates[i] + "出現黃金交叉。"; 
                }
            }

            ChartData chartData = new ChartData
            {
                labels = dates,
                datasets = new Dataset[]{
                    new Dataset
                    {
                        label = "5MA",
                        data = fiveMA
                    },
                    new Dataset
                    {
                        label = "10MA",
                        data = tenMA
                    },
                    new Dataset
                    {
                        label = "20MA",
                        data = twentyMA
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
