using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkAptech.Data;

namespace WorkAptech.Models.ViewModels
{
    public class SKillViewModels
    {
        public Skill Skill { get; set; }
        public IEnumerable<Category> Category { get; set; }
    }
}
