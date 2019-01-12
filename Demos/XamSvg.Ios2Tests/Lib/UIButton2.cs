using System;
using System.Diagnostics.CodeAnalysis;
using CoreGraphics;
using Foundation;
using UIKit;
using XamSvg;
using XamSvg.Shared.Cross;

// Do not remove this notice
// Copyright (c)2016-2018 Vapolia. All rights reserved.

namespace Vapolia.Ios.Lib
{
    public static class AppStyles
    {
        public static readonly UIColor BackColor = UIColor.FromRGB(0x00, 0x78, 0xDB);
        public static readonly UIColor ButtonBackgroundColorDisabled = UIColor.FromRGB(0x5E, 0x85, 0xa3);
        public static readonly UIColor ButtonTextColor = UIColor.White;
        public static readonly UIColor ButtonBackgroundColor = UIColor.FromRGB(0x00,0x78,0xDB);
        public static readonly UIColor TextColorSepOnLightDarker = UIColor.FromRGB(0xe4, 0xe8, 0xed);
        public static readonly UIColor TextColorActions = UIColor.Green; 
        public static readonly UIColor Transparent = UIColor.Clear;
        public static readonly UIColor ButtonBackgroundColorPushed = UIColor.FromRGB(0x03, 0x8d, 0xff);
        public static readonly UIColor ActionBlue = UIColor.FromRGB(0x00, 0x78, 0xDB);
        public static readonly UIColor ButtonTextColorDisabled = UIColor.FromRGB(0x4b, 0x4b, 0x4b);
        public static readonly UIColor TextDarkColor = UIColor.FromRGB(0x4b, 0x4b, 0x4b); //dark grey
        public static readonly UIColor TextLightColorSecondary = UIColor.FromRGB(0x85, 0x85, 0x85); //Grey
        public static readonly UIColor TextColorPrimary = UIColor.FromRGB(0xdb, 0x10, 0x1a); //Red
        public static readonly UIColor TextColorPrimaryHighlighted = UIColor.FromRGB(0xFF, 0x14, 0x20); //Red lighter

        public static UIColor Darker(this UIColor color)
        {
            color.GetHSBA(out var hue, out var saturation, out var brightness, out var alpha);
            return UIColor.FromHSBA(hue, saturation, brightness * (nfloat).8, alpha);
        }

        public static UIColor Lighter(this UIColor color)
        {
            color.GetHSBA(out var hue, out var saturation, out var brightness, out var alpha);
            return UIColor.FromHSBA(hue, saturation, brightness * (nfloat)1.2, alpha);
        }
        
        public static UIFont Bold(this UIFont font)
        {
            return font.WithTraits(UIFontDescriptorSymbolicTraits.Bold);
        }
        
        public static UIFont WithTraits(this UIFont font, UIFontDescriptorSymbolicTraits traits) 
        {
            var descriptor = font.FontDescriptor.CreateWithTraits(traits);
            return UIFont.FromDescriptor(descriptor, 0);
        }
    }

