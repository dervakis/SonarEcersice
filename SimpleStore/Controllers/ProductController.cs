using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SimpleStore.Model;
using SimpleStore.Service;
using System.Text.Json;

namespace SimpleStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
        public async override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.ProductCount = await productService.GetProductCount();
            base.OnActionExecuting(context);
        }
        public async Task<IActionResult> Grid()
        {
            List<Product> products = await productService.GetProducts();
            return View(products);
        }

        public async Task<IActionResult> Index()
        {
            List<Product> products = await productService.GetProducts();
            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("Form");
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product) 
        {
            if (string.IsNullOrWhiteSpace(product.ProductName))
                ModelState.AddModelError("ProductName", "not proper naem");
            if (product.Quantity <= 0)
                ModelState.AddModelError("Quantity", "negative quantity not allowed");
            if (product.Category == null)
                ModelState.AddModelError("Category", "select category");

            if (!ModelState.IsValid)
            {
                return View("Form", product);
            }
            Console.WriteLine(JsonSerializer.Serialize(product));
            await productService.CreateProduct(product);
            return RedirectToAction("Create");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {    
                await productService.DeleteProduct(id);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            if (ModelState.IsValid)
            {   
                var product = await productService.GetProductById(id);
                return View(product);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(Product product) 
        {
            if (string.IsNullOrWhiteSpace(product.ProductName))
                ModelState.AddModelError("ProductName", "not proper naem");
            if (product.Quantity <= 0)
                ModelState.AddModelError("Quantity", "negative quantity not allowed");
            if (product.Category == null)
                ModelState.AddModelError("Category", "select category");

            if (!ModelState.IsValid)
            {
                return View(product);
            }
            await productService.UpdateProduct(product);
            return RedirectToAction("Index");
        }
    }
}
