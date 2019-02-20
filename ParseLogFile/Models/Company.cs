using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParseLogFile.Models
{
    public class Company
    {
        [Key]
        [ForeignKey("DataLog")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public string NominationNetwork { get; set; }
        public DataLog DataLog { get; set; }

    }
}