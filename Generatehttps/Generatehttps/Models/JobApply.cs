using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Generatehttps.Models
{
    public class JobApply
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }


        [Display(Name = "JobId")]
        public int JobId { get; set; }

        [ForeignKey("JobId")]
        public virtual Job Job { get; set; }


        [Display(Name = "Apply DetailsIs")]
        public int ApplyDetailsId { get; set; }

        [ForeignKey("ApplyDetailsId")]
        public virtual AppliedDetails AppliedDetails { get; set; }


        public bool Status { get; set; }

        [Display(Name = "Training")]
        public int TrainingId { get; set; }

        [ForeignKey("TrainingId")]
        public virtual Training Training { get; set; }
    }
}
