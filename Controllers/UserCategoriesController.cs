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
    public class UserCategoriesController : Controller
    {
        private GuardianshipDB db = new GuardianshipDB();

        // GET: UserCategories
        public ActionResult Index()
        {
            string UserID = User.Identity.GetUserId();
            ViewBag.UserID = UserID; // User.Identity.GetUserId();

            // var userCategories = db.UserCategories.Include(u => u.AspNetUser).Where(u => u.UserID == UserID);
            var userCategories = db.UserCategories.Where(u => u.UserID == UserID);
            return View(userCategories.ToList()
                .OrderBy(u => u.StateCode)
                .ThenBy(u => u.CategoryType)
                .ThenBy(u => u.Ordinal));
            
        }

        // GET: UserCategories/Details/5
        public ActionResult Details(int? id)
        {
            // ViewBag.UserID = User.Identity.GetUserId();
            if (id == null)
            {
                // index when no id passed
                return RedirectToAction("Index");
            }
            UserCategory userCategory = db.UserCategories.Find(id);
            if (userCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = userCategory.UserID;  
            return View(userCategory);
        }

        // GET: UserCategories/Create
        public ActionResult Create()
        {
            ViewBag.UserID = User.Identity.GetUserId();
            UserCategory uc = new UserCategory { UserID = ViewBag.UserID };
            return View(uc);
        }

        // POST: UserCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,UserID,StateCode,CategoryType,CategoryClass,CategoryName,Ordinal")] UserCategory userCategory)
        {
            if (ModelState.IsValid)
            {
                // userCategory.Hide = false;
                db.UserCategories.Add(userCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = User.Identity.GetUserId();
            return View(userCategory);
        }

        // GET: UserCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                // index when no id passed
                return RedirectToAction("Index");
            }
            UserCategory userCategory = db.UserCategories.Find(id);
            if (userCategory == null)
            {
                return HttpNotFound();
            }
            // ViewBag.UserID = User.Identity.GetUserId();
            ViewBag.UserID = userCategory.UserID;
            return View(userCategory);
        }

        // POST: UserCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,UserID,StateCode,CategoryType,CategoryClass,CategoryName,Ordinal")] UserCategory userCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // ViewBag.UserID = User.Identity.GetUserId();
            ViewBag.UserID = userCategory.UserID;
            return View(userCategory);
        }

        // GET: UserCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                // index when no id passed
                return RedirectToAction("Index");
            }
            UserCategory userCategory = db.UserCategories.Find(id);
            if (userCategory == null)
            {
                return HttpNotFound();
            }
            return View(userCategory);
        }

        // POST: UserCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserCategory userCategory = db.UserCategories.Find(id);
            db.UserCategories.Remove(userCategory);
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
