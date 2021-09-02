namespace StockMap.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StockChip")]
    public partial class StockChip
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "股票代碼")]
        public string StockId { get; set; }

        [Display(Name = "外資買賣超")]
        public int StockFINBS { get; set; }

        [Display(Name = "投信買賣超")]
        public int StockTCNBS { get; set; }

        [Display(Name = "自營商買賣超")]
        public int StockDNBSProprietary { get; set; }

        [Display(Name = "自營商避險")]
        public int StockDNBSHedge { get; set; }
        
        [Display(Name = "三大法人買賣超")]
        public int ThreeSellBuy { get; set; }

        public virtual Stock Stock { get; set; }
    }
}
