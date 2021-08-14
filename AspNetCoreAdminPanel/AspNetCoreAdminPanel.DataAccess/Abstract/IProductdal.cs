using AspNetCoreAdminPanel.Core.DataAccess;
using AspNetCoreAdminPanel.Entities.ComplexTypes;
using AspNetCoreAdminPanel.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreAdminPanel.DataAccess.Abstract
{
    public interface IProductdal:IEntityRepository<Product>
    {
        List<ProductCategoryComplexData> GetProductWithCategory();
    }
}
