using ParseLogFile.Helpers;
using ParseLogFile.Models.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParseLogFile.Repositories
{
    public class FilesRepository
    {
        public List<DescriptionFile> GetFiles()
        {
            using (var db = ApplicationDbContext.Create())
            {
                var dataIP = db.Files;
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