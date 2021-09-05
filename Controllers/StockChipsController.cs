using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StockMap.Models;
using PagedList;

namespace StockMap.Controllers
{
    public class StockChipsController : Controller
    {
        private StockMapDbContext db = new StockMapDbContext();

        // GET: StockChips
        public ActionResult Index(string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FINBSSortParm = sortOrder == "finbs_esc" ? "finbs_desc" : "finbs_esc";
            ViewBag.ITCNBSSortParm = sortOrder == "itcnbs_esc" ? "itcnbs_desc" : "itcnbs_esc";
            ViewBag.DNBSSortParm = sortOrder == "dnbs_esc" ? "dnbs_desc" : "dnbs_esc";
            ViewBag.TSBSortParm = sortOrder == "tsb_esc" ? "tsb_desc" : "tsb_esc";
            var stockChips = from s in db.StockChips
                             select s;
            switch (sortOrder)
            {
                case "finbs_desc":
                    stockChips = stockChips.OrderByDescending(s => s.StockFINBS);
                    break;
                case "finbs_esc":
                    stockChips = stockChips.OrderBy(s => s.StockFINBS);
                    break;
                case "itcnbs_desc":
                    stockChips = stockChips.OrderByDescending(s => s.StockTCNBS);
                    break;
                case "itcnbs_esc":
                    stockChips = stockChips.OrderBy(s => s.StockTCNBS);
                    break;
                case "dnbs_desc":
                    stockChips = stockChips.OrderByDescending(s => s.StockDNBSProprietary);
                    break;
                case "dnbs_esc":
                    stockChips = stockChips.OrderBy(s => s.StockDNBSProprietary);
                    break;
                case "tsb_desc":
                    stockChips = stockChips.OrderByDescending(s => s.ThreeSellBuy);
                    break;
                case "tsb_esc":
                    stockChips = stockChips.OrderBy(s => s.ThreeSellBuy);
                    break;
                default:
                    stockChips = stockChips.OrderBy(s => s.StockId);
                    break;
            }
            return View(stockChips.ToPagedList(1, 10));
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
