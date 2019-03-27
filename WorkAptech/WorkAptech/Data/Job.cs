using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorkAptech.Data
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
        //[Range(250, int.MaxValue, ErrorMessage = "Salary should be greater than ${150}")]
        public float Salary { get; set; }

        [Display(Name = "Description Details")]
        public string Description { get; set; }

        [Display(Name = "Img")]
        public string Image { get; set; }

        [Display(Name = "Job Role")]
        public string JobRole { get; set; }

        [Required(ErrorMessage = "JobDate can not null")]
        [Display(Name = "JobDate Details")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime JobDate { get; set; }

        [Display(Name = "Experience")]
        public string Experience { get; set; }
        public enum ExperienceJob { OneYear = 0, TwoYears = 1, ThreeYears = 2, ThanThreeYears = 3 }

        public string WorkingTime { get; set; }

        public string WorkingForm { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [Display(Name = "User")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
