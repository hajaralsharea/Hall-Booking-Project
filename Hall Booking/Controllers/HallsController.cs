using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hall_Booking.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Hall_Booking.Controllers
{
    public class HallsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;
   
        public HallsController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }


        // GET: Halls
        public async Task<IActionResult> Index()
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            var modelContext = _context.Halls.Include(p => p.HallCategory);
            return View(await modelContext.ToListAsync());
            
        }
        [HttpGet]
        public async Task<IActionResult> HallsCardHome()
        {
            var modelContext = _context.Halls.Include(p => p.HallCategory);
            return View(await modelContext.ToListAsync());

        }


        public  IActionResult HallsCard(int id)
        {

                var Hall = _context.Halls.Where(x => x.CategoryId == id).ToList();
                return View(Hall);
            
        

        }
        [HttpPost]
        public async Task<IActionResult> HallsCard(string hallName,string address)
        {

            var modelContext = _context.Halls.Include(p => p.HallCategory);
            if(hallName==null&& address == null)
            {
                return View(modelContext);
            }
            else if(hallName!=null && address == null)
            {
                var result = await modelContext.Where(x => x.HallName == (hallName.ToLower())).ToListAsync();
                return View(result);
            }
            else if(hallName==null && address != null)
            {
                var result = await modelContext.Where(x => x.HallAddress == (address.ToLower())).ToListAsync();
                return View(result);

            }
            else
            {
                var result = await modelContext.Where(x => x.HallAddress == address.ToLower() && x.HallName== hallName.ToLower()).ToListAsync();
                return View(result);
            }


        }
        [HttpGet]
        public async Task<IActionResult> UserHallsCard()
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            var modelContext = _context.Halls.Include(p => p.HallCategory);
            return View(await modelContext.ToListAsync());

        }

        [HttpPost]
        public async Task<IActionResult> UserHallsCard(string hallName, string address)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");

            var modelContext = _context.Halls.Include(p => p.HallCategory);
            if (hallName == null && address == null)
            {
                return View(modelContext);
            }
            else if (hallName != null && address == null)
            {
                var result = await modelContext.Where(x => x.HallName == hallName.ToLower()).ToListAsync();
                return View(result);
            }
            else if (hallName == null && address != null)
            {
                var result = await modelContext.Where(x => x.HallAddress == address.ToLower()).ToListAsync();
                return View(result);

            }
            else
            {
                var result = await modelContext.Where(x => x.HallAddress == address.ToLower() && x.HallName == hallName.ToLower()).ToListAsync();
                return View(result);
            }


        }

        // GET: Halls/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hall = await _context.Halls
                .Include(p => p.HallCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hall == null)
            {
                return NotFound();
            }

            return View(hall);
        }

        // GET: Halls/Create
        public IActionResult Create()
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            ViewData["CategoryId"] = new SelectList(_context.HallCategories, "Id", "Id");
            return View();
        }

        // POST: Halls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HallName,HallAddress,Capacity,Price,HallArea,ImagePath,ImageFile,CategoryId")] Hall hall)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            if (ModelState.IsValid)
            {
                if (hall.ImageFile != null)
                {
                    string wwwrootPath = _webHostEnviroment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + hall.ImageFile.FileName;

                    string path = Path.Combine(wwwrootPath + "/Images/" + fileName);
                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await hall.ImageFile.CopyToAsync(filestream);
                    }
                    hall.ImagePath = fileName;
                    _context.Add(hall);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.HallCategories, "Id", "Id", hall.CategoryId);
            return View(hall);
        }

        // GET: Halls/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            if (id == null)
            {
                return NotFound();
            }

            var hall = await _context.Halls.FindAsync(id);
            if (hall == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.HallCategories, "Id", "Id", hall.CategoryId);
            return View(hall);
        }

        // POST: Halls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,HallName,HallAddress,Capacity,Price,HallArea,ImagePath,ImageFile,CategoryId")] Hall hall)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            if (id != hall.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (hall.ImageFile != null)
                    {

                        string wwwrootPath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + hall.ImageFile.FileName;
                        string path = Path.Combine(wwwrootPath + "/Images/" + fileName);
                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            await hall.ImageFile.CopyToAsync(filestream);
                        }
                        hall.ImagePath = fileName;
                    }
                    _context.Update(hall);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HallExists(hall.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.HallCategories, "Id", "Id", hall.CategoryId);
            return View(hall);
        }


        


        // GET: Halls/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            if (id == null)
            {
                return NotFound();
            }

            var hall = await _context.Halls
                 .Include(p => p.HallCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hall == null)
            {
                return NotFound();
            }

            return View(hall);
        }

        // POST: Halls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            var hall = await _context.Halls.FindAsync(id);
            _context.Halls.Remove(hall);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HallExists(decimal id)
        {
            return _context.Halls.Any(e => e.Id == id);
        }
    }
}
