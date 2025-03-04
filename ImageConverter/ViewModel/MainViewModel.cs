using Core.Command;
using Core.Interface;
using Core.ViewModel;
using ImageConverter.View;
using System.Windows;

namespace ImageConverter.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public RelayCommand ExitCommand => new RelayCommand(execute => ExitApplication_Click());
        public RelayCommand ShowAboutWindowCommand => new RelayCommand(execute => ShowAboutDialog());

        public INavigationService Navigation => _navigationService;
        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _navigationService.NavigateTo<HomeViewModel>();
        }

        private void ShowAboutDialog()
        {
            var aboutWindow = new AboutWindow() 
            { 
                DataContext = new AboutWindowViewModel()
            };
            aboutWindow.ShowDialog();
        }

        private void ExitApplication_Click()
        {
            Application.Current.Shutdown();
        }
    }
}
