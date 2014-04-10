using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using IPAdmin.Models;

namespace IPAdmin.Repository
{
    public class PatentRepository : DbContext
    {
            public PatentRepository()
                : base("DefaultConnection")
            {
            }

        public DbSet<Patent> Patents { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<SerialNo> SerialNoes { get; set; }
        public DbSet<SerialNoCustomer> SerialNoCustomers { get; set; }
        public DbSet<PatentCountry> PatentCountries { get; set; }
    }
}