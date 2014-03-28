using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using IPAdmin.Models;
using IPAdmin.Repository;

namespace IPAdmin.Controllers
{
    public class PatentController : Controller
    {
        private PatentRepository db = new PatentRepository();

        //
        // GET: /Patent/

        public ActionResult Index()
        {
            return View(db.Patents.ToList());
        }

        //
        // GET: /Patent/Details/5

        public ActionResult Details(int id = 0)
        {
            //Patent patent = db.Patents.Find(id);

            var patent = (from p in db.Patents.Include("SerialNoes")
                where p.Id == id
                select p).FirstOrDefault();

            if (patent == null)
            {
                return HttpNotFound();
            }
            return View(patent);
        }

        //
        // GET: /Patent/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Patent/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Patent patent)
        {
            if (ModelState.IsValid)
            {
                patent.CreateDate = DateTime.Now;
                db.Patents.Add(patent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patent);
        }

        //
        // GET: /Patent/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Patent patent = db.Patents.Find(id);
            if (patent == null)
            {
                return HttpNotFound();
            }
            return View(patent);
        }

        //
        // POST: /Patent/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Patent patent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patent);
        }

        //
        // GET: /Patent/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Patent patent = db.Patents.Find(id);
            if (patent == null)
            {
                return HttpNotFound();
            }
            return View(patent);
        }

        //
        // POST: /Patent/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patent patent = db.Patents.Find(id);
            db.Patents.Remove(patent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [ActionName("Generate")]
        public ActionResult GenerateSerialNumber(int id, int number = 0)
        {
            Patent patent = db.Patents.Find(id);

            if (number <= 0)
            {
                return View(patent);
            }

            if (patent.SerialNoes == null)
                patent.SerialNoes = new List<SerialNo>();

                for (int i = 0; i<number; i++)
                {
                    var sn = new SerialNo {Patent = patent, SerialNumber = Guid.NewGuid(), CreateDate = DateTime.Now};
                    patent.SerialNoes.Add(sn);
                }

                db.Entry(patent).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Details", patent);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}