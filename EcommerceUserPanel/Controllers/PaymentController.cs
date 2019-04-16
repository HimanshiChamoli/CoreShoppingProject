using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceUserPanel.Helpers;
using EcommerceUserPanel.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace EcommerceUserPanel.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ShoppingDemoooo2Context _context;

        public PaymentController(ShoppingDemoooo2Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var checkout = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.checkout = checkout;
            ViewBag.total = checkout.Sum(item => item.products.ProductPrice * item.Quantity);
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
        public IActionResult Charge(string stripeEmail, string stripeToken)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();
            var checkout = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.checkout = checkout;
            ViewBag.total = checkout.Sum(item => item.products.ProductPrice * item.Quantity);
            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                SourceToken = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            { 
                Amount = 500,
                Description = "Sample Charge",
                Currency = "usd",
                CustomerId = customer.Id
            });
            Payments payment = new Payments();
            {
                payment.PaymentStripeId = charge.PaymentMethodId;
                payment.Amount = ViewBag.total;
                payment.Date = System.DateTime.Now;
                payment.CardNo = Convert.ToInt32(charge.PaymentMethodDetails.Card.Last4);
                payment.OrderId = 5;
                payment.CustomerId = 1;
            }
        
            _context.Add<Payments>(payment);
            _context.Payments.Add(payment);
            _context.SaveChanges();

          
            //var customerservice = new CustomerService();
            //ViewBag.details = charge.PaymentMethodDetails.Card.Last4;

            return RedirectToAction("Invoice", "Cart");
        }
    }
}