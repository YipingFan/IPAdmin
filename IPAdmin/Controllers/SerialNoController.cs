using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using IPAdmin.Models;
using IPAdmin.Repository;
using IPAdmin.ViewModels;

namespace IPAdmin.Controllers
{
    public class SerialNoController : Controller
    {
        private PatentRepository db = new PatentRepository();
        //
        // GET: /SerialNo/id

        public ActionResult Index(int id)
        {
            var snNoCustomers = (from sc in db.SerialNoCustomers.Include("SerialNo").Include("Customer")
                where sc.SerialNoId == id
                select sc).OrderBy(sc=>sc.Order).ToList();

            return View(snNoCustomers);
        }

        public ActionResult Create(int id)
        {
                var serialNo = db.SerialNoes.Find(id);
                if (serialNo == null)
                    return HttpNotFound();

                var availableCustomers = (from c in db.UserProfiles
                    where !(from sc in db.SerialNoCustomers
                        where sc.SerialNoId == id
                        select sc.CustomerId).Contains(c.Id)
                    select c).ToList();

                AvailableCustomer ac = new AvailableCustomer {SerialNo = serialNo, Customers = availableCustomers};
                ac.SerialNoId = id;
                return View(ac);
            //else
            //{
            //    var sn = db.SerialNoes.Find(id);
            //    var customer = db.Customers.Find(customerId);
            //    if (sn == null || customer == null)
            //        return HttpNotFound();

            //    int maxOrder = (from sc in db.SerialNoCustomers
            //                    where sc.SerialNoId == id
            //                    select sc.Order).Max();

            //    SerialNoCustomer nsc = new SerialNoCustomer
            //    {
            //        Customer = customer,
            //        CustomerId = (int)customerId,
            //        Order = ++maxOrder,
            //        SerialNo = sn,
            //        SerialNoId = id,
            //        CreateDate = DateTime.Now
            //    };

            //    db.SerialNoCustomers.Add(nsc);
            //    db.SaveChanges();

            //    return RedirectToAction("Index", new { id = sn.Id });                
            //}
        }

        [HttpPost]
        public ActionResult Create(AvailableCustomer availableCustomer)
        {
            var sn = db.SerialNoes.Find(availableCustomer.SerialNoId);
            if (sn == null)
                return HttpNotFound();

            var customer = db.UserProfiles.Find(availableCustomer.SelectedCustomerId);

            int maxOrder = (from sc in db.SerialNoCustomers
                            where sc.SerialNoId == sn.Id
                            select sc.Order).Max();

            SerialNoCustomer nsc = new SerialNoCustomer
            {
                Customer = customer,
                CustomerId = customer.Id,
                Order = ++maxOrder,
                SerialNo = sn,
                SerialNoId = sn.Id,
                CreateDate = DateTime.Now
            };

            db.SerialNoCustomers.Add(nsc);
            db.SaveChanges();

            return RedirectToAction("Index", new { id = sn.Id });
        }

        public ActionResult PrintChild(int id)
        {
            var snNoCustomers = (from sc in db.SerialNoCustomers.Include("SerialNo").Include("Customer")
                where sc.SerialNoId == id
                select sc).OrderBy(sc=>sc.Order).ToList();

            var customer = snNoCustomers.LastOrDefault();

            string content = string.Empty;

            using (StreamReader reader = new StreamReader(Server.MapPath(@"\Content\CertificateTemplate.html"), Encoding.GetEncoding("GB2312")))
            {
                content = reader.ReadToEnd();
                content = content.Replace("%COMPANY_NAME%", customer.Customer.Company);
                content = content.Replace("%SERIAL_NO%", customer.SerialNo.SerialNumber.ToString());
                content = content.Replace("%MACHINE_ID%", customer.MachineId);
                content = content.Replace("%CERTIFY_NOTE%", String.Empty);
                content = content.Replace("%CERTIFY_DATE%", customer.CreateDate.ToString("yyyy MMMM dd"));
            }

            return Content(content, "text/html", Encoding.GetEncoding("GB2312"));
        }
    }
}
