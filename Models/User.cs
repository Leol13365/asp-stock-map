namespace StockMap.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Favorites = new HashSet<Favorite>();
        }

        [Key]
        [Required(ErrorMessage ="使用者帳號是必填項目")]
        [StringLength(20)]
        [Display(Name = "帳號")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "僅能有英文以及數字")]
        public string Account { get; set; }

        [Required(ErrorMessage = "使用者密碼是必填項目")]
        [StringLength(20)]
        [Display(Name = "密碼")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "僅能有英文以及數字")]
        public string Password { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Favorite> Favorites { get; set; }
    }

    [NotMapped]
    public partial class RegisterUser:User
    {
        [NotMapped]
        [Required(ErrorMessage = "二次密碼確認是必填項目")]
        [StringLength(20)]
        [Display(Name = "確認密碼")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "必須與密碼相同")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "僅能有英文以及數字")]
        public string ConfirmedPassword { get; set; }
    }

}
