namespace StockMap.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StockFound")]
    public partial class StockFound
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "股票代碼")]
        public string StockId { get; set; }

        [Display(Name = "收盤價")]
        public decimal ClosingPrice { get; set; }

        [Display(Name = "本益比")]
        public decimal PERatio { get; set; }

        [Display(Name = "股息")]
        public decimal Dividend { get; set; }

        public virtual Stock Stock { get; set; }
    }
    public class HotSpot
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public bool completed { get; set; }
    }

}
