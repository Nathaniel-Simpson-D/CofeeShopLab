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
    public class ShopController : Controller
    {
        public List<Items> AvalableItems;
        public CoffeeShopDbContext _context;
        public List<Items> ShopCart { get; set; }

        public ShopController(CoffeeShopDbContext context)
        {
            this._context = context;
            this.AvalableItems = _context.Items.ToList();
        }
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("ActiveUser") != null)
            {
                return View(AvalableItems);
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }
        
        public IActionResult InsufficentFunds()
        {
           ViewBag.Funds = Request.Cookies["Funds"].ToString();
            ViewBag.Price = Request.Cookies["Price"].ToString();
            return View();
        }
        public IActionResult AddItem(int Id)
        {
            RegisterUser account = JsonConvert.DeserializeObject<RegisterUser>(HttpContext.Session.GetString("ActiveUser"));
            var item = AvalableItems[Id].Price;
            if (account.Funds - item >=0)
            {
                foreach (var user in _context.RegisterUser.ToList())
                {
                    if(user.Id == account.Id)
                    {
                        user.Funds -= item;
                        HttpContext.Session.SetString("ActiveUser", JsonConvert.SerializeObject(user));
                        _context.RegisterUser.Update(user);
                        _context.SaveChanges();
                      return RedirectToAction("Index");

                    }
                    
                } 
            }

            Response.Cookies.Append("Funds", account.Funds.ToString());
            Response.Cookies.Append("Price", item.ToString());
            
                return RedirectToAction("InsufficentFunds");
            
        }
    }
}