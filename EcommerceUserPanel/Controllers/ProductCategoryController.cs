

using System.Linq;
using System.Threading.Tasks;
using EcommerceUserPanel.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceUserPanel.Controllers
{
    public class ProductCategoryController : Controller
    {
         
        ShoppingDemoooo2Context context = new ShoppingDemoooo2Context();
        public IActionResult Index()
        {

            var productcat = context.Categories.ToList();
            return View(productcat);
        }
        public IActionResult Display(int id)
        {
            var product = context.Products.Where(x => x.ProductCategoryId == id).ToList();
            return View(product);
        }
    }
}
