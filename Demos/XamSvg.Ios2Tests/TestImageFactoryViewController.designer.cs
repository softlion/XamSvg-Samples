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
    [Register ("TestImageFactoryViewController")]
    partial class TestImageFactoryViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        Vapolia.Ios.Lib.UIButton2 TheButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView TheImage { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (TheButton != null) {
                TheButton.Dispose ();
                TheButton = null;
            }

            if (TheImage != null) {
                TheImage.Dispose ();
                TheImage = null;
            }
        }
    }
}