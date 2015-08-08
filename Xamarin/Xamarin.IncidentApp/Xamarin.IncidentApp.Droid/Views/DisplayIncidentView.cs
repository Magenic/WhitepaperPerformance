using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Support.RecyclerView;
using Xamarin.IncidentApp.Droid.MvxMaterial;
using Xamarin.IncidentApp.ViewModels;

namespace Xamarin.IncidentApp.Droid.Views
{
    [Activity(Label = "", Theme = "@style/Theme.Incident.TransparentActionBar",
    ScreenOrientation = ScreenOrientation.Portrait)]
    public class DisplayIncidentView : MvxActionBarActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.DisplayIncident);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            var incidentDetailList = FindViewById<MvxRecyclerView>(Resource.Id.incident_detail_list);
            incidentDetailList.Adapter = new DisplayIncidentAdapter((IMvxAndroidBindingContext)BindingContext);
        }

        protected override void OnResume()
        {
            var fab = FindViewById<FloatingActionButton>(Resource.Id.addIncidentDetail);
            fab.Visibility = ViewModel.Closed ? ViewStates.Gone : ViewStates.Visible;
            fab.Click -= Fab_Click;
            fab.Click += Fab_Click;

            ViewModel.PropertyChanged -= PropertyChanged;
            ViewModel.PropertyChanged += PropertyChanged;
            base.OnResume();
        }

        protected override void OnPause()
        {
            var fab = FindViewById<FloatingActionButton>(Resource.Id.addIncidentDetail);
            fab.Click -= Fab_Click;
            ViewModel.PropertyChanged -= PropertyChanged;

            base.OnPause();
        }

        private void PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Closed")
            {
                var fab = FindViewById<FloatingActionButton>(Resource.Id.addIncidentDetail); 
                fab.Visibility = ViewModel.Closed ? ViewStates.Gone : ViewStates.Visible;                
            }
        }

        public new DisplayIncidentViewModel ViewModel
        {
            get { return (DisplayIncidentViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        private void Fab_Click(object sender, System.EventArgs e)
        {
            ViewModel.AddIncidentDetailCommand.Execute(null);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            if (!ViewModel.Closed)
            {
                MenuInflater inflater = MenuInflater;
                inflater.Inflate(Resource.Menu.DisplayIncidentMenu, menu);                
            }
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.mnuDisplayIncidentClose)
            {
                ViewModel.CloseIncidentCommand.Execute(null);
                return true;
            }
            if (item.ItemId == Android.Resource.Id.Home)
            {
                Finish();
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}