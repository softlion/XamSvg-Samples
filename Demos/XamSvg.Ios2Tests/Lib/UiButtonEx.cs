using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;

// Do not remove this notice
// Copyright (c)2016-2018 Vapolia. All rights reserved.

namespace Vapolia.Ios.Lib
{
    /// <summary>
    /// An UIButton which can have an alternate background color when pressed, or when disabled.
    /// When autolayout is used, this button correctly computes the intrisic dimensions by adding titleinsets.
    /// </summary>
    [Register("UIButtonEx")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class UIButtonEx : UIButton
    {
        public UIColor BackgroundColorHighlighted { get; set; }
        public UIColor BackgroundColorDisabled { get; set; }
        public UIColor BackgroundColorSelected { get; set; }

        private UIColor originalBackgroundColor = UIColor.Clear;

        /// <summary>
        /// If set, this is the value returned by IntrinsicContentSize
        /// </summary>
        [Export(nameof(ForcedHeight))]
        public nfloat ForcedHeight { get; set; }

        public nfloat? ForcedWidth { get; set; }

        /// <summary>
        /// If true, when hidden/shown the space used by the label is freed so the constraints can be recomputed.
        /// </summary>
        public bool ZeroSizeWhenHidden { get => zeroSizeWhenHidden; set { zeroSizeWhenHidden = value; InvalidateIntrinsicContentSize(); } }
        private bool zeroSizeWhenHidden;

        public override UIColor BackgroundColor
        {
            get => originalBackgroundColor;
            set 
            { 
                originalBackgroundColor = value;
                SetBackgroundColor();
            }
        }

        public UIButtonEx()
        {
        }

        public UIButtonEx(IntPtr handle) : base(handle)
        {
        }

        private void SetBackgroundColor([CallerMemberName]string caller = null)
        {
            var color =
                (Highlighted && BackgroundColorHighlighted != null ? BackgroundColorHighlighted :
                (!Enabled && BackgroundColorDisabled!=null) ? BackgroundColorDisabled : 
                (Selected && BackgroundColorSelected!=null ? BackgroundColorSelected : 
                originalBackgroundColor));

            //Vap.Trace($"SetBackgroundColor {color} Highlighted:{Highlighted} Enabled:{Enabled} caller:{caller}");

            base.BackgroundColor = color;
            base.Layer.BackgroundColor = color?.CGColor; //For alpha without ButtonType.Custom
        }

        public override bool Selected
        {
            get => base.Selected;
            set
            {
                base.Selected = value;
                SetBackgroundColor();
            }
        }

        public override bool Highlighted
        {
            get => base.Highlighted;
            set
            {
                base.Highlighted = value;
                SetBackgroundColor();
            }
        }

        public override bool Enabled
        {
            get => base.Enabled;
            set
            {
                base.Enabled = value;
                SetBackgroundColor();
            }
        }

        public override bool Hidden
        {
            get => base.Hidden;
            set
            {
                base.Hidden = value;
                if (zeroSizeWhenHidden)
                {
                    if (Superview != null)
                    {
                        Animate(.3, () =>
                        {
                            InvalidateIntrinsicContentSize();
                            Superview.LayoutIfNeeded();
                        });
                    }
                    else
                        InvalidateIntrinsicContentSize();
                }
            }
        }

        private CALayer topBorder, bottomBorder, rightImage;

        public void AddTopBottomBorders(UIColor color)
        {
            if (topBorder != null)
            {
                topBorder.RemoveFromSuperLayer();
                topBorder = null;
            }
            if (bottomBorder != null)
            {
                bottomBorder.RemoveFromSuperLayer();
                bottomBorder = null;
            }

            topBorder = new CALayer
            {
                BackgroundColor = color.CGColor,
                Frame = new CGRect(0, 0, Bounds.Width, lineWidth)
            };
            Layer.AddSublayer(topBorder);

            bottomBorder = new CALayer
            {
                BackgroundColor = color.CGColor,
                Frame = new CGRect(0, Bounds.Height-1f, Bounds.Width, lineWidth)
            };
            Layer.AddSublayer(bottomBorder);
        }

        private static readonly nfloat rightImageMargin = 12;
        private static readonly nfloat lineWidth = 1/UIScreen.MainScreen.Scale;

        public void AddRightImage(UIImage image)
        {
            //TODO: modify the title rect

            if (rightImage != null)
            {
                rightImage.RemoveFromSuperLayer();
                rightImage = null;
            }
            rightImage = new CALayer
            {
                Frame = Bounds.Inset(lineWidth+rightImageMargin,lineWidth),
                ContentsGravity = CALayer.GravityRight,
                Contents = image.CGImage,
                ContentsScale = UIScreen.MainScreen.Scale,
                RasterizationScale = UIScreen.MainScreen.Scale,
                AllowsEdgeAntialiasing = true,
                ShouldRasterize = true
            };

            Layer.AddSublayer(rightImage);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            if (topBorder != null)
                topBorder.Frame = new CGRect(0, 0, Bounds.Width, lineWidth);
            if (bottomBorder != null)
                bottomBorder.Frame = new CGRect(0, Bounds.Height - lineWidth, Bounds.Width, lineWidth);
            if (rightImage != null)
                rightImage.Frame = new CGRect(0,topBorder != null ? lineWidth : 0, Bounds.Width - rightImageMargin, Bounds.Height-((topBorder!=null ? lineWidth : 0)+(bottomBorder!=null ? lineWidth : 0)));
        }

        /// <summary>
        /// Add TitleEdgeInsets to intrisic content for autolayout
        /// </summary>
        public override CGSize IntrinsicContentSize
        {
            get
            {
                if (zeroSizeWhenHidden && base.Hidden)
                    return CGSize.Empty;

                var s = base.IntrinsicContentSize;
                var size = new CGSize(
                    ForcedWidth ?? s.Width+TitleEdgeInsets.Left+TitleEdgeInsets.Right+ContentEdgeInsets.Left+ContentEdgeInsets.Right, 
                    ForcedHeight != 0 ? ForcedHeight : s.Height+TitleEdgeInsets.Top+TitleEdgeInsets.Bottom+ContentEdgeInsets.Top+ContentEdgeInsets.Bottom);

                return size;
            }
        }
    }
}
