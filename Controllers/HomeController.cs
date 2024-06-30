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

            var products = _productService.GetAllProducts();

            // Per ogni prodotto inserisco la source che verrà mostrata a schermo della Cover del prodotto
                foreach(var product in products)
                {
                     string uploads = Path.Combine(_env.WebRootPath, "images");
                     string image = Path.ChangeExtension(Path.Combine(uploads, product.Id.ToString()),"jpg");
                            if (System.IO.File.Exists(image))
                    
                            product.Cover = $"/images/{product.Id}.jpg";

                }

            return View(products);
        }
        public IActionResult CreateProduct()
        {
            return View(new ProductInputModel());
        }


        [HttpPost]
       
        public IActionResult CreateProduct(ProductInputModel product)
        {
            
            var prod = new Product
            {
                Name = product.Name,          
                Description = product.Description,  
                Price = product.Price         
            };

            
            _productService.Create(prod);
            //ottengo il percorso dove adranno inserite le immagini
            string uploads = Path.Combine(_env.WebRootPath, "images");
            if (product.Cover.Length > 0)
            { 
                // inserimento Prima immagine
                string filePath = Path.ChangeExtension(Path.Combine(uploads, prod.Id.ToString()), "jpg");
                using Stream fileStream = new FileStream(filePath, FileMode.Create);
                product.Cover.CopyTo(fileStream);
            }
            if (product.AdditionalImage1.Length > 0)
            {
                // inserimento Seconda immagine
                string filePath1 = Path.ChangeExtension(Path.Combine(uploads, prod.Id.ToString() + "1"), "jpg");
                using Stream fileStream = new FileStream(filePath1, FileMode.Create);
                product.AdditionalImage1.CopyTo(fileStream);
            }
            if (product.AdditionalImage2.Length > 0)
            {
                // inserimento Terza immagine
                string filePath2 = Path.ChangeExtension(Path.Combine(uploads, prod.Id.ToString() + "2"), "jpg");
                using Stream fileStream = new FileStream(filePath2, FileMode.Create);
                product.AdditionalImage2.CopyTo(fileStream);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int id) {
            // ottengo il prodotto 
            var product = _productService.GetById(id);
            string uploads = Path.Combine(_env.WebRootPath, "images");

            // Percorso della Cover del prodotto
            string image = Path.ChangeExtension(Path.Combine(uploads, product.Id.ToString()), "jpg");

            // Percorso della AdditionalImage1 del prodotto
            string imageextra1 = Path.ChangeExtension(Path.Combine(uploads, product.Id.ToString() + "1"), "jpg");

            // Percorso della AdditionalImage2 del prodotto
            string imageextra2 = Path.ChangeExtension(Path.Combine(uploads, product.Id.ToString() + "2"), "jpg");


            // imposto la proprietà Cover del prodotto
            if (System.IO.File.Exists(image))
                product.Cover = $"/images/{product.Id}.jpg";

            // imposto la proprietà AdditionalImage1 del prodotto
            if (System.IO.File.Exists(imageextra1))
                product.AdditionalImage1 = $"/images/{product.Id}1.jpg";

            // imposto la proprietà AdditionalImage2 del prodotto
            if (System.IO.File.Exists(imageextra2))
                product.AdditionalImage2 = $"/images/{product.Id}2.jpg";

           
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
