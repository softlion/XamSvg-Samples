﻿using System;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace XamSvg.Wp81Demo
{
    public sealed partial class MainPage : Page
    {
        private readonly string[] fileNames;
        private int i;

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;


            #region Move this in App.xaml.cs
            //Initialize the SVG lib
            var assembly = typeof (XamSvgDemo.Shared.App).GetTypeInfo().Assembly;
            XamSvg.Shared.Config.ResourceAssembly = assembly;
            #endregion

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

            //MySvg.Source = new Uri("ms-appx:///Assets/svg/" + fileNames[i++%fileNames.Length]);
            MySvg.Source = new Uri(fileNames[i++%fileNames.Length], UriKind.Absolute);

            #region Test load svg from string
//            var testStringSvg = @"
//<svg viewBox=""0 0 16.002 16.002"">
//  <path d=""m5.1456,8.9503,0-2.0742,0.31641,0,0,0.29492c0.1524-0.2279,0.3724-0.3418,0.6602-0.3418,0.125,0.0000022,0.23991,0.022463,0.34473,0.067383,0.10482,0.044924,0.18327,0.10384,0.23535,0.17676,0.052081,0.072919,0.08854,0.15951,0.10938,0.25977,0.013019,0.065106,0.019529,0.17904,0.019531,0.3418v1.2754h-0.35156v-1.2617c0.0001-0.1434-0.0136-0.2505-0.0409-0.3214-0.0273-0.071-0.0758-0.1276-0.1455-0.17-0.0697-0.0423-0.1514-0.0634-0.2451-0.0634-0.1498,0-0.279,0.0475-0.3877,0.1425-0.1087,0.0951-0.1631,0.2754-0.1631,0.5411v1.1328z""/>
//  <path d=""m3.1955,13.086,0-1.8008-0.31055,0,0-0.27344,0.31055,0,0-0.2207c-4E-7-0.13932,0.012369-0.24284,0.037109-0.31055,0.033854-0.09114,0.093424-0.16504,0.17871-0.22168s0.20475-0.08496,0.3584-0.08496c0.098957,0.000003,0.20833,0.01172,0.32812,0.03516l-0.052734,0.30664c-0.072918-0.01302-0.14193-0.01953-0.20703-0.01953-0.10677,0.000003-0.18229,0.02279-0.22656,0.06836-0.044272,0.04557-0.066407,0.13086-0.066406,0.25586v0.19141h0.4043v0.27344h-0.4043v1.8008z""/>
//";
//            MySvg.Source = new Uri("string:" + testStringSvg);
            #endregion
        }

        //private void OldMethod()
        //{
        //    WebRequest.RegisterPrefix("svg:")
        //    Task.Run(async () =>
        //    {
        //        var installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;
        //        var subFolder = await installedLocation.GetFolderAsync("Assets");
        //        subFolder = await subFolder.GetFolderAsync("svg");
        //        var files = await subFolder.GetFilesAsync();
        //        //var folder = await StorageFolder.GetFolderFromPathAsync("ms-appx:///Assets/svg");
        //        //var files = await folder.GetFilesAsync(CommonFileQuery.OrderByName);
        //        fileNames = files.Where(s => s.Name.EndsWith(".svg")).Select(s => s.Name).OrderBy(n => n).ToArray();
        //    });
        //}
    }

    #region old method, using WebRequest.RegisterPrefix("svg:")
    //public sealed class SvgWebRequestFactory : IWebRequestCreate
    //{
    //    public const string Scheme = "svg";
    //    private static SvgWebRequestFactory _factory = new SvgWebRequestFactory();

    //    private SvgWebRequestFactory()
    //    {
    //    }

    //    // call this before anything else
    //    public static void Register()
    //    {
    //        WebRequest.RegisterPrefix(Scheme, _factory);
    //    }

    //    WebRequest IWebRequestCreate.Create(Uri uri)
    //    {
    //        return new ResXWebRequest(uri);
    //    }

    //    private class ResXWebRequest : WebRequest
    //    {
    //        public ResXWebRequest(Uri uri)
    //        {
    //            Uri = uri;
    //        }

    //        public override string ContentType
    //        {
    //            get
    //            {
    //                return "text/plain";
    //            }

    //            set
    //            {
    //            }
    //        }

    //        public override WebHeaderCollection Headers
    //        {
    //            get
    //            {
    //                return new WebHeaderCollection();
    //            }

    //            set
    //            {
    //            }
    //        }

    //        public override string Method            {                  get                 {                     return "GET";                 }                  set                 {                }            }

    //        public override Uri RequestUri { get { return Uri; } }

    //        public Uri Uri { get; set; }

    //        public override void Abort()
    //        {
    //        }

    //        public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
    //        {
    //            throw new NotImplementedException();
    //        }
    //        public override Stream EndGetRequestStream(IAsyncResult asyncResult)
    //        {
    //            throw new NotImplementedException();
    //        }

    //        public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
    //        {
    //            return null;
    //        }


    //        public override WebResponse EndGetResponse(IAsyncResult asyncResult)
    //        {
    //            return new ResXWebResponse(Uri);
    //        }
    //    }

    //    private class ResXWebResponse : WebResponse
    //    {
    //        public ResXWebResponse(Uri uri)
    //        {
    //            Uri = uri;
    //        }

    //        public Uri Uri { get; set; }

    //        public override Stream GetResponseStream()
    //        {
    //            Assembly asm;
    //            if (string.IsNullOrEmpty(Uri.Host))
    //                asm = Assembly.GetEntryAssembly();
    //            else
    //                asm = Assembly.Load(new AssemblyName(Uri.Host));

    //            int filePos = Uri.LocalPath.LastIndexOf('/');
    //            string baseName = Uri.LocalPath.Substring(1, filePos - 1);
    //            string name = Uri.LocalPath.Substring(filePos + 1);

    //            var rm = new ResourceManager(baseName, asm);
    //            object obj = rm.GetObject(name);

    //            var stream = obj as Stream;
    //            if (stream != null)
    //                return stream;

    //            //var bmp = obj as Bitmap;
    //            //Return other formats
    //            return null;
    //        }
    //    }
    //}
    #endregion
}