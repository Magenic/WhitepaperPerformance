using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
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
            var result = string.Empty;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(StorageUrl);

                var response = await client.GetAsync("api/SasGenerator").ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
            }
            return result;
        }

        public async Task<string> SaveBlobAsync(byte[] bytes, string blobExtension)
        {
            var blobName = Guid.NewGuid().ToString();

            var sasUrl = await RetrieveStorageAccessSignatureAsync().ConfigureAwait(false);


           	var request = (HttpWebRequest)WebRequest.Create(sasUrl);
			request.Method = "PUT";


            var stream = await request.GetRequestStreamAsync();
            stream.Write(bytes, 0, bytes.Length);

			request.Headers["x-ms-blob-type"] = "BlockBlob";
            request.Headers["x-ms-blob-content-disposition"] = String.Format("attachment; filename=\"{0}.{1}\"", blobName, blobExtension);

			var response = await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null).ConfigureAwait(false);
            return blobName;
        }
    }
}
