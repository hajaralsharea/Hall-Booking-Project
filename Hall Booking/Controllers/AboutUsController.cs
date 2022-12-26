using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hall_Booking.Models;
using Microsoft.AspNetCore.Http;

namespace Hall_Booking.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly ModelContext _context;

        public AboutUsController(ModelContext context)
        {
            _context = context;
        }

        // GET: AboutUs
        public async Task<IActionResult> Index()
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            return View(await _context.AboutUses.ToListAsync());
        }
        public async Task<IActionResult> AboutUsHome()
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            return View(await _context.AboutUses.ToListAsync());
        }

        // GET: AboutUs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            if (id == null)
            {
                return NotFound();
            }

            var aboutUs = await _context.AboutUses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aboutUs == null)
            {
                return NotFound();
            }

            return View(aboutUs);
        }

        // GET: AboutUs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AboutUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text,Text2")] AboutUs aboutUs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aboutUs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aboutUs);
        }

        // GET: AboutUs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            if (id == null)
            {
                return NotFound();
            }

            var aboutUs = await _context.AboutUses.FindAsync(id);
            if (aboutUs == null)
            {
                return NotFound();
            }
            return View(aboutUs);
        }

        // POST: AboutUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Text,Text2")] AboutUs aboutUs)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            if (id != aboutUs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aboutUs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutUsExists(aboutUs.Id))
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
            return View(aboutUs);
        }

        // GET: AboutUs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            if (id == null)
            {
                return NotFound();
            }

            var aboutUs = await _context.AboutUses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aboutUs == null)
            {
                return NotFound();
            }

            return View(aboutUs);
        }

        // POST: AboutUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            var aboutUs = await _context.AboutUses.FindAsync(id);
            _context.AboutUses.Remove(aboutUs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutUsExists(decimal id)
        {
            return _context.AboutUses.Any(e => e.Id == id);
        }
    }
}
