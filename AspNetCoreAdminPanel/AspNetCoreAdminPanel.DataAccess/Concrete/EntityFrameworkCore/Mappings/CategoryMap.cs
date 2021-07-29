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
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(@"Categories", @"dbo");
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name).HasColumnName("Name");
            builder.Property(d => d.IsActive).HasColumnName("IsActive");
            builder.Property(d => d.AddedBy).HasColumnName("AddedBy");
            builder.Property(d => d.AddedDate).HasColumnName("AddedDate");
        }
    }
}
