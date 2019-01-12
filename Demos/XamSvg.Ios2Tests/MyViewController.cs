//Comment this line to disable autolayout and use Frame positioning instead
#define USEAUTOLAYOUT

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Cirrious.FluentLayouts.Touch;
using CoreGraphics;
using CoreText;
using Foundation;
using UIKit;
using XamSvg.Platform;
using XamSvg.Shared.Cross;
using XamSvgDemo.Shared;


namespace XamSvg.Ios2Tests
{
    public class MyViewController : UIViewController
    {
        private UILabel title;
        private UISvgImageView image;

        public static MyViewController TheViewController;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.Blue;
            TheViewController = this;

            //Enumerate SVG with BundleResource action in the Resources/svg folder
            //var path = Path.Combine(NSBundle.MainBundle.BundlePath,"svg");
            var bundleSvgs = new List<string>(); //Directory.EnumerateFiles(path, "*.svg").Select(Path.GetFileName).OrderBy(s => s).ToList();

            //Enumerate SVG with EmbeddedResource action in the XamSvgDemo.Shared project, in the images folder.
            var assembly = typeof (App).GetTypeInfo().Assembly;
            var sharedSvgs = assembly.GetManifestResourceNames().Where(n => n.EndsWith(".svg")).OrderBy(n => n).ToArray();

            //Combine both lists
            var svgNames = bundleSvgs.Select(s => "svg/" + s).Concat(
                            sharedSvgs.Select(s => "res:" + s)
                            ).ToList();

            var index = 0;

#if !USEAUTOLAYOUT
            //Fix width, let height be changed by UISvgImageView
            var bounds = UIScreen.MainScreen.Bounds;
            image = new UISvgImageView(svgNames[index], bounds.Width, 0) { Frame = new CGRect(0,0,bounds.Width, bounds.Height) };
#else
            image = new UISvgImageView(svgNames[index]);
#endif

            image.Layer.BorderWidth = 1;
            image.Layer.BorderColor = UIColor.Green.CGColor;
            View.Add(image);

            title = new UILabel
            {
                TextColor=UIColor.White,
                Font = UIFont.SystemFontOfSize(14f),
                LineBreakMode = UILineBreakMode.CharacterWrap,
                Lines = 0,
#if !USEAUTOLAYOUT
                Frame = new CGRect(0,30,320,100),
#endif
            };
            View.Add(title);

#if USEAUTOLAYOUT
            var back = new UIView {BackgroundColor = UIColor.DarkGray.ColorWithAlpha(.6f)};
            var back2 = new UIView { BackgroundColor = UIColor.Clear };
            var inputUrl = new UITextField
            {
                TextColor = UIColor.White, Font = UIFont.SystemFontOfSize(14f),

                AttributedPlaceholder = new NSMutableAttributedString("Enter url of svg file, or tap anywhere for demo",
                    foregroundColor: UIColor.Gray, font: UIFont.ItalicSystemFontOfSize(12)),
                KeyboardType = UIKeyboardType.Url, AutocorrectionType = UITextAutocorrectionType.No,
                AutocapitalizationType = UITextAutocapitalizationType.None,
                //ReturnKeyType = UIReturnKeyType.Go,
                //EnablesReturnKeyAutomatically = true, ShouldReturn = 
            };
            //var inputOk = new UISvgImageView("res:images.download", 25, colorMapping: "000000=FF546D", colorMappingSelected: "000000=00FF59")
            //{
            //    UserInteractionEnabled = true,
            //};
            var inputOk = new UISvgImageView
            {
                UserInteractionEnabled = true,
                TranslatesAutoresizingMaskIntoConstraints = false,
                FillWidth = 25,
                ColorMapping="000000=FF546D",
                ColorMappingSelected="000000=00FF59",
                BundleName = "res:images.download",
                IsLoadAsync = false
            };


            var btn = new UIButton();
            btn.SetTitle("Test1", UIControlState.Normal);
            btn.SetContentCompressionResistancePriority((float)UILayoutPriority.Required, UILayoutConstraintAxis.Vertical);

        //var inputOk = new UISvgImageView("", 25); //for debug
            View.Add(back);
            View.Add(back2);
            View.SendSubviewToBack(back);
            View.SendSubviewToBack(image); //image behind back
            View.Add(inputUrl);
            View.Add(inputOk);
            View.Add(btn);

            inputOk.AddGestureRecognizer(new UITapGestureRecognizer(tap =>
            {
                inputUrl.ResignFirstResponder();
                var dontWait = LoadSvg(inputUrl.Text);
            }));

