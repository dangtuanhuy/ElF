using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkAptech.Data;

namespace WorkAptech.Models.ViewModels
{
    public class CompanyCountryViewModel
    {
        public Company Company { get; set; }
        public IEnumerable<Country> Country { get; set; }
    }
}
