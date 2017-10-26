using System;
using Foundation;
using UIKit;
using Vapolia.Ios.Lib.Effects;
using Xamarin.Forms;
using XamSvg.XamForms.iOS;

namespace XamSvg.Demo.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //To be registered by Xamarin Forms, assemblies containing xamarin forms plugins have to be loaded before Forms.Init()
            SvgImageRenderer.InitializeForms();
            PlatformGestureEffect.Init();

            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App());
            HandleNotifications(options);
            return base.FinishedLaunching(app, options);
        }

        #region Handle deep links
        /// <summary>
        /// Entry point if the app is running
        /// </summary>
        public override bool OpenUrl(UIApplication app, NSUrl url, string sourceApplication, NSObject annotation)
        {
            if (OpenDeepLink(url))
                return true;
            return base.OpenUrl(app, url, sourceApplication, annotation);
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            if (OpenDeepLink(url))
                return true;
            return base.OpenUrl(app, url, options);
        }

        /// <summary>
        /// Entry point if the app was not running
        /// </summary>
        private void HandleNotifications(NSDictionary options)
        {
            if (options != null)
            {
                if (options.TryGetValue(FromObject(UIApplication.LaunchOptionsUrlKey), out var value))
                    OpenDeepLink((NSUrl)value);
            }
        }

        private bool OpenDeepLink(NSUrl url)
        {
            if (url == null)
                return false;

            //var svgurl = url.LastPathComponent; //fails to correctly decode path
            var uri = url.ToString();
            var pathIndex = uri.LastIndexOf('/');
            var svgurl = Uri.UnescapeDataString(uri.Substring(pathIndex + 1));

            if (!String.IsNullOrWhiteSpace(svgurl))
            {
                MessagingCenter.Instance.Send(String.Empty, MessagingCenterConst.OpenDeepLink, svgurl);
                return true;
            }

            return false;
        }
        #endregion
    }
}
