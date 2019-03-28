using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkAptech.Data;

namespace WorkAptech.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PageController : Controller
    {
        private readonly ApplicationDbContext _db;

        public IActionResult Index()
        {
            return View();
        }
        public PageController(ApplicationDbContext db)
        {
            _db = db;
        }
        public  IActionResult _statusRegister()
        {
            var statusRegister = (from r in _db.ApplicationUser where r.LockoutEnabled == false select r).Count();
            ViewBag.statusRegister = statusRegister;
            return PartialView();
        }
        public IActionResult GetStatus()
        {
            return PartialView("_statusRegister", _db.ApplicationUser.Where(m => m.LockoutEnabled == true).Count());

        }
        public ActionResult _countEvent()
        {
            var countEvent =( _db.ApplicationUser.Where(m => m.LockoutEnabled == true).Count());
            ViewBag.countFoods = countEvent;
            return PartialView();
        }
       


    }
}