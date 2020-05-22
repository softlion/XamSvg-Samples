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
            XamSvg.Setup.InitSvgLib();
            var assembly = typeof(XamSvgDemo.Shared.App).GetTypeInfo().Assembly;
            XamSvg.Shared.Config.ResourceAssembly = assembly;
            XamSvg.Shared.Config.License = "eyJhbGciOiJSUzI1NiIsImtpZCI6InZhcG9saWFzaWciLCJ0eXAiOiJKV1QifQ.eyJ1bmlxdWVfbmFtZSI6ImU1ZjRmODZlOGY4OTRjOTI4MmFkZDMyMWNjZTVkYjgxIiwiaHR0cHM6Ly9zY2hlbWFzLnZhcG9saWEuZXUvMjAyMC8wNS9jbGFpbXMvTWF4QnVpbGREYXRlQ2xhaW0iOiIyMDIxLTA1LTEzVDA4OjAxOjE2LjYzNjc2MiswMjowMCIsImh0dHBzOi8vc2NoZW1hcy52YXBvbGlhLmV1LzIwMjAvMDUvY2xhaW1zL1Byb2R1Y3RDb2RlQ2xhaW0iOlsieGFtc3ZnIiwieGFtc3ZnZm9ybXMiXSwiaHR0cHM6Ly9zY2hlbWFzLnZhcG9saWEuZXUvMjAyMC8wNS9jbGFpbXMvQXBwSWRDbGFpbSI6WyJmci52YXBvbGlhLnN2Z3Rlc3QiLCJmci52YXBvbGlhLnN2Z2Zvcm10ZXN0IiwieGFtc3ZnLmRyb2lkLnRlc3RzIiwiWGFtU3ZnLkRlbW8uRHJvaWQiXSwiaHR0cHM6Ly9zY2hlbWFzLnZhcG9saWEuZXUvMjAyMC8wNS9jbGFpbXMvT3NDbGFpbSI6WyJpb3MiLCJhbmRyb2lkIiwidXdwIl0sIm5iZiI6MTU4OTM0OTY3NiwiZXhwIjoxOTA0ODgyNDc2LCJpYXQiOjE1ODkzNDk2NzYsImlzcyI6Imh0dHBzOi8vdmFwb2xpYS5ldS9hdXRob3JpdHkiLCJhdWQiOiJodHRwczovL3ZhcG9saWEuZXUvYXV0aG9yaXR5L2xpY2Vuc2VzIn0.r9SLG24WPQM7mgWNXBP-51IHSYdNcuAMNN8vhWP5hYWip8dWzUQRvI7U0D2z5-w8i8WTrbwkFc3s0R8plF7SB02CeXzTYEDmYhu-tUWnicC_0OrEsfmsQK0HyUyd8jEaehNH7IB5EpgwPG9-8k2RbsXg0803uacnjx7WoEYTwdb8vpxVuCHi9opCReHHL1gztElFN1aXwHbiyle_AqsX9seBKFKQxgi5jXWFSi4blGuwLEe44GWnzyJAAZQcK_jYUDC2PGkcVFBDeyIROmPAmq4_4nEeYrQWF80tPmsbqHNcqR9_lwZUi_ZThtrc-iCwfIIY-r8DFFDP_hnqTmXIkg";
            #endregion

            this.InitializeComponent();

            var sharedSvgs = assembly.GetManifestResourceNames().Where(n => n.EndsWith(".svg")).OrderBy(n => n.Substring(n.LastIndexOf('/') + 1)).ToArray();
            fileNames = sharedSvgs.Select(s => "res:" + s).Concat(new [] {"res:missing"}).ToArray();
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
