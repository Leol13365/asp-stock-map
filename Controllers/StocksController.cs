using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
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
        public async Task<ActionResult> Index(string searchId)
        {
            ViewData["CurrentFilter"] = searchId;
            if (searchId == null) { ViewBag.ErroMsg = "請輸入股票代碼"; return View(); }
            var stockId = db.Stocks.Where(m => m.StockId == searchId).FirstOrDefault();
            if (stockId == null) { ViewBag.ErroMsg = "查無此股"; return View(); }
            DateTime localDate = DateTime.UtcNow;
            DateTime stockUpdate = db.Stocks.Find(searchId).UpdateTime;
            System.TimeSpan between = localDate.Subtract(stockUpdate);
            Stock stock = new Stock();
            if (between.TotalMinutes > 1 && localDate.Hour > 1 && localDate.Hour < 6)
            {
                try
                {
                    //Thread.Sleep(60000);
                    string targetURL = "http://localhost:5000/api/v1/stock?stock_id=" + searchId;
                    HttpClient client = new HttpClient();
                    client.MaxResponseContentBufferSize = Int32.MaxValue;
                    var response = await client.GetStringAsync(targetURL);
                    stock = JsonConvert.DeserializeObject<IEnumerable<Stock>>(response).First();
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine(e.ToString());
                    stock = db.Stocks.Find(searchId);
                }
            }

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
