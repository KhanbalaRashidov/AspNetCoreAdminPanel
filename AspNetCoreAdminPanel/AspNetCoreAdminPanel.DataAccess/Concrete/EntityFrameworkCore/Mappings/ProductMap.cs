using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreAdminPanel.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreAdminPanel.DataAccess.Concrete.EntityFrameworkCore.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(@"Products", @"dbo");
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name).HasColumnName("Name");
            builder.Property(d => d.Description).HasColumnName("Description");
            builder.Property(d => d.AddedBy).HasColumnName("AddedBy");
            builder.Property(d => d.AddedDate).HasColumnName("AddedDate");
            builder.Property(d => d.CategoryId).HasColumnName("CategoryId");
            builder.Property(d => d.Height).HasColumnName("Height");
            builder.Property(d => d.Weight).HasColumnName("Height");
            builder.Property(d => d.Width).HasColumnName("Width");
        }
    }
}
