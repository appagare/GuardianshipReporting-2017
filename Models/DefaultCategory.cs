namespace GFR.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DefaultCategory")]
    public partial class DefaultCategory
    {
        [Required]
        [StringLength(2)]
        public string StateCode { get; set; }

        [Key]
        [Column(Order = 0)]
        public CategoryEnums.CategoryTypes CategoryType { get; set; }

        public CategoryEnums.CategoryBehaviors CategoryClass { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string CategoryName { get; set; }

        public byte Ordinal { get; set; }
    }
}
