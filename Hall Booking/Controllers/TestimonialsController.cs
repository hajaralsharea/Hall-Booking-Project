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
    public class TestimonialsController : Controller
    {
        private readonly ModelContext _context;
        //public string username1=null;
        public TestimonialsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Testimonials
        public async Task<IActionResult> Index()
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            var modelContext = _context.Testimonials.Include(t => t.User).Include(b => b.Status); 
            return View(await modelContext.ToListAsync());
        }

        // GET: Testimonials/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            if (id == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials
                .Include(t => t.User)
                .Include(b => b.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);
        }

        // GET: Testimonials/Create
        public IActionResult Create()
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Id");
            return View();
        }

        // POST: Testimonials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Commint,UserId,StatusId")] Testimonial testimonial)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            //username1 = ViewBag.EmployeeName;
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.ToList();
            ViewBag.username=null;
              
            if (ModelState.IsValid)
            {

                testimonial.UserId = ViewBag.UserId;
                testimonial.StatusId = 3;
                _context.Add(testimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction("UserView","DashBoards");
            }
            
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", testimonial.UserId);
            //testimonial.StatusId=3;
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Id", testimonial.StatusId);

            return View(testimonial);
        }

        // GET: Testimonials/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            if (id == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", testimonial.UserId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Id", testimonial.StatusId);
            return View(testimonial);
        }


        // POST: Testimonials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Commint,UserId,StatusId")] Testimonial testimonial)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            if (id != testimonial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            { 
                try
                {
                    
                    _context.Update(testimonial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestimonialExists(testimonial.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", testimonial.UserId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Id", testimonial.StatusId);
            return View(testimonial);
        }

        // GET: Testimonials/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            if (id == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials
                .Include(t => t.User)
                .Include(b => b.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);
        }

        // POST: Testimonials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            var testimonial = await _context.Testimonials.FindAsync(id);
            _context.Testimonials.Remove(testimonial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestimonialExists(decimal id)
        {
            return _context.Testimonials.Any(e => e.Id == id);
        }
    }
}
