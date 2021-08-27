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
        public string StockId { get; set; }

        public int StockFINBS { get; set; }

        public int StockTCNBS { get; set; }

        public int StockDNBSSelf { get; set; }

        public int StockDNBSRisk { get; set; }

        public int ThreeSellBuy { get; set; }

        public virtual Stock Stock { get; set; }
    }
}
