using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Generatehttps.Data;
using Generatehttps.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Generatehttps.Areas.Admin.Controllers
{
        [Area("Admin")]
        public class NumberOfEmployeeController : Controller
        {

            private readonly ApplicationDbContext _context;

            public NumberOfEmployeeController(ApplicationDbContext context)
            {
                _context = context;
            }

            // GET: Admin/Categories
            public async Task<IActionResult> Index()
            {
                return View(await _context.NumOfEmployee.ToListAsync());
            }

            // GET: Admin/Categories/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var numberofempoyee = await _context.NumOfEmployee
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (numberofempoyee == null)
                {
                    return NotFound();
                }

                return View(numberofempoyee);
            }

            // GET: Admin/Categories/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: Admin/Categories/Create
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,NumberOfEmployee")] NumOfEmployee numOfEmployee)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(numOfEmployee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(numOfEmployee);
            }

            // GET: Admin/Categories/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var numOfEmployee = await _context.NumOfEmployee.FindAsync(id);
                if (numOfEmployee == null)
                {
                    return NotFound();
                }
                return View(numOfEmployee);
            }

            // POST: Admin/Categories/Edit/5
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(NumOfEmployee numOfEmployee)
            {
                if (ModelState.IsValid)
                {
                    _context.Update(numOfEmployee);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                return View(numOfEmployee);
            }

            //GET - DELETE
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }
                var numOfEmployee = await _context.NumOfEmployee.FindAsync(id);
                if (numOfEmployee == null)
                {
                    return NotFound();
                }
                return View(numOfEmployee);
            }

            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int? id)
            {
                var numOfEmployee = await _context.NumOfEmployee.FindAsync(id);

                if (numOfEmployee == null)
                {
                    return View();
                }
                _context.NumOfEmployee.Remove(numOfEmployee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

        }
    }