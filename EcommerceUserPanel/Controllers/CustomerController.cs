using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceUserPanel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceUserPanel.Controllers
{

    public class CustomerController : Controller
    {
        ShoppingDemoooo2Context context = new ShoppingDemoooo2Context();
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customers customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
            return RedirectToAction("Index");

        }


        public IActionResult Login()
        {
            return View();
        }
        //[Route("Login")]
        [HttpPost]

        public ActionResult Login(string Password, string Username)
        {
            var user = context.Customers.Where(x => x.Username == Username).SingleOrDefault();
            ViewBag.cust = user;
            if (user == null)
            {
                ViewBag.Error = "Invalid Credentials";
                return View("Login");
            }
            else
            {
                var userName = user.Username;
                int custId = ViewBag.cust.CustomerId;
                if (userName != null && Password != null && userName.Equals(userName)
                    && Password.Equals("12345"))
                {
                    HttpContext.Session.SetString("uname", userName);
                    return RedirectToAction("Checkout", "Cart", new { @id = custId });
                }
                else
                {
                    ViewBag.Error = "Invalid Credentials";
                    return View("Login");
                }
            }
        }
    }
}