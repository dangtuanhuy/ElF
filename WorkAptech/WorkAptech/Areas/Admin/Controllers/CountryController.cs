using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkAptech.Data;

namespace WorkAptech.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CountryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CountryController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: Category
        public async Task<IActionResult> Index()
        {
            return View(await _db.Country.ToListAsync());
        }

        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }
        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Country country)
        {
            if (ModelState.IsValid)
            {
                //if valid
                _db.Country.Add(country);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            return View(country);
        }

        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var country = await _db.Country.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Country country)
        {
            if (ModelState.IsValid)
            {
                _db.Update(country);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var country = await _db.Country.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var country = await _db.Country.FindAsync(id);

            if (country == null)
            {
                return View();
            }
            _db.Country.Remove(country);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //GET - DETAILS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _db.Country.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }
    }
}