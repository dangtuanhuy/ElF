using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkAptech.Data;

namespace WorkAptech.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ApplicationUserController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _db;
        public ApplicationUserController(ApplicationDbContext db, IEmailSender emailSender)
        {
            _emailSender = emailSender;
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            return View(await _db.ApplicationUser.Where(u => u.Id != claim.Value).ToListAsync());
        }
        public async Task<IActionResult> Lock(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _db.ApplicationUser.FirstOrDefaultAsync(m => m.Id == id);

            if (applicationUser == null)
            {
                return NotFound();
            }
            applicationUser.EmailConfirmed = false;
            applicationUser.LockoutEnabled = false;

            await _db.SaveChangesAsync();
            await _emailSender.SendEmailAsync(_db.ApplicationUser.Where(u => u.Id == id).FirstOrDefault().Email, "Account Success" + "</br>" + "CC", "");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UnLock(string id)
        {
            if (id == null)
             {
                return NotFound();
            }

            var applicationUser = await _db.ApplicationUser.FirstOrDefaultAsync(m => m.Id == id);

            if (applicationUser == null)
            {
                return NotFound();
            }

            applicationUser.LockoutEnabled = true;
            applicationUser.EmailConfirmed = true;
            await _db.SaveChangesAsync();

            await _emailSender.SendEmailAsync(_db.ApplicationUser.Where(u => u.Id == id).FirstOrDefault().Email,"Account Success"+"</br>"+"CC","");
            return RedirectToAction(nameof(Index));
        }
    }
}