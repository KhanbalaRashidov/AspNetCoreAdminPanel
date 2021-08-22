using AspNetCoreAdminPanel.Core.DataAccess.EntityFrameworkCore;
using AspNetCoreAdminPanel.DataAccess.Abstract;
using AspNetCoreAdminPanel.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreAdminPanel.DataAccess.Concrete.EntityFrameworkCore
{
    public class EfProductImageDal : EfEntityRepositoryBase<ProductImage, AspNetCoreAdminPanelContext>, IProductImageDal
    {

    }
}
