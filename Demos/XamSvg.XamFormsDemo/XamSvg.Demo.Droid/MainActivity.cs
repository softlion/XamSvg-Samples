using Android.App;
using Android.OS;
using Android.Runtime;
using Vapolia.Droid.Lib.Effects;

namespace XamSvg.Demo.Droid
{
    [Activity(Label = "XamSvg Demo", Theme = "@style/MainTheme", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Xamarin.Essentials.Platform.Init(this, bundle);

            PlatformGestureEffect.Init();

            global::Xamarin.Forms.Forms.Init(this, bundle);
            XF.Material.Droid.Material.Init(this, bundle);

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}