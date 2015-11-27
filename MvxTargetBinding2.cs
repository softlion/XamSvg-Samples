
using System;
using Cirrious.CrossCore.Exceptions;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Binding;
using Cirrious.MvvmCross.Binding.Bindings;
using Cirrious.MvvmCross.Binding.Bindings.Target;
using Cirrious.MvvmCross.Binding.Bindings.Target.Construction;
using UIKit;

namespace XamSvg
{
    public abstract class MvxTargetBinding2 : MvxBinding, IMvxTargetBinding
    {
        private readonly object _target;

        protected MvxTargetBinding2(object target)
        {
            _target = target;
        }

        protected object Target
        {
            get { return _target; }
        }

        public virtual void SubscribeToEvents()
        {
            // do nothing by default
        }

        protected virtual void FireValueChanged(object newValue)
        {
            var handler = ValueChanged;

            if (handler != null)
                handler(this, new MvxTargetChangedEventArgs(newValue));
        }

        public abstract Type TargetType { get; }
        public abstract void SetValue(object value);

        public event EventHandler<MvxTargetChangedEventArgs> ValueChanged;
        public abstract MvxBindingMode DefaultMode { get; }
    }
  }
  
