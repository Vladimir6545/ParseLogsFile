using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ParseLogFile.Models
{
    public class File
    {
        [Key]
        [ForeignKey("DataLog")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Size { get; set; }
        public string NominationPage { get; set; }
        public DataLog DataLog { get; set; }
    }
}