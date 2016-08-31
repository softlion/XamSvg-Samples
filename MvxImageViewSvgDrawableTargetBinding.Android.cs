using System;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Binding.Droid.Target;
using MvvmCross.Platform.Exceptions;
using MvvmCross.Platform.Platform;

namespace XamSvg.Droid
{
    public class MvxImageViewSvgDrawableTargetBinding : MvxAndroidTargetBinding
    {
        protected SvgImageView ImageView => (SvgImageView)Target;

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

        public static void Register(IMvxTargetBindingFactoryRegistry registry)
        {
            registry.RegisterFactory(new MvxCustomBindingFactory<SvgImageView>("Svg", imageView => new MvxImageViewSvgDrawableTargetBinding(imageView)));
        }

        public MvxImageViewSvgDrawableTargetBinding(SvgImageView imageView) : base(imageView)
        {
        }

        public override Type TargetType => typeof(string);

        protected override void SetValueImpl(object target, object value)
        {
            var imageView = (SvgImageView) target;
            if (String.IsNullOrWhiteSpace(value as string))
                return;

            try
            {
                SvgBitmapDrawable drawable;
                if (!GetDrawable((string)value, out drawable))
                    return;
                imageView.ImageDrawable = drawable;
            }
            catch (Exception ex)
            {
                MvxTrace.Error(ex.ToLongString());
                throw;
            }
        }

        protected virtual bool GetDrawable(string rawSvg, out SvgBitmapDrawable drawable)
        {
            drawable = null;
            var resourceId = ImageView.Resources.GetIdentifier(rawSvg, "raw", ImageView.Context.PackageName);
            if (resourceId <= 0)
                return false;

            drawable = SvgFactory.GetDrawable(ImageView.Resources, resourceId);
            return drawable != null;
        }

        //public override void SetValue(object value)
        //{
        //    if (value == null)
        //    {
        //        MvxBindingTrace.Trace(MvxTraceLevel.Warning, "Null value passed to ImageView binding");
        //        return;
        //    }

        //    var stringValue = value as string;
        //    if (string.IsNullOrWhiteSpace(stringValue))
        //    {
        //        MvxBindingTrace.Trace(MvxTraceLevel.Warning, "Empty value passed to ImageView binding");
        //        return;
        //    }

        //    var drawableResourceName = GetImageAssetName(stringValue);
        //    var assetStream = AndroidGlobals.ApplicationContext.Assets.Open(drawableResourceName);
        //    Drawable drawable = Drawable.CreateFromStream(assetStream, null);
        //    _imageView.SetImageDrawable(drawable);
        //}

        //private static string GetImageAssetName(string rawImage)
        //{
        //    return rawImage.TrimStart('/');
        //}
    }
}
