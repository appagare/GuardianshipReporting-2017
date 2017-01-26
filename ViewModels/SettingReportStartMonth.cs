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
    public class SettingReportStartMonth : UserSetting
    {
        public ReportValues.MonthTypes StartMonth { get; set; }
        // public GFR.Models.UserSetting UserSetting { get; set; }
        public SettingReportStartMonth(UserSetting u)
        {
            this.AspNetUser = u.AspNetUser;
            this.Description = u.Description;
            this.FriendlyName = u.FriendlyName;
            this.Group = u.Group;
            this.Setting = u.Setting;
            this.UserID = u.UserID;
            this.Value = u.Value;

            ReportValues.MonthTypes m;
            Enum.TryParse(this.Value, out m);
            this.StartMonth = m;

        }
    }
}