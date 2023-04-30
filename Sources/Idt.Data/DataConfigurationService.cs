using Idt.Data.Context;
using Idt.Data.Repertoires;
using Idt.Features.Interfaces.Repertoires;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Data
{
    public static class DataConfigurationService
    {
        public static IServiceCollection AddDataServiceConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
           
            services.AddDbContext<IdtDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("connexionString"));
            });

            services.AddScoped<IPointDaccess, PointDaccess>();
            services.AddScoped<IRepertoireDutilisateur, RepertoireDutilisateur>();
            services.AddScoped<IRepertoireDadresse, RepertoireDadresse>();
            services.AddScoped<IRepertoireDemessage, RepertoireDemessage>();
            services.AddScoped(typeof(IRepertoireGenerique<>), typeof(RepertoireGenerique<>));

            return services;
        }
    }
}
