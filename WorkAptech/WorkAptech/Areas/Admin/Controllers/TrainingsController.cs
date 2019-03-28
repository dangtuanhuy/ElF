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
    public class TrainingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public TrainingsController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Admin/Trainings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Training.Include(t => t.Category).Include(t => t.Company);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Trainings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _context.Training
                .Include(t => t.Category)
                .Include(t => t.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (training == null)
            {
                return NotFound();
            }

            return View(training);
        }

        // GET: Admin/Trainings/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Name");
            return View();
        }

        // POST: Admin/Trainings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Type,TrainingDate,Hours,Description,Status,Image,CategoryId,CompanyId")] Training training)
        {
            if (ModelState.IsValid)
            {
                _context.Add(training);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", training.CategoryId);
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Address", training.CompanyId);
            

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var TrainFromDb = await _context.Training.FindAsync(training.Id);

            if (files.Count > 0)
            {
                //files has been uploaded
                var uploads = Path.Combine(webRootPath, "images");
                var extension = Path.GetExtension(files[0].FileName);

                using (var filesStream = new FileStream(Path.Combine(uploads, training.Id + extension), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                TrainFromDb.Image = @"\images\" + training.Id + extension;
            }
            else
            {
                //no file was uploaded, so use default
                var uploads = Path.Combine(webRootPath, @"images\" + SD.DefaultCategoryImage);
                System.IO.File.Copy(uploads, webRootPath + @"\images\" + training.Id + ".png");
                TrainFromDb.Image = @"\images\" + training.Id + ".png";
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Trainings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _context.Training.FindAsync(id);
            if (training == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", training.CategoryId);
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Address", training.CompanyId);
            return View(training);
        }

        // POST: Admin/Trainings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Type,TrainingDate,Hours,Description,Status,Image,CategoryId,CompanyId")] Training training)
        {
            if (id != training.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string webRootPath = _hostingEnvironment.WebRootPath;
                    var files = HttpContext.Request.Form.Files;
                    var TrainFromDb = await _context.Training.FindAsync(training.Id);
                    if (files.Count > 0)
                    {
                        //New Image has been uploaded
                        var uploads = Path.Combine(webRootPath, "images");
                        var extension_new = Path.GetExtension(files[0].FileName);

                        //Delete the original file
                        var imagePath = Path.Combine(webRootPath, TrainFromDb.Image.TrimStart('\\'));

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                        using (var filesStream = new FileStream(Path.Combine(uploads, training.Id + extension_new), FileMode.Create))
                        {
                            files[0].CopyTo(filesStream);
                        }
                        TrainFromDb.Image = @"\images\" + training.Id + extension_new;
                    }
                    TrainFromDb.Title  = training.Title;
                    //_context.Update(training);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingExists(training.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", training.CategoryId);
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Address", training.CompanyId);
            return View(training);
        }

        // GET: Admin/Trainings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _context.Training
                .Include(t => t.Category)
                .Include(t => t.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (training == null)
            {
                return NotFound();
            }

            return View(training);
        }

        // POST: Admin/Trainings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            var training = await _context.Training.FindAsync(id);
            if (training != null)
            {
                var imagePath = Path.Combine(webRootPath, training.Image.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                _context.Training.Remove(training);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Lock(Int32? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.Training.FirstOrDefaultAsync(m => m.Id == id);

            if (applicationUser == null)
            {
                return NotFound();
            }

            applicationUser.Status = true;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UnLock(Int32? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.Training.FirstOrDefaultAsync(m => m.Id == id);

            if (applicationUser == null)
            {
                return NotFound();
            }

            applicationUser.Status = false;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        private bool TrainingExists(int id)
        {
            return _context.Training.Any(e => e.Id == id);
        }
    }
}
