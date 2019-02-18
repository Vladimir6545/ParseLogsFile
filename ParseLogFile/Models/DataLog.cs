using System;

using System.Linq;
using System.Web;

namespace ParseLogFile.Models
{
    public class DataLog
    {
        public int Id { get; set; }
        public string DateRequest { get; set; }
        public string TimeRequest { get; set; }
        public string TypeRequest { get; set; }
        public int? RezultRequest { get; set; }
        public Company Company { get; set; }
        public File File { get; set; }
    }
}