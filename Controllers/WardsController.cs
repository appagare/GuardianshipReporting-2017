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
    public class WardsController : Controller
    {
        private GuardianshipDB db = new GuardianshipDB();

        // GET: Wards
        public ActionResult Index()
        {
            string UserID = User.Identity.GetUserId();
            ViewBag.UserID = UserID; // User.Identity.GetUserId();
            // var wards = db.Wards.Include(w => w.AspNetUser).Where(w => w.UserID == UserID);
            var wards = db.Wards.Where(w => w.UserID == UserID)
                .Where(w => w.DeletedDate == null);


            return View(wards.ToList());
        }

        // GET: Wards/Details/5
        public ActionResult Details(int? id)
        {
            // ViewBag.UserID = User.Identity.GetUserId();
            if (id == null)
            {
                // index when no id passed
                return RedirectToAction("Index");
            }
            Ward ward = db.Wards.Find(id);
            if (ward == null)
            {
                // index or error
                return HttpNotFound();
            } else if (ward.DeletedDate != null)
            {
                // don't show deleted wards
                return HttpNotFound();
            }

            // ViewBag.UserID = User.Identity.GetUserId();
            ViewBag.UserID = ward.UserID;
            return View(ward);
        }

        // GET: Wards/Create
        public ActionResult Create()
        {
            string UserID = User.Identity.GetUserId();
            ViewBag.UserID = UserID; // User.Identity.GetUserId();
            Ward ward = new Ward();
            ward.UserID = UserID;
             
            List<UserSetting> defaults = db.UserSettings.Where(u => u.UserID == UserID).ToList();

            foreach (UserSetting u in defaults)
            {
                if (u.Setting.ToUpper() == ReportValues.REPORT_SETTING_PERIOD_DURATION.ToUpper())
                {

                    ward.PeriodDuration = Convert.ToByte(u.Value);

                }
                else if (u.Setting.ToUpper() == ReportValues.REPORT_SETTING_PERIOD_START_MONTH.ToUpper())
                {
                    ward.PeriodStartMonth = Convert.ToByte(u.Value);

                }
            }

            return View(ward);

            //return View();
        }

        // POST: Wards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WardID,UserID,FirstName,MiddleName,LastName,Suffix,Gender,PeriodStartMonth,PeriodDuration,CreateDate,LastUpdated,DeletedDate")] Ward ward)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(ward.MiddleName))
                {
                    ward.MiddleName = "";
                }
                if (string.IsNullOrEmpty(ward.Suffix))
                {
                    ward.Suffix = "";
                }
                ward.CreateDate = DateTime.Now;
                ward.LastUpdated = DateTime.Now;
                db.Wards.Add(ward);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // ViewBag.UserID = User.Identity.GetUserId();
            ViewBag.UserID = ward.UserID;
            return View(ward);
        }

        // GET: Wards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                // index when no id passed
                return RedirectToAction("Index");
            }
            Ward ward = db.Wards.Find(id);
            if (ward == null)
            {
                // index or error
                return HttpNotFound();
            }
            else if (ward.DeletedDate != null)
            {
                // don't show deleted wards
                return HttpNotFound();
            }


            // ViewBag.UserID = User.Identity.GetUserId();
            // ViewBag.UserID = User.Identity.GetUserId();
            ViewBag.UserID = ward.UserID;
            return View(ward);
        }

        // POST: Wards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WardID,UserID,FirstName,MiddleName,LastName,Suffix,Gender,PeriodStartMonth,PeriodDuration,CreateDate")] Ward ward)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(ward.MiddleName))
                {
                    ward.MiddleName = "";
                }
                if (string.IsNullOrEmpty(ward.Suffix))
                {
                    ward.Suffix = "";
                }
                ward.LastUpdated = DateTime.Now;
                db.Entry(ward).State = EntityState.Modified;
                db.SaveChanges();

                //try
                //{
                //} catch (Exception e)
                //{
                //    Console.WriteLine(e.Message); 
                //}
                return RedirectToAction("Index");
            }
            //ViewBag.UserID = User.Identity.GetUserId();
            ViewBag.UserID = ward.UserID;
            return View(ward);
        }

        // GET: Wards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                // index when no id passed
                return RedirectToAction("Index");
            }
            Ward ward = db.Wards.Find(id);
            if (ward == null)
            {
                // index or error
                return HttpNotFound();
            }
            else if (ward.DeletedDate != null)
            {
                // don't show deleted wards
                return HttpNotFound();
            }

            ViewBag.UserID = ward.UserID;
            return View(ward);
        }

        // POST: Wards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // todo: change this to only set the DeletedDate;
            Ward ward = db.Wards.Find(id);
            db.Wards.Remove(ward);
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
