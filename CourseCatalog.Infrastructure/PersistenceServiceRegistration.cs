using CourseCatalog.Domain.Contracts;
using CourseCatalog.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCatalog.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CourseCatalogContext>(options => options.UseSqlServer(configuration.GetConnectionString("Server=DESKTOP-GFOAKVJ;Database=CourseCatalog;Integrated Security = true;TrustServerCertificate=True")));

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            return services;
        }
    }
}
