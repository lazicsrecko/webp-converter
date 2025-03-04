namespace Core.Interface
{
    public interface IConverterService
    {
        bool ConvertToWebp(string inputFile, string outputFile);
        bool ConvertMultipleImages(string[] imagesPaths);
    }
}
