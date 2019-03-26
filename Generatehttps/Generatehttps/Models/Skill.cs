using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Generatehttps.Models
{
    public class Skill
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Skill can not null")]
        [Display(Name = "Skill")]
        [MaxLength(90)]
        public string Name { get; set; }
    }
}
