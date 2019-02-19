using ParseLogFile.Helpers;
using ParseLogFile.Models;
using ParseLogFile.Models.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParseLogFile.Repositories
{
    public class IpAddressRepository
    {
        public List<ListIP> GetIP(string sort)
        {
            using (var db = ApplicationDbContext.Create())
            {
                IQueryable<Company> dataLogs = db.Companies;

                switch (sort)
                {
                    case "IP":
                        dataLogs = dataLogs.OrderBy(l => l.IP);
                        break;
                    case "IPDesc":
                        dataLogs = dataLogs.OrderByDescending(l => l.IP);
                        break;
                    case "Company":
                        dataLogs = dataLogs.OrderBy(l => l.Name);
                        break;
                    case "CompanyDesc":
                        dataLogs = dataLogs.OrderByDescending(l => l.Name);
                        break;
                    case "Network":
                        dataLogs = dataLogs.OrderBy(l => l.NominationNetwork);
                        break;
                    case "NetworkDesc":
                        dataLogs = dataLogs.OrderByDescending(l => l.NominationNetwork);
                        break;
                    default:
                        dataLogs = dataLogs.OrderBy(l => l.Id);
                        break;
                }

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