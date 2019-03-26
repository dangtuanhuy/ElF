using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Generatehttps.Models
{
    public class Job
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Job can not null")]
        [Display(Name = "Job")]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Salary can not null")]
        [Display(Name = "Salary")]
        public double Salary { get; set; }

        [Display(Name = "Description Details")]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [Display(Name = "Status")]
        public Boolean Status { get; set; }

        [Display(Name = "Img")]
        public string Image { get; set; }

        [Display(Name = "Status Enable")]
        public DateTime StatusEnable { get; set; }

        [Required(ErrorMessage = "JobDate can not null")]
        [Display(Name = "JobDate Details")]
        public DateTime JobDate { get; set; }

        [Display(Name = "Experience")]
        public string Experience { get; set; }
        public enum ExperienceJob { OneYear = 0, TwoYears = 1, ThreeYears = 2, ThanThreeYears = 3 }

        [Display(Name = "Others Experience")]
        public string Others { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public string WorkingTime { get; set; }

        public string WorkingForm { get; set; }
    }
}
