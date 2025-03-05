using Core.Command;
using Core.Interface;
using Core.ViewModel;
using Microsoft.Win32;
using System.Diagnostics;
using System.Reflection;
using static System.Net.WebRequestMethods;

namespace ImageConverter.ViewModel
{
    public class AboutWindowViewModel : ViewModelBase
    {
        private string gitHubRepoLink = "https://github.com/lazicsrecko/webp-converter";
        public AboutWindowViewModel()
        {
        }

        public RelayCommand OpenGithubRepoCommand => new RelayCommand(args => OpenGithubRepo());

        public string VersionString 
        {
            get
            {
                var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
                return $"Version {assemblyVersion}";
            }
        }

        private void OpenGithubRepo()
        {
            var psi = new ProcessStartInfo(gitHubRepoLink);
            psi.UseShellExecute = true;
            Process.Start(psi);
        }
    }
}
