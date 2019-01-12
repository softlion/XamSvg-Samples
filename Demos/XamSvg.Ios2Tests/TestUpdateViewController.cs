
using System;
using System.Drawing;
using System.Threading.Tasks;
using Foundation;
using UIKit;

namespace XamSvg.Ios2Tests
{
    public partial class TestUpdateViewController : UIViewController
    {
        //"svg.viewBox" is mandatory when using the "compute other dimension automatically" feature.
        const string Trial = @"<svg viewBox=""0 0 200 200""><circle style = ""fill:#f3f9ff"" cx=""100"" cy=""100"" r=""100"" /></svg>";
        const string Trial2 = @"<svg viewBox=""0 0 200 200""><circle style = ""fill:#00ff00"" cx=""100"" cy=""100"" r=""100"" /></svg>";

        public TestUpdateViewController(IntPtr handle) : base(handle)
        {
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Task.Delay(3000).ContinueWith(t =>
            {
                TheSvg.TraceEnabled = true;
                TheSvg.BundleString = Trial;

                Task.Delay(6000).ContinueWith(t2 =>
                {
                    TheSvg.BundleString = Trial2;

                }, TaskScheduler.FromCurrentSynchronizationContext());

            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
