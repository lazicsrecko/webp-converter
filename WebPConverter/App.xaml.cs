using ImageConverter.Bootstrapper;
using Microsoft.Extensions.DependencyInjection;
using Services;
using System.Windows;

namespace ImageConverter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _servicePorvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddServicesToContainer();
            services.AddViewModels();
            _servicePorvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _servicePorvider.GetRequiredService<MainWindow>().Show();
            base.OnStartup(e);
        }
    }

}
