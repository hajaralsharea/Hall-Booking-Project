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
    public class ContactUsController : Controller
    {
        private readonly ModelContext _context;

        public ContactUsController(ModelContext context)
        {
            _context = context;
        }

        // GET: ContactUs
        public async Task<IActionResult> Index()
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName"); ;
            var modelContext = _context.ContactUs.Include(c => c.User);
            return View(await modelContext.ToListAsync());
        }
       

        public async Task<IActionResult> AdminMessages()
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            var modelContext = _context.ContactUs.Include(c => c.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: ContactUs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            if (id == null)
            {
                return NotFound();
            }

            var contactU = await _context.ContactUs
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactU == null)
            {
                return NotFound();
            }

            return View(contactU);
        }
        //=======CreatedAtActionResult user contact=====

        // GET: ContactUs/Create
        public IActionResult UserCreateMessage()
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ContactUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserCreateMessage([Bind("Id,FullName,Email,Message,UserId")] ContactU contactU)
        {
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");

            if (ModelState.IsValid)
            {
                _context.Add(contactU);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(UserCreateMessage));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", contactU.UserId);
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            return View(contactU);
        }


        //=======CreatedAtActionResult user contact end=====


        // GET: ContactUs/Create
        public IActionResult Create()
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ContactUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Email,Message,UserId")] ContactU contactU)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");

            if (ModelState.IsValid)
            {
                _context.Add(contactU);
                await _context.SaveChangesAsync();
                return RedirectToAction("Loginss", "LoginAndRegister");
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", contactU.UserId);
            
            return View(contactU);
        }

        // GET: ContactUs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            if (id == null)
            {
                return NotFound();
            }

            var contactU = await _context.ContactUs.FindAsync(id);
            if (contactU == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", contactU.UserId);
            return View(contactU);
        }

        // POST: ContactUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,FullName,Email,Message,UserId")] ContactU contactU)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            if (id != contactU.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactU);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactUExists(contactU.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", contactU.UserId);
            return View(contactU);
        }

        // GET: ContactUs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            if (id == null)
            {
                return NotFound();
            }

            var contactU = await _context.ContactUs
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactU == null)
            {
                return NotFound();
            }

            return View(contactU);
        }

        // POST: ContactUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            var contactU = await _context.ContactUs.FindAsync(id);
            _context.ContactUs.Remove(contactU);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AdminMessages));
        }

        private bool ContactUExists(decimal id)
        {
            return _context.ContactUs.Any(e => e.Id == id);
        }
    }
}
