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
        [Display(Name = "�Ѳ��N�X")]
        public string StockId { get; set; }

        [Display(Name = "�~��R��W")]
        public int StockFINBS { get; set; }

        [Display(Name = "��H�R��W")]
        public int StockTCNBS { get; set; }

        [Display(Name = "����ӶR��W")]
        public int StockDNBSProprietary { get; set; }

        [Display(Name = "��������I")]
        public int StockDNBSHedge { get; set; }
        
        [Display(Name = "�T�j�k�H�R��W")]
        public int ThreeSellBuy { get; set; }

        public virtual Stock Stock { get; set; }
    }
}
