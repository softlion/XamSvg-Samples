using System;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Exceptions;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Binding;
using Cirrious.MvvmCross.Binding.Bindings.Target;
using Cirrious.MvvmCross.Binding.Bindings.Target.Construction;
using MonoTouch.UIKit;
using XamSvg;

/*
 * 
 * Register this binding from your mvvmcross Setup class
 * 
 * 
    public class Setup : MvxTouchDialogSetup
    {
        ...

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            base.FillTargetFactories(registry);
            MvxImageViewSvgDrawableTargetBinding.Register(registry);
        }
    }
*/

namespace XamSvg.iOS
{
    public class MvxImageViewSvgDrawableTargetBinding : MvxTargetBinding
    {
        protected UIImageView ImageView
        {
            get { return (UIImageView)Target; }
        }

        public override MvxBindingMode DefaultMode
        {
            get { return MvxBindingMode.OneWay; }
        }

        public static void Register(IMvxTargetBindingFactoryRegistry registry)
        {
            registry.RegisterFactory(new MvxCustomBindingFactory<UIImageView>("Svg", imageView => new MvxImageViewSvgDrawableTargetBinding(imageView)));
        }

        public MvxImageViewSvgDrawableTargetBinding(UIImageView imageView) : base(imageView)
        {
        }

        public override Type TargetType
        {
            get { return typeof(string); }
        }

        public override void SetValue(object value)
        {
            var imageView = ImageView;
            if (imageView == null)
            {
                Mvx.Warning("Target is null in MvxImageViewSvgDrawableTargetBinding, skipping");
                return;
            }

            if (!(value is string) || String.IsNullOrWhiteSpace((string) value))
                return;

            try
            {
                UIImage drawable;
                if (!GetDrawable((string)value, out drawable))
                    return;
                imageView.Image = drawable;
            }
            catch (Exception ex)
            {
                MvxTrace.Error(ex.ToLongString());
                throw;
            }
        }

        protected virtual bool GetDrawable(string rawSvg, out UIImage drawable)
        {
            drawable = SvgFactory.FromBundle("vectors/"+rawSvg+".svg");
            return drawable != null;
        }
    }
}
