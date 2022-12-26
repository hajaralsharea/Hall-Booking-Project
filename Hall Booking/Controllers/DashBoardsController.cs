using Hall_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hall_Booking.Controllers
{
    public class DashBoardsController : Controller
    {
        private readonly ModelContext _context;
        public DashBoardsController(ModelContext context)
        {
            _context = context;
        }
        
        public IActionResult Admin()
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            //var User = _context.Users.ToList();
            //var UsersLogin = _context.UsersLogins.ToList();
            //ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            //var model3 = Tuple.Create<IEnumerable<User>, IEnumerable<UsersLogin>>(User, UsersLogin);
            //return View(model3);
            ViewBag.NumberOfUsers =_context.Users.Count();
            ViewBag.NumberOfHalls = _context.Halls.Count();
            ViewBag.Bookings = _context.Bookings.Select(x=>x.HallId).Distinct().Count();
            ViewBag.Acceptedbooking = _context.Bookings.Where(x => x.StatusId == 1).Count();
            ViewBag.Rejectedbooking = _context.Bookings.Where(x => x.StatusId == 2).Count();
            ViewBag.Waitingbooking = _context.Bookings.Where(x => x.StatusId == 3).Count();
            var bookings = _context.Bookings.ToList();
            var halls = _context.Halls.ToList();
           

            //                from emp in db.EmployeeMaster
            //Join dept in db.DepartmentMaster
            //On emp.eID equals dept.empID
            //Select new
            //{
            //    emp.eID,
            //    emp.eName,
            //    dept.dName
            //};


            return View();

        }
        //public IActionResult UserView()
        //{
        //    ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
        //    ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
        //    ViewBag.UserId = HttpContext.Session.GetString("UserId");

        //    return View();
        //}
        public async Task<IActionResult> UserView()
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            ViewBag.UserId = HttpContext.Session.GetString("UserId");
            var modelContext = _context.Halls.Include(p => p.HallCategory);
            return View(await modelContext.ToListAsync());


        }
    }
}
