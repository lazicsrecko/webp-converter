namespace Core.Interface
{
    public interface IConverterService
    {
        bool ConvertToWebp(string inputFile, string outputFile, short imageQuality);
        bool ConvertMultipleImages(string[] imagesPaths, short imageQuality);
    }
}
