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
            
            //Tells XamSvg in which assembly to search for svg when "res:" is used
            XamSvg.Shared.Config.ResourceAssembly = typeof (App).GetTypeInfo().Assembly;

            window = new UIWindow(UIScreen.MainScreen.Bounds) {RootViewController = new MyViewController()};
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
