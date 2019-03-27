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
    public class LocationController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LocationController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: Location
        public async Task<ActionResult> Index()
        {
            return View(await _db.Location.ToListAsync());
        }

        // GET: Location/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _db.Location.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: Location/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Location/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Location location)
        {
            try
            {
                if (ModelState.IsValid)
                    _db.Location.Add(location);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(location);
            }
        }

        // GET: Location/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var location = await _db.Location.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);

        }

        // POST: Location/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Location location)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    _db.Update(location);
                    await _db.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(location);
            }
        }

        // GET: Location/Delete/5
        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var location = await _db.Location.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var location = await _db.Location.FindAsync(id);

            if (location == null)
            {
                return View();
            }
            _db.Location.Remove(location);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}