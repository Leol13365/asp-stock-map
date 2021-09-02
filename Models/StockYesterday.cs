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
        [Display(Name = "布絏")]
        public string StockId { get; set; }

        [Display(Name = "ら戳")]
        public DateTime StockDate { get; set; }

        [Display(Name = "ユ秖")]
        public int Volumn { get; set; }

        [Display(Name = "秨絃基")]
        public decimal OpenPrice { get; set; }

        [Display(Name = "程蔼基")]
        public decimal HighestPrice { get; set; }

        [Display(Name = "程基")]
        public decimal LowestPrice { get; set; }

        [Display(Name = "Μ絃基")]
        public decimal ClosingPrice { get; set; }

        [Display(Name = "禦芥禬")]
        public int SellBuy { get; set; }

        public virtual Stock Stock { get; set; }
    }
}
