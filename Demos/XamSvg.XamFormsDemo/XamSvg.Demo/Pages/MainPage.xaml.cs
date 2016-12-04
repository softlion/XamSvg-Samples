using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamSvg.XamForms;

namespace XamSvg.Demo
{

    public partial class MainPage : ContentPage
    {
        private readonly string[] names;
        private int i;

        public string ColorMapping
        {
            get { return (string)GetValue(ColorMappingProperty); }
            set { SetValue(ColorMappingProperty, value); }
        }

        public static BindableProperty ColorMappingProperty = BindableProperty.Create(nameof(ColorMapping), typeof(string), typeof(MainPage),
            null, BindingMode.OneWay,
            propertyChanged: (bindable, oldValue, newValue) => ((MainPage) bindable).ColorMapping = (string)newValue);


        public MainPage(string[] names)
        {
            this.names = names;
            InitializeComponent();

            //FileImageSource is sealed. We can currently not derive SvgImageSource from FileImageSource.
            //FileImageSource only references a local file path. It does not contains the methods to access it.
            //var svg = new SvgImageSource { Svg = "res:images.hand", HeightRequest = 20,  ColorMapping = "ffffff=00ff00" };
            //var image = svg.Image;

            //var toolbarImage = SvgImageSource.CreateFile("res:images.hand", height: 20);

            //ToolbarItems.Add(new ToolbarItem
            //{
            //    Icon = toolbarImage,
            //    Order = ToolbarItemOrder.Primary, Command = new Command(() =>
            //    {
            //        Navigation.PushAsync(new Page2());
            //    })
            //});

            //try
            //{
            //    var image = SvgImageSource.Create("res:images.refresh");
            //    var t = 0;
            //}
            //catch (Exception e)
            //{
            //    Debug.WriteLine(e);
            //}



            var svgRefresh = new SvgImageSource
            {
                Svg = "res:images.refresh", 
                HeightRequest = 15
            };
            TestButton.Image = svgRefresh.Image;
        }

        [Preserve]
        private void OnSvgClicked(object sender, EventArgs args)
        {

             var svgName = "res:" + names[i++];
            Svg.Svg = svgName;
            i = i%names.Length;
        }

        private void HideIt(object sender, EventArgs args)
        {
            TheGrid.IsVisible = false;
            Task.Delay(5000).ContinueWith(t =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    TheGrid.IsVisible = true;
                });
            });
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            Debug.WriteLine("TapGesture Tap recognized");
        }

        private void NextPageButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Page2());
        }

        private void ColorMappingSelectedButton_OnClicked(object sender, EventArgs e)
        {
            var rand = new Random();
            var bytes = new byte[3];
            rand.NextBytes(bytes);
            ColorMapping = $"ffffff=00ff00;000000={bytes[0]:X2}{bytes[1]:X2}{bytes[2]:X2}";
        }
    }
}
