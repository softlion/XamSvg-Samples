using System;
using Android.Graphics.Drawables;
using Cirrious.CrossCore.Exceptions;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Binding;
using Cirrious.MvvmCross.Binding.Bindings.Target.Construction;
using Cirrious.MvvmCross.Binding.Droid.Target;
using XamSvg;


/*
 * 
 * Register this binding from your mvvmcross Setup class
 * 
 * 
    public class Setup : MvxAndroidSetup
    {
        ...

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            base.FillTargetFactories(registry);
            MvxImageViewSvgDrawableTargetBinding.Register(registry);
        }
    }
*/


namespace XamSvg.Droid
{
    public class MvxImageViewSvgDrawableTargetBinding : MvxAndroidTargetBinding
    {
        protected SvgImageView ImageView
        {
            get { return (SvgImageView)Target; }
        }

        public override MvxBindingMode DefaultMode
        {
            get { return MvxBindingMode.OneWay; }
        }

        public static void Register(IMvxTargetBindingFactoryRegistry registry)
        {
            registry.RegisterFactory(new MvxCustomBindingFactory<SvgImageView>("Svg", imageView => new MvxImageViewSvgDrawableTargetBinding(imageView)));
        }

        public MvxImageViewSvgDrawableTargetBinding(SvgImageView imageView) : base(imageView)
        {
        }

        public override Type TargetType
        {
            get { return typeof(string); }
        }

        protected override void SetValueImpl(object target, object value)
        {
            var imageView = (SvgImageView) target;
            if (!(value is string) || String.IsNullOrWhiteSpace((string) value))
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
    }
}
