using Core.Command;
using Core.Interface;
using Core.ViewModel;
using Microsoft.Win32;
using System.Diagnostics;
using System.Reflection;

namespace ImageConverter.ViewModel
{
    public class AboutWindowViewModel : ViewModelBase
    {
        public AboutWindowViewModel()
        {
        }

        public RelayCommand OpenGithubRepoCommand => new RelayCommand(args => OpenGithubRepo(args.ToString()));

        public string VersionString 
        {
            get
            {
                var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
                return $"Version {assemblyVersion}";
            }
        }

        private void OpenGithubRepo(string url)
        {
            var psi = new ProcessStartInfo(url);
            psi.UseShellExecute = true;
            Process.Start(psi);
        }
    }
}
