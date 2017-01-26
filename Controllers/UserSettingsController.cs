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
    public class UserSettingsController : Controller
    {
        private GuardianshipDB db = new GuardianshipDB();

        // GET: UserSettings
        public ActionResult Index()
        {
            string UserID = User.Identity.GetUserId();
            ViewBag.UserID = UserID; // User.Identity.GetUserId();
            // var userSettings = db.UserSettings.Include(u => u.AspNetUser).Where(u => u.UserID == UserID);
            var userSettings = db.UserSettings.Where(u => u.UserID == UserID);
            return View(userSettings.ToList());
        }

        //// GET: UserSettings/Details/5
        //public ActionResult Details(string id)
        //{
        //    ViewBag.UserID = User.Identity.GetUserId();
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    UserSetting userSetting = db.UserSettings.Find(id);
        //    if (userSetting == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(userSetting);
        //}

        //// GET: UserSettings/Create
        //public ActionResult Create()
        //{
        //    ViewBag.UserID = User.Identity.GetUserId();
        //    return View();
        //}

        //// POST: UserSettings/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "UserID,Group,Setting,Value")] UserSetting userSetting)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.UserSettings.Add(userSetting);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.UserID = User.Identity.GetUserId();
        //    return View(userSetting);
        //}

        // GET: UserSettings/Edit/5
        public ActionResult Edit(string group, string setting)
        {

            if ((group == null) || (setting == null))
            {
                // index when insufficient info passed
                return RedirectToAction("Index");
            }
            ViewBag.UserID = User.Identity.GetUserId();
            UserSetting userSetting = db.UserSettings.Find(ViewBag.UserID, group, setting);
            if (userSetting == null)
            {
                return HttpNotFound();
            }

            switch (setting)
            {
                case ReportValues.REPORT_SETTING_PERIOD_DURATION:
                    // cast as specific 
                    GFR.ViewModels.SettingReportDuration srd = new ViewModels.SettingReportDuration(userSetting);
                    //srd.UserSetting = userSetting;
                    return View("ReportDuration", srd);
                    break;
                case ReportValues.REPORT_SETTING_PERIOD_START_MONTH:
                    GFR.ViewModels.SettingReportStartMonth srm = new ViewModels.SettingReportStartMonth(userSetting);
                    //srm.UserSetting = userSetting;
                    return View("ReportStartMonth", srm);
                    break;
                default:
                    GFR.ViewModels.SettingReportStartYear sry = new ViewModels.SettingReportStartYear(userSetting);
                    //sry.UserSetting = userSetting;
                    return View("ReportStartYear", sry);
                    break;
            }
            return View(userSetting);
        }

        // POST: UserSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Group,Setting,Value,FriendlyName,Description")] UserSetting userSetting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userSetting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // ViewBag.UserID = User.Identity.GetUserId();
            ViewBag.UserID = userSetting.UserID;
            return View(userSetting);
        }

        //// GET: UserSettings/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    UserSetting userSetting = db.UserSettings.Find(id);
        //    if (userSetting == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(userSetting);
        //}

        //// POST: UserSettings/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    UserSetting userSetting = db.UserSettings.Find(id);
        //    db.UserSettings.Remove(userSetting);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
