using Core.Converters;
using Core.Interface;
using Core.Interface.ViewModel;
using System.Diagnostics;

namespace Services.Services
{
    public class ConverterService : IConverterService
    {
        private readonly IConverterOptions _converterOptions;

        public ConverterService(IConverterOptions converterOptions)
        {
            _converterOptions = converterOptions;
        }

        public bool ConvertMultipleImages(string[] imagesPaths)
        {
            bool isSuccess = false;
            if (imagesPaths.Length == 0)
            {
                return isSuccess;
            }

            foreach (string imagePath in imagesPaths)
            {
                string fileExtension = imagePath.Substring(imagePath.IndexOf("."));
                string inputPath = imagePath;
                string outputPath = imagePath.Replace(fileExtension, ".webp");
                isSuccess = ConvertToWebp(inputPath, outputPath);
            }

            return isSuccess;
        }

        public bool ConvertToWebp(string inputFile, string outputFile)
        {
            var cwebpPath = Converter.ExtractCwebExe();

            if (!string.IsNullOrEmpty(cwebpPath)) 
            {
                string args = $"-q {_converterOptions.ImageQuality} -alpha_q {_converterOptions.AlphaQuality} -m {_converterOptions.CompressionMethod} -metadata {_converterOptions.CopyMetadataFromSource} -mt {_converterOptions.UseMulitthreading} -o \"{outputFile}\" \"{inputFile}\"";
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = cwebpPath;
                processStartInfo.Arguments = args;
                processStartInfo.RedirectStandardError = true;
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.UseShellExecute = false;

                Process process = Process.Start(processStartInfo);
                process.WaitForExit();
                string errorOutput = process.StandardError.ReadToEnd();

                if (process.ExitCode != 0)
                {
                    // log error
                    return false;
                }


                Converter.RemoveCwebpExe();
                return true;
            }

            return false;
        }
    }
}
