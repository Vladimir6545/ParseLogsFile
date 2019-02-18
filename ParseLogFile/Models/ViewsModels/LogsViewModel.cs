using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParseLogFile.Models.ViewsModels
{
    public class LogsViewModel
    {
        public ListIP ip;
        public DescriptionFile descriptionFile;
        public LogsViewModel()
        {
            ip = new ListIP();
            descriptionFile = new DescriptionFile();
        }
        public string Date { get; set; }
        public string Time { get; set; }
        public string TypeRequest { get; set; }
        public string TransmittedBytes { get; set; }
        public string RezultRequest { get; set; }

    }

    public class ListIP
    {
        public string IP { get; set; }
        public string CompanyName { get; set; }
        public string NominationNetwork { get; set; }
    }

    public class DescriptionFile
    {
        public string Name { get; set; }
        public string NominationFile { get; set; }
        public string PathToFile { get; set; }
        public string NominationPage { get; set; }
        public string Size { get; set; }
    }
}