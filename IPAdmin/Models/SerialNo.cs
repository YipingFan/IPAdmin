﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IPAdmin.Models
{
    public class SerialNo
    {
        [Key]
        public int Id { get; set; }
        public Guid SerialNumber { get; set; }
        public Patent Patent { get; set; }
        public DateTime CreateDate { get; set; }

        public ICollection<SerialNoCustomer> SerialNoCustomers { get; set; }
    }
}