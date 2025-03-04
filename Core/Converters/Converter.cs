using System.IO;
using System.Reflection;

namespace Core.Converters
{
    public static class Converter
    {
        static string cwebpExe = "cwebp.exe";
        static string outputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), cwebpExe);
        public static string ExtractCwebExe()
        {
            

            using (Stream stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream($"Core.Converters.{cwebpExe}"))
            {
                if (stream != null)
                {
                    using (FileStream fileStream = new FileStream(outputPath, FileMode.Create))
                    {
                        stream.CopyTo(fileStream);
                    }
                    return outputPath;
                }
                else
                {
                    Console.WriteLine("Embedded EXE not found.");
                    return string.Empty;
                }
            }
        }

        public static void RemoveCwebpExe()
        {
            File.Delete(outputPath);
        }
    }
}
