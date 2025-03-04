using Core.Command;
using Core.Interface;
using Core.ViewModel;
using Microsoft.Win32;
using System.Windows;

namespace ImageConverter.ViewModel
{
    public class MultipleImageConvertViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IConverterService _converterService;
        private OpenFileDialog _imagesPaths;
        private short _imageQuality = 80;

        public MultipleImageConvertViewModel(INavigationService navigationService, IConverterService converterService)
        {
            _navigationService = navigationService;
            _converterService = converterService;
        }

        public short ImageQuality 
        { 
            get => _imageQuality;
            set
            {
                if (value < 0)
                {
                    _imageQuality = 0;
                }
                else if (value > 100) 
                {
                    _imageQuality = 100;
                }
                else
                {
                    _imageQuality = value;
                }
                OnPropertyChanged(nameof(ImageQuality));
            }
        }
        public OpenFileDialog ImagesPaths
        { 
            get => _imagesPaths;
            set 
            {
                _imagesPaths = value;
                OnPropertyChanged(nameof(ImagesPaths));
            }
        }
        public RelayCommand SelectImagesCommand => new RelayCommand(execute => SelectImages());
        public RelayCommand BackCommand => new RelayCommand(execute => ReturnHome());
        public RelayCommand ConvertImagesCommand => new RelayCommand(execute => ConvertImages(), canExecute => ImagesPaths != null);

        private void ConvertImages()
        {
            var isConverted = _converterService.ConvertMultipleImages(ImagesPaths.FileNames, ImageQuality);

            if (isConverted)
            {
                ShowSuccessMessageBox();
                ReturnHome();
            }
            else
            {
                ShowErrorMessageBox();
            }
        }
        private void ShowSuccessMessageBox()
        {
            string messageText = "Images successfuly converted!";
            string caption = "Success";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBoxResult result;

            result = MessageBox.Show(messageText, caption, button, icon);
        }

        private void ShowErrorMessageBox()
        {
            string messageText = "Images conversion failed!";
            string caption = "Failed";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;
            MessageBoxResult result;

            result = MessageBox.Show(messageText, caption, button, icon);
        }

        private void SelectImages()
        {
            var dialog = new OpenFileDialog();
            dialog.FileName = "";
            dialog.DefaultExt = "";
            dialog.Filter = "Image Files(.png;.jpg;.jpeg)|*.png;*.jpg;*.jpeg";
            dialog.Multiselect = true;

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                ImagesPaths = dialog;
            }
        }

        private void ResetState()
        {
            ImageQuality = 80;
            ImagesPaths = null;
        }

        private void ReturnHome()
        {
            ResetState();
            _navigationService.NavigateTo<HomeViewModel>();
        }
    }
}
