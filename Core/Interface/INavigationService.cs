using Core.ViewModel;

namespace Core.Interface
{
    public interface INavigationService
    {
        ViewModelBase CurrentView { get; }
        void NavigateTo<T>() where T : ViewModelBase;
    }
}
