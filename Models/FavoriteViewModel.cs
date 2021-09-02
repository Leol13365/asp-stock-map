using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockMap.Models
{
    public class FavoriteViewModel
    {
        public StockMap.Models.Favorite FavoriteCreate { get; set; }
        public IEnumerable<StockMap.Models.Favorite> FavoriteIndex { get; set; }
    }
}