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
    public class StockChipsController : Controller
    {
        private StockMapDbContext db = new StockMapDbContext();

        // GET: StockChips
        public ActionResult Index()
        {
            var stockChips = db.StockChips.Include(s => s.Stock);
            return View(stockChips.ToList());
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
