
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace Xamarin.IncidentApp.Interfaces
{
    public interface IAzureService
    {
        IMobileServiceClient MobileService { get; }
        Task<string> SaveBlobAsync(byte[] bytes, string blobExtension);
    }
}
