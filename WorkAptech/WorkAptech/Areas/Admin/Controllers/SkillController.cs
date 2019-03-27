using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkAptech.Data;
using WorkAptech.Models.ViewModels;

namespace WorkAptech.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public SKillViewModels SKillViewModels { get; set; }
        public SkillController(ApplicationDbContext db)
        {
            _db = db;
            SKillViewModels = new SKillViewModels()
            {
                Category = _db.Category,
                Skill = new Data.Skill()
            };
        }

        public async Task<IActionResult> Index()
        {
            var skill = await _db.Skill.Include(m => m.Category).ToListAsync();
            return View(skill);
        }
    }
}