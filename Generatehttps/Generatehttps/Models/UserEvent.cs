using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Generatehttps.Models
{
    public class UserEvent
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Display(Name = "Event")]
        public int EventId { get; set; }

        [ForeignKey("EventId")]
        public virtual Event Event { get; set; }

        [Display(Name = "User")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
