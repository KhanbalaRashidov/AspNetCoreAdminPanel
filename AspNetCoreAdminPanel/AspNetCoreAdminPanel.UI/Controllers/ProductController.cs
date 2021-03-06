using AspNetCoreAdminPanel.Business.Abstract;
using AspNetCoreAdminPanel.Entities.Concrete;
using AspNetCoreAdminPanel.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreAdminPanel.UI.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        IProductService _productService;
        ICategoryService _categoryService;
        IProductImageService _productImageService;
        IHostingEnvironment _enivorement;
       
        public ProductController(IProductService productService, ICategoryService categoryService, IProductImageService productImageService, IHostingEnvironment enivorement)
        {
            _productService = productService;
            _categoryService = categoryService;
            _productImageService = productImageService;
        }
        public IActionResult GetProducts()
        {
            var productViewModel = new ProductViewModel
            {
                Products = _productService.GetProductWithCategory(),
                Categories = LoadCategories()
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
        public IActionResult GetProductDetail(int id)
        {
            if (id != 0)
            {
                var productIsValid = _productService.GetById(id);
                var productImages=_productImageService.GetListByProductId(id);
                var productViewModel = new ProductViewModel
                {
                    Product = productIsValid,
                    ProductImages = productImages,
                    Categories = LoadCategories()
                };
                return View(productViewModel);
            }
            return RedirectToAction(nameof(GetProducts));
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
                    var addedProduct = _productService.Add(productForAdd);
                    if (productViewModel.FormFiles != null)
                    {
                        foreach (var image in productViewModel.FormFiles)
                        {
                            var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                            var filePath = Path.DirectorySeparatorChar.ToString() + "ProductImages" + Path.DirectorySeparatorChar.ToString() + uniqueFileName;
                            var upLoadsFolder = Path.Combine(_enivorement.WebRootPath, "ProductImages");
                            var filePathForCopy = Path.Combine(upLoadsFolder, uniqueFileName);
                            image.CopyTo(new FileStream(filePathForCopy, FileMode.Create));

                            var productImageForAdd = new ProductImage
                            {
                                AddedBy = "Khanbala Rashidov",
                                AddedDate = DateTime.Now,
                                ProductId = addedProduct.Id,
                                FileName = uniqueFileName,
                                FilePath = filePath
                            };
                            _productImageService.Add(productImageForAdd);
                            return RedirectToAction(nameof(GetProducts));
                        }
                    }
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
