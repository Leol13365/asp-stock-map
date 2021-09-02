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
    public class StockYesterdaysController : Controller
    {
        private StockMapDbContext db = new StockMapDbContext();

        // GET: StockYesterdays
        public ActionResult Index()
        {
            var stockYesterdays = db.StockYesterdays.Include(s => s.Stock);
            return View(stockYesterdays.ToList());
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
