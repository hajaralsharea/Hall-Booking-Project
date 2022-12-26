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
    public class PaymentsController : Controller
    {
        private readonly ModelContext _context;
        
        public PaymentsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            var modelContext = _context.Payments.Include(p => p.User);
       
            return View(await modelContext.ToListAsync());
        }
        public IActionResult UserVisa()
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            var modelContext = _context.Payments.Include(p => p.User);
            var hall = _context.Halls.ToList();
            var booking = _context.Bookings.ToList();
            var payment=_context.Payments.ToList();
            //var tuple3 = Tuple.Create<IEnumerable<Payment>, IEnumerable<Hall>, IEnumerable<Booking>>(payment, hall, booking);
 //double invoice = 0;
 //           foreach(var item in payment) {
 //           if (ViewBag.UserId == item.UserId) { 
 //               ViewBag.balance = (double)item.CardBalance;
 //              }}
        
 //           foreach (var itemb in booking)
 //           {
 //                 foreach (var itemh in hall)
 //                {
 //               if (ViewBag.UserId == itemb.UserId)
 //               {
 //                   if (itemb.StatusId == 1)
 //                   {
                       
 //                           if (itemb.HallId == itemh.Id)
 //                           {
 //                               invoice = ViewBag.balance - (double)itemh.Price;
 //                               if (invoice >= 0)
 //                               {
 //                                   ViewBag.balance -= (double)itemh.Price;
 //                               }
 //                               else
 //                               {
 //                                   itemb.BookingDate = null;
 //                                   itemb.StatusId = 3;
 //                               }

 //                           }
 //                       }

 //                   }
 //               }}
            
            return View(modelContext);
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PaymentDate,CardName,CardSequanceNumber,CardBalance,UserId")] Payment payment)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            if (ModelState.IsValid)
            {   payment.PaymentDate = DateTime.Now;
                payment.UserId = ViewBag.UserId;
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Bookings");
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", payment.UserId);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", payment.UserId);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,PaymentDate,CardName,CardSequanceNumber,CardBalance,UserId")] Payment payment)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            if (id != payment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(UserVisa));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", payment.UserId);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            var payment = await _context.Payments.FindAsync(id);
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction("UserView", "DashBoards");
        }

        private bool PaymentExists(decimal id)
        {
            return _context.Payments.Any(e => e.Id == id);
        }
    }
}
