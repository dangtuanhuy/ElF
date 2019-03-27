using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorkAptech.Data
{
    public class Notification
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Display(Name = "From User Id")]
        public string FromUserId { get; set; }

        [ForeignKey("FromUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "To User Id")]
        public string ToUserId { get; set; }

        public Boolean Status { get; set; }
        public string Type { get; set; }

        [Display(Name = "Apply Details")]
        public int AppliDetailId { get; set; }

        [ForeignKey("AppliDetailId")]
        public virtual ApplyDetails ApplyDetails { get; set; }
    }
}
