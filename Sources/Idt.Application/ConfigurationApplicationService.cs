using Idt.Application.Services;
using Idt.Features.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Application
{
    public static class ConfigurationApplicationService
    {
        public static IServiceCollection AddConfigurationApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IServiceDadresse, ServiceDadresse>();
            services.AddScoped<IServiceDutilisateur, ServiceDutilisateur>();
            services.AddScoped<IServiceDemessage, ServiceDemessage>();
            return services;
        }
    }
}
