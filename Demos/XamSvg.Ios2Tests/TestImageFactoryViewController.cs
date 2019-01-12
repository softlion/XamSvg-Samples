using System;
using UIKit;
using Vapolia.Ios.Lib;
using XamSvg.Shared.Cross;

namespace XamSvg.Ios2Tests
{
	public partial class TestImageFactoryViewController : UIViewController
	{
		public TestImageFactoryViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad()
		{
            base.ViewDidLoad();

            //TheImage has a width constraint (.5*screen) and 2 center constraints (centerx and top=118)

            //Test AlignmentMode.
            //When fillMode is set to Fit and alignmentMode is set to something other than TopLeft (which is the default) and both fillWidthPixel/fillHeightPixel are non zero,
            //an image of fillWidthPixel x fillHeightPixel is created
            //and the svg is drawn inside, fitted either vertically or horizontally, and aligned (only centered currently) on the other dimension.
            //
            //This mimics the "gravity" attribute on Android
            //TheImage.Image = SvgFactory.FromBundle("res:images.logo_auchan", 200, 200, fillMode: SvgFillMode.Fit, alignmentMode: SvgAlignmentMode.Center);

		    TheImage.BackgroundColor = UIColor.Green;
		    TheImage.ContentMode = UIViewContentMode.ScaleAspectFit;

		    //Test CGImage to UIImage cut
		    var bounds = SvgFactory.GetBounds("res:images.cosmo", 0, TheImage.Bounds.Width, SvgFillMode.Fill);
            var image = SvgFactory.FromBundle("res:images.cosmo", 0, TheImage.Bounds.Width);
		    TheImage.Image = image;

            //Test CGImage with button
		    TheButton.Style = UIButton2.ButtonStyle.Action;
		    TheButton.LeftImage = "res:images.cosmo";
		    //TheButton.IsImageOnly = true;
		    TheButton.Text = "The button";
		    TheButton.UpdateStyle();
		}
    }
}
