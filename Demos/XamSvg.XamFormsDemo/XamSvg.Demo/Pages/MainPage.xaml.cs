using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
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
                if (args.PropertyName == nameof(MainPageModel.Zoom) || args.PropertyName == nameof(MainPageModel.Translation))
                {
                    var emptyZoom = Math.Abs(model.ZoomValue) < float.Epsilon;
                    var emptyPan = model.Translation.IsEmpty;
                    if (emptyZoom && emptyPan)
                        TheSvg.ViewportTransform = null;
                    else
                    {
                        var matrixFinal = DependencyService.Get<IMatrixFactory>().Create();

                        if (!emptyPan)
                        {
                            var matrix = DependencyService.Get<IMatrixFactory>().Create();
                            var innerSize = TheSvg.InnerSize;
                            var translation = model.Translation;
                            matrix.Translate(
                                (float)(-translation.X * innerSize.Width / (TheSvg.Width* model.ZoomValue)), 
                                (float)(-translation.Y * innerSize.Height / (TheSvg.Height * model.ZoomValue)));

                            matrixFinal.Concat(matrix);
                        }

                        if (!emptyZoom)
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
                            matrixFinal.Concat(matrix2);
                        }

                        TheSvg.ViewportTransform = matrixFinal;
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
        private CancellationTokenSource cancelLoadSvg = new CancellationTokenSource();

        public MainPageModel(INavigation navigation)
        {
            this.navigation = navigation;
            names = typeof(App).GetTypeInfo().Assembly.GetManifestResourceNames()
                .Where(n => n.EndsWith(".svg")).OrderBy(n => n).ToArray();

            MessagingCenter.Subscribe<string, string>(this, MessagingCenterConst.OpenDeepLink, async (sender, args) =>
            {
                await LoadSvg(args);
            });
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

        public bool IsTranslationEnabled { get { return isTranslationEnabled; } set { isTranslationEnabled = value; OnPropertyChanged(); } }
        private bool isTranslationEnabled;        
        #endregion

        #region commands
        public Command NextByTapImageCommand => new Command(() =>
        {
            if (isTranslationEnabled)
            {
                ResetPosition();
                i = FixIndex(++i);
                ImageName = $"res:{names[i]}";
            }
        });

        public Command NextImageCommand => new Command(() =>
        {
            if (!isTranslationEnabled)
            {
                ResetPosition();
                i = FixIndex(++i);
                ImageName = $"res:{names[i]}";
            }
        });

        public Command PrevImageCommand => new Command(() =>
        {
            if (!isTranslationEnabled)
            {
                ResetPosition();
                i = FixIndex(--i);
                ImageName = $"res:{names[i]}";
            }
        });

        private int FixIndex(int index)
        {
            return (index + names.Length) % names.Length;
        }

        public Command<Point> PanCommand => new Command<Point>(offset =>
        {
            if (isTranslationEnabled)
                Translation = offset;
        });

        public Command ColorMappingCommand => new Command(() =>
        {
            var rand = new Random();
            var bytes = new byte[3];
            rand.NextBytes(bytes);
            ColorMapping = $"ffffff=00ff00;000000={bytes[0]:X2}{bytes[1]:X2}{bytes[2]:X2}";
        });


        public Command OpenVapoliaCommand => new Command(async () =>
        {
            //await navigation.PushAsync(new ContentPage { Content = new WebView
            //{
            //    Source = new UrlWebViewSource { Url = "http://vapolia.fr" },
            //    HorizontalOptions = LayoutOptions.Fill,
            //    VerticalOptions = LayoutOptions.Fill
            //}});
        });
        #endregion

        private async Task LoadSvg(string url)
        {
            cancelLoadSvg.Cancel();
            var c = cancelLoadSvg = new CancellationTokenSource();

            //title.Text = $"Loading {url}";
            //title.TextColor = UIColor.White;
            using (var client = new HttpClient())
            {
                try
                {
                    var result = await client.GetAsync(url, c.Token);
                    if (!result.IsSuccessStatusCode)
                        throw new WebException();
                    var svgString = await result.Content.ReadAsStringAsync();

                    ResetPosition();
                    ImageName = "string:" + svgString;
                    //title.Text = $"Displayed {url}";
                }
                catch (Exception e)
                {
                    //title.Text = $"Error loading url: {e.Message}";
                    //title.TextColor = UIColor.Red;
                }
            }
        }

        private void ResetPosition()
        {
            if(!String.IsNullOrWhiteSpace(ColorMapping))
                ColorMapping = "";
            translation = Point.Zero;
            Zoom = 0;
        }
    }
}
