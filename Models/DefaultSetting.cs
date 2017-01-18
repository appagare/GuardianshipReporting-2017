namespace GFR.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DefaultSetting
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string SystemGroup { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string SystemParameter { get; set; }

        [Required]
        public string SystemValue { get; set; }

        [StringLength(50)]
        public string SystemFriendlyName { get; set; }

        [StringLength(255)]
        public string SystemDescription { get; set; }
    }
}
