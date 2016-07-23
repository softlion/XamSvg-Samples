﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using XamSvg.XamForms.Droid;


namespace XamSvg.Demo.Droid
{
    [Activity(Label = "XamSvg Demo", MainLauncher = true, Icon = "@drawable/icon", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //Required by SvgImageBuilder to register its services
            SvgImageRenderer.InitializeForms();

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        /// <summary>
        /// Overrides this to enable SVG in toolbar icons.
        /// </summary>
        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            //don't call base version. 
            //var ok=base.OnPrepareOptionsMenu(menu); 
            SvgImageRenderer.PrepareMenu(this, menu);
            return true;
        }
    }
}
