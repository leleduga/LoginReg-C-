using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using LoginReg.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace LoginReg.Controllers
{
    public class HomeController : Controller
    {
        private Context _context;
        public HomeController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("Register");
        }

        [HttpGet]
        [Route("login")]    
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RestViewModel model)
        {
            if (ModelState.IsValid)
            {
                PasswordHasher<RestViewModel> hasher = new PasswordHasher<RestViewModel>();
                User ExistingUser = _context.Users.SingleOrDefault(user => user.Email == model.Email);

                if (ExistingUser != null)
                {
                    ViewBag.Message = "User with this email already exists!";
                    return View("Register", model);
                }
                User NewUser = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = hasher.HashPassword(model, model.Password),
                    // Created_At = DateTime.UtcNow,
                    // Updated_At = DateTime.UtcNow
                };
                _context.Add(NewUser);
                _context.SaveChanges();

                User LoggedInUser = _context.Users.SingleOrDefault(user => user.Email == NewUser.Email);
                HttpContext.Session.SetString("UserName", (LoggedInUser.FirstName + " " + LoggedInUser.LastName));

                return RedirectToAction("Success");
                
            }
            return View("Register", model);
            
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string EmailToCheck, string PasswordToCheck)
        {
            PasswordHasher<object> hasher = new PasswordHasher<object>();
            User RetrievedUser = _context.Users.SingleOrDefault(user => user.Email == EmailToCheck);
            if (RetrievedUser == null)
            {
                ViewBag.Message = "Login unsuccessful";
                return View("Login");
            }
            else if(hasher.VerifyHashedPassword(RetrievedUser, RetrievedUser.Password, PasswordToCheck) == 0)
            {
                ViewBag.Message = "Login unsuccessful";
                return View("Login");
            }
            else
            {
                HttpContext.Session.SetString("UserName", (RetrievedUser.FirstName + " " + RetrievedUser.LastName));
                HttpContext.Session.SetInt32("UserID", RetrievedUser.UserID);
                return RedirectToAction("Success");
            }
        }

        [HttpGet]
        [Route("Success")]
        public IActionResult Success()
        {
            ViewBag.activeUser = HttpContext.Session.GetString("UserName");
            return View("Success");
        }
    }
}
