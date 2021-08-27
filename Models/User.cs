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
        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "�ȯ�]�t�^��r���H�μƦr")]
        public string Account { get; set; }

        [Required]
        [StringLength(20)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "�ȯ�]�t�^��r���H�μƦr")]
        public string Password { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Favorite> Favorites { get; set; }
    }

    [NotMapped]
    public partial class RegisterUser:User
    {
        [NotMapped]
        [Required]
        [StringLength(20)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "���ݻPPassword���ȬۦP")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "�ȯ�]�t�^��r���H�μƦr")]
        public string ConfirmedPassword { get; set; }
    }

}
