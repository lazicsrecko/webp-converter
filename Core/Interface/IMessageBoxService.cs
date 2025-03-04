namespace Core.Interface
{
    public interface IMessageBoxService
    {
        void ShowSuccessMessageBox(string message);
        void ShowErrorMessageBox(string message);
    }
}
