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
    public class ReportDetailsController : Controller
    {
        private GuardianshipDB db = new GuardianshipDB();

        // GET: ReportDetails GuardianshipDB
        public ActionResult Index()
        {
            int ReportID = 0; // must exist and be passed into controller

            string UserID = User.Identity.GetUserId();
            ViewBag.UserID = UserID; // User.Identity.GetUserId();
            // var reportDetails = db.ReportDetails.Include(r => r.AspNetUser).Include(r => r.Report).Include(r => r.UserCategory).Include(r => r.Ward);
            var reportDetails = db.ReportDetails.Where(u => u.UserID == UserID)
                .Include(r => r.Report).Where(r => r.ReportID == ReportID) 
                .Include(uc => uc.UserCategory).Where(uc => uc.UserID  == UserID);
            return View(reportDetails.ToList());
        }

        // GET: ReportDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                // index when no id passed
                return RedirectToAction("Index");
            }
            ReportDetail reportDetail = db.ReportDetails.Find(id);
            if (reportDetail == null)
            {
                return HttpNotFound();
            }
            return View(reportDetail);
        }

        // GET: ReportDetails/Create
        public ActionResult Create()
        {
            ViewBag.UserID = User.Identity.GetUserId();
            ViewBag.ReportID = new SelectList(db.Reports, "ReportID", "UserID");
            ViewBag.CategoryID = new SelectList(db.UserCategories, "CategoryID", "UserID");
            ViewBag.WardID = new SelectList(db.Wards, "WardID", "UserID");
            return View();
        }

        // POST: ReportDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReportDetailID,ReportID,UserID,WardID,CategoryID,Worksheet,Description,Month,Value,Ordinal,LastUpdated,DeletedDate")] ReportDetail reportDetail)
        {
            if (ModelState.IsValid)
            {
                db.ReportDetails.Add(reportDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = User.Identity.GetUserId();
            ViewBag.ReportID = new SelectList(db.Reports, "ReportID", "UserID", reportDetail.ReportID);
            ViewBag.CategoryID = new SelectList(db.UserCategories, "CategoryID", "UserID", reportDetail.CategoryID);
            ViewBag.WardID = new SelectList(db.Wards, "WardID", "UserID", reportDetail.WardID);
            return View(reportDetail);
        }

        // GET: ReportDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                // index when no id passed
                return RedirectToAction("Index");
            }
            ReportDetail reportDetail = db.ReportDetails.Find(id);
            if (reportDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", reportDetail.UserID);
            ViewBag.ReportID = new SelectList(db.Reports, "ReportID", "UserID", reportDetail.ReportID);
            ViewBag.CategoryID = new SelectList(db.UserCategories, "CategoryID", "UserID", reportDetail.CategoryID);
            ViewBag.WardID = new SelectList(db.Wards, "WardID", "UserID", reportDetail.WardID);
            return View(reportDetail);
        }

        // POST: ReportDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReportDetailID,ReportID,UserID,WardID,CategoryID,Worksheet,Description,Month,Value,Ordinal,LastUpdated,DeletedDate")] ReportDetail reportDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reportDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", reportDetail.UserID);
            ViewBag.ReportID = new SelectList(db.Reports, "ReportID", "UserID", reportDetail.ReportID);
            ViewBag.CategoryID = new SelectList(db.UserCategories, "CategoryID", "UserID", reportDetail.CategoryID);
            ViewBag.WardID = new SelectList(db.Wards, "WardID", "UserID", reportDetail.WardID);
            return View(reportDetail);
        }

        // GET: ReportDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                // index when no id passed
                return RedirectToAction("Index");
            }
            ReportDetail reportDetail = db.ReportDetails.Find(id);
            if (reportDetail == null)
            {
                return HttpNotFound();
            }
            return View(reportDetail);
        }

        // POST: ReportDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReportDetail reportDetail = db.ReportDetails.Find(id);
            db.ReportDetails.Remove(reportDetail);
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