            inputUrl.EditingDidBegin += (sender, args) =>
            {
                inputUrl.SelectAll(this);
            };

            inputUrl.SetContentHuggingPriority((float)UILayoutPriority.FittingSizeLevel, UILayoutConstraintAxis.Horizontal);
            inputOk.SetContentCompressionResistancePriority((float)UILayoutPriority.Required, UILayoutConstraintAxis.Horizontal);
            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            View.AddConstraints(
                back.WithSameTop(inputOk).Minus(5),
                back.AtLeftOf(View),
                back.AtRightOf(View),
                back.WithSameBottom(title).Plus(5),

                back2.Below(back),
                back2.AtLeftOf(View),
                back2.AtRightOf(View),
                back2.AtBottomOf(View),

                inputUrl.AtLeftOf(View, 5),
                inputUrl.WithSameCenterY(inputOk),

                inputOk.AtTopOf(View,30),
                inputOk.AtRightOf(View, 5),
                inputOk.ToRightOf(inputUrl,5),

                title.Below(inputUrl, 20),
                title.AtLeftOf(View, 5),
                title.AtRightOf(View,5),
                //No height for title, use its intrinsic height

                btn.AtRightOf(View),
                btn.AtLeftOf(View),
                btn.AtBottomOf(View),

                image.Below(back),
                image.AtLeftOf(View),
                //Test: Width forced, free height
                image.WithSameWidth(View),
                //Test: Width forced, Height forced to view height
                //image.Height().LessThanOrEqualTo().HeightOf(View)
                image.Above(btn)
                //Test: Width forced, Height forced (50)
                );


            back2.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                index = ++index%svgNames.Count;
                image.BundleName = svgNames[index];
                title.Text = $"Displaying {svgNames[index]}";
                title.TextColor = UIColor.White;

            }) { NumberOfTapsRequired = 1 });


            btn.TouchUpInside += (sender, args) =>
            {
                //var sb = UIStoryboard.FromName("TestFullWidthConstraint", null);
                //var vc = sb.InstantiateViewController("TestFullWidthConstraintViewController");
                var sb = UIStoryboard.FromName("AllViews", null);

                //var vc = sb.InstantiateViewController(nameof(TestUpdateViewController));
                var vc = sb.InstantiateViewController(nameof(TestImageFactoryViewController));

                NavigationController.PushViewController(vc, true);
            };
#endif
            image.FillMode = SvgFillMode.Fit;

            NavigationItem.TitleView = new UISvgImageView("res:images.atom")
            {
                IsLoadAsync = false,
                FillMode = SvgFillMode.Fit,
                AlignmentMode = SvgAlignmentMode.Center,
                Frame = new CGRect(0,0,NavigationController.NavigationBar.Bounds.Width, NavigationController.NavigationBar.Bounds.Height)
            };

            //var t = new UIImageView(new CGRect(0, 0, 100, 100));
            //t.Image = LoadLastSvgFromString();
            //View.Add(t);

            //image.UserInteractionEnabled = true;


            //var bounds = SvgFactory.GetBounds("res:images.logoImage", 100, 100, fillMode: SvgFillMode.Fit);
            //var image = SvgFactory.FromBundle("res:images.logoImage", 100, 100, fillMode: SvgFillMode.Fit);

        }

        CancellationTokenSource cancel = new CancellationTokenSource();

        public async Task LoadSvg(string url)
        {
            cancel.Cancel();
            var c = cancel = new CancellationTokenSource();

            title.Text = $"Loading {url}";
            title.TextColor = UIColor.White;
            using (var client = new HttpClient())
            {
                try
                {
                    var result = await client.GetAsync(url, c.Token);
                    if(!result.IsSuccessStatusCode)
                        throw new WebException();
                    var svgString = await result.Content.ReadAsStringAsync();

                    image.BundleString = svgString;
                    title.Text = $"Displayed {url}";
                }
                catch (Exception e)
                {
                    title.Text = $"Error loading url: {e.Message}";
                    title.TextColor = UIColor.Red;
                }
            }
        }

        //private UIImage LoadLastSvgFromString()
        //{
        //    const string svgString = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?><svg width=""100"" height=""100""><circle cx=""50"" cy=""50"" r=""50"" style=""fill:#ff0000"" /></svg>";
        //    var image = SvgFactory.FromString(svgString, 100);
        //    return image;
        //}
    }
}
