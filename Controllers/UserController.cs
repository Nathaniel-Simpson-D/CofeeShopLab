using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShopLab2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoffeeShopLab2.Controllers
{
    public class UserController : Controller
    {
        private readonly CoffeeShopDbContext _context;
        public UserController(CoffeeShopDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("ActiveUser") != null)
            {
                return Redirect("/Home/Index");
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(string email, string password)
        {
            List<RegisterUser> userList = _context.RegisterUser.ToList();
            foreach (RegisterUser account in userList)
            {
                if (account.Email == email && account.Password == password)
                {
                    HttpContext.Session.SetString("ActiveUser", JsonConvert.SerializeObject(account));
                    return Redirect("/Home/Index");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult AddUser()
        {
            return View(new RegisterUser());
        }
        [HttpPost]
        public IActionResult AddUser(RegisterUser user)
        {
            if (ModelState.IsValid && user != null)
            {
                _context.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(user);
            }
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("ActiveUser");

            return Redirect("/Home/Index");
        }
        public IActionResult Profile()
        {
            RegisterUser account = JsonConvert.DeserializeObject<RegisterUser>(HttpContext.Session.GetString("ActiveUser"));
            ViewBag.Name = account.UserName;
            ViewBag.Age = account.Age;
            ViewBag.Email = account.Email;
            ViewBag.Password = account.Password;
            ViewBag.Funds = account.Funds;
            return View();
        }
    }
}