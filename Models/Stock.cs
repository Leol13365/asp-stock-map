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
        [Display(Name = "�Ѳ��N�X")]
        public string StockId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "�Ѳ��W��")]
        public string StockName { get; set; }

        [Display(Name = "�����")]
        public decimal PriceNow { get; set; }

        [Display(Name = "��q")]
        public decimal VolumeNow { get; set; }

        [Display(Name = "�`�q")]
        public decimal VolumeAll { get; set; }

        [Display(Name = "�}�L��")]
        public decimal OpenPrice { get; set; }

        [Display(Name = "�̰���")]
        public decimal HighestPrice { get; set; }

        [Display(Name = "�̧C��")]
        public decimal LowestPrice { get; set; }

        [Display(Name = "���L��")]
        public decimal ClosingPrice { get; set; }

        [Display(Name = "���T")]
        public decimal Increase { get; set; }

        [Display(Name = "���T")]
        public decimal Amplitude { get; set; }

        [Display(Name = "���^")]
        public decimal Updown { get; set; }

        [Display(Name = "�Q���æ���")]
        public int FavoriteCount { get; set; }

        public DateTime CreateTime { get; set; }

        [Display(Name = "��Ƨ�s�ɶ�")]
        public DateTime UpdateTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Favorite> Favorites { get; set; }

        public virtual StockChip StockChip { get; set; }

        public virtual StockFound StockFound { get; set; }

        public virtual StockTech StockTech { get; set; }

        public virtual StockYesterday StockYesterday { get; set; }
    }
}
