using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IPAdmin.Models;

namespace IPAdmin.ViewModels
{
    public class AvailableCustomer
    {
        public SerialNo SerialNo { get; set; }
        public ICollection<UserProfile> Customers { get; set; }
        public int SelectedCustomerId { get; set; }
        public int SerialNoId { get; set; }
    }
}