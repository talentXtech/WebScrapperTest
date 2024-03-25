using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalentX.WebScrapper.API.Entities
{
    public class DetailedScrapOutputData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CompanyName { get; set; }

        public string AllabolagUrl { get; set; }
        public string OrgNo { get; set; }
        public string CEO { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string YearOfEstablishment { get; set; }

        public string Revenue { get; set; }

        public string EmployeeNames { get; set; }
    }
}