using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkAptech.Data;
using WorkAptech.Models.ViewModels;

namespace WorkAptech.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FormSignInController : Controller
    {
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public ClassVM ClassVM { get; set; }
        public FormSignInController(ApplicationDbContext context)
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
        [HttpPost, ActionName("AddCompany")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> AddCompanyPost()
        {
            if (!ModelState.IsValid)
            {
                return View(ClassVM);
            }
            _context.Company.Add(ClassVM.Company);
            await _context.SaveChangesAsync();
            var f = ClassVM.Company.Id;
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name");
            ViewData["LocationId"] = new SelectList(_context.Location, "Id", "Name");
            return RedirectToAction("Adduser", new { id = f });
        }
        public IActionResult Adduser(int id)
        {
            ViewBag.f = id;
            var a = _context.Company.Where(m => m.Id == id);
            ViewData["CompanyId"] = new SelectList(a, "Id", "Name");
            return View();
        }
        [HttpPost, ActionName("Adduser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdduserPost()
        {
            if (!ModelState.IsValid)
            {
                return View(ClassVM);
            }

            // ClassVM.ApplicationUser.CompanyId = ViewBag.f;
            _context.ApplicationUser.Add(ClassVM.ApplicationUser);
            await _context.SaveChangesAsync();
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Name");
            return RedirectToAction(nameof(Index));
        }
        // GET: FormSignIn
        public ActionResult Index()
        {
            return View();
        }

        // GET: FormSignIn/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FormSignIn/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FormSignIn/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FormSignIn/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FormSignIn/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FormSignIn/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FormSignIn/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}