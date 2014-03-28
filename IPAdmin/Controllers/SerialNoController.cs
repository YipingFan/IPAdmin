using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IPAdmin.Models;
using IPAdmin.Repository;

namespace IPAdmin.Controllers
{
    public class SerialNoController : Controller
    {
        private PatentRepository db = new PatentRepository();
        //
        // GET: /SerialNo/id

        public ActionResult Index(int id)
        {
            var snNoCustomers = (from sc in db.SerialNoCustomers
                where sc.SerialNoId == id
                select sc).OrderBy(sc=>sc.Order).ToList();

            return View(snNoCustomers);
        }
    }
}
