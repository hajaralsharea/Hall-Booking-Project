using Hall_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Hall_Booking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;
        public HomeController(ILogger<HomeController> logger, ModelContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {   var hallcat=_context.HallCategories.ToList();
            var test = _context.Testimonials.ToList();
            var user = _context.Users.ToList();
            var model3 = Tuple.Create<IEnumerable<HallCategory>, IEnumerable<Testimonial>, IEnumerable<User>>(hallcat, test,user);
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");

            return View(model3);
        }
      
        //public IActionResult CatHall(int id)
        //{
        //    var Hall = _context.Halls.Where(x => x.CategoryId == id).ToList();
        //    return View(Hall);
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
