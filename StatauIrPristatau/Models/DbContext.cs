using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace StatauIrPristatau.Models
{
    public class SIPDbContext : DbContext
    {
        public DbSet<User> userAccount { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Parcel> parcels { get; set; }
        public DbSet<Ranking> ranking { get; set; }
    }
}
