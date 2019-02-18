using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ParseLogFile.Helpers;
using ParseLogFile.Models;
using ParseLogFile.Models.ViewsModels;
using Whois.NET;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ParseLogFile.Controllers
{
    public class DataLogsController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        private List<DataLog> _dataLogs;
        private List<LogsViewModel> _dataView;
        //DataLog data;
        // GET: DataLogs
        public ActionResult Index()
        {
            var dataLogs = _db.DataLogs.Include(d => d.Company).Include(d => d.File);
            _dataLogs = dataLogs.ToList();
            _dataView = new List<LogsViewModel>();
            foreach (var item in _dataLogs)
            {
                var temp1 = new LogsViewModel();
                temp1.Date = item.DateRequest;
                temp1.Time = item.TimeRequest;
                temp1.TypeRequest = item.TypeRequest;
                temp1.descriptionFile.NominationFile = item.File.Name;
                temp1.descriptionFile.PathToFile = item.File.Path;
                temp1.ip.IP = item.Company.IP;
                temp1.ip.NominationNetwork = item.Company.NominationNetwork;
                temp1.TransmittedBytes = item.File.Size;
                temp1.RezultRequest = item.RezultRequest.ToString();
                _dataView.Add(temp1);
            }
            if (_dataView != null)
            {
                return View(_dataView);
            }
            return View();
        }

        public ActionResult IpAddress()
        {
            var dataLogs = _db.Companies;
            var temp = dataLogs.ToList();
            var dataView = new List<ListIP>();
            foreach (var item in temp)
            {
                var temp1 = new ListIP();
                temp1.IP = item.IP;
                temp1.CompanyName = item.Name;
                temp1.NominationNetwork = item.NominationNetwork;
                dataView.Add(temp1);
            }
            return View(dataView); ;
        }

        public ActionResult Files()
        {
            var dataLogs = _db.Files;
            var temp = dataLogs.ToList();
            var dataView = new List<DescriptionFile>();
            foreach (var item in temp)
            {
                var temp1 = new DescriptionFile();
                temp1.Name = item.Name;
                temp1.PathToFile = item.Path;
                temp1.NominationPage = item.NominationPage;
                dataView.Add(temp1);
            }
            return View(dataView); ;
        }

        public string GetWhoisCompanyName(string ip)
        {
            var result = WhoisClient.Query(ip);
            if (result != null)
            {
                var organizationName = (result.OrganizationName);
                return organizationName;
            }
            return null;
        }

        public string GetWhoisNominationNetwork(string ip)
        {
            var result = WhoisClient.Query(ip);
            if (result != null)
            {
                var nominationNetwork = (result.AddressRange.Begin.AddressFamily);
                return nominationNetwork.ToString();
            }
            return null;
        }

        [HttpPost]
        public JsonResult Upload()
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                int fileSize = file.ContentLength;
                string fileName = file.FileName;
                string mimeType = file.ContentType;
                System.IO.Stream fileContent = file.InputStream;
                file.SaveAs(Server.MapPath("~/Files/") + fileName);
                SaveFileToDatabase();
            }
            return Json("Uploaded " + Request.Files.Count + " files");
        }

        public void SaveFileToDatabase()
        {

            string path = Server.MapPath("~/Files/");
            var filename = new DirectoryInfo(path).GetFiles();
            string[] readText = System.IO.File.ReadAllLines(path + filename[0].Name);

            for (int i = 0; i < readText.Length; i++)
            {
                string[] words = readText[i].Split(' ');
                words[3] = Regex.Replace(words[3], @"\[", " ");
                string date = words[3].Remove(words[3].IndexOf(":"));
                string time = words[3].Substring(words[3].IndexOf(":") + 1);
                int indexPath = words[6].LastIndexOf('/');

                DataLog data = new DataLog
                {
                    DateRequest = date,
                    TimeRequest = time,
                    TypeRequest = words[5].Replace('"', ' '),
                    RezultRequest = int.Parse(words[8]),
                    Company = new Company
                    {
                        IP = words[0],
                        Name = GetWhoisCompanyName(words[0]),
                        NominationNetwork = GetWhoisNominationNetwork(words[0])
                    },
                    File = new Models.File
                    {
                        Name = filename[0].Name,
                        Path = words[6].Substring(0, indexPath + 1),
                        NominationPage = words[6].Split('/').Last(),
                        Size = words[9] + " b"
                    }

                };
                _db.DataLogs.Add(data);
            }

            _db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
