using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IPAdmin.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Name")]
        public string ContractName { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }

        public ICollection<SerialNoCustomer> SerialNoCustomers { get; set; }
    }
}