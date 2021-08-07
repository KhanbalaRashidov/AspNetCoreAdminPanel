using AspNetCoreAdminPanel.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAdminPanel.WebUI.Models
{
    public class ProductViewModel
    {
        public Product Product{ get; set; }
        public List<Product> Products { get; set; }
    }
}
