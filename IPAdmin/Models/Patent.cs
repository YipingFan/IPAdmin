using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPAdmin.Models
{
    public class Patent
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public ICollection<SerialNo> SerialNoes { get; set; }
        public DateTime CreateDate { get; set; }
    }
}