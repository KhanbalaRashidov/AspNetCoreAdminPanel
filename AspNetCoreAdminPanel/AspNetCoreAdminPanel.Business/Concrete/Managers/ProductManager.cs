using AspNetCoreAdminPanel.Business.Abstract;
using AspNetCoreAdminPanel.DataAccess.Abstract;
using AspNetCoreAdminPanel.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreAdminPanel.Business.Concrete.Managers
{
    public class ProductManager : IProductService
    {
        private IProductdal _productDal;
        public ProductManager(IProductdal productDal)
        {
            _productDal = productDal;
        }
        public Product Add(Product product)
        {
            return _productDal.Add(product);
        }

        public async Task<Product> AddAsync(Product product)
        {
            return await _productDal.AddAsync(product);
        }

        public void Delete(Product product)
        {
            _productDal.Delete(product);
        }

        public Product GetById(int id)
        {
            return _productDal.Get(d => d.Id == id);
        }

        public Product GetByName(string name)
        {
            return _productDal.Get(d=> d.Name == name);
        }

        public List<Product> GetList()
        {
            return _productDal.GetAll();
        }

        public List<Product> GetListByCategoryId(int categoryId)
        {
            return _productDal.GetAll(d => d.CategoryId == categoryId);
        }

        public Product Update(Product product)
        {
            return _productDal.Update(product);
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            return await _productDal.UpdateAsync(product);
        }
    }
}
