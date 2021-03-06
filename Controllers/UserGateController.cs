using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using MovieTheater.Models;

namespace MovieTheater.Controllers
{
    public sealed class UserGateController : Controller
    {
        private readonly WebContext _context;

        public UserGateController(WebContext context) => _context = context;

        public IActionResult
        ALogin()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("user_group")) && 
                HttpContext.Session.GetString("user_group").Equals("admin"))
                return RedirectToAction("Index", "Backend");
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
        ALogin(string uid, string pwd)
        {
            
            var user = await _context.Admins
                                     .FirstOrDefaultAsync(a => a.AdminName.Equals(uid) &&
                                                               a.AdminPwd.Equals(pwd));
            
            // Log in fault
            if (user == null) return View();

            HttpContext.Session.SetString("user_group", "admin");
            HttpContext.Session.SetInt32("user_id", user.AdminId);
            return RedirectToAction("Index", "Backend");
        }

        public IActionResult
        CusLogin()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("user_group")) &&
                !HttpContext.Session.GetString("user_group").Equals("admin"))
            {
                return RedirectToAction("Index", "Home");
            }
                
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>
        CusLogin(string uid, string pwd)
        {
            var user = await _context.Customers
                                     .FirstOrDefaultAsync(c => c.CustomerName.Equals(uid) &&
                                                               c.CustomerPwd.Equals(pwd));
            
            // Log in fault
            if (user == null) return View();

            HttpContext.Session.SetString("user_group", "customer");
            HttpContext.Session.SetInt32("user_id", user.CustomerId);
            
            return RedirectToAction("Index", "Home");
        }
    
        public IActionResult
        CusSignOn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
        CusSignOn(int id,
                  [Bind("CustomerName,CustomerPwd")]
                  Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(customer);
                    await _context.SaveChangesAsync();
                    
                    return RedirectToAction("Index", "Home");
                }
            } catch (Exception)
            {
                Console.WriteLine("User Create Error.");
                throw;
            }
            return View(customer);
        }
    
        [HttpGet]
        public IActionResult
        SignOut()
        {
            HttpContext.Session.Remove("user_group");
            HttpContext.Session.Remove("user_id");
            return RedirectToAction("Index", "Home");
        }
    }
}