using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamSvg.Shared.Utils;

namespace XamSvg.Demo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var model = new MainPageModel(Navigation);
            BindingContext = model;

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

        private void PanGestureRecognizer_OnPanUpdated(object sender, PanUpdatedEventArgs e)
            => ((MainPageModel)BindingContext).PanCommand.Execute(new Point(e.TotalX, e.TotalY));
    }

    public class MainPageModel : BindableObject
    {
        private readonly string[] names;
        private readonly INavigation navigation;
        
        private int currentPosition;
        private CancellationTokenSource cancelLoadSvg = new CancellationTokenSource();

        public MainPageModel(INavigation navigation)
        {
            this.navigation = navigation;
            names = typeof(App).GetTypeInfo().Assembly.GetManifestResourceNames()
                .Where(n => n.EndsWith(".svg") && !n.Contains("templates")).OrderBy(n => n).ToArray();

            MessagingCenter.Subscribe<string, string>(this, MessagingCenterConst.OpenDeepLink, async (sender, args) =>
            {
                await LoadSvg(args);
            });
        }

        #region properties
        public string ColorMapping { get => colorMapping; set { colorMapping = value; OnPropertyChanged(); } }
        private string colorMapping;

        public string ImageName { get => imageName; set { imageName = value; OnPropertyChanged(); } }
        private string imageName = "https://upload.wikimedia.org/wikipedia/commons/1/15/Svg.svg";

        public double Zoom { get => zoom; set { zoom = value; OnPropertyChanged(); OnPropertyChanged(nameof(ZoomText)); } }
        private double zoom = 0;
        public double ZoomValue => Math.Pow(10, zoom);

        public string ZoomText => $"{ZoomValue:F1}";

        public Point Translation { get => translation; set { translation = value; OnPropertyChanged(); } }
        private Point translation;

        public bool IsTranslationEnabled { get => isTranslationEnabled; set { isTranslationEnabled = value; OnPropertyChanged(); } }
        private bool isTranslationEnabled;        
        #endregion

        #region commands
        public Command NextImageCommand => new Command(() =>
        {
            if (!isTranslationEnabled)
            {
                ResetPosition();
                currentPosition = FixIndex(++currentPosition);
                ImageName = names[currentPosition];
            }
        });

        public Command PrevImageCommand => new Command(() =>
        {
            if (!isTranslationEnabled)
            {
                ResetPosition();
                currentPosition = FixIndex(--currentPosition);
                ImageName = names[currentPosition];
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


        public Command OpenVapoliaCommand => new Command(() =>
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
                    ImageName = svgString;
                    //title.Text = $"Displayed {url}";
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
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
