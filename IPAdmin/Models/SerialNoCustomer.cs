using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Cache;

namespace IPAdmin.Models
{
    public class SerialNoCustomer
    {
        [Key, Column(Order = 0)]
        public int SerialNoId { get; set; }
        [Key, Column(Order = 1)]
        public int CustomerId { get; set; }

        public SerialNo SerialNo { get; set; }
        public UserProfile Customer { get; set; }

        public string ContractNo { get; set; }
        public string InvoiceNo { get; set; }
        public decimal Price { get; set; }
        public string MachineId { get; set; }

        public int Order { get; set; }
        public DateTime CreateDate { get; set; }
    }
}