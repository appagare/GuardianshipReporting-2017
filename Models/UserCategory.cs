namespace GFR.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserCategory")]
    public partial class UserCategory
    {
        private const string WA = "WA";
        private string stateCode = WA; // initially, we only support Washington

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserCategory()
        {
            ReportDetails = new HashSet<ReportDetail>();
        }

        [Key]
        public int CategoryID { get; set; }

        [Required]
        [StringLength(128)]
        public string UserID { get; set; }

        [Required]
        [StringLength(2)]
        [Display(Name = "State")]
        public string StateCode
        {
            get
            {
                return stateCode;
            }
            set
            {
                //stateCode = value;
                stateCode = WA;
            }
        }

        [Display(Name = "Type")]
        [Range(0, 1)]
        [Required(ErrorMessage = @"Type must be ""Income"" (0) or ""Expense"" (1)")]
        public CategoryEnums.CategoryTypes CategoryType { get; set; }

        [Display(Name = "Behavior")]
        [Range(0, 1)]
        [Required(ErrorMessage = @"Behavior must be ""Single-entry"" (0) or ""Multi-entry"" (1)")]
        public CategoryEnums.CategoryBehaviors CategoryClass { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Display(Name = "Display Order")]
        [Required(ErrorMessage = @"Field must be between 0 and 255")]
        public byte Ordinal { get; set; }

        [Display(Name = "Hidden")]
        public bool Hide { get; set; }


        public virtual AspNetUser AspNetUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReportDetail> ReportDetails { get; set; }
    }
}
