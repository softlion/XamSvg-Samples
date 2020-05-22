using Android.App;
using Android.Content.PM;
using Android.OS;
using Vapolia.Droid.Lib.Effects;


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

            PlatformGestureEffect.Init();

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

