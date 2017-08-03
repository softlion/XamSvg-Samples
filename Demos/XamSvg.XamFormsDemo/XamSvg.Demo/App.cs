using System.Linq;
using System.Reflection;

using Xamarin.Forms;
using XamSvg.Demo.Pages;

namespace XamSvg.Demo
{
    public class App : Application
    {
        public App()
        {
            var assembly = typeof (App).GetTypeInfo().Assembly;
            XamSvg.Shared.Config.ResourceAssembly = assembly;

            MainPage = new NavigationPage(new TabContainer());
        }
    }
}