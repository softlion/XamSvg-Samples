using System;
using System.Threading;
using Android.Content;
using Android.Support.Design.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;
using XamSvg.Demo.Droid;
using XamSvg.XamForms;

[assembly:ExportRenderer(typeof(TabbedPage), typeof(SvgTabbedPageRenderer))]

namespace XamSvg.Demo.Droid
{
    /// <summary>
    /// SetTabIcon does not use SvgImageSourceHandler
    /// https://github.com/xamarin/Xamarin.Forms/blob/master/Xamarin.Forms.Platform.Android/AppCompat/TabbedPageRenderer.cs
    /// </summary>
    public class SvgTabbedPageRenderer : TabbedPageRenderer
    {
        public SvgTabbedPageRenderer(Context context) : base(context)
        {
        }

        protected override void SetTabIcon(TabLayout.Tab tab, FileImageSource icon)
        {
            if (icon.File.StartsWith("xamsvg:"))
            {
                var svgImageSource = new SvgImageSource(icon.File);
                var colorMapper = String.IsNullOrWhiteSpace(svgImageSource.ColorMapping) ? null : SvgColorMapperFactory.FromString(svgImageSource.ColorMapping);
                tab.SetIcon(SvgFactory.GetDrawable(Context, svgImageSource.Svg, CancellationToken.None, colorMapper, svgImageSource.FillMode, null));
            }
            else
                base.SetTabIcon(tab, icon);
        }
    }
}
