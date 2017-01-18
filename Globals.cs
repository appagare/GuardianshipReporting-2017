using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GFR
{
    public static class Globals
    {
        /*  constants */
        public const string ONE_YEAR = "1 Yr.";
        public const string TWO_YEAR = "2 Yr.";
        public const string THREE_YEAR = "3 Yr.";

        public const string STATUS_ACTIVE = "In-process";
        // const string STATUS_COMPLETE = "Ready to submit";
        public const string STATUS_SUBMITTED = "Submitted";
    }
    public static class ReportValues
    {
        public const string REPORT_SETTING_PERIOD_DURATION = "PeriodDuration";
        public const string REPORT_SETTING_PERIOD_START_MONTH = "PeriodStartMonth";
        public const string REPORT_SETTING_PERIOD_START_YEAR = "PeriodStartYear";

        public enum DurationTypes : byte
        {
            OneYear = 12,
            TwoYear = 24,
            ThreeYear = 36
        }

       
        public enum MonthTypes : byte
        {
            Jan = 1,
            Feb = 2,
            Mar = 3,
            Apr = 4,
            May = 5,
            Jun = 6,
            Jul = 7,
            Aug = 8,
            Sep = 9,
            Oct = 10,
            Nov = 11,
            Dec = 12
        }

        public enum StatusTypes : byte
        {
            Active = 0,
            //  Complete = 1,
            Submitted = 2
        }
    }

    public static class CategoryEnums
    {
        public enum CategoryTypes : byte
        {
            Income = 0,
            Expense = 1
        }
        public enum CategoryBehaviors : byte
        {
            Single = 0,
            Multiple = 1
        }
    }
}