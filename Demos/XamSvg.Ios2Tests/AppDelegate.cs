using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Foundation;
using UIKit;
using XamSvg.Ios2Tests;
using XamSvgDemo.Shared;

namespace XamSvg.TouchTests
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        private UIWindow window;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //Initialize the cross platform color helper
	        Setup.InitSvgLib();
            XamSvg.Shared.Config.License = "eyJhbGciOiJSUzI1NiIsImtpZCI6InZhcG9saWFzaWciLCJ0eXAiOiJKV1QifQ.eyJodHRwczovL3NjaGVtYXMudmFwb2xpYS5ldS8yMDIwLzA1L2NsYWltcy9MaWNlbnNlc0NsYWltIjoie1wiTGljZW5zZXNcIjpbe1wiUHJvZHVjdFwiOlwieGFtc3ZnXCIsXCJPc1wiOlwiaW9zXCIsXCJBcHBJZFwiOlwiZnIudmFwb2xpYS5zdmd0ZXN0XCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z1wiLFwiT3NcIjpcImFuZHJvaWRcIixcIkFwcElkXCI6XCJmci52YXBvbGlhLnN2Z3Rlc3RcIixcIk1heEJ1aWxkXCI6XCIyMDIxLTA3LTEwVDEyOjUxOjUwLjYxNTcxODYrMDI6MDBcIn0se1wiUHJvZHVjdFwiOlwieGFtc3ZnXCIsXCJPc1wiOlwidXdwXCIsXCJBcHBJZFwiOlwiZnIudmFwb2xpYS5zdmd0ZXN0XCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z2Zvcm1zXCIsXCJPc1wiOlwiaW9zXCIsXCJBcHBJZFwiOlwiZnIudmFwb2xpYS5zdmd0ZXN0XCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z2Zvcm1zXCIsXCJPc1wiOlwiYW5kcm9pZFwiLFwiQXBwSWRcIjpcImZyLnZhcG9saWEuc3ZndGVzdFwiLFwiTWF4QnVpbGRcIjpcIjIwMjEtMDctMTBUMTI6NTE6NTAuNjE1NzE4NiswMjowMFwifSx7XCJQcm9kdWN0XCI6XCJ4YW1zdmdmb3Jtc1wiLFwiT3NcIjpcInV3cFwiLFwiQXBwSWRcIjpcImZyLnZhcG9saWEuc3ZndGVzdFwiLFwiTWF4QnVpbGRcIjpcIjIwMjEtMDctMTBUMTI6NTE6NTAuNjE1NzE4NiswMjowMFwifSx7XCJQcm9kdWN0XCI6XCJ4YW1zdmdcIixcIk9zXCI6XCJpb3NcIixcIkFwcElkXCI6XCJmci52YXBvbGlhLnN2Z2Zvcm10ZXN0XCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z1wiLFwiT3NcIjpcImFuZHJvaWRcIixcIkFwcElkXCI6XCJmci52YXBvbGlhLnN2Z2Zvcm10ZXN0XCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z1wiLFwiT3NcIjpcInV3cFwiLFwiQXBwSWRcIjpcImZyLnZhcG9saWEuc3ZnZm9ybXRlc3RcIixcIk1heEJ1aWxkXCI6XCIyMDIxLTA3LTEwVDEyOjUxOjUwLjYxNTcxODYrMDI6MDBcIn0se1wiUHJvZHVjdFwiOlwieGFtc3ZnZm9ybXNcIixcIk9zXCI6XCJpb3NcIixcIkFwcElkXCI6XCJmci52YXBvbGlhLnN2Z2Zvcm10ZXN0XCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z2Zvcm1zXCIsXCJPc1wiOlwiYW5kcm9pZFwiLFwiQXBwSWRcIjpcImZyLnZhcG9saWEuc3ZnZm9ybXRlc3RcIixcIk1heEJ1aWxkXCI6XCIyMDIxLTA3LTEwVDEyOjUxOjUwLjYxNTcxODYrMDI6MDBcIn0se1wiUHJvZHVjdFwiOlwieGFtc3ZnZm9ybXNcIixcIk9zXCI6XCJ1d3BcIixcIkFwcElkXCI6XCJmci52YXBvbGlhLnN2Z2Zvcm10ZXN0XCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z1wiLFwiT3NcIjpcImlvc1wiLFwiQXBwSWRcIjpcInhhbXN2Zy5kcm9pZC50ZXN0c1wiLFwiTWF4QnVpbGRcIjpcIjIwMjEtMDctMTBUMTI6NTE6NTAuNjE1NzE4NiswMjowMFwifSx7XCJQcm9kdWN0XCI6XCJ4YW1zdmdcIixcIk9zXCI6XCJhbmRyb2lkXCIsXCJBcHBJZFwiOlwieGFtc3ZnLmRyb2lkLnRlc3RzXCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z1wiLFwiT3NcIjpcInV3cFwiLFwiQXBwSWRcIjpcInhhbXN2Zy5kcm9pZC50ZXN0c1wiLFwiTWF4QnVpbGRcIjpcIjIwMjEtMDctMTBUMTI6NTE6NTAuNjE1NzE4NiswMjowMFwifSx7XCJQcm9kdWN0XCI6XCJ4YW1zdmdmb3Jtc1wiLFwiT3NcIjpcImlvc1wiLFwiQXBwSWRcIjpcInhhbXN2Zy5kcm9pZC50ZXN0c1wiLFwiTWF4QnVpbGRcIjpcIjIwMjEtMDctMTBUMTI6NTE6NTAuNjE1NzE4NiswMjowMFwifSx7XCJQcm9kdWN0XCI6XCJ4YW1zdmdmb3Jtc1wiLFwiT3NcIjpcImFuZHJvaWRcIixcIkFwcElkXCI6XCJ4YW1zdmcuZHJvaWQudGVzdHNcIixcIk1heEJ1aWxkXCI6XCIyMDIxLTA3LTEwVDEyOjUxOjUwLjYxNTcxODYrMDI6MDBcIn0se1wiUHJvZHVjdFwiOlwieGFtc3ZnZm9ybXNcIixcIk9zXCI6XCJ1d3BcIixcIkFwcElkXCI6XCJ4YW1zdmcuZHJvaWQudGVzdHNcIixcIk1heEJ1aWxkXCI6XCIyMDIxLTA3LTEwVDEyOjUxOjUwLjYxNTcxODYrMDI6MDBcIn0se1wiUHJvZHVjdFwiOlwieGFtc3ZnXCIsXCJPc1wiOlwiaW9zXCIsXCJBcHBJZFwiOlwiWGFtU3ZnLkRlbW8uRHJvaWRcIixcIk1heEJ1aWxkXCI6XCIyMDIxLTA3LTEwVDEyOjUxOjUwLjYxNTcxODYrMDI6MDBcIn0se1wiUHJvZHVjdFwiOlwieGFtc3ZnXCIsXCJPc1wiOlwiYW5kcm9pZFwiLFwiQXBwSWRcIjpcIlhhbVN2Zy5EZW1vLkRyb2lkXCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z1wiLFwiT3NcIjpcInV3cFwiLFwiQXBwSWRcIjpcIlhhbVN2Zy5EZW1vLkRyb2lkXCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z2Zvcm1zXCIsXCJPc1wiOlwiaW9zXCIsXCJBcHBJZFwiOlwiWGFtU3ZnLkRlbW8uRHJvaWRcIixcIk1heEJ1aWxkXCI6XCIyMDIxLTA3LTEwVDEyOjUxOjUwLjYxNTcxODYrMDI6MDBcIn0se1wiUHJvZHVjdFwiOlwieGFtc3ZnZm9ybXNcIixcIk9zXCI6XCJhbmRyb2lkXCIsXCJBcHBJZFwiOlwiWGFtU3ZnLkRlbW8uRHJvaWRcIixcIk1heEJ1aWxkXCI6XCIyMDIxLTA3LTEwVDEyOjUxOjUwLjYxNTcxODYrMDI6MDBcIn0se1wiUHJvZHVjdFwiOlwieGFtc3ZnZm9ybXNcIixcIk9zXCI6XCJ1d3BcIixcIkFwcElkXCI6XCJYYW1TdmcuRGVtby5Ecm9pZFwiLFwiTWF4QnVpbGRcIjpcIjIwMjEtMDctMTBUMTI6NTE6NTAuNjE1NzE4NiswMjowMFwifV19IiwibmJmIjoxNTk0Mzc4MzEwLCJleHAiOjE5MDk5MTExMTAsImlhdCI6MTU5NDM3ODMxMCwiaXNzIjoiaHR0cHM6Ly92YXBvbGlhLmV1L2F1dGhvcml0eSIsImF1ZCI6Imh0dHBzOi8vdmFwb2xpYS5ldS9hdXRob3JpdHkvbGljZW5zZXMifQ.FQbOEycOVjIEvMDWE2ZsfXRXo_yaDPEA4IHXqCKlpA9eHVwdj1B9wKjp-oMGWIaUw0ugHbgRAMWNXlQM30vWn5mGcqoDI_ANRyM7uQkgQ_ox_Wc9gZPXqcyM59NZnOAVBI8XjSPf6JvTStpXsjVu4I7IiP6U1TiOwVWeRO6_WqSOPbnpKXXO4DI8veAvNfeYPfBCyxCeASsewtBvY9sYbjKhYDbpbry-zrOZ8ayj178ewQ1lgeGROyArEjt9vhmeOGp0WFIM_THuiQ9oZXFf5tw54ImIuTKmfoM3yryBfVvGKhSkGfhTaC5u5ZuVPrsDLPgHDPiiXQrXknnhsSV5sg";

            //Tells XamSvg in which assembly to search for svg when "res:" is used
            XamSvg.Shared.Config.ResourceAssembly = typeof (App).GetTypeInfo().Assembly;

            window = new UIWindow(UIScreen.MainScreen.Bounds) { RootViewController = new UINavigationController(new MyViewController()) };
            window.MakeKeyAndVisible();
            HandleNotifications(options);

            return true;
        }

        //public override bool ContinueUserActivity(UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
        //{
        //    if (userActivity == null)
        //        return false;

        //    if (userActivity.ActivityType == NSUserActivityType.BrowsingWeb)
        //    {
        //        //Requires "Associated Domains" in app id at developer.apple.com
        //        //Does not work on simulators
        //        return OpenDeepLink(userActivity.WebPageUrl);
        //    }

        //    return false;
        //}

        /// <summary>
        /// Entry point if the app is running: 
        /// app launched from a safari URL, using a scheme registered in info.plist
        /// </summary>
        public override bool OpenUrl(UIApplication app, NSUrl url, string sourceApplication, NSObject annotation)
        {
            return OpenDeepLink(url);
        }

        /// <summary>
        /// Entry point if the app was not running
        /// </summary>
        private void HandleNotifications(NSDictionary options)
        {
            var userInfo = options;
            if (userInfo != null)
            {
                NSObject value;
                if (userInfo.TryGetValue(FromObject(UIApplication.LaunchOptionsUrlKey), out value))
                {
                    OpenDeepLink((NSUrl)value);
                }
            }
        }

        private bool OpenDeepLink(NSUrl url)
        {
            //var svgurl = url.LastPathComponent; //fails to correctly decode path
            var uri = url.ToString();
            var pathIndex = uri.LastIndexOf('/');
            var svgurl = Uri.UnescapeDataString(uri.Substring(pathIndex + 1));

            if (!String.IsNullOrWhiteSpace(svgurl))
            {
                var dontWait = MyViewController.TheViewController.LoadSvg(svgurl);
                return true;
            }

            return false;
        }
    }
}
