using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Generatehttps.Data;
using Generatehttps.Models;

namespace Generatehttps.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class JobSkillsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobSkillsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/JobSkills
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.JobSkill.Include(j => j.Job).Include(j => j.Skill);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/JobSkills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobSkill = await _context.JobSkill
                .Include(j => j.Job)
                .Include(j => j.Skill)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobSkill == null)
            {
                return NotFound();
            }

            return View(jobSkill);
        }

        // GET: Admin/JobSkills/Create
        public IActionResult Create()
        {
            ViewData["JobId"] = new SelectList(_context.Job, "Id", "Name");
            ViewData["SkillId"] = new SelectList(_context.Skill, "Id", "Name");
            return View();
        }

        // POST: Admin/JobSkills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SkillId,JobId")] JobSkill jobSkill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobSkill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobId"] = new SelectList(_context.Job, "Id", "Name", jobSkill.JobId);
            ViewData["SkillId"] = new SelectList(_context.Skill, "Id", "Name", jobSkill.SkillId);
            return View(jobSkill);
        }

        // GET: Admin/JobSkills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobSkill = await _context.JobSkill.FindAsync(id);
            if (jobSkill == null)
            {
                return NotFound();
            }
            ViewData["JobId"] = new SelectList(_context.Job, "Id", "Name", jobSkill.JobId);
            ViewData["SkillId"] = new SelectList(_context.Skill, "Id", "Name", jobSkill.SkillId);
            return View(jobSkill);
        }

        // POST: Admin/JobSkills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SkillId,JobId")] JobSkill jobSkill)
        {
            if (id != jobSkill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobSkill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobSkillExists(jobSkill.Id))
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
            ViewData["JobId"] = new SelectList(_context.Job, "Id", "Name", jobSkill.JobId);
            ViewData["SkillId"] = new SelectList(_context.Skill, "Id", "Name", jobSkill.SkillId);
            return View(jobSkill);
        }

        // GET: Admin/JobSkills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobSkill = await _context.JobSkill
                .Include(j => j.Job)
                .Include(j => j.Skill)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobSkill == null)
            {
                return NotFound();
            }

            return View(jobSkill);
        }

        // POST: Admin/JobSkills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobSkill = await _context.JobSkill.FindAsync(id);
            _context.JobSkill.Remove(jobSkill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobSkillExists(int id)
        {
            return _context.JobSkill.Any(e => e.Id == id);
        }
    }
}
