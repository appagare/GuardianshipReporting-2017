using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GFR.Models;
using Microsoft.AspNet.Identity;

namespace GFR.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private GuardianshipDB db = new GuardianshipDB();

        private IQueryable<Ward> userWards(string UserID)
        {
            return db.Wards.Where(w => w.UserID == UserID)
                            .Where(w => w.DeletedDate == null);
                
        }
        // GET: Reports
        //public ActionResult Index()
        //{
        //    string UserID = User.Identity.GetUserId();
        //    // var reports = db.Reports.Include(r => r.AspNetUser).Where(r => r.UserID == UserID);
        //    var reports = db.Reports.Where(r => r.UserID == UserID);
        //    reports = db.Reports.Where(r => r.UserID == UserID);
        //    return View(reports.ToList());
        //}
        public ActionResult Index(int? id)
        {
            // id = WardID

            string UserID = User.Identity.GetUserId();
            // var reports = db.Reports.Include(r => r.AspNetUser).Where(r => r.UserID == UserID);
            // var reports = db.Reports.Where(r => r.UserID == UserID);
            GFR.ViewModels.ReportViewModel rvm;
            if (id != null)
            {
                rvm = new ViewModels.ReportViewModel(UserID, id.Value);
            }
            else
            {
                rvm = new ViewModels.ReportViewModel(UserID);
            }
            return View(rvm);

        }

        //public ActionResult SelectWard()
        //{

        //    List<SelectListItem> items = new List<SelectListItem>();

        //    items.Add(new SelectListItem { Text = "Action", Value = "0" });


        //    items.Add(new SelectListItem { Text = "Drama", Value = "1" });

        //    items.Add(new SelectListItem { Text = "Comedy", Value = "2", Selected = true });

        //    items.Add(new SelectListItem { Text = "Science Fiction", Value = "3" });

        //    ViewBag.MovieType = items;

        //    //List<SelectListItem> listItems = new List<SelectListItem>();
        //    //listItems.Add(new SelectListItem
        //    //{
        //    //    Text = "",
        //    //    Value = "0"
        //    //});
        //    //foreach (var item in Model.WardsList.ToList())
        //    //{
        //    //    listItems.Add(new SelectListItem
        //    //    {
        //    //        Text = item.FullName,
        //    //        Value = item.WardID.ToString()
        //    //    });
        //    //}

        //    return View();

        //}

        // GET: Reports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                // index when no id passed
                return RedirectToAction("Index");
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            else if (report.DeletedDate != null)
            {
                // don't show deleted records
                return HttpNotFound();
            }

            ViewBag.UserID = report.UserID;
            return View(report);
        }

        // GET: Reports/Create
        public ActionResult Create(int? id)
        {

            string UserID = User.Identity.GetUserId();
            // var reports = db.Reports.Include(r => r.AspNetUser).Where(r => r.UserID == UserID);
            ViewBag.UserID = UserID;

            
            // non-deleted 
            UserSetting s = new UserSetting();
            Report r = new Report { UserID = UserID };
            
            // set defaults
            List<UserSetting> defaults = db.UserSettings.Where(u => u.UserID == UserID).ToList();

            foreach (UserSetting u in defaults)
            {
                if (u.Setting.ToUpper() == ReportValues.REPORT_SETTING_PERIOD_DURATION.ToUpper())
                {
                    r.PeriodDuration = (ReportValues.DurationTypes)Enum.Parse(typeof(ReportValues.DurationTypes), u.Value);
                    
                }
                else if (u.Setting.ToUpper() == ReportValues.REPORT_SETTING_PERIOD_START_MONTH.ToUpper())
                {
                    r.PeriodStartMonth = (ReportValues.MonthTypes)Enum.Parse(typeof(ReportValues.MonthTypes), u.Value);

                }
                else if (u.Setting.ToUpper() == ReportValues.REPORT_SETTING_PERIOD_START_YEAR.ToUpper())
                {
                    r.PeriodStartYear = Convert.ToInt16(u.Value);

                }
            }

            // ward-specific
            // todo: fix this; it should be the underlying value

            if (id != null)
            {
                r.PeriodStartMonth = (ReportValues.MonthTypes)Enum.Parse(typeof(ReportValues.MonthTypes), db.Wards.Find(id).PeriodStartMonth.ToString());
                r.PeriodDuration = (ReportValues.DurationTypes)Enum.Parse(typeof(ReportValues.DurationTypes), db.Wards.Find(id).PeriodDuration.ToString());

                // todo: set this to the last completed report + duration
                // r.PeriodStartYear = 
                ViewBag.WardID = new SelectList(userWards(UserID), "WardID", "FullName", id);

            } else
            {
                ViewBag.WardID = new SelectList(userWards(UserID), "WardID", "FullName");
            }

            
            
            
     return View(r);
 }

 // POST: Reports/Create
 // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
 // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
 [HttpPost]
 [ValidateAntiForgeryToken]
 public ActionResult Create([Bind(Include = "ReportID,WardID,UserID,StateCode,PeriodStartMonth,PeriodStartYear,PeriodDuration,CreateDate,LastUpdated,SubmittedDate,DeletedDate")] Report report)
 {
     if (ModelState.IsValid)
     {
         report.CreateDate = DateTime.Now;
         report.LastUpdated = DateTime.Now;
         db.Reports.Add(report);
         db.SaveChanges();
         return RedirectToAction("Index", new { id = report.WardID });
     }

     ViewBag.UserID = User.Identity.GetUserId();
     ViewBag.WardID = new SelectList(userWards(report.UserID), "WardID", "FullName", report.WardID);
     return View(report);
 }

 // GET: Reports/Edit/5
 public ActionResult Edit(int? id)
 {
     if (id == null)
     {
         // index when no id passed
         return RedirectToAction("Index");
     }
     Report report = db.Reports.Find(id);
     if (report == null)
     {
         return HttpNotFound();
     }
     else if (report.DeletedDate != null)
     {
        // don't show deleted records
        return HttpNotFound();
     }
     ViewBag.UserID = report.UserID;
     //var wards = db.Wards.Where(w => w.UserID == report.UserID);
     ViewBag.WardID = new SelectList(userWards(report.UserID), "WardID", "FullName");
     return View(report);
 }

 // POST: Reports/Edit/5
 // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
 // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
 [HttpPost]
 [ValidateAntiForgeryToken]
 public ActionResult Edit([Bind(Include = "ReportID,WardID,UserID,StateCode,PeriodStartMonth,PeriodStartYear,PeriodDuration,CreateDate")] Report report)
 {
     if (ModelState.IsValid)
     {
        report.LastUpdated = DateTime.Now;
        db.Entry(report).State = EntityState.Modified;
         db.SaveChanges();
         return RedirectToAction("Index", new { id = report.WardID });
     }
     ViewBag.UserID = report.UserID;
     ViewBag.WardID = new SelectList(userWards(report.UserID), "WardID", "FullName");
     return View(report);
 }

 // GET: Reports/Delete/5
 public ActionResult Delete(int? id)
 {
     if (id == null)
     {
         // index when no id passed
         return RedirectToAction("Index");
     }
     Report report = db.Reports.Find(id);
     if (report == null)
     {
         return HttpNotFound();
     }
     else if (report.DeletedDate != null)
     {
        // don't show deleted records
        return HttpNotFound();
     }
     ViewBag.UserID = report.UserID;
     return View(report);
 }

 // POST: Reports/Delete/5
 [HttpPost, ActionName("Delete")]
 [ValidateAntiForgeryToken]
 public ActionResult DeleteConfirmed(int id)
 {
     // todo: change this to only set the DeletedDate;
     Report report = db.Reports.Find(id);
     db.Reports.Remove(report);
     db.SaveChanges();
     return RedirectToAction("Index");
 }

 protected override void Dispose(bool disposing)
 {
     if (disposing)
     {
         db.Dispose();
     }
     base.Dispose(disposing);
 }
}
    
}
