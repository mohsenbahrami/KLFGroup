using KLFGroup.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLFGroup.Data
{
    public class KLFGroupContext : DbContext
    {
        public KLFGroupContext(DbContextOptions<KLFGroupContext> options) :
            base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }
    }
}
