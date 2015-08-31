using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Xamarin.IncidentApp.Droid.MvxMaterial;
using Xamarin.IncidentApp.Droid.Services;
using Xamarin.IncidentApp.ViewModels;

namespace Xamarin.IncidentApp.Droid.Views
{
    [Activity(Label = "", Theme = "@style/Theme.Incident.ActionBar",
    ScreenOrientation = ScreenOrientation.Portrait)]
    public class AudioRecorderView : MvxActionBarActivity
    {
        private MediaService _mediaService;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AudioRecorder);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            _mediaService = new MediaService(this);
            ViewModel.SetActivityServices(_mediaService);
        }

        public new AudioRecorderViewModel ViewModel
        {
            get { return (AudioRecorderViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater inflater = MenuInflater;
            inflater.Inflate(Resource.Menu.RecordAudio, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                ViewModel.CancelCommand.Execute(null);
                return true;
            }
            else if (item.ItemId == Resource.Id.mnuRecordAudioContinue)
            {
                ViewModel.ReturnResultCommand.Execute(null);
                return true;
            }
            else
            {
                throw new NotImplementedException();
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}