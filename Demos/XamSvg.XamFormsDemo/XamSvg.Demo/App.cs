using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

using Xamarin.Forms;

namespace XamSvg.Demo
{
    public class App : Application
    {
        public App()
        {
            var assembly = typeof (App).GetTypeInfo().Assembly;
            XamSvg.Shared.Config.ResourceAssembly = assembly;
            var names = assembly.GetManifestResourceNames().Where(n => n.EndsWith(".svg")).OrderBy(n => n).ToArray();

            //Xamarin alpha version does not generate partial classes on XAML files with Xamarin Forms prerelease
            //So the following line won't work
            //MainPage.xaml.cs build action has been set to none
            MainPage = CreateMainPageFromXaml(names);
            //MainPage = CreateMainPageFromCode(names);
        }

        private Page CreateMainPageFromXaml(string[] names)
        {
            return new NavigationPage(new MainPage(names));
                        }

        //private ContentPage CreateMainPageFromCode(string[] names)
        //{
        //    SvgImage svg;
        //    var page = new ContentPage
        //    {
        //        BackgroundColor = Color.FromHex("E08080"),
        //        Content = new ContentView
        //        {
        //            Padding = new Thickness(10,40,40,10),
        //            Content = new StackLayout
        //            {
        //                Orientation = StackOrientation.Vertical,
        //                VerticalOptions = LayoutOptions.Start,
        //                Children =
        //                {
        //                    new Label
        //                    {
        //                        Text="Clicking the vector image will select it and switch to next image. Get more svg images on http://www.flaticon.com/",
        //                        HorizontalOptions = LayoutOptions.Center
        //                    },
        //                    (svg = new SvgImage
        //                    {
        //                        HorizontalOptions = LayoutOptions.CenterAndExpand,
        //                        Svg = "res:images.hand",
        //                        ColorMappingSelected="ffffff=00ff00"
        //                    })                     
        //                }
        //            }
        //        }
        //    };

        //    var i = 0;
        //    svg.Clicked += (sender, args) =>
        //    {
        //        svg.Svg = "res:" + names[i++];
        //        i = i%names.Length;
        //    };

        //    return page;
        //}
    }
}