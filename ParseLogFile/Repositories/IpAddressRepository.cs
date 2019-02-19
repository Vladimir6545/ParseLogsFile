using ParseLogFile.Helpers;
using ParseLogFile.Models.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParseLogFile.Repositories
{
    public class IpAddressRepository
    {
        public List<ListIP> GetIP()
        {
            using (var db = ApplicationDbContext.Create())
            {
                var dataLogs = db.Companies;
                var temp = dataLogs.ToList();
                var ip = new List<ListIP>();
                if (ip != null)
                {
                    foreach (var item in temp)
                    {
                        var temp1 = new ListIP();
                        temp1.IP = item.IP;
                        temp1.CompanyName = item.Name;
                        temp1.NominationNetwork = item.NominationNetwork;
                        ip.Add(temp1);
                    }
                    return ip;
                }
                return null;
            }
        }
    }
}