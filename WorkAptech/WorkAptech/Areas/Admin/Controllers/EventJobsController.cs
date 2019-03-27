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
    public class EventJobsController : Controller
    {
        private readonly ApplicationDbContext _context;

        //upload Img
        private readonly IHostingEnvironment _hostingEnvironment;
        public EventJobsController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        // GET: Admin/EventJobs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EventJob.Include(e => e.Category).Include(e => e.Company);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/EventJobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventJob = await _context.EventJob
                .Include(e => e.Category)
                .Include(e => e.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventJob == null)
            {
                return NotFound();
            }

            return View(eventJob);
        }

        // GET: Admin/EventJobs/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Address");
            return View();
        }

        // POST: Admin/EventJobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TiTle,FromDate,ToDate,Hour,EventStatus,Image,CategoryId,CompanyId,Details")] EventJob eventJob)
        {
            if (!ModelState.IsValid)
            {
                return View(eventJob);

            }

            _context.Add(eventJob);
            await _context.SaveChangesAsync();
            //Work on the image saving section

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var enVentJobFromDb = await _context.EventJob.FindAsync(eventJob.Id);

            if (files.Count > 0)
            {
                //files has been uploaded
                var uploads = Path.Combine(webRootPath, "images");
                var extension = Path.GetExtension(files[0].FileName);

                using (var filesStream = new FileStream(Path.Combine(uploads, eventJob.Id + extension), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                enVentJobFromDb.Image = @"\images\" + eventJob.Id + extension;
            }
            else
            {
                //no file was uploaded, so use default
                var uploads = Path.Combine(webRootPath, @"images\" + SD.DefaultEventImage);
                System.IO.File.Copy(uploads, webRootPath + @"\images\" + eventJob.Id + ".png");
                enVentJobFromDb.Image = @"\images\" + eventJob.Id + ".png";
            }
            await _context.SaveChangesAsync();

            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", eventJob.CategoryId);
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Address", eventJob.CompanyId);
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/EventJobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventJob = await _context.EventJob.FindAsync(id);
            if (eventJob == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", eventJob.CategoryId);
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Address", eventJob.CompanyId);
            return View(eventJob);
        }

        // POST: Admin/EventJobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TiTle,FromDate,ToDate,Hour,EventStatus,Image,CategoryId,CompanyId,Details")] EventJob eventJob)
        {
            if (id != eventJob.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string webRootPath = _hostingEnvironment.WebRootPath;
                    var files = HttpContext.Request.Form.Files;

                    var enVentFromDb = await _context.EventJob.FindAsync(eventJob.Id);

                    if (files.Count > 0)
                    {
                        //New Image has been uploaded
                        var uploads = Path.Combine(webRootPath, "images");
                        var extension_new = Path.GetExtension(files[0].FileName);

                        //Delete the original file
                        var imagePath = Path.Combine(webRootPath, enVentFromDb.Image.TrimStart('\\'));

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                        //we will upload the new file
                        using (var filesStream = new FileStream(Path.Combine(uploads, eventJob.Id + extension_new), FileMode.Create))
                        {
                            files[0].CopyTo(filesStream);
                        }
                        enVentFromDb.Image = @"\images\" + eventJob.Id + extension_new;
                    }
                    enVentFromDb.TiTle = eventJob.TiTle;
                    enVentFromDb.FromDate = eventJob.FromDate;
                    enVentFromDb.ToDate = eventJob.ToDate;
                    enVentFromDb.Hour = eventJob.Hour;
                    enVentFromDb.EventStatus = eventJob.EventStatus;
                    enVentFromDb.CategoryId = eventJob.CategoryId;
                    enVentFromDb.CompanyId = eventJob.CompanyId;

                    //_context.Update(eventJob);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventJobExists(eventJob.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", eventJob.CategoryId);
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Address", eventJob.CompanyId);
            return View(eventJob);
        }

        // GET: Admin/EventJobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventJob = await _context.EventJob
                .Include(e => e.Category)
                .Include(e => e.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventJob == null)
            {
                return NotFound();
            }

            return View(eventJob);
        }

        // POST: Admin/EventJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            var eventJob = await _context.EventJob.FindAsync(id);
            if(eventJob != null)
            {
                var imagePath = Path.Combine(webRootPath, eventJob.Image.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                _context.EventJob.Remove(eventJob);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool EventJobExists(int id)
        {
            return _context.EventJob.Any(e => e.Id == id);
        }
    }
}
