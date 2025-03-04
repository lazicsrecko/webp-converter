using Core.Command;
using Core.Interface;
using Core.Interface.ViewModel;
using Core.ViewModel;
using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;

namespace ImageConverter.ViewModel
{
    public class MultipleImageConvertViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IConverterService _converterService;
        private readonly IMessageBoxService _messageBoxService;
        private readonly IConverterOptions _converterOptions;
        private OpenFileDialog _imagesPaths;

        public MultipleImageConvertViewModel(
            INavigationService navigationService,
            IConverterService converterService,
            IMessageBoxService messageBoxService,
            IConverterOptions converterOptions)
        {
            _navigationService = navigationService;
            _converterService = converterService;
            _messageBoxService = messageBoxService;
            _converterOptions = converterOptions;
        }

        public IConverterOptions ConverterOptionsDataContext => _converterOptions;

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
            var nonExistantPaths = CheckIfPathsExists(ImagesPaths.FileNames);

            if (nonExistantPaths.Count != 0)
            {
                string errorPaths = ExtractErrorPaths(nonExistantPaths);
                string message = $"Images at paths: {errorPaths} doesn't exist!";
                _messageBoxService.ShowErrorMessageBox(message);
                return;
            }

            var isConverted = _converterService.ConvertMultipleImages(ImagesPaths.FileNames);

            if (isConverted)
            {
                _messageBoxService.ShowSuccessMessageBox("Images successfuly converted!");
                ReturnHome();
            }
            else
            {
                _messageBoxService.ShowErrorMessageBox("Images conversion failed!");
            }
        }

        private string ExtractErrorPaths(List<string> paths)
        {
            StringBuilder sb = new StringBuilder("");
            if (paths == null || paths.Count == 0)
            {
                return sb.ToString();
            }

            for (var i = 0; i < paths.Count; i++)
            {
                if (i + 1 == paths.Count)
                {
                    sb.Append($"\"{paths[i]}\"");
                }
                else
                {
                    sb.Append($"\"{paths[i]}\", ");
                }
            }

            return sb.ToString();
        }

        private List<string> CheckIfPathsExists(string[] paths)
        {
            List<string> nonExistantPaths = new List<string>();

            foreach (string path in paths)
            {
                if (!File.Exists(path))
                {
                    nonExistantPaths.Add(path);
                }
            }

            return nonExistantPaths;
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
            ImagesPaths = null;
            _converterOptions.ResetState();
        }

        private void ReturnHome()
        {
            ResetState();
            _navigationService.NavigateTo<HomeViewModel>();
        }
    }
}
