using System;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Database;
using Android.OS;
using Android.Provider;
using Android.Views;
using Android.Widget;
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
                var bytes = ReadFully(file.GetStream());
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

        public byte[] ReadFully(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        //private override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        //{
        //    base.OnActivityResult(requestCode, resultCode, data);

        //    // Make it available in the gallery

        //    var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
        //    Uri contentUri = Uri.FromFile(App._file);
        //    mediaScanIntent.SetData(contentUri);
        //    SendBroadcast(mediaScanIntent);

        //    // Display in ImageView. We will resize the bitmap to fit the display.
        //    // Loading the full sized image will consume to much memory
        //    // and cause the application to crash.

        //    int height = Resources.DisplayMetrics.HeightPixels;
        //    int width = _imageView.Height;
        //    App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);
        //    if (App.bitmap != null)
        //    {
        //        _imageView.SetImageBitmap(App.bitmap);
        //        App.bitmap = null;
        //    }

        //    // Dispose of the Java side bitmap.
        //    GC.Collect();
        //}
    }
}