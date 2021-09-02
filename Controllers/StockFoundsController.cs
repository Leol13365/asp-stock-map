using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using StockMap.Models;

namespace StockMap.Controllers
{
    public class StockFoundsController : Controller
    {
        private StockMapDbContext db = new StockMapDbContext();

        public async Task<ActionResult> Test()
        {
            string targetUrl = "https://jsonplaceholder.typicode.com/todos";

            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = Int32.MaxValue;
            var response = await client.GetStringAsync(targetUrl);
            var collection = JsonConvert.DeserializeObject<IEnumerable<HotSpot>>(response);
            //ViewBag.Result = response;
            return View(collection);
        }

        // GET: StockFounds
        public ActionResult Index()
        {
            var stockFounds = db.StockFounds.Include(s => s.Stock);
            return View(stockFounds.ToList());
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
