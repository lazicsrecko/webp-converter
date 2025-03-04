using Core.Command;
using Core.Interface.ViewModel;
using Core.ViewModel;

namespace ImageConverter.ViewModel
{
    public class ConverterOptionsViewModel : ViewModelBase, IConverterOptions
    {
        private short _imageQuality = 75;
        private short _alphaQuality = 100;
        private short _compressionMethod = 4;
        private string _copyMetadaFromSource = "none";
        private bool _useMultithreading = false;
        private bool _useMetadataNone = true;

        public RelayCommand CopyMetadataCommand => new RelayCommand(arg => SetCopyMetadataValue(arg.ToString()));

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

        public short AlphaQuality
        {
            get => _alphaQuality;
            set
            {
               if (value > 100)
                {
                    _alphaQuality = 100;
                }
                else if (value < 0)
                {
                    _alphaQuality = 0;
                }
                else
                {
                    _alphaQuality = value;
                }
                OnPropertyChanged(nameof(AlphaQuality));
            }
        }

        public short CompressionMethod
        {
            get => _compressionMethod;
            set
            {
                if (value < 0)
                {
                    _compressionMethod = 0;
                }
                else if (value > 6)
                {
                    _compressionMethod = 6;
                }
                else
                {
                    _compressionMethod = value;
                }
                OnPropertyChanged(nameof(CompressionMethod));
            }
        }

        public bool UseMulitthreading
        {
            get => _useMultithreading;
            set
            {
                _useMultithreading = value;
                OnPropertyChanged(nameof(UseMulitthreading));
            }
        }

        public string CopyMetadataFromSource
        {
            get => _copyMetadaFromSource;
            set
            {
                _copyMetadaFromSource = value;
                OnPropertyChanged(nameof(CopyMetadataFromSource));
            }
        }

        public bool UseMetadataNone
        {
            get => _useMetadataNone;
            set
            {
                _useMetadataNone = value;
                OnPropertyChanged(nameof(UseMetadataNone));
            }
        }

        public void ResetState()
        {
            ImageQuality = 75;
            AlphaQuality = 100;
            CompressionMethod = 4;
            UseMulitthreading = false;
        }

        private void SetCopyMetadataValue(string value)
        {
            if (value == "none")
            {
                UseMetadataNone = true;
            } else
            {
                UseMetadataNone = false;
            }
            CopyMetadataFromSource = value;
        }
    }
}
