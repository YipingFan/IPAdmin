using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IPAdmin.Models;

namespace IPAdmin.ViewModels
{
    public class GenerateSerialNo
    {
        public Patent Patent { get; set; }
        public string Prefix { get; set; }
        public int Count { get; set; }
    }
}