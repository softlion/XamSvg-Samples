using System;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace XamSvg.UwDemo
{
    public sealed partial class MainPage : Page
    {
        private readonly string[] fileNames;
        private int i;

        public MainPage()
        {
            //Initialize the SVG lib
            #region Move this in App.xaml.cs
            var assembly = typeof(XamSvgDemo.Shared.App).GetTypeInfo().Assembly;
            XamSvg.Shared.Config.ResourceAssembly = assembly;
#if DEBUG
            XamSvg.Shared.Config.NativeLogger = new DebugLogger();
#endif
            #endregion

            this.InitializeComponent();

            var sharedSvgs = assembly.GetManifestResourceNames().Where(n => n.EndsWith(".svg")).OrderBy(n => n.Substring(n.LastIndexOf('/') + 1)).ToArray();
            fileNames = sharedSvgs.Select(s => "res:" + s).ToArray();
        }

        /// <summary>
        /// Change svg on button click
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (fileNames == null)
                return;

            //MySvg.Source = "ms-appx:///Assets/svg/" + fileNames[i++%fileNames.Length];
            MySvg.Source = fileNames[i++ % fileNames.Length];

            #region Test load svg from string
//            var testStringSvg = @"
//<svg viewBox=""0 0 8 8"">
//    <path fill=""#3C5A99"" d=""M3.5 0c-1.93 0-3.5 1.57-3.5 3.5s1.57 3.5 3.5 3.5c.59 0 1.17-.14 1.66-.41a1 1 0 0 0 .13.13l1 1a1.02 1.02 0 1 0 1.44-1.44l-1-1a1 1 0 0 0-.16-.13c.27-.49.44-1.06.44-1.66 0-1.93-1.57-3.5-3.5-3.5zm0 1c1.39 0 2.5 1.11 2.5 2.5 0 .66-.24 1.27-.66 1.72-.01.01-.02.02-.03.03a1 1 0 0 0-.13.13c-.44.4-1.04.63-1.69.63-1.39 0-2.5-1.11-2.5-2.5s1.11-2.5 2.5-2.5z"" />
//</svg>
//";
//            MySvg.Source = "string:" + testStringSvg;
            #endregion
        }
    }
}
