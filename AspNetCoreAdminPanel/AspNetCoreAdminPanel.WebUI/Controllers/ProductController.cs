using AspNetCoreAdminPanel.Business.Abstract;
using AspNetCoreAdminPanel.Entities.Concrete;
using AspNetCoreAdminPanel.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAdminPanel.WebUI.Controllers
{
    public class ProductController : Controller
    {
        IProductService _productService;
        ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public IActionResult GetProducts()
        {
            var productViewModel = new ProductViewModel
            {
                Products = _productService.GetProductWithCategory(),
                Categories=LoadCategories()
            };
            return View(productViewModel);
        }
        private List<SelectListItem> LoadCategories()
        {
            List<SelectListItem> categories = (from category in _categoryService.GetList()
                                               select new SelectListItem
                                               {
                                                   Value = category.Id.ToString(),
                                                   Text = category.Name
                                               }).ToList();
            return categories;
        }
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
                    CategoryId = productViewModel.Product.CategoryId,
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

        public JsonResult Edit(int id)
        {
            if (id > 0)
            {
                var result = _productService.GetById(id);
                return Json(result);
            }
            return Json(0);
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                var productIsValid = _productService.GetById(productViewModel.Product.Id);
                if (productIsValid == null)
                {
                    return RedirectToAction(nameof(GetProducts));
                }
                var productForEdit = new Product
                {
                    Id = productIsValid.Id,
                    Description = productViewModel.Product.Description,
                    Height = productViewModel.Product.Height,
                    Weight = productViewModel.Product.Weight,
                    Width = productViewModel.Product.Width,
                    AddedBy = productIsValid.AddedBy,
                    AddedDate = productIsValid.AddedDate,
                    Name = productViewModel.Product.Name,
                    CategoryId = productViewModel.Product.CategoryId,
                };
                try
                {
                    _productService.Update(productForEdit);
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

        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                var productIsValid = _productService.GetById(id);
                if (productIsValid == null)
                {
                    return RedirectToAction(nameof(GetProducts));
                }
                _productService.Delete(productIsValid);
                return RedirectToAction(nameof(GetProducts));

            }
            return RedirectToAction(nameof(GetProducts));
        }
    }
}
