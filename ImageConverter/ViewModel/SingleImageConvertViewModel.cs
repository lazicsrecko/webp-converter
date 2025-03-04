using Core.Command;
using Core.Interface;
using Core.ViewModel;
using Microsoft.Win32;
using System.Timers;
using System.Windows;

namespace ImageConverter.ViewModel
{
    class SingleImageConvertViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IConverterService _converterService;
        private short _imageQuality = 80;
        private OpenFileDialog _imagePath;

        public SingleImageConvertViewModel(INavigationService navigationService, IConverterService converterService)
        {
            _navigationService = navigationService;
            _converterService = converterService;
        }

        public RelayCommand SelectImageCommand => new RelayCommand(execute => SelectImage());
        public RelayCommand ReturnHomeCommand => new RelayCommand(execute => ReturnHome());
        public RelayCommand ConvertImageCommand => new RelayCommand(execute => ConvertImage(), canExecute => ImagePath != null);       

        public short ImageQuality
        {
            get => _imageQuality;
            set
            {
                if (value > 100)
                {
                    _imageQuality = 100;
                }
                else if (value < 0)
                {
                    _imageQuality = 0;
                }
                else
                {
                    _imageQuality = value;
                }
                OnPropertyChanged(nameof(ImageQuality));
            }
        }
        public OpenFileDialog ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                OnPropertyChanged();
            }
        }

        private void ConvertImage()
        {
            string fileExtension = ImagePath.SafeFileName.Substring(ImagePath.SafeFileName.IndexOf("."));
            string inputPath = ImagePath.FileName;
            string outputPath = ImagePath.FileName.Replace(fileExtension, ".webp");
            var isConverted = _converterService.ConvertToWebp(inputPath, outputPath, ImageQuality);

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
            string messageText = "Image successfuly converted!";
            string caption = "Success";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBoxResult result;

            result = MessageBox.Show(messageText, caption, button, icon);
        }

        private void ShowErrorMessageBox()
        {
            string messageText = "Image conversion failed!";
            string caption = "Failed";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;
            MessageBoxResult result;

            result = MessageBox.Show(messageText, caption, button, icon);
        }

        private void SelectImage()
        {
            var dialog = new OpenFileDialog();
            dialog.FileName = "";
            dialog.DefaultExt = ".jpg";

            bool? resul = dialog.ShowDialog();

            if (resul != null)
            {
                ImagePath = dialog;
            }
        }

        private void ResetState()
        {
            ImagePath = null;
            ImageQuality = 80;
        }

        private void ReturnHome()
        {
            ResetState();
            _navigationService.NavigateTo<HomeViewModel>();
        }
    }
}
