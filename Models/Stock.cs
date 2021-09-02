namespace StockMap.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Stock")]
    public partial class Stock
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Stock()
        {
            Favorites = new HashSet<Favorite>();
        }

        [StringLength(20)]
        public string StockId { get; set; }

        [Required]
        [StringLength(50)]
        public string StockName { get; set; }

        public decimal PriceNow { get; set; }

        public decimal VolumeNow { get; set; }

        public decimal VolumeAll { get; set; }

        public decimal OpenPrice { get; set; }

        public decimal HighestPrice { get; set; }

        public decimal LowestPrice { get; set; }

        public decimal ClosingPrice { get; set; }

        public decimal Increase { get; set; }

        public decimal Amplitude { get; set; }

        public decimal Updown { get; set; }

        public int FavoriteCount { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Favorite> Favorites { get; set; }

        public virtual StockChip StockChip { get; set; }

        public virtual StockFound StockFound { get; set; }

        public virtual StockTech StockTech { get; set; }

        public virtual StockYesterday StockYesterday { get; set; }
    }
}
