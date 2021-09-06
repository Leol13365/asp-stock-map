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
using PagedList;

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
        public ActionResult Index(string sortOrder, string currentFilterMin, string currentFilterMax , String SearchIntMin , String SearchIntMax ,int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.PERSortParm = sortOrder == "per_esc" ? "per_desc" : "per_esc";
            if(SearchIntMin != null && SearchIntMax != null)
            {
                page = 1;
            }
            else
            {
                SearchIntMin = currentFilterMin;
                SearchIntMax = currentFilterMax;
            }
            int Max = 0;
            int Min = 0;
            bool isNumberOrNotNull = int.TryParse(SearchIntMin, out Min) == true && int.TryParse(SearchIntMax, out Max) == true;
            bool isNumber = (int.TryParse(SearchIntMin, out Min) == false && SearchIntMin != null) || (int.TryParse(SearchIntMax, out Max) == false && SearchIntMax != null);
            var Erro = "請輸入整數";
            if (isNumber) { ViewBag.erroMsg = Erro; }
            ViewBag.CurrentFilterMin = SearchIntMin;
            ViewBag.CurrentFilterMax = SearchIntMax;
            var stockFound = from s in db.StockFounds
                             select s;
            if (isNumberOrNotNull)
            {
                stockFound = db.StockFounds.Where(m => m.PERatio >= Min & m.PERatio <= Max);
            }
            switch (sortOrder)
            {
                case "per_desc":
                    stockFound = stockFound.OrderByDescending(s => s.PERatio);
                    break;
                case "per_esc":
                    stockFound = stockFound.OrderBy(s => s.PERatio);
                    break;
                default:
                    stockFound = stockFound.OrderBy(s => s.StockId);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(stockFound.ToPagedList(pageNumber, pageSize));
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
