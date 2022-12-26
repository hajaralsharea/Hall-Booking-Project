using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hall_Booking.Models;
using System.Net.Mail;

namespace Hall_Booking.Controllers
{
    public class MailRequestsController : Controller
    {
        private readonly ModelContext _context;

        public MailRequestsController(ModelContext context)
        {
            _context = context;
        }

        // GET: MailRequests
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.MailRequests.Include(m => m.Booking);
            return View(await modelContext.ToListAsync());
        }

        // GET: MailRequests/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mailRequest = await _context.MailRequests
                .Include(m => m.Booking)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mailRequest == null)
            {
                return NotFound();
            }

            return View(mailRequest);
        }

        // GET: MailRequests/Create
        public IActionResult Create()
        {
            ViewData["BookingId"] = new SelectList(_context.Bookings, "Id", "Id");
            return View();
        }

        // POST: MailRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ToEmail,Subject,Body,FilePath,BookingId")] MailRequest mailRequest)
        {
            //int checkbookingstatus = 0;
            //var userid = 0;
            //var hallid = 0;
            //string email = null;
            //double userbalance = 0;
            //double hallprice = 0;
            //var booking = _context.Bookings.ToList();
            //var user = _context.Users.ToList();
            //var payment=_context.Payments.ToList();
            //var hall = _context.Halls.ToList();
        if (ModelState.IsValid)
        {

            //foreach(var itemb in booking)
            //{
            //    if (itemb.StatusId == 1)
            //    {
            //        checkbookingstatus = 1;
            //        userid = (int)itemb.UserId;
            //        hallid = (int)itemb.HallId;
            //        foreach(var itemu in user)
            //        {
            //            if (userid == itemu.Id)
            //            {
            //                email = itemu.Email;
            //                break;
            //            }
            //        }
            //        foreach(var itemp in payment)
            //        {
            //            if (userid == itemp.UserId)
            //            {
            //                userbalance = (double)itemp.CardBalance;
            //                break;
            //            }
            //        }
            //        foreach(var itemh in hall)
            //        {
            //            if (hallid == itemh.Id)
            //            {
            //                hallprice = (double)itemh.Price;
            //                break;
            //            }
            //        }

            //            try
            //            {
            //                SmtpClient Client = new SmtpClient("smyp-mail.outlook.com");
            //                Client.Port = 587;
            //                Client.DeliveryMethod=SmtpDeliveryMethod.Network;
            //                Client.UseDefaultCredentials = false;
            //                System.Net.NetworkCredential credential= new System.Net.NetworkCredential("hallbooking99@outlook.com","hall12345");
            //                Client.EnableSsl = true;
            //                Client.Credentials = credential;

            //                MailMessage message =new MailMessage("hallbooking99@outlook.com", "hallbooking99@outlook.com");
            //                message.Subject = "Hall Booking";
            //                if ((userbalance - hallprice) >= 0)
            //                {
            //                    message.Body = "Your Booking is Accepted And your  Total Invoice is" + hallprice + " Enjoy Your Time";
            //                }
            //                else
            //                {
            //                    message.Body = "Your Booking is Accepted And but your  balance not enough to hall booking please check it and try again";

            //                }
            //                message.IsBodyHtml = true;
            //            }
            //            catch (Exception)
            //            {
            //                throw;
            //            }



            //    }
            //        else
            //        {

            //        }
            //}
            




            
                _context.Add(mailRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookingId"] = new SelectList(_context.Bookings, "Id", "Id", mailRequest.BookingId);
            return View(mailRequest);
        }

        // GET: MailRequests/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mailRequest = await _context.MailRequests.FindAsync(id);
            if (mailRequest == null)
            {
                return NotFound();
            }
            ViewData["BookingId"] = new SelectList(_context.Bookings, "Id", "Id", mailRequest.BookingId);
            return View(mailRequest);
        }

        // POST: MailRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,ToEmail,Subject,Body,FilePath,BookingId")] MailRequest mailRequest)
        {
            if (id != mailRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mailRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MailRequestExists(mailRequest.Id))
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
            ViewData["BookingId"] = new SelectList(_context.Bookings, "Id", "Id", mailRequest.BookingId);
            return View(mailRequest);
        }

        // GET: MailRequests/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mailRequest = await _context.MailRequests
                .Include(m => m.Booking)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mailRequest == null)
            {
                return NotFound();
            }

            return View(mailRequest);
        }

        // POST: MailRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var mailRequest = await _context.MailRequests.FindAsync(id);
            _context.MailRequests.Remove(mailRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MailRequestExists(decimal id)
        {
            return _context.MailRequests.Any(e => e.Id == id);
        }
    }
}
