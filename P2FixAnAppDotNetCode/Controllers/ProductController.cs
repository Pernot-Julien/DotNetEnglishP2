using Microsoft.AspNetCore.Mvc;
using P2FixAnAppDotNetCode.Models;
using P2FixAnAppDotNetCode.Models.Services;
using System.Collections.Generic;

namespace P2FixAnAppDotNetCode.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILanguageService _languageService;

        public ProductController(IProductService productService, ILanguageService languageService)
        {
            _productService = productService;
            _languageService = languageService;
        }

        public IActionResult Index()
        {
            List<Product> products = _productService.GetAllProducts();
            return View(products);
        }

        //Ajout de la méthode pour afficher la vue d'un produit en particulier 
        public IActionResult ProductDetail( int id)
        {
            Product product = _productService.GetProductById(id);

           if (product == null)
            {
                return NotFound();
            }
           else
            {
                return View(product);
            }
        }
    
    }
}