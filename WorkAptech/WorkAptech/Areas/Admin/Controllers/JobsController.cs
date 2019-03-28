using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkAptech.Data;
using WorkAptech.Utility;

namespace WorkAptech.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class JobsController : Controller
    {
        private readonly ApplicationDbContext _context;
        //Sử dụng để upload hình
        private readonly IHostingEnvironment _hostingEnvironment;
        public JobsController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Admin/Jobs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Job.Include(j => j.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job
                .Include(j => j.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // GET: Admin/Jobs/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return View();
        }

        // POST: Admin/Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Salary,Description,Image,JobRole,JobDate,Experience,WorkingTime,WorkingForm,CategoryId,UserId")] Job job)
        {
            if (!ModelState.IsValid)
            {
                return View(job);
            }
            _context.Add(job);
            await _context.SaveChangesAsync();
            //Work on the image saving section

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var JobFromDb = await _context.Job.FindAsync(job.Id);

            if (files.Count > 0)
            {
                //files has been uploaded
                var uploads = Path.Combine(webRootPath, "images");
                var extension = Path.GetExtension(files[0].FileName);

                using (var filesStream = new FileStream(Path.Combine(uploads, job.Id + extension), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                JobFromDb.Image = @"\images\" + job.Id + extension;
            }
            else
            {
                //no file was uploaded, so use default
                var uploads = Path.Combine(webRootPath, @"images\" + SD.DefaultJobImage);
                System.IO.File.Copy(uploads, webRootPath + @"\images\" + job.Id + ".png");
                JobFromDb.Image = @"\images\" + job.Id + ".png";
            }
            await _context.SaveChangesAsync();
            ViewData["CategoryId"] = new SelectList(_context.Category, "Name", "Name", job.CategoryId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Name", "Name",job.UserId);
            return View(job);
        }

        // GET: Admin/Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", job.CategoryId);
            return View(job);
        }

        // POST: Admin/Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Salary,Description,Image,JobRole,JobDate,Experience,WorkingTime,WorkingForm,CategoryId,UserId")] Job job)
        {
            if (id != job.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string webRootPath = _hostingEnvironment.WebRootPath;
                    var files = HttpContext.Request.Form.Files;

                    var JobFromDb = await _context.Job.FindAsync(job.Id);

                    if (files.Count > 0)
                    {
                        //New Image has been uploaded
                        var uploads = Path.Combine(webRootPath, "images");
                        var extension_new = Path.GetExtension(files[0].FileName);

                        //Delete the original file
                        var imagePath = Path.Combine(webRootPath, JobFromDb.Image.TrimStart('\\'));

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                        //we will upload the new file
                        using (var filesStream = new FileStream(Path.Combine(uploads, job.Id + extension_new), FileMode.Create))
                        {
                            files[0].CopyTo(filesStream);
                        }
                        JobFromDb.Image = @"\images\" + job.Id + extension_new;
                    }
                    //_context.Update(job);

                    JobFromDb.Name = job.Name;
                    JobFromDb.Salary = job.Salary;
                    JobFromDb.Description = job.Description;
                    JobFromDb.Experience = job.Experience;
                    JobFromDb.CategoryId = job.CategoryId;
                    JobFromDb.WorkingForm = job.WorkingForm;
                    JobFromDb.WorkingTime = job.WorkingTime;
                    JobFromDb.JobDate = job.JobDate;
                    JobFromDb.JobRole = job.JobRole;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.Id))
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
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", job.CategoryId);
            return View(job);
        }

        // GET: Admin/Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job
                .Include(j => j.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: Admin/Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            var job = await _context.Job.FindAsync(id);
            if (job != null)
            {
                var imagePath = Path.Combine(webRootPath, job.Image.TrimStart('\\'));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                _context.Job.Remove(job);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(int id)
        {
            return _context.Job.Any(e => e.Id == id);
        }
    }
}
