using AspNetCoreAdminPanel.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAdminPanel.WebUI.Models
{
    public class CategoryViewModel
    {
        public Category Category { get; set; }
        public List<Category> Categories { get; set; }
    }
}
