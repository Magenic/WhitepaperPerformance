using System;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Widget;

namespace Xamarin.IncidentApp.Droid.Views
{
    public class BitmapImageView : ImageView
    {
        protected BitmapImageView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public BitmapImageView(Context context) : base(context)
        {
        }

        public BitmapImageView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public BitmapImageView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public BitmapImageView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        public Bitmap CurrentBitmap
        {
            set
            {
                SetImageBitmap(value);
            }
        }
    }
}