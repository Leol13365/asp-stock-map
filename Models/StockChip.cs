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

        public int StockFINB { get; set; }

        public int StockFINS { get; set; }

        public int StockTCNB { get; set; }

        public int StockTCNS { get; set; }

        public int StockDNB { get; set; }

        public int StockDNS { get; set; }

        public virtual Stock Stock { get; set; }
    }
}
