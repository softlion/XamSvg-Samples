using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamSvg.XamForms;

namespace XamSvg.Demo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabContainer : TabbedPage
    {
        public TabContainer()
        {
            InitializeComponent();

            MainPage.Icon = new SvgImageSource { Svg = "res:images.intertwingly", Height = 30 }.CreateFileImageSource();

            AnimationPage.Icon = new SvgImageSource { Svg = "res:images.0GoldMirror", Height = 30 }.CreateFileImageSource();
        }
    }
}
