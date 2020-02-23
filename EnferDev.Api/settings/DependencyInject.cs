using EnferDev.Api.Context;
using EnferDev.Domain.Commands;
using EnferDev.Domain.Handlers;
using EnferDev.Domain.Repositories;
using EnferDev.Infra.Infra;
using EnferDev.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EnferDev.Api.settings
{
    public static class DependencyInject
    {
        public static IServiceCollection ResolverDependencies(this IServiceCollection services)
        {
            services.AddScoped<IDB, MSSQLDB>();
            services.AddScoped<IDbConfiguration, AppConfiguration>();
            services.AddTransient<IHospitalRepository, HospitalRepository>();
            services.AddTransient<INurseRepository, NurseRepository>();
            services.AddTransient<IAddressRepositoty, AddressRepositoty>();
            services.AddTransient<GenericCommandResult, GenericCommandResult>();
            services.AddTransient<HospitalHandler, HospitalHandler>();
            services.AddTransient<NurseHandler, NurseHandler>();

            return services;
        }
    }
}
