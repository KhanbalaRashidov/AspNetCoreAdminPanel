using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreAdminPanel.Entities.ComplexTypes
{
    public class ProductCategoryComplexData
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }

    }
}
