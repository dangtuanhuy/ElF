using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Generatehttps.Models
{
    public class JobSkill
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }


        [Display(Name = "Skill")]
        public int SkillId { get; set; }

        [ForeignKey("SkillId")]
        public virtual Skill Skill { get; set; }

        [Display(Name = "Job")]
        public int JobId { get; set; }

        [ForeignKey("JobId")]
        public virtual Job Job { get; set; }
    }
}
