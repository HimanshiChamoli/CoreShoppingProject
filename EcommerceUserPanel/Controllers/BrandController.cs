using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceUserPanel.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceUserPanel.Controllers
{
    public class BrandController : Controller
    {
        private readonly ShoppingDemoooo2Context _context;

        public BrandController(ShoppingDemoooo2Context context)
        {
            _context = context;
        }
        [Route("index")]
        [HttpGet]
        public IActionResult Index()
        {
            var brand = _context.Brands.ToList();
            return View(brand);
       
        }
        [Route("branddisplay/{id}")]
        [HttpGet]
        public async Task<IActionResult> BrandDisplay(int? id)
        {
            var p = _context.Products.Where(x => x.BrandId == id);
            return View(p);
        }
        
        [HttpGet]
        public async Task<IActionResult> Display(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var brand= await _context.Brands.FindAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            return Ok(brand);


        }
    }
}