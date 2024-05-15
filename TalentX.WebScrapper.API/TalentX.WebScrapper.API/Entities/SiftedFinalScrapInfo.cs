using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalentX.WebScrapper.API.Entities
{
    public class SiftedFinalScrapInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Sector { get; set; }
        public string? Sectorurl { get; set; }
        public string? ContentType { get; set; }
        public string? Date { get; set; }
        public string? Subject { get; set; }
        public string? Summary { get; set; }
        public string? articleUrl { get; set; }
        public string? Tags { get; set; }
    }
}