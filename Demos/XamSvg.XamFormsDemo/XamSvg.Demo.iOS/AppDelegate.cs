using Foundation;
using UIKit;
using XamSvg.XamForms.iOS;

namespace XamSvg.Demo.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //Required since Xamarin.Forms 1.4.3 
            //To be registered, the lib containing the custom renderer must be loaded before Forms.Init()
            //Also required by SvgImageBuilder to register its services
            SvgImageRenderer.InitializeForms();

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
