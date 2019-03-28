using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorkAptech.Data
{
    public class ApplyDetails
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }


        [Display(Name = "User")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Job")]
        public int JobId { get; set; }

        [ForeignKey("JobId")]
        public virtual Job Job { get; set; }

        public DateTime AppliedDate { get; set; }

        public Boolean Status { get; set; }
        public string CV { get; set; }
    }
}
