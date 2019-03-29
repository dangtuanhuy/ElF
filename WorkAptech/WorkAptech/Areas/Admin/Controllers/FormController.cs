using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkAptech.Data;
using WorkAptech.Models.ViewModels;

namespace WorkAptech.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FormController : Controller
    {
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public ClassVM ClassVM { get; set; }
        public FormController(ApplicationDbContext context)
        {
            _context = context;
            ClassVM ClassVM = new ClassVM()
            {
                Country = _context.Country,
                Location = _context.Location,
                Company = new Data.Company(),
                ApplicationUser = new Data.ApplicationUser(),
            };
        }
        public IActionResult AddCompany()
        {
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name");
            ViewData["LocationId"] = new SelectList(_context.Location, "Id", "Name");
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}