    /// <summary>
    /// Create a button
    /// Not tested with leftImage+rightImage
    /// </summary>
    [Register("UIButton2")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class UIButton2 : UIButtonEx
    {
        public const int ButtonHeight = 60;
        public static readonly float BtnRadius = ButtonHeight / 2.0f;

        private string rightImage, leftImage;

        private bool hasBorder = true;
        private nfloat radius;
        private nfloat imageWidth;

        public enum ButtonStyle
        {
            Action, //White button (facebook, google)
            LinkWhite,
            ActionDark,
            ActionSecondary,
            Border,
            Callout,
            ActionDarkCallout,
            LinkRightIcon,
            LinkTop,
            Tab, //Fake tabs
            Map, //Button on map

            NavTopRightButton,
            ImageOnly,
            LinkGrey,
        }

        [Export(nameof(Style))]
        public string StyleString
        {
            set
            {
                if (Enum.TryParse<ButtonStyle>(value, true, out var style))
                    Style = style;
            }
        }

        public ButtonStyle Style { get; set; } = ButtonStyle.Action;

        //[Export(nameof(LeftImage))]
        //public string LeftImage { get => leftImage; set => SetLeftImage(value); }
        [Export(nameof(LeftImage))]
        public string LeftImage { get; set; }
        [Export(nameof(LeftImageSelected))]
        public string LeftImageSelected { get; set; }

        [Export(nameof(RightImage))]
        public string RightImage { get => rightImage; set => SetRightImage(value); }
        [Export(nameof(RightImageSelected))]
        public string RightImageSelected { get; set; }
        [Export(nameof(RightImageLeftMargin))]
        public nfloat RightImageLeftMargin { get; set; } = 16f;

        [Export(nameof(ImagePercentHeight))]
        public nfloat ImagePercentHeight { get; set; } = 1f;

        //Use ContentEdgeInsets to add spacing before left image.
        //Note: when setting any ContentEdgeInsets, ALL default values (6) are replaced by yours.

        [Export(nameof(LeftImageRightMargin))]
        public nfloat LeftImageRightMargin { get; set; } = 3;

        [Export(nameof(ColorMapping))]
        public string ColorMapping { get => colorMapping; set { colorMapping = value; UpdateImage(); SetNeedsLayout(); } }

        [Export(nameof(ColorMappingSelected))]
        public string ColorMappingSelected { get => colorMappingSelected; set { colorMappingSelected = value; UpdateImage(); SetNeedsLayout(); } }

        /// <summary>
        /// title is ignored, and no space is made for it
        /// </summary>
        [Export(nameof(IsImageOnly))]
        public bool IsImageOnly { get; set; }

        [Export(nameof(IsMedium))]
        public bool IsMedium { get; set; }
        /// <summary>
        /// radius will be BtnRadius/2
        /// </summary>
        [Export(nameof(IsSmall))]
        public bool IsSmall { get; set; }
        /// <summary>
        /// radius will be BtnRadius/3
        /// </summary>
        [Export(nameof(IsVerySmall))]
        public bool IsVerySmall { get; set; }

        [Export(nameof(Text))]
        public string Text { set => SetTitle(value, UIControlState.Normal); }

        [Export(nameof(HasPerfectRadius))]
        public bool HasPerfectRadius { get; set; }

        public UIButton2()
        {
            SetDefaults();
        }

        [Preserve]
        public UIButton2(IntPtr handle) : base(handle)
        {
            SetDefaults();
        }

        public override void LayoutSubviews()
        {
            nfloat height;
            if (IsImageOnly)
                height = ContentRectForBounds(Bounds).Height* ImagePercentHeight;
            else
                height = TitleLabel.Font.LineHeight* ImagePercentHeight;

            if (height != imageHeight)
            {
                imageHeight = height;
                UpdateImage();
            }

            if (HasPerfectRadius)
            {
                Layer.MasksToBounds = true;
                Layer.CornerRadius = Bounds.Height/2;
            }

            base.LayoutSubviews();
        }


        private void SetDefaults()
        {
            Opaque = true;
            BackgroundColorDisabled = AppStyles.ButtonBackgroundColorDisabled;
            BackgroundColor = AppStyles.BackColor;
        }

        /// <summary>
        /// Call this after setting the Style in code (not in storyboard/XIB)
        /// Note: ios applies a default ContentEdgeInset of 6 if it is empty
        /// </summary>
        public void UpdateStyle()
        {
            radius = IsSmall ? BtnRadius / 2 : (IsVerySmall ? BtnRadius / 3 : (IsMedium ? BtnRadius / 1.5f : BtnRadius));

            switch (Style)
            {
                //facebook
                case ButtonStyle.Action:
                    SetTitleColor(UIColor.FromRGB(0x04, 0x25, 0x47), UIControlState.Normal);
                    SetTitleColor(UIColor.White, UIControlState.Highlighted);
                    SetTitleColor(UIColor.White, UIControlState.Disabled);
                    BackgroundColor = UIColor.Brown;
                    BackgroundColorHighlighted = UIColor.FromRGB(0x04, 0x52, 0x93);
                    BackgroundColorDisabled = AppStyles.ButtonBackgroundColorDisabled;
                    radius = 3;
                    break;

                //texte blanc, fond bleu
                case ButtonStyle.ActionDark:
                    {
                        SetTitleColor(AppStyles.ButtonTextColor, UIControlState.Normal);
                        SetTitleColor(UIColor.White, UIControlState.Highlighted);
                        SetTitleColor(UIColor.FromRGB(0x81,0x9f,0xb8), UIControlState.Disabled);
                        BackgroundColor = AppStyles.ButtonBackgroundColor;
                        BackgroundColorHighlighted = UIColor.FromRGB(0x03, 0x8d, 0xff);
                        BackgroundColorDisabled = UIColor.FromRGB(0x00, 0x4c, 0x8B);
                        radius = 3;
                        ContentEdgeInsets = new UIEdgeInsets(18,6,18,6);
                    }
                    break;


                //texte gris foncé, fond gris clair
                case ButtonStyle.ActionSecondary:
                    {
                        var textColor = UIColor.FromRGB(0x73, 0x73, 0x73);
                        SetTitleColor(textColor, UIControlState.Normal);
                        BackgroundColor = UIColor.FromRGB(0xF1,0xF1,0xF1);
                        radius = 3;
                        var h = TitleLabel.Font.LineHeight / 3;
                        TitleEdgeInsets = new UIEdgeInsets(h, 0, h, 0);
                        Layer.BorderWidth = .5f;
                        Layer.BorderColor = textColor.CGColor;
                    }
                    break;

                case ButtonStyle.Border:
                    SetTitleColor(UIColor.FromRGB(0x00, 0x78, 0xDB), UIControlState.Normal);
                    SetTitleColor(UIColor.White, UIControlState.Highlighted);
                    SetTitleColor(UIColor.White, UIControlState.Disabled);
                    BackgroundColor = UIColor.FromRGB(0xFA, 0xFA, 0xFA);
                    BackgroundColorHighlighted = UIColor.FromRGB(0x00, 0x78, 0xDB);
                    BackgroundColorDisabled = AppStyles.ButtonBackgroundColorDisabled;
                    TitleLabel.Font = UIFont.PreferredBody;
                    radius = 2.5f;
                    Layer.BorderWidth = .5f;
                    Layer.BorderColor = UIColor.FromRGB(0.59f,0.59f,0.59f).CGColor;
                    ContentEdgeInsets = new UIEdgeInsets(6, 6, 6, 6);
                    break;

                //texte blanc et fond bleu, arrondi parfait
                case ButtonStyle.Callout:
                    SetTitleColor(UIColor.White, UIControlState.Normal);
                    SetTitleColor(UIColor.White, UIControlState.Highlighted);
                    SetTitleColor(UIColor.LightGray, UIControlState.Disabled);
                    BackgroundColor = UIColor.FromRGB(0x00, 0x78, 0xDB);
                    BackgroundColorHighlighted = BackgroundColor.Darker();
                    BackgroundColorDisabled = AppStyles.ButtonBackgroundColorDisabled;
                    TitleLabel.Font = UIFont.PreferredCaption1;
                    HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
                    ContentEdgeInsets = new UIEdgeInsets(4, 0, 4, 0);
                    //radius = (TitleLabel.Font.LineHeight + 6)/2;
                    HasPerfectRadius = true;
                    break;

                //texte blanc, fond bleu
                case ButtonStyle.ActionDarkCallout:
                    SetTitleColor(UIColor.White, UIControlState.Normal);
                    SetTitleColor(UIColor.White, UIControlState.Highlighted);
                    SetTitleColor(UIColor.FromRGB(0x81, 0x9f, 0xb8), UIControlState.Disabled);
                    BackgroundColor = AppStyles.ButtonBackgroundColor;
                    BackgroundColorHighlighted = UIColor.FromRGB(0x03, 0x8d, 0xff);
                    BackgroundColorDisabled = UIColor.FromRGB(0x00, 0x4c, 0x8B);
                    TitleLabel.Font = UIFont.PreferredCaption1.Bold();
                    radius = 3;
                    ContentEdgeInsets = new UIEdgeInsets(6, 0, 6, 0);
                    break;

                    //Texte avec icone à droite
                case ButtonStyle.LinkRightIcon:
                    Opaque = false;
                    BackgroundColor = AppStyles.Transparent;
                    BackgroundColorDisabled = AppStyles.Transparent;
                    hasBorder = false;
                    TitleLabel.Font = UIFont.PreferredCaption1;
                    HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
                    ContentEdgeInsets = new UIEdgeInsets(3, 0, 3, 0);
                    break;

                case ButtonStyle.LinkTop:
                    Opaque = false;
                    BackgroundColor = AppStyles.Transparent;
                    BackgroundColorDisabled = AppStyles.Transparent;
                    hasBorder = false;
                    TitleLabel.Font = UIFont.PreferredBody;
                    break;

                case ButtonStyle.Tab:
                    radius = 0;
                    SetTitleColor(UIColor.FromRGB(0x9B, 0x9B, 0x9B), UIControlState.Normal);
                    BackgroundColor = UIColor.White;
                    BackgroundColorHighlighted = BackgroundColorDisabled.Darker();

                    SetTitleColor(UIColor.White, UIControlState.Selected);
                    BackgroundColorSelected = UIColor.FromRGB(0x00, 0x78, 0xDB);
                    break;

                case ButtonStyle.Map:
                    radius = 4f;
                    SetTitleColor(UIColor.FromRGB(0x4a, 0x4a, 0x4a), UIControlState.Normal);
                    BackgroundColor = UIColor.FromRGB(0xe8, 0xf1, 0xff);
                    break;

                case ButtonStyle.LinkGrey:
                    SetTitleColor(AppStyles.TextColorSepOnLightDarker, UIControlState.Normal);
                    SetTitleColor(AppStyles.ButtonTextColorDisabled, UIControlState.Disabled);
                    SetTitleColor(AppStyles.TextColorActions, UIControlState.Highlighted);
                    Opaque = false;
                    BackgroundColor = AppStyles.Transparent;
                    BackgroundColorDisabled = AppStyles.Transparent;
                    hasBorder = false;
                    TitleLabel.Font = UIFont.PreferredBody;
                    break;

                case ButtonStyle.NavTopRightButton:
                    SetTitleColor(AppStyles.TextColorPrimary, UIControlState.Normal);
                    SetTitleColor(AppStyles.ButtonTextColorDisabled, UIControlState.Disabled);
                    SetTitleColor(AppStyles.TextColorPrimaryHighlighted, UIControlState.Highlighted);
                    Opaque = false;
                    BackgroundColor = AppStyles.Transparent;
                    BackgroundColorDisabled = AppStyles.Transparent;
                    hasBorder = false;
                    TitleLabel.Font = UIFont.PreferredCaption1;
                    break;

                case ButtonStyle.LinkWhite:
                    SetTitleColor(UIColor.White, UIControlState.Normal);
                    SetTitleColor(AppStyles.TextColorSepOnLightDarker, UIControlState.Disabled);
                    SetTitleColor(AppStyles.TextColorActions, UIControlState.Highlighted);
                    Opaque = false;
                    BackgroundColor = AppStyles.Transparent;
                    BackgroundColorSelected = AppStyles.ButtonBackgroundColorPushed;
                    BackgroundColorDisabled = AppStyles.Transparent;
                    hasBorder = false;
                    TitleLabel.Font = IsSmall ? UIFont.PreferredCaption1 : UIFont.PreferredBody;
                    radius = 0;
                    break;

                case ButtonStyle.ImageOnly:
                    Opaque = false;
                    hasBorder = false;
                    BackgroundColor = AppStyles.Transparent;
                    BackgroundColorDisabled = UIColor.Clear;
                    BackgroundColorSelected = UIColor.Clear;
                    BackgroundColorHighlighted = UIColor.Clear;
                    IsImageOnly = true;
                    Text = String.Empty;
                    break;
            }

            if (hasBorder)
            {
                Layer.CornerRadius = radius;
                Layer.MasksToBounds = true;
            }
        }

        private void SetRightImage(string image)
        {
            if (rightImage == image)
                return;
            rightImage = image;
            UpdateImage();
            SetNeedsLayout();
        }

        private void SetLeftImage(string image)
        {
            if (leftImage == image)
                return;
            leftImage = image;
            UpdateImage();
            SetNeedsLayout();
        }

        private void UpdateImage()
        {
            var imageString = LeftImage ?? rightImage;

            if (imageString != null && imageHeight != 0)
            {
                var imageSelectedString = LeftImageSelected ?? RightImageSelected;

                //var cgImage = SvgFactory.FromUri(imageString, 0, imageHeight, ColorMapping, SvgFillMode.Fit);
                //var image = new UIImage(cgImage,0, UIImageOrientation.DownMirrored).ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
                var image = SvgFactory.FromBundle(imageString, 0, imageHeight, ColorMapping, SvgFillMode.Fit)
                    .ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
                SetImage(image, UIControlState.Normal);

                if (imageSelectedString != null)
                {
                    image = SvgFactory.FromBundle(imageSelectedString, 0, imageHeight, ColorMappingSelected, SvgFillMode.Fit)
                        .ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
                    SetImage(image, UIControlState.Highlighted);
                    SetImage(image, UIControlState.Selected);
                }
                else if (ColorMappingSelected != null)
                {
                    image = SvgFactory.FromBundle(imageString, 0, imageHeight, ColorMappingSelected, SvgFillMode.Fit)
                        .ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
                    SetImage(image, UIControlState.Highlighted);
                    SetImage(image, UIControlState.Selected);
                }

                imageWidth = image?.Size.Width ?? 0;
            }
        }

        nfloat imageHeight;
        private string colorMapping;
        private string colorMappingSelected;

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            UpdateStyle();
        }

        public override CGSize IntrinsicContentSize
        {
            get
            {
                if (ZeroSizeWhenHidden && Hidden)
                    return CGSize.Empty;

                var size = IsImageOnly ? CGSize.Empty : TitleLabel.IntrinsicContentSize;
                if(!IsImageOnly)
                {
                    size.Width += TitleEdgeInsets.Left + TitleEdgeInsets.Right;
                    size.Height += TitleEdgeInsets.Top + TitleEdgeInsets.Bottom;
                }
                //if (ForcedHeight != 0)
                //    size.Height = ForcedHeight;

                //size.Height *= 1.6f;
                if (ContentEdgeInsets == UIEdgeInsets.Zero && !IsImageOnly)
                {
                    size.Height += 12;
                    size.Width += 12;
                }
                else
                {
                    size.Height += ContentEdgeInsets.Top + ContentEdgeInsets.Bottom;
                    size.Width += ContentEdgeInsets.Left + ContentEdgeInsets.Right;
                }

                var paddingH = hasBorder ? radius : 0;
                size.Width += 2*paddingH;

                if(LeftImage != null || rightImage != null)
                {
                    if (!IsImageOnly)
                    {
                        size.Width += imageWidth;
                        if (rightImage != null)
                            size.Width += RightImageLeftMargin;

                        if (LeftImage != null)
                            size.Width += LeftImageRightMargin;
                    }
                    else
                    {
                        var imageString = LeftImage ?? rightImage;
                        var bounds = SvgFactory.GetBounds(imageString, 0, ContentRectForBounds(Bounds).Height * ImagePercentHeight, SvgFillMode.Fit);
                        size.Height += bounds.Height;
                        size.Width += bounds.Width;
                    }
                }

                size.Width += Layer.BorderWidth;

                if (ForcedWidth.HasValue)
                    size.Width = ForcedWidth.Value;
                if (ForcedHeight>0)
                    size.Height = ForcedHeight;

                return size;
            }
        }

        public override CGRect ContentRectForBounds(CGRect rect)
        {
            var inset = ContentEdgeInsets;

            if(inset == UIEdgeInsets.Zero && !IsImageOnly)
                inset = new UIEdgeInsets(6, 6, 6, 6);

            var paddingH = hasBorder ? radius : 0;
            inset.Left += paddingH + Layer.BorderWidth;
            inset.Right += paddingH + Layer.BorderWidth;

            return new CGRect(rect.X + inset.Left, rect.Y + inset.Top, rect.Width - inset.Left - inset.Right, rect.Height - inset.Top - inset.Bottom);
        }

        public override CGRect TitleRectForContentRect(CGRect rect)
        {
            var contentRect = rect;

            var textRect = base.TitleRectForContentRect(contentRect);
            if(rightImage != null)
            {
                if (HorizontalAlignment == UIControlContentHorizontalAlignment.Left)
                    textRect.Offset(-imageWidth, 0);
                else //if (HorizontalAlignment == UIControlContentHorizontalAlignment.Center)
                    textRect.Offset(-(imageWidth + RightImageLeftMargin), 0);
            }
            if(LeftImage != null)
            {
                textRect.Offset(LeftImageRightMargin,0);
            }

            return textRect;
        }

        public override CGRect ImageRectForContentRect(CGRect rect)
        {
            var hasImage = LeftImage != null || rightImage != null;
            if (hasImage && rect.Width>0 && rect.Height>0)
            {
                //var width = (nfloat)Math.Min(rect.Width, imageWidth);
                //var height = (nfloat)Math.Min(rect.Height, imageHeight);
                var width = imageWidth;
                var height = imageHeight;
                var imageRect = new CGRect(rect.X, rect.Y + (rect.Height - height) / 2, width, height);

                if (rightImage != null)
                {
                    imageRect.X = rect.Right - width;
                }

                return imageRect;
            }

            return CGRect.Empty;
        }
    }
}
