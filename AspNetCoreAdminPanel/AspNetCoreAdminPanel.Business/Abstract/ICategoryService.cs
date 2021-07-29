using AspNetCoreAdminPanel.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreAdminPanel.Business.Abstract
{
    public interface ICategoryService
    {
        Category Add(Category category);
        Task<Category> AddAsync(Category category);

        Category Update(Category category);
        Task<Category> UpdateAsync(Category category);
        void Delete(Category category);
        List<Category> GetList();
        Category GetById(int id);
    }
}
