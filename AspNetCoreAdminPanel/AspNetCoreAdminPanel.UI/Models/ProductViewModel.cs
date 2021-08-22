using AspNetCoreAdminPanel.Entities.ComplexTypes;
using AspNetCoreAdminPanel.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAdminPanel.UI.Models
{
    public class ProductViewModel
    {
        public Product Product{ get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<ProductCategoryComplexData> Products { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public List<IFormFile> FormFiles { get; set; }
    }
}
