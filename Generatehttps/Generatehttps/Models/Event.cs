using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Generatehttps.Models
{
    public class Event
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "TiTle can not null")]
        [Display(Name = "TiTle")]
        [MaxLength(90)]
        public string TiTle { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Hour { get; set; }

        public string Address { get; set; }

        public string Information { get; set; }
    }
}
