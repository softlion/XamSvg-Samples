using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Text;
using Android.Util;
using Android.Widget;
using Java.Lang;
using XamSvg;
using XamSvgDemo.Shared;

namespace XamSvgTests
{
	[Activity (Label = "XamSvgTests", MainLauncher = true)]
	public class MainActivity : Activity
	{
	    protected override void OnCreate(Bundle bundle)
	    {
	        base.OnCreate(bundle);

            var ok = (Application.ApplicationInfo.Flags & Android.Content.PM.ApplicationInfoFlags.SupportsRtl) != 0;

            //Initialize the cross platform color helper
	        Setup.InitSvgLib();

            //Tells XamSvg in which assembly to search for svg when "res:" is used
	        var assembly = typeof (App).GetTypeInfo().Assembly;
            XamSvg.Shared.Config.ResourceAssembly = assembly;
            //You can also set a list of assemblies
            //XamSvg.Shared.Config.ResourceAssemblies = new List<Assembly> { assembly };

	        SetContentView(Resource.Layout.Main);

            //Get all svg resources in the shared assembly
            var svgShared = assembly.GetManifestResourceNames().Where(n => n.EndsWith(".svg")).OrderBy(n => n).ToList();

            //Get all svg resource ids in the raw folder
            var rawIds = typeof(Resource.Raw).GetFields().Where(f => f.IsLiteral).Select(f => Tuple.Create((int)f.GetRawConstantValue(),f.Name)).ToList();

            ////Get all drawing zones in the current layout
            //var drawableViewIds = typeof(Resource.Id).GetFields().Where(f => f.IsLiteral && f.Name.StartsWith("drawable")).Select(f => (int)f.GetRawConstantValue());

            //Test colorMappingDisabled
	        var svgBack = FindViewById<SvgImageView>(Resource.Id.back);
	        svgBack.Enabled = false;

            int index =0;
            var svg = FindViewById<SvgImageView>(Resource.Id.icon);

	        void NextImage(object sender, EventArgs e)
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

	            index = (++index) % (rawIds.Count + svgShared.Count);
	        }

            //When clicked, change the svg source in all zones
            var contentView = FindViewById(Resource.Id.content);
            var btnNextImage = FindViewById(Resource.Id.btnNextImage);
            btnNextImage.Click += NextImage;
            contentView.Click += NextImage;

            //refresh demo
            var btnGoEmpty = FindViewById(Resource.Id.btnGoEmpty);
            btnGoEmpty.Click += (sender, e) => StartActivity(typeof(EmptyActivity));

	        var btnList = FindViewById(Resource.Id.btnList);
	        btnList.Click += (sender, e) => StartActivity(typeof(CollectionActivity));

	        int btnLeftI = -1;
	        var btnLeft = FindViewById<SvgImageView>(Resource.Id.btnLeft);
	        btnLeft.Click += (sender, args) =>
	        {
	            btnLeftI = (btnLeftI+1)%3;
	            switch (btnLeftI)
	            {
	                case 0:
	                    btnLeft.Clickable = true;
	                    btnLeft.Selected = true;
	                    break;
	                case 1:
	                    btnLeft.Selected = false;
	                    btnLeft.Enabled = false;
	                    break;
	                case 2:
	                    btnLeft.Enabled = true;
	                    btnLeft.Clickable = false;
	                    break;
	            }
	        };

	        ////Test using a string
	        //var bitmap = LoadLastSvgFromString();
	        //FindViewById<ImageView>(Resource.Id.bitmap1).SetImageBitmap(bitmap);

            //Test svg as tab icons
            var tabIcons = new[] { "res:images.tabChannels", "res:images.tabNetwork", "res:images.tabSettings" };
            var tabs = FindViewById<TabLayout>(Resource.Id.tabs);
            
            var tabView = LayoutInflater.Inflate(Resource.Layout.tab_withicon, null);
            tabView.FindViewById<TextView>(Resource.Id.title).SetText("Tab1", TextView.BufferType.Normal);
            tabView.FindViewById<SvgImageView>(Resource.Id.icon).Svg = tabIcons[0];
            tabs.AddTab(tabs.NewTab().SetCustomView(tabView),true);

            tabView = LayoutInflater.Inflate(Resource.Layout.tab_withicon, null);
            tabView.FindViewById<TextView>(Resource.Id.title).SetText("Tab2", TextView.BufferType.Normal);
            tabView.FindViewById<SvgImageView>(Resource.Id.icon).Svg = tabIcons[1];
            tabs.AddTab(tabs.NewTab().SetCustomView(tabView),false);

            tabView = LayoutInflater.Inflate(Resource.Layout.tab_withicon, null);
            tabView.FindViewById<TextView>(Resource.Id.title).SetText("Tab3", TextView.BufferType.Normal);
            tabView.FindViewById<SvgImageView>(Resource.Id.icon).Svg = tabIcons[2];
            tabs.AddTab(tabs.NewTab().SetCustomView(tabView),false);
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
