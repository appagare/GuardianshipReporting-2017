using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GFR.Models;

namespace GFR.ViewModels
{
    [NotMapped]
    public class SettingReportStartYear : UserSetting
    {
        [YearRangeAttribute(ErrorMessage = "Year must be between {0} and {1}.")]
        public short StartYear { get; set; }

        public SettingReportStartYear(UserSetting u)
        {
            this.AspNetUser = u.AspNetUser;
            this.Description = u.Description;
            this.FriendlyName = u.FriendlyName;
            this.Group = u.Group;
            this.Setting = u.Setting;
            this.UserID = u.UserID;
            this.Value = u.Value;
            
            this.StartYear = Int16.Parse(this.Value);
            
        }

    }

    // derived from http://stackoverflow.com/questions/16100300/asp-net-mvc-custom-validation-by-dataannotation
    // and  https://msdn.microsoft.com/en-us/library/cc668224.aspx
    // and https://www.codeproject.com/articles/260177/custom-validation-attribute-in-asp-net-mvc3

    public class YearRangeAttribute : ValidationAttribute
    {
        private int min;
        private int max;

        public YearRangeAttribute()
        {
            this.min = DateTime.Now.Year - 6;
            this.max = DateTime.Now.Year + 6;
        }

        public override bool IsValid(object value)
        {
            if (((int)value >= min) && ((int)value <= max))
            {
                return true;
            }
            return false;
        }
        public override string FormatErrorMessage(string name)
        {

            return String.Format(ErrorMessageString, name, min, max);
        }
    }
        

}