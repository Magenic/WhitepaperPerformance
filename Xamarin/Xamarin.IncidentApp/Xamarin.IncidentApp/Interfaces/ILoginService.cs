using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace Xamarin.IncidentApp.Interfaces
{
    public interface ILoginService
    {
        Task<MobileServiceUser> LoginAsync();
    }
}
