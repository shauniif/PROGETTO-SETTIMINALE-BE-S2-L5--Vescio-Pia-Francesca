using Microsoft.AspNetCore.Mvc;
using PROGETTO_SETTIMINALE_BE_S2_L5__Vescio_Pia_Francesca.Entities;
using PROGETTO_SETTIMINALE_BE_S2_L5__Vescio_Pia_Francesca.Models;
using PROGETTO_SETTIMINALE_BE_S2_L5__Vescio_Pia_Francesca.Services;
using System.Diagnostics;

namespace PROGETTO_SETTIMINALE_BE_S2_L5__Vescio_Pia_Francesca.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _env;
        public HomeController(ILogger<HomeController> logger, IProductService productService, IWebHostEnvironment env)
        {   
            _logger = logger;
            _productService = productService;
            _env = env;
        }
        public IActionResult Index()
        { 
            Dictionary<int, string> productImages = new Dictionary<int, string>();

            

            var products = _productService.GetAllProducts();
           foreach(var product in products)
            {
                string uploads = Path.Combine(_env.WebRootPath, "images");
                string image = Path.ChangeExtension(Path.Combine(uploads, product.Name.ToString()),"jpg");
                if (System.IO.File.Exists(image))
                    productImages[product.Id] = $"/images/{product.Name}.jpg";
            }
            ViewBag.Cover = productImages; 
            return View(products);
        }
        public IActionResult CreateProduct()
        {
            return View(new ProductInputModel());
        }


        [HttpPost]
       
        public IActionResult CreateProduct(ProductInputModel product)
        {   
            var prod = new Product { Name = product.Name, Description = product.Description, Price = product.Price, Cover = product.Cover, AdditionalImage1 = product.AdditionalImage1, AdditionalImage2 = product.AdditionalImage2 };
            _productService.Create(prod);
            string uploads = Path.Combine(_env.WebRootPath, "images");
            if (product.Cover.Length > 0)
            {
                string filePath = Path.ChangeExtension(Path.Combine(uploads, prod.Name.ToString()), "jpg");
                using Stream fileStream = new FileStream(filePath, FileMode.Create);
                product.Cover.CopyTo(fileStream);
            }
            if (product.AdditionalImage1.Length > 0)
            {
                string filePath1 = Path.ChangeExtension(Path.Combine(uploads, prod.Name.ToString() + "1"), "jpg");
                using Stream fileStream = new FileStream(filePath1, FileMode.Create);
                product.AdditionalImage1.CopyTo(fileStream);
            }
            if (product.AdditionalImage2.Length > 0)
            {
                string filePath2 = Path.ChangeExtension(Path.Combine(uploads, prod.Name.ToString() + "2"), "jpg");
                using Stream fileStream = new FileStream(filePath2, FileMode.Create);
                product.AdditionalImage2.CopyTo(fileStream);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int id) {
            var product = _productService.GetById(id);
            string uploads = Path.Combine(_env.WebRootPath, "images");
            string image = Path.ChangeExtension(Path.Combine(uploads, product.Name.ToString()), "jpg");
            string imageextra1 = Path.ChangeExtension(Path.Combine(uploads, product.Name.ToString() + "1"), "jpg");
            string imageextra2 = Path.ChangeExtension(Path.Combine(uploads, product.Name.ToString() + "2"), "jpg");
            var imagePaths = new List<string>();

            if (System.IO.File.Exists(image))
                imagePaths.Add($"/images/{product.Name}.jpg");
            if (System.IO.File.Exists(imageextra1))
                imagePaths.Add($"/images/{product.Name}1.jpg");
            if (System.IO.File.Exists(imageextra2))
                imagePaths.Add($"/images/{product.Name}2.jpg");
            ViewBag.ImagePaths = imagePaths;
            return View(product);
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
