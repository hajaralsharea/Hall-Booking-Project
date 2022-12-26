using Hall_Booking.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hall_Booking.Controllers
{
    public class LoginAndRegisterController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnviroment;

        public LoginAndRegisterController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            this.webHostEnviroment = webHostEnviroment;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,Fname,Lname,Email,UserName,Phonenumber,ImagePath,ImageFile")] User user, string password)
        {

            try
            {
               
                if (ModelState.IsValid)
                {
                    string wwwrootPath = webHostEnviroment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + user.ImageFile.FileName;
                    string extension = Path.GetExtension(user.ImageFile.FileName);
                    user.ImagePath = fileName;
                    string path = Path.Combine(wwwrootPath + "/Images/" + fileName);
                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await user.ImageFile.CopyToAsync(filestream);
                    }
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    var LastId = _context.Users.OrderByDescending(p => p.Id).FirstOrDefault().Id;

                    UsersLogin login1 = new UsersLogin();
                    login1.RoleId = 2;
                    login1.UserName = user.UserName;
                    login1.Passwordd = password;
                    login1.UserId = LastId;
                    _context.Add(login1);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Loginss", "LoginAndRegister");
                }

            }
            catch (Exception e)
            {
                TempData["alertMessage"] = "Please Try Again!";
                return RedirectToAction("Register", "LoginAndRegister");

            }
            return View(user);

        }

        public IActionResult Loginss()
        {
            return View();
        }
        public IActionResult Register()
        {

            return View();
        }
        
        [HttpPost]
       
        public IActionResult Loginss([Bind("UserName,Passwordd")] UsersLogin userslogin)
        {
            try { 
            var auth = _context.UsersLogins.Where(x => x.UserName == userslogin.UserName && x.Passwordd == userslogin.Passwordd).SingleOrDefault();
            var userphoto=_context.Users.Where(x => x.Id == auth.UserId).SingleOrDefault();

            if (auth != null)
            {
                switch (auth.RoleId)
                {
                    case 1:
                        HttpContext.Session.SetInt32("UserId", (int)auth.UserId);
                        HttpContext.Session.SetString("AdminName", auth.UserName);
                        HttpContext.Session.SetString("UserPhoto", userphoto.ImagePath);
                        return RedirectToAction("Admin", "DashBoards");

                    case 2:
                        HttpContext.Session.SetInt32("UserId", (int)auth.UserId);
                        HttpContext.Session.SetString("AdminName", auth.UserName);
                        HttpContext.Session.SetString("UserPhoto", userphoto.ImagePath);
                        return RedirectToAction("UserView", "DashBoards");


                }
            }
            }
            catch (Exception e)
            {
                TempData["alertMessage"] = "Please Try Again!";
                return RedirectToAction("Loginss", "LoginAndRegister");

            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Loginss", "LoginAndRegister");
        }
    }
}
