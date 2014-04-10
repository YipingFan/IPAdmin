using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using IPAdmin.Common;
using IPAdmin.Models;
using IPAdmin.Repository;
using IPAdmin.ViewModels;

namespace IPAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
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
            var patent = (from p in db.Patents.Include("SerialNoes.SerialNoCustomers")
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
                patent.CreateBy = Helper.GetCurrentUserName();
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
        public ActionResult GenerateSerialNumber(int id)
        {
            Patent patent = db.Patents.Find(id);
            if (patent == null)
                return HttpNotFound();

            return View(new GenerateSerialNo { Patent = patent });
        }


        [HttpPost]
        [ActionName("Generate")]
        public ActionResult GenerateSerialNumber(GenerateSerialNo generateSerialNo)
        {
            //Patent patent = db.Patents.Find(generateSerialNo.Patent.Id);

            var patent = (from p in db.Patents.Include(x => x.SerialNoes)
                where p.Id == generateSerialNo.Patent.Id
                select p).FirstOrDefault();
            if (patent == null)
                return HttpNotFound();

            if (patent.SerialNoes == null)
                patent.SerialNoes = new List<SerialNo>();

            var customer = db.UserProfiles.Find(2);  // find admin user

            var sns = (from s in patent.SerialNoes
                where s.SerialNumber.StartsWith(generateSerialNo.Prefix) && s.CreateDate > DateTime.Today
                orderby s.Id
                select s).LastOrDefault();

            int startNumber;
            if (sns == null)
            {
                startNumber = int.Parse(DateTime.Now.ToString("yyMMdd") + "001");
            }
            else
            {
                int maxNumber = int.Parse(sns.SerialNumber.Split('-').Last());
                startNumber = maxNumber + 1;
            }

            for (int i = 0; i < generateSerialNo.Count; i++)
            {
                var sn = new SerialNo
                {
                    Patent = patent,
                    SerialNumber = generateSerialNo.Prefix + "-" + (startNumber+i).ToString(),
                    CreateDate = DateTime.Now
                };
                SerialNoCustomer snc = new SerialNoCustomer
                {
                    Customer = customer,
                    CustomerId = customer.Id,
                    SerialNo = sn,
                    SerialNoId = sn.Id,
                    CreateDate = DateTime.Now
                };

                sn.SerialNoCustomers = new Collection<SerialNoCustomer>();
                sn.SerialNoCustomers.Add(snc);
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