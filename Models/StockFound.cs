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
        public string StockId { get; set; }

        public decimal ClosingPrice { get; set; }

        public decimal PERatio { get; set; }

        public decimal Dividend { get; set; }

        public virtual Stock Stock { get; set; }
    }
}
