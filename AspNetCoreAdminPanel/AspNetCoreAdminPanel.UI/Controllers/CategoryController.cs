using AspNetCoreAdminPanel.Business.Abstract;
using AspNetCoreAdminPanel.Entities.Concrete;
using AspNetCoreAdminPanel.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAdminPanel.UI.Controllers
{
    public class CategoryController : Controller
    {
        ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult GetCategories()
        {
            var categoryViewModel = new CategoryViewModel
            {
                Categories = _categoryService.GetList()
            };
            return View(categoryViewModel);
        }
     
        public IActionResult Add(CategoryViewModel categoryViewModel)
        {
            
            if (!ModelState.IsValid)
            {
                var categoryForAdd = new Category
                {
                    AddedBy = "Khanbala Rashidov",
                    AddedDate = DateTime.Now,
                    IsActive = true,
                    Name = categoryViewModel.Category.Name
                };

                try
                {
                    _categoryService.Add(categoryForAdd);
                    return RedirectToAction(nameof(GetCategories));
                }
                catch (Exception)
                {

                }
            }
            return RedirectToAction(nameof(GetCategories));
        }
        public JsonResult Edit(int id)
        {
            if (id == 0)
            {
                return Json(0);
            }
            var category = _categoryService.GetById(id);
            if (category == null)
            {
                return Json(0);
            }
            return Json(category);
        }
        [HttpPost]
        public IActionResult Edit(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                var categoryIsValid = _categoryService.GetById(categoryViewModel.Category.Id);
                if (categoryIsValid == null)
                {
                    return RedirectToAction(nameof(GetCategories));
                }
                try
                {
                    var categoryForAdd = new Category
                    {
                        AddedBy = categoryIsValid.AddedBy,
                        AddedDate = categoryIsValid.AddedDate,
                        Id = categoryIsValid.Id,
                        IsActive = categoryViewModel.Category.IsActive,
                        Name = categoryViewModel.Category.Name
                    };
                    _categoryService.Update(categoryForAdd);
                    return RedirectToAction(nameof(GetCategories));
                }
                catch (Exception)
                {
                    return RedirectToAction(nameof(GetCategories));
                }
            }
            return RedirectToAction(nameof(GetCategories));
        }
        public JsonResult Delete(int id)
        {
            if (id == 0)
            {
                return Json(0);
            }
            var categoryIsValid = _categoryService.GetById(id);
            if (categoryIsValid == null)
            {
                return Json(0);
            }
            try
            {
                _categoryService.Delete(categoryIsValid);
                return Json(1);
            }
            catch (Exception)
            {
                return Json(0);
            }
        }
    }
}
