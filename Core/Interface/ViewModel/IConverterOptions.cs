namespace Core.Interface.ViewModel
{
    public interface IConverterOptions
    {
        short ImageQuality { get; }
        short AlphaQuality { get; }
        short CompressionMethod { get; }
        bool UseMulitthreading { get; }
        string CopyMetadataFromSource { get; }
        public void ResetState();
    }
}
