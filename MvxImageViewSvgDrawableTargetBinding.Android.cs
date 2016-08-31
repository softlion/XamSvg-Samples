using System;
using Android.App;
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
                //TODO: create a bindable Svg property in SvgImageView
                //TODO: preserve existing colormapping
                imageView.SetSvg(Application.Context, (string)value);
            }
            catch (Exception ex)
            {
                MvxTrace.Error(ex.ToLongString());
                throw;
            }
        }
    }
}
