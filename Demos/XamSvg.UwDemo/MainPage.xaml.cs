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
            XamSvg.Shared.Config.License = "eyJhbGciOiJSUzI1NiIsImtpZCI6InZhcG9saWFzaWciLCJ0eXAiOiJKV1QifQ.eyJodHRwczovL3NjaGVtYXMudmFwb2xpYS5ldS8yMDIwLzA1L2NsYWltcy9MaWNlbnNlc0NsYWltIjoie1wiTGljZW5zZXNcIjpbe1wiUHJvZHVjdFwiOlwieGFtc3ZnXCIsXCJPc1wiOlwiaW9zXCIsXCJBcHBJZFwiOlwiZnIudmFwb2xpYS5zdmd0ZXN0XCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z1wiLFwiT3NcIjpcImFuZHJvaWRcIixcIkFwcElkXCI6XCJmci52YXBvbGlhLnN2Z3Rlc3RcIixcIk1heEJ1aWxkXCI6XCIyMDIxLTA3LTEwVDEyOjUxOjUwLjYxNTcxODYrMDI6MDBcIn0se1wiUHJvZHVjdFwiOlwieGFtc3ZnXCIsXCJPc1wiOlwidXdwXCIsXCJBcHBJZFwiOlwiZnIudmFwb2xpYS5zdmd0ZXN0XCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z2Zvcm1zXCIsXCJPc1wiOlwiaW9zXCIsXCJBcHBJZFwiOlwiZnIudmFwb2xpYS5zdmd0ZXN0XCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z2Zvcm1zXCIsXCJPc1wiOlwiYW5kcm9pZFwiLFwiQXBwSWRcIjpcImZyLnZhcG9saWEuc3ZndGVzdFwiLFwiTWF4QnVpbGRcIjpcIjIwMjEtMDctMTBUMTI6NTE6NTAuNjE1NzE4NiswMjowMFwifSx7XCJQcm9kdWN0XCI6XCJ4YW1zdmdmb3Jtc1wiLFwiT3NcIjpcInV3cFwiLFwiQXBwSWRcIjpcImZyLnZhcG9saWEuc3ZndGVzdFwiLFwiTWF4QnVpbGRcIjpcIjIwMjEtMDctMTBUMTI6NTE6NTAuNjE1NzE4NiswMjowMFwifSx7XCJQcm9kdWN0XCI6XCJ4YW1zdmdcIixcIk9zXCI6XCJpb3NcIixcIkFwcElkXCI6XCJmci52YXBvbGlhLnN2Z2Zvcm10ZXN0XCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z1wiLFwiT3NcIjpcImFuZHJvaWRcIixcIkFwcElkXCI6XCJmci52YXBvbGlhLnN2Z2Zvcm10ZXN0XCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z1wiLFwiT3NcIjpcInV3cFwiLFwiQXBwSWRcIjpcImZyLnZhcG9saWEuc3ZnZm9ybXRlc3RcIixcIk1heEJ1aWxkXCI6XCIyMDIxLTA3LTEwVDEyOjUxOjUwLjYxNTcxODYrMDI6MDBcIn0se1wiUHJvZHVjdFwiOlwieGFtc3ZnZm9ybXNcIixcIk9zXCI6XCJpb3NcIixcIkFwcElkXCI6XCJmci52YXBvbGlhLnN2Z2Zvcm10ZXN0XCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z2Zvcm1zXCIsXCJPc1wiOlwiYW5kcm9pZFwiLFwiQXBwSWRcIjpcImZyLnZhcG9saWEuc3ZnZm9ybXRlc3RcIixcIk1heEJ1aWxkXCI6XCIyMDIxLTA3LTEwVDEyOjUxOjUwLjYxNTcxODYrMDI6MDBcIn0se1wiUHJvZHVjdFwiOlwieGFtc3ZnZm9ybXNcIixcIk9zXCI6XCJ1d3BcIixcIkFwcElkXCI6XCJmci52YXBvbGlhLnN2Z2Zvcm10ZXN0XCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z1wiLFwiT3NcIjpcImlvc1wiLFwiQXBwSWRcIjpcInhhbXN2Zy5kcm9pZC50ZXN0c1wiLFwiTWF4QnVpbGRcIjpcIjIwMjEtMDctMTBUMTI6NTE6NTAuNjE1NzE4NiswMjowMFwifSx7XCJQcm9kdWN0XCI6XCJ4YW1zdmdcIixcIk9zXCI6XCJhbmRyb2lkXCIsXCJBcHBJZFwiOlwieGFtc3ZnLmRyb2lkLnRlc3RzXCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z1wiLFwiT3NcIjpcInV3cFwiLFwiQXBwSWRcIjpcInhhbXN2Zy5kcm9pZC50ZXN0c1wiLFwiTWF4QnVpbGRcIjpcIjIwMjEtMDctMTBUMTI6NTE6NTAuNjE1NzE4NiswMjowMFwifSx7XCJQcm9kdWN0XCI6XCJ4YW1zdmdmb3Jtc1wiLFwiT3NcIjpcImlvc1wiLFwiQXBwSWRcIjpcInhhbXN2Zy5kcm9pZC50ZXN0c1wiLFwiTWF4QnVpbGRcIjpcIjIwMjEtMDctMTBUMTI6NTE6NTAuNjE1NzE4NiswMjowMFwifSx7XCJQcm9kdWN0XCI6XCJ4YW1zdmdmb3Jtc1wiLFwiT3NcIjpcImFuZHJvaWRcIixcIkFwcElkXCI6XCJ4YW1zdmcuZHJvaWQudGVzdHNcIixcIk1heEJ1aWxkXCI6XCIyMDIxLTA3LTEwVDEyOjUxOjUwLjYxNTcxODYrMDI6MDBcIn0se1wiUHJvZHVjdFwiOlwieGFtc3ZnZm9ybXNcIixcIk9zXCI6XCJ1d3BcIixcIkFwcElkXCI6XCJ4YW1zdmcuZHJvaWQudGVzdHNcIixcIk1heEJ1aWxkXCI6XCIyMDIxLTA3LTEwVDEyOjUxOjUwLjYxNTcxODYrMDI6MDBcIn0se1wiUHJvZHVjdFwiOlwieGFtc3ZnXCIsXCJPc1wiOlwiaW9zXCIsXCJBcHBJZFwiOlwiWGFtU3ZnLkRlbW8uRHJvaWRcIixcIk1heEJ1aWxkXCI6XCIyMDIxLTA3LTEwVDEyOjUxOjUwLjYxNTcxODYrMDI6MDBcIn0se1wiUHJvZHVjdFwiOlwieGFtc3ZnXCIsXCJPc1wiOlwiYW5kcm9pZFwiLFwiQXBwSWRcIjpcIlhhbVN2Zy5EZW1vLkRyb2lkXCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z1wiLFwiT3NcIjpcInV3cFwiLFwiQXBwSWRcIjpcIlhhbVN2Zy5EZW1vLkRyb2lkXCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z2Zvcm1zXCIsXCJPc1wiOlwiaW9zXCIsXCJBcHBJZFwiOlwiWGFtU3ZnLkRlbW8uRHJvaWRcIixcIk1heEJ1aWxkXCI6XCIyMDIxLTA3LTEwVDEyOjUxOjUwLjYxNTcxODYrMDI6MDBcIn0se1wiUHJvZHVjdFwiOlwieGFtc3ZnZm9ybXNcIixcIk9zXCI6XCJhbmRyb2lkXCIsXCJBcHBJZFwiOlwiWGFtU3ZnLkRlbW8uRHJvaWRcIixcIk1heEJ1aWxkXCI6XCIyMDIxLTA3LTEwVDEyOjUxOjUwLjYxNTcxODYrMDI6MDBcIn0se1wiUHJvZHVjdFwiOlwieGFtc3ZnZm9ybXNcIixcIk9zXCI6XCJ1d3BcIixcIkFwcElkXCI6XCJYYW1TdmcuRGVtby5Ecm9pZFwiLFwiTWF4QnVpbGRcIjpcIjIwMjEtMDctMTBUMTI6NTE6NTAuNjE1NzE4NiswMjowMFwifV19IiwibmJmIjoxNTk0Mzc4MzEwLCJleHAiOjE5MDk5MTExMTAsImlhdCI6MTU5NDM3ODMxMCwiaXNzIjoiaHR0cHM6Ly92YXBvbGlhLmV1L2F1dGhvcml0eSIsImF1ZCI6Imh0dHBzOi8vdmFwb2xpYS5ldS9hdXRob3JpdHkvbGljZW5zZXMifQ.FQbOEycOVjIEvMDWE2ZsfXRXo_yaDPEA4IHXqCKlpA9eHVwdj1B9wKjp-oMGWIaUw0ugHbgRAMWNXlQM30vWn5mGcqoDI_ANRyM7uQkgQ_ox_Wc9gZPXqcyM59NZnOAVBI8XjSPf6JvTStpXsjVu4I7IiP6U1TiOwVWeRO6_WqSOPbnpKXXO4DI8veAvNfeYPfBCyxCeASsewtBvY9sYbjKhYDbpbry-zrOZ8ayj178ewQ1lgeGROyArEjt9vhmeOGp0WFIM_THuiQ9oZXFf5tw54ImIuTKmfoM3yryBfVvGKhSkGfhTaC5u5ZuVPrsDLPgHDPiiXQrXknnhsSV5sg";
            var assembly = typeof(XamSvgDemo.Shared.App).GetTypeInfo().Assembly;
            XamSvg.Shared.Config.ResourceAssembly = assembly;
            XamSvg.Setup.InitSvgLib();
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
