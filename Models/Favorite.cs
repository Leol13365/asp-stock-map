namespace StockMap.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Favorite")]
    public partial class Favorite
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string UserAccount { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "股票代碼")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "請輸入正確的股票代碼格式")]
        public string StockId { get; set; }

        public virtual Stock Stock { get; set; }

        public virtual User User { get; set; }

        public IEnumerable<StockMap.Models.Favorite> FavoriteIndex { get; set; }
    }
}
