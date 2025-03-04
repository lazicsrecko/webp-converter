using Core.Interface;
using Core.Interface.ViewModel;
using Core.ViewModel;
using ImageConverter.Services;
using ImageConverter.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace ImageConverter.Bootstrapper
{
    public static class ViewModelDependencyInjection
    {
        public static void AddViewModels(this IServiceCollection services)
        {
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<SingleImageConvertViewModel>();
            services.AddSingleton<MultipleImageConvertViewModel>();
            services.AddSingleton<AboutWindowViewModel>();
            services.AddSingleton<IConverterOptions, ConverterOptionsViewModel>();
            services.AddSingleton<Func<Type, ViewModelBase>>(serviceProvider => viewModelType => (ViewModelBase)serviceProvider.GetRequiredService(viewModelType));
        }
    }
}
