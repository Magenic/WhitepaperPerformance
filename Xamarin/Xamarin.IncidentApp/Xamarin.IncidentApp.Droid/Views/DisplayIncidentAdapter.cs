using Android.Views;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Support.RecyclerView;

namespace Xamarin.IncidentApp.Droid.Views
{
    public class DisplayIncidentAdapter : MvxRecyclerAdapter
    {
        private const int HeaderTemplate = 0;
        private const int DetailTemplate = 1;

        public DisplayIncidentAdapter(IMvxAndroidBindingContext bindingContext) : base(bindingContext)
        {
        }

        public override int GetItemViewType(int position)
        {
            return (position == 0) ? HeaderTemplate : DetailTemplate;
        }

        protected override View InflateViewForHolder(ViewGroup parent, int viewType, IMvxAndroidBindingContext bindingContext)
        {
            var templateId = (viewType == HeaderTemplate ? Resource.Layout.DisplayIncidentHeader : Resource.Layout.DisplayIncidentDetail);
            return bindingContext.BindingInflate(templateId, parent, false);
        }
    }
}