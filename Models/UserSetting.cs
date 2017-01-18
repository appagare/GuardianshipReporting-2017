namespace GFR.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserSetting
    {
        [Key]
        [Column(Order = 0)]
        public string UserID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Group { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string Setting { get; set; }

        [Required]
        public string Value { get; set; }

        [StringLength(50)]
        [Display(Name = "Setting")]
        public string FriendlyName { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
    }
}
