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
    public class UsersConfig : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.Property(p => p.UserType).IsRequired().HasMaxLength(50).HasColumnType("varchar");
            builder.Property(p => p.FullName).IsRequired().HasMaxLength(255).HasColumnType("varchar");
        }
    }
}
