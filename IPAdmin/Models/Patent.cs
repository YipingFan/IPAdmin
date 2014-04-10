using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IPAdmin.Models
{
    public class Patent
    {
        public int Id { get; set; }
        [MaxLength(256)]
        public string IntSearchCode { get; set; }
        [MaxLength(256)]
        public string Name { get; set; }
        [MaxLength(256)]
        public string Owner { get; set; }
        [MaxLength(256)]
        public string Agency { get; set; }

        public DateTime CreateDate { get; set; }

        public string CreateBy { get; set; }

        public ICollection<SerialNo> SerialNoes { get; set; }
    }
}