using AspNetCoreAdminPanel.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreAdminPanel.DataAccess.Concrete.EntityFrameworkCore.Mappings
{
    public class ProductImageMap : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable(@"ProductImages","dbo");
            builder.HasKey(d => d.Id);

            builder.Property(d => d.ProductId).HasColumnName("ProductId");
            builder.Property(d => d.FileName).HasColumnName("FileName");
            builder.Property(d => d.FilePath).HasColumnName("FilePath");
            builder.Property(d => d.AddedBy).HasColumnName("AddedBy");
            builder.Property(d => d.AddedDate).HasColumnName("AddedDate");
        }
    }
}
