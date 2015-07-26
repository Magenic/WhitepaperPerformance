using System;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Binding.Droid.Target;

namespace Xamarin.IncidentApp.Droid.Bindings
{
    public class ViewWidthBinging : MvxAndroidTargetBinding
    {
        public ViewWidthBinging(object target)
            : base(target)
        {
        }

        public override Type TargetType
        {
            get { return typeof(int); }
        }

        protected override void SetValueImpl(object target, object value)
        {
            var targetView = target as View;
            if (targetView == null)
                return;
            var width = (int)value;

            var layoutParameters = targetView.LayoutParameters;
            if (layoutParameters != null)
            {
                layoutParameters.Width = width;                
            }
            else
            {
                targetView.LayoutParameters = new LinearLayout.LayoutParams(width, 20);
            }
            targetView.RequestLayout();
        }
    }
}