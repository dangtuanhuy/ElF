using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkAptech.Data;
using WorkAptech.Models;
using WorkAptech.Models.ViewModels;

namespace WorkAptech.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public HomeLocationVM HomeLocationVM { get; set; }
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
            HomeLocationVM = new HomeLocationVM()
            {
                Category = _db.Category.ToList(),
                Location = _db.Location.ToList(),
                Job = _db.Job.Include(m => m.Category)
                    .Include(m => m.ApplicationUser)
                    .Include(m => m.ApplicationUser.Company)
                    .Include(m => m.ApplicationUser.Company.Country),
                ApplicationUser = _db.ApplicationUser
                    .Include(m => m.Company)
                    .Include(m => m.Company.Country)
                    .Include(m => m.Company.Location)
                .ToList()
            };
        }
        public IActionResult Index(string sortOrder, string searchString, int idLocation, int idCategory)
        {
            ViewData["CurrentFilter"] = searchString;

            HomeLocationVM.Job = _db.Job.Include(m => m.Category)
                    .Include(m => m.ApplicationUser)
                    .Include(m => m.ApplicationUser.Company)
                    .Include(m => m.ApplicationUser.Company.Country);

            if (idCategory != 0)
            {
                HomeLocationVM.Job = HomeLocationVM.Job.Where(s => s.CategoryId == idCategory);
            }
            if(idLocation != 0)
            {
                HomeLocationVM.Job = HomeLocationVM.Job.Where(m => m.ApplicationUser.Company.Location.Id == idLocation);
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                HomeLocationVM.Job = HomeLocationVM.Job.Where(m => m.Name.Contains(searchString));
            }

            ViewData["LocationId"] = new SelectList(_db.Location, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_db.Category, "Id", "Name");
            return View(HomeLocationVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Test()
        {
            return View();
        }
        public async Task<IActionResult> DetailsJob(int? id)
        {
            var query = (from a in _db.Skill
                         join b in _db.SkillJob on a.Id equals b.SkillId
                         where b.JobId == id
                         select a.Name).ToList();
            ViewBag.query = query;
            var queryjb = await _db.Job.Include(m => m.Category)
                .Include(m => m.ApplicationUser)
                .Include(m => m.ApplicationUser.Company)
                .Include(m => m.ApplicationUser.Company.Country).ToListAsync();
            //var getlist = ViewBag.query as IE
            ViewBag.queryjob = queryjb;
            var js = await _db.Job.Include(m => m.Category)
                .Include(m => m.ApplicationUser)
                .Include(m => m.ApplicationUser.Company)
                .Include(m => m.ApplicationUser.Company.Country)
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();
            return View(js);
        }
        public IActionResult JobHome()
        {
            return View();
        }
    }
}
