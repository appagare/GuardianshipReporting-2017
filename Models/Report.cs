namespace GFR.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Report")]
    public partial class Report
    {
        private const string WA = "WA";
        private string stateCode = WA; // initially, we only support Washington


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Report()
        {
            ReportDetails = new HashSet<ReportDetail>();
        }

        public int ReportID { get; set; }

        public int WardID { get; set; }

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

        [Required]
        [Display(Name = "Start Month")]
        [Range(1, 12, ErrorMessage = "Month is required")]
        public ReportValues.MonthTypes PeriodStartMonth { get; set; }

        [Required]
        [Display(Name = "Start year")]
        [Range(2015, 2025, ErrorMessage = "Year is required")]
        public short PeriodStartYear { get; set; }

        [Required]
        [Display(Name = "Duration")]
        public ReportValues.DurationTypes PeriodDuration { get; set; }

        [Display(Name = "Date Created")]
        public DateTime CreateDate { get; set; } // date created

        [Display(Name = "Last Updated")]
        public DateTime LastUpdated { get; set; } // date updated

        [Display(Name = "Date Completed")]
        public DateTime? SubmittedDate { get; set; } // date accepted by count

        public DateTime? DeletedDate { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual Ward Ward { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReportDetail> ReportDetails { get; set; }

        [Display(Name = "Report Duration")]
        public string DurationText
        {
            get
            {
                string r = Globals.ONE_YEAR;
                switch (PeriodDuration)
                {
                    case ReportValues.DurationTypes.TwoYear:
                        r = Globals.TWO_YEAR;
                        break;
                    case ReportValues.DurationTypes.ThreeYear:
                        r = Globals.THREE_YEAR;
                        break;
                }
                return r;
            }
        }
        [Display(Name = "Status")]
        public string StatusText
        {
            get
            {
                string r = Globals.STATUS_ACTIVE;
                if (SubmittedDate != null)
                {
                    r = Globals.STATUS_SUBMITTED;
                }
                return r;
            }
        }
        [Display(Name = "Period")]
        public string PeriodText
        {
            get
            {
                string r = PeriodStartMonth + "/" + PeriodStartYear + " - ";
                if (PeriodStartMonth == ReportValues.MonthTypes.Jan)
                {
                    r += ReportValues.MonthTypes.Dec + "/";
                }
                else
                {
                    r += (PeriodStartMonth - 1) + "/";
                }

                switch (PeriodDuration)
                {
                    case ReportValues.DurationTypes.ThreeYear:
                        r += (PeriodStartYear + 3);
                        break;
                    case ReportValues.DurationTypes.TwoYear:
                        r += (PeriodStartYear + 2);
                        break;
                    case ReportValues.DurationTypes.OneYear:
                        r += (PeriodStartYear + 1);
                        break;
                    case default(ReportValues.DurationTypes):
                        r += (PeriodStartYear + 1);
                        break;

                }
                return r;
            }
        }

    }
}
