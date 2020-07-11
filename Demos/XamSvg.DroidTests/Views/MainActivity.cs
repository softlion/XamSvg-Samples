using System;
using System.Linq;
using System.Reflection;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Widget;
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
            XamSvg.Shared.Config.License = "eyJhbGciOiJSUzI1NiIsImtpZCI6InZhcG9saWFzaWciLCJ0eXAiOiJKV1QifQ.eyJodHRwczovL3NjaGVtYXMudmFwb2xpYS5ldS8yMDIwLzA1L2NsYWltcy9MaWNlbnNlc0NsYWltIjoie1wiTGljZW5zZXNcIjpbe1wiUHJvZHVjdFwiOlwieGFtc3ZnXCIsXCJPc1wiOlwiaW9zXCIsXCJBcHBJZFwiOlwiZnIudmFwb2xpYS5zdmd0ZXN0XCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z1wiLFwiT3NcIjpcImFuZHJvaWRcIixcIkFwcElkXCI6XCJmci52YXBvbGlhLnN2Z3Rlc3RcIixcIk1heEJ1aWxkXCI6XCIyMDIxLTA3LTEwVDEyOjUxOjUwLjYxNTcxODYrMDI6MDBcIn0se1wiUHJvZHVjdFwiOlwieGFtc3ZnXCIsXCJPc1wiOlwidXdwXCIsXCJBcHBJZFwiOlwiZnIudmFwb2xpYS5zdmd0ZXN0XCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z2Zvcm1zXCIsXCJPc1wiOlwiaW9zXCIsXCJBcHBJZFwiOlwiZnIudmFwb2xpYS5zdmd0ZXN0XCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z2Zvcm1zXCIsXCJPc1wiOlwiYW5kcm9pZFwiLFwiQXBwSWRcIjpcImZyLnZhcG9saWEuc3ZndGVzdFwiLFwiTWF4QnVpbGRcIjpcIjIwMjEtMDctMTBUMTI6NTE6NTAuNjE1NzE4NiswMjowMFwifSx7XCJQcm9kdWN0XCI6XCJ4YW1zdmdmb3Jtc1wiLFwiT3NcIjpcInV3cFwiLFwiQXBwSWRcIjpcImZyLnZhcG9saWEuc3ZndGVzdFwiLFwiTWF4QnVpbGRcIjpcIjIwMjEtMDctMTBUMTI6NTE6NTAuNjE1NzE4NiswMjowMFwifSx7XCJQcm9kdWN0XCI6XCJ4YW1zdmdcIixcIk9zXCI6XCJpb3NcIixcIkFwcElkXCI6XCJmci52YXBvbGlhLnN2Z2Zvcm10ZXN0XCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z1wiLFwiT3NcIjpcImFuZHJvaWRcIixcIkFwcElkXCI6XCJmci52YXBvbGlhLnN2Z2Zvcm10ZXN0XCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z1wiLFwiT3NcIjpcInV3cFwiLFwiQXBwSWRcIjpcImZyLnZhcG9saWEuc3ZnZm9ybXRlc3RcIixcIk1heEJ1aWxkXCI6XCIyMDIxLTA3LTEwVDEyOjUxOjUwLjYxNTcxODYrMDI6MDBcIn0se1wiUHJvZHVjdFwiOlwieGFtc3ZnZm9ybXNcIixcIk9zXCI6XCJpb3NcIixcIkFwcElkXCI6XCJmci52YXBvbGlhLnN2Z2Zvcm10ZXN0XCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z2Zvcm1zXCIsXCJPc1wiOlwiYW5kcm9pZFwiLFwiQXBwSWRcIjpcImZyLnZhcG9saWEuc3ZnZm9ybXRlc3RcIixcIk1heEJ1aWxkXCI6XCIyMDIxLTA3LTEwVDEyOjUxOjUwLjYxNTcxODYrMDI6MDBcIn0se1wiUHJvZHVjdFwiOlwieGFtc3ZnZm9ybXNcIixcIk9zXCI6XCJ1d3BcIixcIkFwcElkXCI6XCJmci52YXBvbGlhLnN2Z2Zvcm10ZXN0XCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z1wiLFwiT3NcIjpcImlvc1wiLFwiQXBwSWRcIjpcInhhbXN2Zy5kcm9pZC50ZXN0c1wiLFwiTWF4QnVpbGRcIjpcIjIwMjEtMDctMTBUMTI6NTE6NTAuNjE1NzE4NiswMjowMFwifSx7XCJQcm9kdWN0XCI6XCJ4YW1zdmdcIixcIk9zXCI6XCJhbmRyb2lkXCIsXCJBcHBJZFwiOlwieGFtc3ZnLmRyb2lkLnRlc3RzXCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z1wiLFwiT3NcIjpcInV3cFwiLFwiQXBwSWRcIjpcInhhbXN2Zy5kcm9pZC50ZXN0c1wiLFwiTWF4QnVpbGRcIjpcIjIwMjEtMDctMTBUMTI6NTE6NTAuNjE1NzE4NiswMjowMFwifSx7XCJQcm9kdWN0XCI6XCJ4YW1zdmdmb3Jtc1wiLFwiT3NcIjpcImlvc1wiLFwiQXBwSWRcIjpcInhhbXN2Zy5kcm9pZC50ZXN0c1wiLFwiTWF4QnVpbGRcIjpcIjIwMjEtMDctMTBUMTI6NTE6NTAuNjE1NzE4NiswMjowMFwifSx7XCJQcm9kdWN0XCI6XCJ4YW1zdmdmb3Jtc1wiLFwiT3NcIjpcImFuZHJvaWRcIixcIkFwcElkXCI6XCJ4YW1zdmcuZHJvaWQudGVzdHNcIixcIk1heEJ1aWxkXCI6XCIyMDIxLTA3LTEwVDEyOjUxOjUwLjYxNTcxODYrMDI6MDBcIn0se1wiUHJvZHVjdFwiOlwieGFtc3ZnZm9ybXNcIixcIk9zXCI6XCJ1d3BcIixcIkFwcElkXCI6XCJ4YW1zdmcuZHJvaWQudGVzdHNcIixcIk1heEJ1aWxkXCI6XCIyMDIxLTA3LTEwVDEyOjUxOjUwLjYxNTcxODYrMDI6MDBcIn0se1wiUHJvZHVjdFwiOlwieGFtc3ZnXCIsXCJPc1wiOlwiaW9zXCIsXCJBcHBJZFwiOlwiWGFtU3ZnLkRlbW8uRHJvaWRcIixcIk1heEJ1aWxkXCI6XCIyMDIxLTA3LTEwVDEyOjUxOjUwLjYxNTcxODYrMDI6MDBcIn0se1wiUHJvZHVjdFwiOlwieGFtc3ZnXCIsXCJPc1wiOlwiYW5kcm9pZFwiLFwiQXBwSWRcIjpcIlhhbVN2Zy5EZW1vLkRyb2lkXCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z1wiLFwiT3NcIjpcInV3cFwiLFwiQXBwSWRcIjpcIlhhbVN2Zy5EZW1vLkRyb2lkXCIsXCJNYXhCdWlsZFwiOlwiMjAyMS0wNy0xMFQxMjo1MTo1MC42MTU3MTg2KzAyOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z2Zvcm1zXCIsXCJPc1wiOlwiaW9zXCIsXCJBcHBJZFwiOlwiWGFtU3ZnLkRlbW8uRHJvaWRcIixcIk1heEJ1aWxkXCI6XCIyMDIxLTA3LTEwVDEyOjUxOjUwLjYxNTcxODYrMDI6MDBcIn0se1wiUHJvZHVjdFwiOlwieGFtc3ZnZm9ybXNcIixcIk9zXCI6XCJhbmRyb2lkXCIsXCJBcHBJZFwiOlwiWGFtU3ZnLkRlbW8uRHJvaWRcIixcIk1heEJ1aWxkXCI6XCIyMDIxLTA3LTEwVDEyOjUxOjUwLjYxNTcxODYrMDI6MDBcIn0se1wiUHJvZHVjdFwiOlwieGFtc3ZnZm9ybXNcIixcIk9zXCI6XCJ1d3BcIixcIkFwcElkXCI6XCJYYW1TdmcuRGVtby5Ecm9pZFwiLFwiTWF4QnVpbGRcIjpcIjIwMjEtMDctMTBUMTI6NTE6NTAuNjE1NzE4NiswMjowMFwifV19IiwibmJmIjoxNTk0Mzc4MzEwLCJleHAiOjE5MDk5MTExMTAsImlhdCI6MTU5NDM3ODMxMCwiaXNzIjoiaHR0cHM6Ly92YXBvbGlhLmV1L2F1dGhvcml0eSIsImF1ZCI6Imh0dHBzOi8vdmFwb2xpYS5ldS9hdXRob3JpdHkvbGljZW5zZXMifQ.FQbOEycOVjIEvMDWE2ZsfXRXo_yaDPEA4IHXqCKlpA9eHVwdj1B9wKjp-oMGWIaUw0ugHbgRAMWNXlQM30vWn5mGcqoDI_ANRyM7uQkgQ_ox_Wc9gZPXqcyM59NZnOAVBI8XjSPf6JvTStpXsjVu4I7IiP6U1TiOwVWeRO6_WqSOPbnpKXXO4DI8veAvNfeYPfBCyxCeASsewtBvY9sYbjKhYDbpbry-zrOZ8ayj178ewQ1lgeGROyArEjt9vhmeOGp0WFIM_THuiQ9oZXFf5tw54ImIuTKmfoM3yryBfVvGKhSkGfhTaC5u5ZuVPrsDLPgHDPiiXQrXknnhsSV5sg";
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
