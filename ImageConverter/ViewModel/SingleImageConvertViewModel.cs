using Core.Command;
using Core.Interface;
using Core.Interface.ViewModel;
using Core.ViewModel;
using Microsoft.Win32;
using System.IO;

namespace ImageConverter.ViewModel
{
    class SingleImageConvertViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IConverterService _converterService;
        private readonly IConverterOptions _converterOptionsDataContext;
        private readonly IMessageBoxService _messageBoxService;
        private OpenFileDialog _imagePath;

        public SingleImageConvertViewModel(
            INavigationService navigationService,
            IConverterService converterService,
            IConverterOptions converterOptionsDataContext,
            IMessageBoxService messageBoxService)
        {
            _navigationService = navigationService;
            _converterService = converterService;
            _converterOptionsDataContext = converterOptionsDataContext;
            _messageBoxService = messageBoxService;
        }

        public RelayCommand SelectImageCommand => new RelayCommand(execute => SelectImage());
        public RelayCommand ReturnHomeCommand => new RelayCommand(execute => ReturnHome());
        public RelayCommand ConvertImageCommand => new RelayCommand(execute => ConvertImage(), canExecute => ImagePath != null);

        public IConverterOptions ConverterOptionsDataContext => _converterOptionsDataContext;

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
            string inputPath = ImagePath.FileName;

            if (!File.Exists(inputPath)) 
            {
                _messageBoxService.ShowErrorMessageBox($"Image at path \"{inputPath}\" doesn't exist!");
                return;
            }

            string fileExtension = ImagePath.SafeFileName.Substring(ImagePath.SafeFileName.IndexOf("."));
            string outputPath = ImagePath.FileName.Replace(fileExtension, ".webp");
            var isConverted = _converterService.ConvertToWebp(inputPath, outputPath);

            if (isConverted)
            {
                _messageBoxService.ShowSuccessMessageBox("Image successfully converted!");
                ReturnHome();
            }
            else
            {
                _messageBoxService.ShowErrorMessageBox("Image conversion failed!");
            }
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
            ConverterOptionsDataContext.ResetState();
        }

        private void ReturnHome()
        {
            ResetState();
            _navigationService.NavigateTo<HomeViewModel>();
        }
    }
}
