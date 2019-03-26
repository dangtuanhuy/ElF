using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Generatehttps.Models
{
    public class Company
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        [Required]
        public string Contact { get; set; }

       
        [Display(Name = "Number Of Employee")]
        public int NumberOfEmployeeId { get; set; }

        [ForeignKey("NumberOfEmployeeId")]
        public virtual NumOfEmployee NumOfEmployee { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

    }
}
