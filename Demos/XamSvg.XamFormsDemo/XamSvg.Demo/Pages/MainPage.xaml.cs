using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using XamSvg.Shared.Utils;
using XamSvg.XamForms;

namespace XamSvg.Demo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var model = new MainPageModel(Navigation);
            BindingContext = model;

            #region Set left icon on button (usin a svg)
            var svgRefresh = new SvgImageSource { Svg = "res:images.refresh", Height = 15 }; //ColorMapping = "ffffff=00ff00"
            //FileImageSource imageThatCanBeUsedInTabsWithACustomTabRenderer = svgRefresh.Image;
            ColorMappingSelectedButton.Image = svgRefresh.CreateFileImageSource();
            #endregion

            model.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(MainPageModel.Zoom))
                {
                    if (Math.Abs(model.ZoomValue)<float.Epsilon)
                        TheSvg.ViewportTransform = null;
                    else
                    {
                        //Rotation whose pivot point is at center
                        var matrix1 = DependencyService.Get<IMatrixFactory>().Create();
                        matrix1.Scale((float) (1 / model.ZoomValue), (float) (1 / model.ZoomValue));

                        var innerSize = TheSvg.InnerSize;
                        var matrix0 = DependencyService.Get<IMatrixFactory>().Create();
                        matrix0.Translate(-(float) (innerSize.Width / 2), -(float) (innerSize.Height / 2));
                        var matrix2 = DependencyService.Get<IMatrixFactory>().Create();
                        matrix2.Translate((float) (innerSize.Width / 2), (float) (innerSize.Height / 2));
                        matrix1.Concat(matrix0);
                        matrix2.Concat(matrix1);

                        TheSvg.ViewportTransform = matrix2;
                    }
                }
                else if (args.PropertyName == nameof(MainPageModel.Translation))
                {
                    if (model.Translation.IsEmpty)
                        TheSvg.ViewportTransform = null;
                    else
                    {
                        //Rotation whose pivot point is at center
                        var matrix = DependencyService.Get<IMatrixFactory>().Create();
                        var innerSize = TheSvg.InnerSize;
                        var translation = model.Translation;
                        matrix.Translate((float)(-translation.X*innerSize.Width/TheSvg.Width), (float)(-translation.Y * innerSize.Height/ TheSvg.Height));
                        TheSvg.ViewportTransform = matrix;
                    }
                }
            };
        }
    }

    public class MainPageModel : BindableObject
    {
        private readonly string[] names;
        private readonly INavigation navigation;
        private int i;

        public MainPageModel(INavigation navigation)
        {
            this.navigation = navigation;
            names = typeof(App).GetTypeInfo().Assembly.GetManifestResourceNames()
                .Where(n => n.EndsWith(".svg")).OrderBy(n => n).ToArray();
        }

        #region properties
        public string ColorMapping { get { return colorMapping; } set { colorMapping = value; OnPropertyChanged(); } }
        private string colorMapping;

        public string ImageName { get { return imageName; } set { imageName = value; OnPropertyChanged(); } }
        private string imageName = "res:images.000myprofil";

        public double Zoom { get { return zoom; } set { zoom = value; OnPropertyChanged(); OnPropertyChanged(nameof(ZoomText)); } }
        private double zoom = 0;
        public double ZoomValue => Math.Pow(10, zoom);

        public string ZoomText => $"{ZoomValue:F1}";

        public Point Translation { get { return translation; } set { translation = value; OnPropertyChanged(); } }
        private Point translation;

        #endregion

        #region commands
        public Command NextImageCommand => new Command(() =>
        {
            Zoom = 0;
            ImageName = $"res:{names[i]}";
            i = ++i % names.Length;
        });

        public Command ColorMappingCommand => new Command(() =>
        {
            var rand = new Random();
            var bytes = new byte[3];
            rand.NextBytes(bytes);
            ColorMapping = $"ffffff=00ff00;000000={bytes[0]:X2}{bytes[1]:X2}{bytes[2]:X2}";
        });

        public Command<Point> PanCommand => new Command<Point>(offset =>
        {
            Translation = offset;
        });

        public Command OpenVapoliaCommand => new Command(async () =>
        {
            await navigation.PushAsync(new ContentPage { Content = new WebView { Source = new UrlWebViewSource { Url = "https://vapolia.fr" } }});
        });
        #endregion
    }
}
