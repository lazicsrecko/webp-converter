using Core.Converters;
using Core.Interface;
using System.Diagnostics;

namespace Services.Services
{
    public class ConverterService : IConverterService
    {
        public bool ConvertMultipleImages(string[] imagesPaths, short imageQuality)
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
                isSuccess = ConvertToWebp(inputPath, outputPath, imageQuality);
            }

            return isSuccess;
        }
        public bool ConvertToWebp(string inputFile, string outputFile, short imageQuality)
        {
            var cwebpPath = Converter.ExtractCwebExe();

            if (!string.IsNullOrEmpty(cwebpPath)) 
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = cwebpPath;
                processStartInfo.Arguments = $"-q {imageQuality} -o \"{outputFile}\" \"{inputFile}\"";
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
