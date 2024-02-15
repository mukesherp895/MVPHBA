using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MVPHBA.Model.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPHBA.DataAccess.DBModelConfigurations
{
    public class PropertyInfosConfig : IEntityTypeConfiguration<PropertyInfos>
    {
        public void Configure(EntityTypeBuilder<PropertyInfos> builder)
        {
            builder.Property(p => p.PropertyType).IsRequired().HasMaxLength(50).HasColumnType("varchar");
            builder.Property(p => p.Description).IsRequired().HasMaxLength(500).HasColumnType("varchar");
            builder.Property(p => p.Location).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            builder.Property(p => p.ImagePath).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            builder.Property(p => p.Feature).IsRequired().HasMaxLength(500).HasColumnType("varchar");
            builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.UserId).IsRequired().HasMaxLength(450).HasColumnType("nvarchar");
            builder.HasOne(h => h.User).WithMany().HasForeignKey(fk => fk.UserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
