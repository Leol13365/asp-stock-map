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
        [Display(Name = "布絏")]
        public string StockId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "布嘿")]
        public string StockName { get; set; }

        [Display(Name = "Θユ基")]
        public decimal PriceNow { get; set; }

        [Display(Name = "虫秖")]
        public decimal VolumeNow { get; set; }

        [Display(Name = "羆秖")]
        public decimal VolumeAll { get; set; }

        [Display(Name = "秨絃基")]
        public decimal OpenPrice { get; set; }

        [Display(Name = "程蔼基")]
        public decimal HighestPrice { get; set; }

        [Display(Name = "程基")]
        public decimal LowestPrice { get; set; }

        [Display(Name = "Μ絃基")]
        public decimal ClosingPrice { get; set; }

        [Display(Name = "害碩")]
        public decimal Increase { get; set; }

        [Display(Name = "碩")]
        public decimal Amplitude { get; set; }

        [Display(Name = "害禴")]
        public decimal Updown { get; set; }

        [Display(Name = "砆Μ旅Ω计")]
        public int FavoriteCount { get; set; }

        public DateTime CreateTime { get; set; }

        [Display(Name = "戈穝丁")]
        public DateTime UpdateTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Favorite> Favorites { get; set; }

        public virtual StockChip StockChip { get; set; }

        public virtual StockFound StockFound { get; set; }

        public virtual StockTech StockTech { get; set; }

        public virtual StockYesterday StockYesterday { get; set; }
    }
}
