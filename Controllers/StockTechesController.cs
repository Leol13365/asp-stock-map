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
            //string[] dates = new string[20];
            //for (int i = 1; i < 20; i++)
            //{
            //    dates[i-1] = DateTime.Now.AddDays(-i).ToString("yyyy/MM/dd");
            //}
            //ViewBag.DateLabel = dates;

            //new ModelChartJs { stockName = db.StockTeches}

            var stockTeches = db.StockTeches.Include(s => s.Stock);
            return View(stockTeches.ToList());
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
