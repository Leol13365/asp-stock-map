namespace StockMap.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StockYesterday")]
    public partial class StockYesterday
    {
        [Key]
        [StringLength(20)]
        public string StockId { get; set; }

        public DateTime StockDate { get; set; }

        public int Volumn { get; set; }

        public decimal OpenPrice { get; set; }

        public decimal HighestPrice { get; set; }

        public decimal LowestPrice { get; set; }

        public decimal ClosingPrice { get; set; }

        public int SellBuy { get; set; }

        public virtual Stock Stock { get; set; }
    }
}
