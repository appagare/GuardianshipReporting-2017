namespace GFR.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReportDetail")]
    public partial class ReportDetail
    {
        public int ReportDetailID { get; set; }

        public int ReportID { get; set; }

        [Required]
        [StringLength(128)]
        public string UserID { get; set; }

        public int WardID { get; set; }

        public int CategoryID { get; set; }

        public byte Worksheet { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public byte Month { get; set; }

        public short Year { get; set; }

        [Column(TypeName = "money")]
        public decimal Value { get; set; }

        public byte VOrdinal { get; set; }

        public byte HOrdinal { get; set; }

        public DateTime LastUpdated { get; set; }

        public DateTime? DeletedDate { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual Report Report { get; set; }

        public virtual UserCategory UserCategory { get; set; }

        public virtual Ward Ward { get; set; }
    }
}
