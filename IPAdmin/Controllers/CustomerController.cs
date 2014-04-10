using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IPAdmin.Common;
using IPAdmin.Models;
using IPAdmin.Repository;

namespace IPAdmin.Controllers
{
    public class CustomerController : Controller
    {
        private PatentRepository db = new PatentRepository();

        //
        // GET: /Customer/

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.UserProfiles);
        }

        //
        // GET: /Customer/Details/5

        public ActionResult Details(int id = 0)
        {
            var customer = db.UserProfiles.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //
        // GET: /Customer/Edit/5

        public ActionResult Edit(int id = 0)
        {
            var customer = db.UserProfiles.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //
        // POST: /Customer/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserProfile customer)
        {
            if (ModelState.IsValid)
            {
                customer.UpdateDate = DateTime.Now;
                customer.UpdateBy = Helper.GetCurrentUserName();
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        //
        // GET: /Customer/Delete/5

        public ActionResult Delete(int id = 0)
        {
            var customer = db.UserProfiles.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //
        // POST: /Customer/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var customer = db.UserProfiles.Find(id);
            db.UserProfiles.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}