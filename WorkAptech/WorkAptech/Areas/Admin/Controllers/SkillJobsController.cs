using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkAptech.Data;

namespace WorkAptech.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillJobsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SkillJobsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/SkillJobs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SkillJob.Include(s => s.Job).Include(s => s.Skill);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/SkillJobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skillJob = await _context.SkillJob
                .Include(s => s.Job)
                .Include(s => s.Skill)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skillJob == null)
            {
                return NotFound();
            }

            return View(skillJob);
        }

        // GET: Admin/SkillJobs/Create
        public IActionResult Create()
        {
            ViewData["JobId"] = new SelectList(_context.Job, "Id", "Name");
            ViewData["SkillId"] = new SelectList(_context.Skill, "Id", "Name");
            return View();
        }

        // POST: Admin/SkillJobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SkillId,JobId")] SkillJob skillJob)
        {
            if (ModelState.IsValid)
            {
                _context.Add(skillJob);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobId"] = new SelectList(_context.Job, "Id", "Name", skillJob.JobId);
            ViewData["SkillId"] = new SelectList(_context.Skill, "Id", "Name", skillJob.SkillId);
            return View(skillJob);
        }

        // GET: Admin/SkillJobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skillJob = await _context.SkillJob.FindAsync(id);
            if (skillJob == null)
            {
                return NotFound();
            }
            ViewData["JobId"] = new SelectList(_context.Job, "Id", "Name", skillJob.JobId);
            ViewData["SkillId"] = new SelectList(_context.Skill, "Id", "Name", skillJob.SkillId);
            return View(skillJob);
        }

        // POST: Admin/SkillJobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SkillId,JobId")] SkillJob skillJob)
        {
            if (id != skillJob.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skillJob);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkillJobExists(skillJob.Id))
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
            ViewData["JobId"] = new SelectList(_context.Job, "Id", "Name", skillJob.JobId);
            ViewData["SkillId"] = new SelectList(_context.Skill, "Id", "Name", skillJob.SkillId);
            return View(skillJob);
        }

        // GET: Admin/SkillJobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skillJob = await _context.SkillJob
                .Include(s => s.Job)
                .Include(s => s.Skill)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skillJob == null)
            {
                return NotFound();
            }

            return View(skillJob);
        }

        // POST: Admin/SkillJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skillJob = await _context.SkillJob.FindAsync(id);
            _context.SkillJob.Remove(skillJob);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkillJobExists(int id)
        {
            return _context.SkillJob.Any(e => e.Id == id);
        }
    }
}
