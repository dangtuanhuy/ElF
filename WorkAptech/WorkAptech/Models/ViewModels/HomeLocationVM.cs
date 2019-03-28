using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkAptech.Data;

namespace WorkAptech.Models.ViewModels
{
    public class HomeLocationVM
    {
        public IEnumerable<ApplicationUser> ApplicationUser { get; set; }
        public IEnumerable<Location> Location { get; set; }
        public IEnumerable<Category> Category { get; set; }
        public IEnumerable<Job> Job { get; set; }
        public IEnumerable<SkillJob> SkillJob { get; set; }
        public IEnumerable<Training> Training { get; set; }
    }
}
