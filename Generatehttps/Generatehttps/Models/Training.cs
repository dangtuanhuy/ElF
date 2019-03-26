using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Generatehttps.Models
{
    public class Training
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title can not null")]
        public string Title { get; set; }

        [Display(Name = "Training Date")]
        [Required]
        public DateTime TrainingDate { get; set; }

        [Required]
        public TimeSpan Hours { get; set; }

        [Required(ErrorMessage = "Address can not null")]
        public string Address { get; set; }

        public Boolean Type { get; set; }
    }
}
