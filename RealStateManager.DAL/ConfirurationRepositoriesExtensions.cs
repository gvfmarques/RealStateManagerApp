using Microsoft.Extensions.DependencyInjection;
using RealStateManager.DAL.Interface;
using RealStateManager.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealStateManager.DAL
{
    public static class ConfirurationRepositoriesExtensions
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IFunctionRepository, FunctionRepository>();
            services.AddTransient<IVehicleRepository, VehicleRepository>();
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<IServiceRepository, ServiceRepository>();
            services.AddTransient<IServiceBuildingRepository, ServiceBuildingRepository>();
            services.AddTransient<IHistoricResourcesRepository, HistoricResourcesRepository>();
        }
    }
}
