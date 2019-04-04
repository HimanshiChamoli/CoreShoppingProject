using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceUserPanel.Helpers;
using EcommerceUserPanel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceUserPanel.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {  
        ShoppingDemoooo2Context context = new ShoppingDemoooo2Context();
       
        [Route("index")]
        public IActionResult Index()
        {

            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int i = 0;
            if (cart != null)
            {

                foreach (var item in cart)
                {
                    i++;
                }
                if (i != 0)
                {
                    ViewBag.cart = cart;
                    ViewBag.total = cart.Sum(item => item.products.ProductPrice * item.Quantity);
                    return View();
                }

            }
            return View("GoBack");
        }

        [Route("Details/{id}")]
        public IActionResult Details(int id)
        {
            
            var detail= context.Products.Find(id);
            var cid = context.Products.Find(id);
            ViewBag.cname = context.Categories.Find(cid.ProductCategoryId);
            return View(detail);
        }
        [HttpGet]
        public IActionResult Checkout()
        {
            int i = 0;
            ViewBag.i = i;
            var checkout = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.checkout = checkout;
            ViewBag.total = checkout.Sum(item => item.products.ProductPrice * item.Quantity);
            string cust = HttpContext.Session.GetString("uname");
            Customers cus = context.Customers.Where(x => x.Username == cust).SingleOrDefault();
            ViewBag.cus = cus;
            ViewBag.totalitem = checkout.Count();
            TempData["total"] = ViewBag.total;

            return View();
        }
        [HttpPost]
        public IActionResult Checkout(Customers customer)
        {
            //int custId = int.Parse(TempData["cust"].ToString());
            //Customers customers = context.Customers.Where(x => x.CustomerId == custId).SingleOrDefault();
            //ViewBag.Customers = customers;
            //if (ModelState.IsValid)
            //{
            var c = context.Customers.Where(x => x.Username == customer.Username).SingleOrDefault();
            c.FirstName = customer.FirstName;
            c.LastName= customer.LastName;
            c.Username = customer.Username;
            c.EmailId = customer.EmailId;
            c.Address = customer.Address;
            c.PhoneNo = customer.PhoneNo;
            c.Country = customer.Country;
            c.State = customer.State;
            c.Zip = customer.Zip;
            context.SaveChanges();
            var price = TempData["total"];
            Orders order = new Orders()
                {
                    OrderPrice = Convert.ToSingle(price),
                    OrderDate = DateTime.Now,
                    CustomerId = c.CustomerId
                };
                context.Orders.Add(order);
                context.SaveChanges();
                var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                List<OrderProducts> OrderProducts = new List<OrderProducts>();
                for (int i = 0; i < cart.Count; i++)
                {
                    OrderProducts orderProducts = new OrderProducts()
                    {
                        OrderId = order.OrderId,
                        ProductId = cart[i].products.ProductId,
                        Quantity = cart[i].Quantity
                    };
                    OrderProducts.Add(orderProducts);

                }
                OrderProducts.ForEach(n => context.OrderProducts.Add(n));
                context.SaveChanges();
                TempData["cust"] = c.CustomerId;
                return RedirectToAction("Invoice");
            }
     
    
        [Route("Buy/{id}")]
        public IActionResult Buy(int id)
        {
           if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { products = context.Products.Find(id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item
                    {
                        products = context.Products.Find(id),
                        Quantity = 1
                    });
                }
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                }
            
                return RedirectToAction("Index","Home");
            }
        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            int j = int.Parse(HttpContext.Session.GetString("cartitem"));
            int i = 0;
            foreach (var item in cart)
            {
                i++;
            }
            if (i != 0)
            {
                j--;
                HttpContext.Session.SetString("cartitem", j.ToString());
            }
            else
            {
                HttpContext.Session.Remove("cartitem");
                return View("GoBack");
            }


            //SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            return RedirectToAction("Index");

        }
        [Route("GoBack")]
        public IActionResult GoBack()
        {
            return View();
        }
        private int isExist(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].products.ProductId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
        [Route("Invoice")]
        public IActionResult Invoice()
        {
            int custId = int.Parse(TempData["cust"].ToString());
            Customers customers = context.Customers.Where(x => x.CustomerId == custId).SingleOrDefault();
            ViewBag.Cust = customers;


            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            foreach(var Item1 in cart)
            {
                Products p = context.Products.Find(Item1.products.ProductId);
                p.ProductQty = p.ProductQty - Item1.Quantity;
                context.SaveChanges();
            }
            ViewBag.total = cart.Sum(item => item.products.ProductPrice * item.Quantity);
            cart = null;
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            HttpContext.Session.Remove("cartitem");
            return View();

        }
        [Route("Plus")]
        [HttpGet]
        public IActionResult Plus(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            if (index != -1)
            {
                cart[index].Quantity++;
            }
            else
            {
                cart.Add(new Item
                {
                    products = context.Products.Find(id),
                    Quantity = 1
                });

            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }
        [Route("Minus")]
        [HttpGet]
        public IActionResult Minus(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            if (index != -1)
            {
                if (cart[index].Quantity != 1)
                {
                    cart[index].Quantity--;
                }

                else
                    return RedirectToAction("Remove", "Cart", new { @id = id });
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }
    }
}   
       
  