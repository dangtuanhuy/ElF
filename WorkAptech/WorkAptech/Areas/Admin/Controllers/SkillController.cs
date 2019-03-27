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

        public IActionResult Create()
        {
            return View(SKillViewModels);
        }
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            //SKillViewModels.Skill.CategoryId = Convert.ToInt32(Request.Form["CategoryId"].ToString());
            if (!ModelState.IsValid)
            {
                return View(SKillViewModels);
            }
            _db.Skill.Add(SKillViewModels.Skill);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = await _db.Skill.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_db.Category, "Id", "Name", skill.CategoryId);
            return View(skill);
        }

        // POST: Admin/Skills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CategoryId")] Skill skill)
        {
            if (id != skill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(skill);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkillExists(skill.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_db.Category, "Id", "Name", skill.CategoryId);
            return View(skill);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = await _db.Skill
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = await _db.Skill
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        // POST: Admin/Skills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skill = await _db.Skill.FindAsync(id);
            _db.Skill.Remove(skill);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkillExists(int id)
        {
            return _db.Skill.Any(e => e.Id == id);
        }
    }
}