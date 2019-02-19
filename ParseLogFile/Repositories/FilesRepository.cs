using ParseLogFile.Helpers;
using ParseLogFile.Models;
using ParseLogFile.Models.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParseLogFile.Repositories
{
    public class FilesRepository
    {
        public List<DescriptionFile> GetFiles(string sort)
        {
            using (var db = ApplicationDbContext.Create())
            {
                IQueryable<File> dataIP = db.Files;

                switch (sort)
                {
                    case "Name":
                        dataIP = dataIP.OrderBy(l => l.Name);
                        break;
                    case "NameDesc":
                        dataIP = dataIP.OrderByDescending(l => l.Name);
                        break;
                    case "Page":
                        dataIP = dataIP.OrderBy(l => l.NominationPage);
                        break;
                    case "PageDesc":
                        dataIP = dataIP.OrderByDescending(l => l.NominationPage);
                        break;
                    default:
                        dataIP = dataIP.OrderBy(l => l.Id);
                        break;
                }

                var listIP = dataIP.ToList();
                if (listIP != null)
                {
                    var files = new List<DescriptionFile>();
                    foreach (var item in listIP)
                    {
                        var file = new DescriptionFile();
                        file.Name = item.Name;
                        file.PathToFile = item.Path;
                        file.NominationPage = item.NominationPage;
                        files.Add(file);
                    }
                    return files;
                }
                return null;
            }
        }
    }
}