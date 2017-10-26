// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace XamSvg.Ios2Tests
{
    [Register ("TestFullWidthConstraintViewController")]
    partial class TestFullWidthConstraintViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        XamSvg.UISvgImageView FullWidthFreeHeight { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton OkButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (FullWidthFreeHeight != null) {
                FullWidthFreeHeight.Dispose ();
                FullWidthFreeHeight = null;
            }

            if (OkButton != null) {
                OkButton.Dispose ();
                OkButton = null;
            }
        }
    }
}