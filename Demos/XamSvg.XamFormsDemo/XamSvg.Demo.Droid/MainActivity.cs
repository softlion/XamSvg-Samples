using Android.App;
using Android.Content.PM;
using Android.OS;
using Vapolia.Droid.Lib.Effects;
using XamSvg.XamForms.Droid;


namespace XamSvg.Demo.Droid
{
    /// <summary>
    /// FormsAppCompatActivity required to display icons on tabs (instead of FormsApplicationActivity)
    /// </summary>
    [Activity(Label = "XamSvg Demo", MainLauncher = true, Icon = "@drawable/icon", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            ToolbarResource = Resource.Layout.Toolbar;
            TabLayoutResource = Resource.Layout.Tabbar;
            base.OnCreate(bundle);

            //Required by SvgImageBuilder to register its services
            SvgImageRenderer.InitializeForms();

#if DEBUG
            XamSvg.Shared.Config.NativeLogger = new XamSvg.Platform.LoggerImpl {TraceEnabled = true};
#endif

            PlatformGestureEffect.Init();

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

