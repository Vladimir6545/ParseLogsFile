using ParseLogFile.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ParseLogFile.Helpers
{
    public class ApplicationDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            //DataLog data = new DataLog
            //{
            //    Id = 1,
            //    DateRequest = "19/6/2012",
            //    TimeRequest = "10:51:52",
            //    TypeRequest = "GET",
            //    RezultRequest = 200,
            //    Company = new Company
            //    {
            //        Id = 1,
            //        Name = "name company",
            //        IP = "19.168.1.0.1"
            //    },
            //    File = new File
            //    {
            //        Id = 1,
            //        Name = "Name file",
            //        Path = "/uk/74-support_uk/administrator-guide-uk/624-telecom-node-2.html",
            //        Size = "4.0 mb"
            //    }
            //};
            //context.DataLogs.Add(data);
        }
    }
}