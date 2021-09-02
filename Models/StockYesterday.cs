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
        [Display(Name = "�Ѳ��N�X")]
        public string StockId { get; set; }

        [Display(Name = "���")]
        public DateTime StockDate { get; set; }

        [Display(Name = "����q")]
        public int Volumn { get; set; }

        [Display(Name = "�}�L��")]
        public decimal OpenPrice { get; set; }

        [Display(Name = "�̰���")]
        public decimal HighestPrice { get; set; }

        [Display(Name = "�̧C��")]
        public decimal LowestPrice { get; set; }

        [Display(Name = "���L��")]
        public decimal ClosingPrice { get; set; }

        [Display(Name = "�R��W")]
        public int SellBuy { get; set; }

        public virtual Stock Stock { get; set; }
    }
}
