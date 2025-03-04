using Core.Interface;
using ImageConverter.Services;
using Microsoft.Extensions.DependencyInjection;
using Services.Services;

namespace Services
{
    public static class ServicesDependencyInjection
    {
        public static void AddServicesToContainer(this IServiceCollection services)
        {
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IConverterService, ConverterService>();
            services.AddSingleton<IMessageBoxService, MessageBoxService>();
        }
    }
}
