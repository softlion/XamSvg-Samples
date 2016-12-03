using System;
using System.Linq;
using System.Reflection;
using Android.App;
using Android.OS;
using Android.Util;
using XamSvg;
using XamSvgDemo.Shared;

namespace XamSvgTests
{
	[Activity (Label = "XamSvgTests", MainLauncher = true, Theme = "@android:style/Theme.Holo.Light.NoActionBar.Fullscreen")]
	public class MainActivity : Activity
	{
	    protected override void OnCreate(Bundle bundle)
	    {
	        base.OnCreate(bundle);

            //Initialize the cross platform color helper
	        Setup.InitSvgLib();
            
            //Tells XamSvg in which assembly to search for svg when "res:" is used
	        var assembly = typeof (App).GetTypeInfo().Assembly;
            XamSvg.Shared.Config.ResourceAssembly = assembly;



	        SetContentView(Resource.Layout.Main);

            //Get all svg resources in the shared assembly
            var svgShared = assembly.GetManifestResourceNames().Where(n => n.EndsWith(".svg")).OrderBy(n => n).ToList();

            //Get all svg resource ids in the raw folder
            var rawIds = typeof(Resource.Raw).GetFields().Where(f => f.IsLiteral).Select(f => Tuple.Create((int)f.GetRawConstantValue(),f.Name)).ToList();

            ////Get all drawing zones in the current layout
            //var drawableViewIds = typeof(Resource.Id).GetFields().Where(f => f.IsLiteral && f.Name.StartsWith("drawable")).Select(f => (int)f.GetRawConstantValue());



            int index=0;
            var svg = FindViewById<SvgImageView>(Resource.Id.icon);
            EventHandler nextImage = (sender, e) =>
            {
                if (index < svgShared.Count)
                {
                    Log.Debug("svg", "displaying res:" + svgShared[index]);
                    svg.Svg = "res:" + svgShared[index];
                }
                else
                {
                    Log.Debug("svg", "displaying raw/" + rawIds[index - svgShared.Count].Item2);
                    svg.SetSvg(this, rawIds[index - svgShared.Count].Item1);
                }

                index = (++index)%(rawIds.Count + svgShared.Count);
            };

            //When clicked, change the svg source in all zones
            var contentView = FindViewById(Resource.Id.content);
            var btnNextImage = FindViewById(Resource.Id.btnNextImage);
            btnNextImage.Click += nextImage;
            contentView.Click += nextImage;

            //refresh demo
            var btnGoEmpty = FindViewById(Resource.Id.btnGoEmpty);
            btnGoEmpty.Click += (sender, e) => StartActivity(typeof(EmptyActivity));

//            //Test using a string
//            var bitmap = LoadLastSvgFromString();
//            FindViewById<ImageView>(Resource.Id.bitmap1).SetImageBitmap(bitmap);
        }

	    //void LoadImageTest(int rawId)
     //   {
     //       foreach (var drawableId in drawableViewIds)
     //       {
     //           //if (drawableID != Resource.Id.drawable100)
     //           //    continue;

     //           var v = FindViewById<ImageView>(drawableId);
     //           var drawable = SvgFactory.GetDrawable(Resources, rawId);
     //           v.SetImageDrawable(drawable);
     //       }

     //       using (var bitmap1 = SvgFactory.GetBitmap(Resources, rawId, width: 40, height: 69))
     //           FindViewById<ImageView>(Resource.Id.bitmap1).SetImageBitmap(bitmap1);
     //       using (var bitmap2 = SvgFactory.GetBitmap(Resources, rawId, width: 98, height: 98))
     //           FindViewById<ImageView>(Resource.Id.bitmap2).SetImageBitmap(bitmap2);
     //       using (var bitmap3 = SvgFactory.GetBitmap(Resources, rawId, width: 47, height: 32))
     //           FindViewById<ImageView>(Resource.Id.bitmap3).SetImageBitmap(bitmap3);
     //   }



        //private Bitmap LoadLastSvgFromString()
        //{
        //    const string svgString = @"<svg width=""100"" height=""100""><circle cx=""50"" cy=""50"" r=""50"" style=""fill:#ff0000"" /></svg>";
        //    var image = SvgFactory.GetBitmap(new StringReader(svgString),100,100);
        //    return image;
        //}

	}
}
