using AspNetCoreAdminPanel.Core.DataAccess.EntityFrameworkCore;
using AspNetCoreAdminPanel.DataAccess.Abstract;
using AspNetCoreAdminPanel.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreAdminPanel.DataAccess.Concrete.EntityFrameworkCore
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, AspNetCoreAdminPanelContext>, ICategoryDal
    {
    }
}
