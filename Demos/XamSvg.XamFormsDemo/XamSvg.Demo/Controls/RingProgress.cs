using System;
using System.Globalization;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using XamSvg.XamForms;

namespace XamSvg.Demo.Controls
{
    public class RingProgress : Grid
    {
        private readonly string svgTemplate;

        public static readonly BindableProperty ProgressProperty = BindableProperty.Create(nameof(Progress), typeof(double), typeof(RingProgress), 0.0);
        public static readonly BindableProperty ProgressMaxValueProperty = BindableProperty.Create(nameof(MaxValue), typeof(double), typeof(RingProgress), 100.0);

        public double Progress
        {
            get { return (double)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }

        public double MaxValue
        {
            get { return (double)GetValue(ProgressMaxValueProperty); }
            set { SetValue(ProgressMaxValueProperty, value); }
        }


        public RingProgress()
        {
            RowSpacing = 0;
            ColumnSpacing = 0;

            var back = new SvgImage {Svg = "res:images.0GoldMirror", HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Start};
            Children.Add(back);
            var cercleDegrade = new SvgImage { HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Start };

            svgTemplate = XamSvg.Shared.Utils.ResourceLoader.GetEmbeddedResourceString(typeof(App).GetTypeInfo().Assembly, "images.templates.ring.svg");
            cercleDegrade.Svg = BuildSvgRing(back);
            Children.Add(cercleDegrade);

            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == ProgressProperty.PropertyName)
                    cercleDegrade.Svg = BuildSvgRing(back);
            };
        }

        private string BuildSvgRing(View back)
        {
            var angle = 360*Progress/MaxValue % 360;
            if (angle <= 0)
                angle = 1;

            back.Rotation = -angle;
            var svgString = new StringBuilder(svgTemplate);

            var endpoint1 = angle>180 ? new Point(200,394) : PolarToCartesian(200,200,194,angle);
            svgString.Replace("ENDPOINT1", String.Format(CultureInfo.InvariantCulture, "{0:F5},{1:F5}", endpoint1.X, endpoint1.Y));

            if (angle < 180)
            {
                svgString.Replace("url(#gLeft)", "none");
                svgString.Replace("ENDPOINT2", "0,0");
            }
            else
            {
                var endpoint2 = PolarToCartesian(200, 200, 194, angle);
                svgString.Replace("ENDPOINT2", String.Format(CultureInfo.InvariantCulture, "{0:F5},{1:F5}", endpoint2.X, endpoint2.Y));
            }

            svgString.Insert(0, "string:");
            return svgString.ToString();
        }

        private Point PolarToCartesian(double centerX, double centerY, double radius, double angleInDegrees)
        {
            var angleInRadians = (angleInDegrees - 90)*Math.PI/180.0;
            var x = centerX + radius*Math.Cos(angleInRadians);
            var y = centerY + radius*Math.Sin(angleInRadians);
            return new Point(x, y);
        }
    }
}
