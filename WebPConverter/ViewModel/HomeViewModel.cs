using Core.Command;
using Core.Interface;
using Core.ViewModel;

namespace ImageConverter.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public RelayCommand GoToSingleConvertCommand => new RelayCommand(execute => GoToSingleConvert());
        public RelayCommand GoToMultipleConvertCommand => new RelayCommand(execute => GoToMultipleConvert());
        public HomeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private void GoToSingleConvert()
        {
            _navigationService.NavigateTo<SingleImageConvertViewModel>();
        }

        private void GoToMultipleConvert()
        {
            _navigationService.NavigateTo<MultipleImageConvertViewModel>();
        }
    }
}
