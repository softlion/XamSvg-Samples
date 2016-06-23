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
    public partial class AppDelegate : UIApplicationDelegate
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
            return true;
        }
    }
}

