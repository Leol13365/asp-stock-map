namespace StockMap.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StockTech")]
    public partial class StockTech
    {
        [Key]
        [StringLength(20)]
        public string StockId { get; set; }

        public decimal Stock5MA { get; set; }

        public decimal Stock10MA { get; set; }

        public decimal Stock20MA { get; set; }

        public decimal Stock60MA { get; set; }

        public virtual Stock Stock { get; set; }
    }
}
