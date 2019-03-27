using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorkAptech.Data
{
    public class Training
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title can not null")]
        public string Title { get; set; }
        public Boolean Type { get; set; }

        [Display(Name = "Training Date")]
        [Required]
        public string TrainingDate { get; set; }

        [Required]
        public TimeSpan Hours { get; set; }

        [Required(ErrorMessage = "Description can not null")]
        public string Description { get; set; }

        public Boolean Status { get; set; }

        [Display(Name = "IMG")]
        public string Image { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        [Display(Name = "Company Id")]
        public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
    }
}
