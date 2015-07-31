
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace Xamarin.IncidentApp.Interfaces
{
    public interface IAzureServices
    {
        IMobileServiceClient MobileService { get; }
        Task<string> SaveBlobAsync(byte[] bytes, string blobExtension);
    }
}
