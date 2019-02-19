using HtmlAgilityPack;
using ParseLogFile.Helpers;
using ParseLogFile.Models;
using ParseLogFile.Models.ViewsModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using Whois.NET;

namespace ParseLogFile.Repositories
{
    public class DataLogRepository
    {
        private const string SITE = "http://tariscope.com";
        private List<DataLog> _dataLogs;
        private List<LogsViewModel> _data;
        private string GetTextFromTitleTag(string path)
        {
            var temp = GetPageTitle(path);
            string text = temp.Result;
            return text;
        }
        private async Task<string> GetPageTitle(string path)
        {
            try
            {
                HttpClient http = new HttpClient();
                HttpResponseMessage result = http.GetAsync(SITE + path).Result;
                if (result.IsSuccessStatusCode)
                {
                    Stream stream = await result.Content.ReadAsStreamAsync();
                    HtmlDocument doc = new HtmlDocument();
                    doc.Load(stream);
                    var links = doc.DocumentNode.SelectNodes("//title");

                    foreach (HtmlNode node in links)
                    {
                        var text = node.InnerHtml.ToString();
                        return text;
                    }
                }
                return "do not have tag title";
            }
            catch (Exception ex)
            {
                return "Search tag title " + ex.Message;
            }
        }
        private string GetWhoisCompanyName(string ip)
        {
            var result = WhoisClient.Query(ip);
            if (result != null)
            {
                var organizationName = (result.OrganizationName);
                return organizationName;
            }
            return null;
        }
        private string GetWhoisNominationNetwork(string ip)
        {
            var result = WhoisClient.Query(ip);
            if (result != null)
            {
                var nominationNetwork = (result.AddressRange.Begin.AddressFamily);
                return nominationNetwork.ToString();
            }
            return null;
        }
        public void SaveToDatabase()
        {
            using (var db = ApplicationDbContext.Create())
            {
                string path = HttpContext.Current.Server.MapPath("~/Files/");
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
                            Path = words[6],//.Substring(0, indexPath + 1),
                            NominationPage = GetTextFromTitleTag(words[6]),
                            Size = words[9] + " b"
                        }

                    };
                    db.DataLogs.Add(data);
                }

                db.SaveChanges();
            }
        }
        public List<LogsViewModel> GetDataLogs()
        {
            using (var db = ApplicationDbContext.Create())
            {
                IQueryable<DataLog> logs = db.DataLogs.Include("Company").Include("File");
                _dataLogs = logs.ToList();
                _data = new List<LogsViewModel>();
                foreach (var item in _dataLogs)
                {
                    var log = new LogsViewModel();
                    log.Date = item.DateRequest;
                    log.Time = item.TimeRequest;
                    log.TypeRequest = item.TypeRequest;
                    log.descriptionFile.NominationFile = item.File.Name;
                    log.descriptionFile.PathToFile = item.File.Path;
                    log.ip.IP = item.Company.IP;
                    log.ip.NominationNetwork = item.Company.NominationNetwork;
                    log.TransmittedBytes = item.File.Size;
                    log.RezultRequest = item.RezultRequest.ToString();
                    _data.Add(log);
                }
                if (_data != null)
                {
                    return _data;
                }
                return null;
            }
        }
    }
}