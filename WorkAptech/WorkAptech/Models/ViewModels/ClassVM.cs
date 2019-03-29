using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkAptech.Data;

namespace WorkAptech.Models.ViewModels
{
    public class ClassVM
    {
        public Company Company { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<Country> Country { get; set; }
        public IEnumerable<Location> Location { get; set; }
    }
}
