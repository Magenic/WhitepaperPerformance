using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Database;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Views;
using Xamarin.IncidentApp.Droid.MvxMaterial;
using Xamarin.IncidentApp.Droid.Services;
using Xamarin.IncidentApp.ViewModels;
using Xamarin.Media;

namespace Xamarin.IncidentApp.Droid.Views
{
    [Activity(Label = "New Incident", Theme = "@style/Theme.Incident.ActionBar",
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class AddIncidentView : MvxActionBarActivity
    {

        private MediaService _mediaService;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AddIncident);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            _mediaService = new MediaService(this);
            ViewModel.SetActivityServices(_mediaService);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater inflater = MenuInflater;
            inflater.Inflate(Resource.Menu.AddIncidentMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public new AddIncidentViewModel ViewModel
        {
            get { return (AddIncidentViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.mnuAddIncidentTakePhoto)
            {
                ViewModel.TakeNewPhotoCommand.Execute(null);
                return true;
            }
            else if (item.ItemId == Resource.Id.mnuAddIncidentAttachImage)
            {
                ViewModel.SelectPhotoCommand.Execute(null);
                return true;
            }
            else if (item.ItemId == Resource.Id.mnuAddIncidentRecordAudio)
            {
                ViewModel.RecordAudioCommand.Execute(null);
                return true;
            }
            else if (item.ItemId == Android.Resource.Id.Home)
            {
                Finish();
                return true;
            }
            else if (item.ItemId == Resource.Id.mnuAddIncidentContinue)
            {
                ViewModel.SaveNewIncidentCommand.Execute(null);
                return true;
            }
            else
            {
                throw new NotImplementedException();
            }

            return base.OnOptionsItemSelected(item);
        }

        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == Constants.IntentConstants.TakePhoto)
            {
                await HandlePhotoResultAsync(resultCode, data);
            }
            else if (requestCode == Constants.IntentConstants.SelectPhoto)
            {
                await HandlePhotoResultAsync(resultCode, data);
            }
            else if (requestCode == Constants.IntentConstants.RecordAudio)
            {
                await HandleAudioResultAsync(resultCode, data);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private async Task HandlePhotoResultAsync(Result resultCode, Intent data)
        {
            if (resultCode == Result.Ok)
            {
                MediaFile file = await data.GetMediaFileExtraAsync(this);
                var bytes = Utilities.BinaryHandling.ReadFully(file.GetStream());
                _mediaService.PhotoResult(bytes);
            }
        }

        private async Task HandleAudioResultAsync(Result resultCode, Intent data)
        {
            if (resultCode == Result.Ok)
            {
                var audioUri = data.Data;
                var realAudioUri = GetRealPathFromUri(audioUri);
                var bytes = File.ReadAllBytes(realAudioUri);
                _mediaService.AudioResult(bytes);
                // Todo: file not being removed correctly, OK for demo app.
                var file = new Java.IO.File(realAudioUri);
                file.DeleteOnExit();
            }
        }

        private String GetRealPathFromUri(Android.Net.Uri contentUri)
        {
            String[] proj = { MediaStore.Audio.AudioColumns.Data };
            ICursor cursor = ManagedQuery(contentUri, proj, null, null, null);
            var columnIndex = cursor.GetColumnIndex(MediaStore.Audio.AudioColumns.Data);
            cursor.MoveToFirst();
            return cursor.GetString(columnIndex);
        }
    }
}