using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hall_Booking.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;
using System.Net;

namespace Hall_Booking.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ModelContext _context;

        public BookingsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Bookings
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            var modelContext = _context.Bookings.Include(b => b.Hall).Include(b => b.User).Include(b => b.Status);
            return View(await modelContext.ToListAsync());

        }
        [HttpGet]
        public async Task<IActionResult> BookingHistory()
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            var modelContext = _context.Bookings.Include(b => b.Hall).Include(b => b.User).Include(b => b.Status);
            return View(await modelContext.ToListAsync());

        }
        [HttpGet]
        public async Task<IActionResult> Report()
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.UserBookings = _context.Bookings.Select(x => x.UserId).Distinct().Count();
            ViewBag.NumberOfbooking = _context.Bookings.Select(x => x.HallId).Count();
            ViewBag.NumberOfAcceptedBooking = _context.Bookings.Where(x=>x.StatusId==1).Count();
            ViewBag.NumberOfRejectedBooking = _context.Bookings.Where(x => x.StatusId == 2).Count();
            ViewBag.NumberOfWaitingBooking = _context.Bookings.Where(x => x.StatusId == 3).Count();
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            var modelContext = _context.Bookings.Include(b => b.Hall).Include(b => b.User).Include(b => b.Status);
            return View(await modelContext.ToListAsync());

        }
        [HttpPost]
        public async Task<IActionResult> Report(DateTime? startDate, DateTime? endDate)
        {
            ViewBag.NumberOfAcceptedBooking = _context.Bookings.Where(x => x.StatusId == 1).Count();
            ViewBag.NumberOfRejectedBooking = _context.Bookings.Where(x => x.StatusId == 2).Count();
            ViewBag.NumberOfWaitingBooking = _context.Bookings.Where(x => x.StatusId == 3).Count();
            ViewBag.NumberOfbooking = _context.Bookings.Select(x => x.HallId).Count();
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.UserBookings = _context.Bookings.Select(x => x.UserId).Distinct().Count();
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            var modelContext = _context.Bookings.Include(b => b.Hall).Include(b => b.User).Include(b => b.Status);
            if (startDate == null && endDate == null)
            {
                ViewBag.UserBookings = _context.Bookings.Select(x => x.UserId).Distinct().Count();
                ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
                return View(modelContext);
            }
            else if (startDate == null && endDate != null)
            {
                ViewBag.UserBookings = _context.Bookings.Select(x => x.UserId).Distinct().Count();
                ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
                var result = await modelContext.Where(x => x.EndDate.Value.Date == endDate).ToListAsync();
                return View(result);
            }
            else if (startDate != null && endDate == null)
            {
                ViewBag.UserBookings = _context.Bookings.Select(x => x.UserId).Distinct().Count();
                ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
                var result = await modelContext.Where(x => x.StartDate.Value.Date == startDate).ToListAsync();
                return View(result);

            }
            else
            {
                ViewBag.UserBookings = _context.Bookings.Select(x => x.UserId).Distinct().Count();
                ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
                var result = await modelContext.Where(x => x.StartDate >= startDate && x.EndDate <= endDate).ToListAsync();
                return View(result);
            }

        }
    

        [HttpGet]
        public async Task<IActionResult> SearchBooking()
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            //ViewBag.UserBookings = _context.Bookings.Select(x => x.UserId).Distinct().Count();
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            var modelContext = _context.Bookings.Include(b => b.Hall).Include(b => b.User).Include(b => b.Status);
            return View(await modelContext.ToListAsync());

        }
        [HttpPost]
        public async Task<IActionResult> SearchBooking(DateTime? startDate, DateTime? endDate)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            var modelContext = _context.Bookings.Include(b => b.Hall).Include(b => b.User).Include(b => b.Status);
            if (startDate == null && endDate == null)
            {
                return View(modelContext);
            }
            else if (startDate == null && endDate != null)
            {

                var result = await modelContext.Where(x => x.EndDate.Value.Date == endDate).ToListAsync();
                return View(result);
            }
            else if (startDate != null && endDate == null)
            {
                var result = await modelContext.Where(x => x.StartDate.Value.Date == startDate).ToListAsync();
                return View(result);

            }
            else
            {
                var result = await modelContext.Where(x => x.StartDate >= startDate && x.EndDate <= endDate).ToListAsync();
                return View(result);
            }

        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Hall)
                .Include(b => b.User)
                .Include(b => b.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            //var bookid = from item in _context.Bookings where item.HallId == id select item;
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            ViewData["HallId"] = new SelectList(_context.Halls, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Id");


            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookingNotes,BookingDate,StartDate,EndDate,HallId,UserId,StatusId")] Booking booking)
        {
            
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            var user = _context.Users.ToList();
         
            var check = 0;
            var checkpayment = 0;
            var bookinglist = _context.Bookings.ToList();
            var payment = _context.Payments.ToList();
            var hall = _context.Halls.ToList();
            if (ModelState.IsValid)
            {
                foreach (var itempay in payment)
                {
                    if (ViewBag.UserId == itempay.UserId)
                    {
                        checkpayment = 1;
                        break;
                    }
                }


                if (checkpayment == 1)

                {
                    foreach (var item in bookinglist)
                    {
                        if (item.HallId == booking.HallId)
                        {
                            check = 1;
                            break;
                        }

                    }

                    foreach (var item in bookinglist)

                    {
                        //if (item.HallId == booking.HallId)
                        //{


                          if (check == 1)
                        {
                                if (item.HallId == booking.HallId)
                                {

                                    if (booking.StartDate < item.StartDate && booking.EndDate < item.StartDate)
                            {

                                booking.BookingDate = null;
                                booking.UserId = ViewBag.UserId;
                                booking.StatusId = 3;
                                _context.Add(booking);
                                await _context.SaveChangesAsync();
                                return RedirectToAction("UserHallsCard", "Halls");

                            }
                            else if (booking.StartDate > item.EndDate && booking.EndDate > item.EndDate)
                            {

                                booking.BookingDate = null;
                                booking.UserId = ViewBag.UserId;
                                booking.StatusId = 3;
                                _context.Add(booking);
                                await _context.SaveChangesAsync();
                                return RedirectToAction("UserHallsCard", "Halls");
                            }

                            else
                            {
                                TempData["alertMessage"] = "Sorry This Date Not available , Change the date Please! ";
                                return RedirectToAction("Create", "Bookings");
                            }
                        }}
                        else
                        {

                            booking.BookingDate = null;
                            booking.UserId = ViewBag.UserId;
                            booking.StatusId = 3;
                            _context.Add(booking);
                            await _context.SaveChangesAsync();
                            return RedirectToAction("UserHallsCard", "Halls");
                        }

                    
                
                        //else
                        //{
                        //    continue;
                        //}
                    } 
                    
                }
                else
                {
                    TempData["alertMessage"] = "Hi " + ViewBag.EmployeeName + " Please Fill your Visa Info";
                    return RedirectToAction("Create", "Payments");
                }

              




            }
            ViewData["HallId"] = new SelectList(_context.Halls, "Id", "Id", booking.HallId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", booking.UserId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Id", booking.StatusId);

            return View(booking);
        }


        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");

            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["HallId"] = new SelectList(_context.Halls, "Id", "Id", booking.HallId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", booking.UserId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "StatusName", booking.StatusId);

            return View(booking);
        }



        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public int userbalance = 0;
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,BookingNotes,BookingDate,StartDate,EndDate,HallId,UserId,StatusId")] Booking booking)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            var payment = _context.Payments.ToList();
            var hall = _context.Halls.ToList();
            int hallprice = 0;
           
            int invoice = 0;
            var user = _context.Users.ToList();
            string email = null;
            var name = "name";
            var tax = 10;
            foreach (var item in payment)
            {
                if (item.UserId == booking.UserId)
                {
                    userbalance = (int)item.CardBalance;
                    break;
                }
            }
            foreach (var item in hall)
            {
                if (item.Id == booking.HallId)
                {
                    hallprice = (int)item.Price;
                    break;
                }
            }
            var totalInvoice = hallprice + tax;
            foreach (var item3 in user)
            {
                if (booking.UserId == item3.Id)
                {
                    email = item3.Email;
                    name = item3.Fname;
                    break;
                }
            }
            invoice=userbalance- (int)hallprice;


            if (id != booking.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    
                        if (booking.StatusId == 1)
                        {
                        if (invoice >= 0)
                        {
                            userbalance -=(int) hallprice;
                            SmtpClient Client = new SmtpClient("smtp.office365.com");
                            Client.Port = 587;
                            Client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            Client.UseDefaultCredentials = false;
                            NetworkCredential credential = new NetworkCredential("hallbooking99@outlook.com", "hall12345");
                            Client.EnableSsl = true;
                            Client.Credentials = credential;

                            MailMessage message = new MailMessage("hallbooking99@outlook.com", email);
                            message.Subject = "Hall Booking";


                            message.Body = "Hello " + name + " !" + "</br> Your booking is accepted and Total invoice is " + hallprice + "JD your balance after pay is " + userbalance + "JD </br>Thank you For your trust enjoy your time.";
                            message.IsBodyHtml = true;
                            Client.Send(message);

                            booking.BookingDate = DateTime.Now;

                        }
                        else if (invoice < 0)
                        {
                            
                            SmtpClient Client = new SmtpClient("smtp.office365.com");
                            Client.Port = 587;
                            Client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            Client.UseDefaultCredentials = false;
                            NetworkCredential credential = new NetworkCredential("hallbooking99@outlook.com", "hall12345");
                            Client.EnableSsl = true;
                            Client.Credentials = credential;

                            MailMessage message = new MailMessage("hallbooking99@outlook.com", email);
                            message.Subject = "Hall Booking";

                            message.Body = "Hello " + name + " !" + "</br> Your booking is still waiting  couse your balance is " + userbalance + "JD not enough for hall price " + hallprice + " JD charge you visa and try again Thank you For your trust enjoy your time.";
                            message.IsBodyHtml = true;
                            Client.Send(message);
                            booking.BookingDate = null;
                            booking.StatusId = 3;


                        }
                    }
                        else if (booking.StatusId == 2)
                        {

                            SmtpClient Client = new SmtpClient("smtp.office365.com");
                            Client.Port = 587;
                            Client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            Client.UseDefaultCredentials = false;
                            NetworkCredential credential = new NetworkCredential("hallbooking99@outlook.com", "hall12345");
                            Client.EnableSsl = true;
                            Client.Credentials = credential;

                            MailMessage message = new MailMessage("hallbooking99@outlook.com", email);
                            message.Subject = "Hall Booking";


                        message.Body = "Hello " + name + " !" + "</br> Sorry your booking not accepted try again later" + " </br> Thank you For your trust enjoy your time.";
                        message.IsBodyHtml = true;
                            Client.Send(message);
                            booking.BookingDate = DateTime.Now;
                        }
                        else
                        {
                            booking.BookingDate = null;
                            booking.StatusId = 3;
                        }


                    foreach (var item in payment)
                    {
                        if (item.UserId == booking.UserId)
                        {
                           item.CardBalance=userbalance;
                            break;
                        }
                    }
                    _context.Update(booking);
                    await _context.SaveChangesAsync();


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("SearchBooking", "Bookings");




            }
            ViewData["HallId"] = new SelectList(_context.Halls, "Id", "Id", booking.HallId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", booking.UserId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Id", booking.StatusId);


            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Hall)
                .Include(b => b.User)
                .Include(b => b.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            var booking = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(SearchBooking));
        }

        private bool BookingExists(decimal id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}
