using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorkAptech.Data
{
    public class ApplicationUser:IdentityUser
    {
      
        [Display(Name = "Full Name")]
        [MaxLength(90)]
        public string Name { get; set; }
        [Display(Name = "Picture")]
        public byte[] Avarta { get; set; }
        [Display(Name = "Company Id")]
        public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        public string UserSkill { get; set; }

        [Display(Name = "Experience")]
        public string Experience { get; set; }
        public enum ExperienceJob { OneYear = 0, TwoYears = 1, ThreeYears = 2, ThanThreeYears = 3 }

        public Boolean BlockStatus { get; set; }

        public int NotificationId { get; set; }
    }
}
