using ParseLogFile.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ParseLogFile.Helpers
{
    public class ApplicationDbContext : DbContext
    {
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public ApplicationDbContext() : base("dbConnect") { }
        public DbSet<DataLog> DataLogs { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<File> Files { get; set; }

    }
}