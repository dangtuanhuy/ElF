using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkAptech.Data;
using WorkAptech.Models.ViewModels;

namespace WorkAptech.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SIgnInController : Controller
    {
        [BindProperty]
        public RegisterVM RegisterVM { get; set; }
        public SIgnInController(ApplicationDbContext db)
        {
            _db = db;
            RegisterVM RegisterVM = new RegisterVM()
            {
                Company = _db.Company,
                Country = _db.Country.ToList(),
                Location = _db.Location,
                Skill = _db.Skill,
                ApplicationUser = new Data.ApplicationUser()
            };
        }
        [ActionName("Category")]
        public async Task<IActionResult> Category()
        {
            List<Category> Categories = new List<Category>();
            Categories = await (from Category in _db.Category select Category).ToListAsync();
            //ViewBag.listtest = new SelectList(Categories, "Id", "Name");
            return Json(new SelectList(Categories, "Id", "Name"));
        }
        [ActionName("Skill")]
        [HttpGet]
        public async Task<IActionResult> Skill(int id)
        {
            List<Skill> Skills = new List<Skill>();
            Skills = await (from s in _db.Skill where s.CategoryId == id select s).ToListAsync();
            return Json(new SelectList(Skills, "Id", "Name"));
        }
        private readonly ApplicationDbContext _db;
        public IActionResult Register()
        {
            ViewData["CountryId"] = new SelectList(_db.Country, "Id", "Name");
            ViewData["LocationId"] = new SelectList(_db.Location, "Id", "Name");
            List<Category> Categories = new List<Category>();
            Categories = (from Category in _db.Category select Category).ToList();
            ViewBag.listtest = new SelectList(Categories, "Id", "Name");
            return View(RegisterVM);

        }
        //[HttpPost, ActionName("Register")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register()
        //{
        //    RegisterVM.ApplicationUser.BlockStatus = false;

        //    return View();
        //}
    }
}