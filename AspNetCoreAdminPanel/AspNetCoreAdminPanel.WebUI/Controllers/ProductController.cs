using AspNetCoreAdminPanel.Business.Abstract;
using AspNetCoreAdminPanel.Entities.Concrete;
using AspNetCoreAdminPanel.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAdminPanel.WebUI.Controllers
{
    public class ProductController : Controller
    {
        IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult GetProducts()
        {
            var productViewModel = new ProductViewModel
            {
                Products = _productService.GetList()
            };
            return View(productViewModel);
        }
        [HttpPost]
       public IActionResult Add(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                var productIsValid = _productService.GetByName(productViewModel.Product.Name);
                if (productIsValid != null)
                {
                    return RedirectToAction(nameof(GetProducts));
                }
                var productForAdd = new Product
                {
                    Name = productViewModel.Product.Name,
                    CategoryId=productViewModel.Product.CategoryId,
                    AddedBy = "Khanbala Rashidov",
                    AddedDate = DateTime.Now,
                    Description = productViewModel.Product.Description,
                    Height = productViewModel.Product.Height,
                    Weight = productViewModel.Product.Weight,
                    Width = productViewModel.Product.Width,
                };
                try
                {
                    _productService.Add(productForAdd);
                    return RedirectToAction(nameof(GetProducts));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return RedirectToAction(nameof(GetProducts));
                }
            }
            return RedirectToAction(nameof(GetProducts));
        }
    }
}
