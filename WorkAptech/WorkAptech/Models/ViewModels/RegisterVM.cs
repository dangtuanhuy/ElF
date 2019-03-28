using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkAptech.Data;

namespace WorkAptech.Models.ViewModels
{
    public class RegisterVM
    {
       public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<Company> Company { get; set; }
        public IEnumerable<Country> Country { get; set; }
        public IEnumerable<Location> Location { get; set; }
        public IEnumerable<Category> Category { get; set; }
        public IEnumerable<Skill> Skill { get; set; }
    }
}
