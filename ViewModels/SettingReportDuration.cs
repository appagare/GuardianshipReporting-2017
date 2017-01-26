using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//using System.Data;
//using System.Data.Entity;
//using System.Net;
//using System.Web.Mvc;
using GFR.Models;
//using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFR.ViewModels
{
    [NotMapped]
    public class SettingReportDuration : UserSetting
    {
        public ReportValues.DurationTypes DurationType { get; set; }
        public SettingReportDuration(UserSetting u)
        {
            this.AspNetUser = u.AspNetUser;
            this.Description = u.Description;
            this.FriendlyName = u.FriendlyName;
            this.Group = u.Group;
            this.Setting = u.Setting;
            this.UserID = u.UserID;
            this.Value = u.Value;

            ReportValues.DurationTypes d;
            Enum.TryParse(this.Value, out d);

            this.DurationType = d;
            
            //switch (this.Value)
            //{
            //    case "12" :
            //        this.DurationType = ReportValues.DurationTypes.OneYear;
            //        break;
            //    case "24":
            //        this.DurationType = ReportValues.DurationTypes.TwoYear;
            //        break;
            //    default:
            //        this.DurationType = ReportValues.DurationTypes.ThreeYear;
            //        break;
            //}
        }
        
    }
}