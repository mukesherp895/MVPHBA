using Microsoft.AspNetCore.Identity;
using MVPHBA.DataAccess.Infrastructures;
using MVPHBA.DataAccess.Interfaces;
using MVPHBA.DataAccess.Repositories;
using MVPHBA.Model.DBModels;
using MVPHBA.Service;
using MVPHBA.Service.Interfaces;

namespace MVPHBA.API.Extensions
{
    public static class DIExtension
    {
        public static IServiceCollection RegisterAllDependencies(this IServiceCollection services)
        {
            #region DBFactory
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IDBFactory), typeof(DBFactory));
            #endregion

            #region Service
            services.AddScoped(typeof(IService<>), typeof(Service<>));
            services.AddScoped(typeof(IPropertyInfoService), typeof(PropertyInfoService));
            #endregion

            #region Repository
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IRepository<PropertyInfos>), typeof(Repository<PropertyInfos>));
            services.AddTransient(typeof(ISearchRepository), typeof(SearchRepository));
            #endregion

            return services;
        }
    }
}
