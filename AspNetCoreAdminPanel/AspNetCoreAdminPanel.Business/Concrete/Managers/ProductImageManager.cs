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
    public class ProductImageManager : IProductImageService
    {
        IProductImageDal _productImageDal;
        public ProductImageManager(IProductImageDal productImageDal)
        {
            _productImageDal = productImageDal;
        }
        public ProductImage Add(ProductImage productImage)
        {
            return _productImageDal.Add(productImage);
        }

        public void Delete(ProductImage productImage)
        {
            _productImageDal.Delete(productImage);
        }

        public ProductImage GetById(int id)
        {
            return _productImageDal.Get(d => d.Id == id);
        }

        public List<ProductImage> GetList()
        {
            return _productImageDal.GetAll();
        }

        public List<ProductImage> GetListByProductId(int productId)
        {
            return _productImageDal.GetAll(d => d.ProductId == productId);
        }

        public ProductImage Update(ProductImage productImage)
        {
            return _productImageDal.Update(productImage);
        }
    }
}
