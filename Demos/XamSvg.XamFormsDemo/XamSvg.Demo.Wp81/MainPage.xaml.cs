using Windows.UI.Xaml.Navigation;
using Xamarin.Forms;
using XamSvg.XamForms.Universal;

namespace XamSvg.Demo.Wp81
{
    public sealed partial class MainPage 
    {
#if DEBUG
        Windows.System.Display.DisplayRequest keepActive;
#endif

        public MainPage()
        {
            InitializeComponent();

#if DEBUG
            // Avoid screen locks while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                var keepActive = new Windows.System.Display.DisplayRequest();
                keepActive.RequestActive();
            }
#endif

            SvgImageRenderer.InitializeForms();
            LoadApplication(new XamSvg.Demo.App());

            NavigationCacheMode = NavigationCacheMode.Required;
        }
    }
}
