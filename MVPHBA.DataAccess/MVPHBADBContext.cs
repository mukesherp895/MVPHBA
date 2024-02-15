using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVPHBA.DataAccess.DBModelConfigurations;
using MVPHBA.Model.DBModels;

namespace MVPHBA.DataAccess
{
    public class MVPHBADBContext : IdentityDbContext<Users>
    {
        public MVPHBADBContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
        {
            if (!OptionsBuilder.IsConfigured)
            {
                OptionsBuilder.UseSqlServer(@"Data Source=DESKTOP-L1KJUI3\\MSSQLSERVER01;Initial Catalog=MVPHBA;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=true;");
            }
            //OptionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UsersConfig());
            builder.ApplyConfiguration(new PropertyInfosConfig());
        }
        public DbSet<PropertyInfos> PropertyInfos { get; set; }
    }
}
