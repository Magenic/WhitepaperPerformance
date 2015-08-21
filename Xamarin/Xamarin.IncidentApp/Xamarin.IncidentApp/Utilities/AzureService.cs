using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.IncidentApp.Constants;
using Xamarin.IncidentApp.Interfaces;

namespace Xamarin.IncidentApp.Utilities
{
    public class AzureService : IAzureService
    {
        private const string StorageUrl = "https://testincidentqueue.blob.core.windows.net/";

        private static IMobileServiceClient _mobileService = new MobileServiceClient(
            "https://testincidentqueue.azure-mobile.net/",
            "SetKeyHere"
            );

        public IMobileServiceClient MobileService
        {
            get { return _mobileService; }
        }

        private async Task<string> RetrieveStorageAccessSignatureAsync()
        {
            if (MobileService.CurrentUser == null)
            {
                return string.Empty;
            }
            return await MobileService.InvokeApiAsync<string>("SasGenerator", HttpMethod.Get, null);
                // Do something with the result. Used to know Id of currently logged on person and their name/display name and if they are a manager.
        }

        public async Task<string> SaveBlobAsync(byte[] bytes, string blobExtension)
        {
            var blobName = string.Format("{0}.{1}", Guid.NewGuid().ToString(), blobExtension);

            var sasUrl = await RetrieveStorageAccessSignatureAsync().ConfigureAwait(false);
            sasUrl = sasUrl.Replace(AzureConstants.StorageAccountName, String.Format("{0}/{1}", AzureConstants.StorageAccountName, blobName));

           	var request = (HttpWebRequest)WebRequest.Create(sasUrl);
			request.Method = "PUT";


            var stream = await request.GetRequestStreamAsync();
            stream.Write(bytes, 0, bytes.Length);
           
			request.Headers["x-ms-blob-type"] = "BlockBlob";
            request.Headers["x-ms-blob-content-disposition"] = String.Format("attachment; filename=\"{0}\"", blobName);

			await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null).ConfigureAwait(true);
            return string.Format(AzureConstants.StorageAccount, blobName);
        }
    }
}
