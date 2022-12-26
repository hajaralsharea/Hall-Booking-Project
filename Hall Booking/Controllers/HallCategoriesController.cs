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
    public class HallCategoriesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public HallCategoriesController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        // GET: HallCategories
        public async Task<IActionResult> Index()
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            return View(await _context.HallCategories.ToListAsync());
        }

        // GET: HallCategories/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hallCategory = await _context.HallCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hallCategory == null)
            {
                return NotFound();
            }

            return View(hallCategory);
        }

        // GET: HallCategories/Create
        public IActionResult Create()
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            return View();
        }

        // POST: HallCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryName,HallDescription,ImagePath,ImageFile")] HallCategory hallCategory)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            if (ModelState.IsValid)
            {
                if (hallCategory.ImageFile != null)
                {
                    string wwwrootPath = _webHostEnviroment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + hallCategory.ImageFile.FileName;

                    string path = Path.Combine(wwwrootPath + "/Images/" + fileName);
                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await hallCategory.ImageFile.CopyToAsync(filestream);
                    }
                    hallCategory.ImagePath = fileName;
                    _context.Add(hallCategory);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(hallCategory);
        }

        // GET: HallCategories/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            if (id == null)
            {
                return NotFound();
            }

            var hallCategory = await _context.HallCategories.FindAsync(id);
            if (hallCategory == null)
            {
                return NotFound();
            }
            return View(hallCategory);
        }

        // POST: HallCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,CategoryName,HallDescription,ImagePath,ImageFile")] HallCategory hallCategory)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            if (id != hallCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (hallCategory.ImageFile != null)
                    {

                        string wwwrootPath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + hallCategory.ImageFile.FileName;
                        string path = Path.Combine(wwwrootPath + "/Images/" + fileName);
                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            await hallCategory.ImageFile.CopyToAsync(filestream);
                        }
                        hallCategory.ImagePath = fileName;
                    }
                    _context.Update(hallCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HallCategoryExists(hallCategory.Id))
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
            return View(hallCategory);
        }

        // GET: HallCategories/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            if (id == null)
            {
                return NotFound();
            }

            var hallCategory = await _context.HallCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hallCategory == null)
            {
                return NotFound();
            }

            return View(hallCategory);
        }

        // POST: HallCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            ViewBag.UserPhoto = HttpContext.Session.GetString("UserPhoto");
            ViewBag.EmployeeName = HttpContext.Session.GetString("AdminName");
            var hallCategory = await _context.HallCategories.FindAsync(id);
            _context.HallCategories.Remove(hallCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HallCategoryExists(decimal id)
        {
            return _context.HallCategories.Any(e => e.Id == id);
        }
    }
}
