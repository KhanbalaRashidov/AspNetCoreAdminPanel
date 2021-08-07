using AspNetCoreAdminPanel.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreAdminPanel.Business.Abstract
{
    public interface IProductService
    {
        Product Add(Product product);
        Task<Product> AddAsync(Product product);
        Product Update(Product product);
        Task<Product> UpdateAsync(Product product);
        void Delete(Product product);
        List<Product> GetList();
        Product GetByName(string name);
        Product GetById(int id);
        List<Product> GetListByCategoryId(int categoryId);
    }
}
