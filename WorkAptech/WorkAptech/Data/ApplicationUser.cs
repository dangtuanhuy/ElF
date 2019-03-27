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
    }
}
