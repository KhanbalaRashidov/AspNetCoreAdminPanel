using AspNetCoreAdminPanel.Core.DataAccess.EntityFrameworkCore;
using AspNetCoreAdminPanel.DataAccess.Abstract;
using AspNetCoreAdminPanel.Entities.ComplexTypes;
using AspNetCoreAdminPanel.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreAdminPanel.DataAccess.Concrete.EntityFrameworkCore
{
    public class EfProductDal : EfEntityRepositoryBase<Product, AspNetCoreAdminPanelContext>, IProductdal
    {
        public List<ProductCategoryComplexData> GetProductWithCategory()
        {
            using (var _context = new AspNetCoreAdminPanelContext())
            {
                var result = from product in _context.Products
                             join category in _context.Categories on product.CategoryId equals category.Id
                             select new ProductCategoryComplexData
                             {
                                 CategoryName = category.Name,
                                 Height = product.Height,
                                 Weight = product.Weight,
                                 Width = product.Width,
                                 ProductId = product.Id,
                                 ProductName = product.Name

                             };
                return result.ToList();
            }
        }
    }
}
