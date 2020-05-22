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
            XamSvg.Shared.Config.License = "eyJhbGciOiJSUzI1NiIsImtpZCI6InZhcG9saWFzaWciLCJ0eXAiOiJKV1QifQ.eyJ1bmlxdWVfbmFtZSI6ImU1ZjRmODZlOGY4OTRjOTI4MmFkZDMyMWNjZTVkYjgxIiwiaHR0cHM6Ly9zY2hlbWFzLnZhcG9saWEuZXUvMjAyMC8wNS9jbGFpbXMvTWF4QnVpbGREYXRlQ2xhaW0iOiIyMDIxLTA1LTEzVDA4OjAxOjE2LjYzNjc2MiswMjowMCIsImh0dHBzOi8vc2NoZW1hcy52YXBvbGlhLmV1LzIwMjAvMDUvY2xhaW1zL1Byb2R1Y3RDb2RlQ2xhaW0iOlsieGFtc3ZnIiwieGFtc3ZnZm9ybXMiXSwiaHR0cHM6Ly9zY2hlbWFzLnZhcG9saWEuZXUvMjAyMC8wNS9jbGFpbXMvQXBwSWRDbGFpbSI6WyJmci52YXBvbGlhLnN2Z3Rlc3QiLCJmci52YXBvbGlhLnN2Z2Zvcm10ZXN0IiwieGFtc3ZnLmRyb2lkLnRlc3RzIiwiWGFtU3ZnLkRlbW8uRHJvaWQiXSwiaHR0cHM6Ly9zY2hlbWFzLnZhcG9saWEuZXUvMjAyMC8wNS9jbGFpbXMvT3NDbGFpbSI6WyJpb3MiLCJhbmRyb2lkIiwidXdwIl0sIm5iZiI6MTU4OTM0OTY3NiwiZXhwIjoxOTA0ODgyNDc2LCJpYXQiOjE1ODkzNDk2NzYsImlzcyI6Imh0dHBzOi8vdmFwb2xpYS5ldS9hdXRob3JpdHkiLCJhdWQiOiJodHRwczovL3ZhcG9saWEuZXUvYXV0aG9yaXR5L2xpY2Vuc2VzIn0.r9SLG24WPQM7mgWNXBP-51IHSYdNcuAMNN8vhWP5hYWip8dWzUQRvI7U0D2z5-w8i8WTrbwkFc3s0R8plF7SB02CeXzTYEDmYhu-tUWnicC_0OrEsfmsQK0HyUyd8jEaehNH7IB5EpgwPG9-8k2RbsXg0803uacnjx7WoEYTwdb8vpxVuCHi9opCReHHL1gztElFN1aXwHbiyle_AqsX9seBKFKQxgi5jXWFSi4blGuwLEe44GWnzyJAAZQcK_jYUDC2PGkcVFBDeyIROmPAmq4_4nEeYrQWF80tPmsbqHNcqR9_lwZUi_ZThtrc-iCwfIIY-r8DFFDP_hnqTmXIkg";

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
