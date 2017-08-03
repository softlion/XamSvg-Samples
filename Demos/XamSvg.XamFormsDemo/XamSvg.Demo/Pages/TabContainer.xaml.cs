using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            MainPage.Icon = new SvgImageSource
                {
                    Svg = "res:images.pin",
                    Height = 30,
                }
                .CreateFileImageSource();
        }
    }
}
