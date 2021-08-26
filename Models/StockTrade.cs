namespace StockMap.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StockTrade")]
    public partial class StockTrade
    {
        [Key]
        [StringLength(20)]
        public string StockId { get; set; }

        public int SSell { get; set; }

        public int SBuy { get; set; }

        public decimal Price { get; set; }

        public virtual Stock Stock { get; set; }
    }
}
