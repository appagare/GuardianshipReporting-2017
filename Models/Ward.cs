namespace GFR.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ward")]
    public partial class Ward
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ward()
        {
            Reports = new HashSet<Report>();
            ReportDetails = new HashSet<ReportDetail>();
        }

        public int WardID { get; set; }

        [Required]
        [StringLength(128)]
        public string UserID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(10)]
        [Display(Name = "Middle")]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(10)]
        public string Suffix { get; set; }

        [RegularExpression(@"[MF]")]
        [Required(ErrorMessage = @"Gender must be ""M"" or ""F""")]
        [StringLength(1)]
        public string Gender { get; set; }

        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DOB { get; set; }

        [Display(Name = "Record Created")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Record Updated")]
        public DateTime? LastUpdated { get; set; }

        [Display(Name = "Record Deleted")]
        public DateTime? DeletedDate { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Report> Reports { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReportDetail> ReportDetails { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return FirstName + ' ' + LastName; }
        }
    }
}
