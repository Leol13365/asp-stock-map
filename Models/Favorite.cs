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
        [Display(Name = "�Ѳ��N�X")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "�п�J���T���Ѳ��N�X�榡")]
        public string StockId { get; set; }

        public virtual Stock Stock { get; set; }

        public virtual User User { get; set; }

        public IEnumerable<StockMap.Models.Favorite> FavoriteIndex { get; set; }
    }
}
