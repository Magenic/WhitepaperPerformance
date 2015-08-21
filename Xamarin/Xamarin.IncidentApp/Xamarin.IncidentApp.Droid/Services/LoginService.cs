using System.Threading.Tasks;
using Android.App;
using Cirrious.CrossCore;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.IncidentApp.Interfaces;

namespace Xamarin.IncidentApp.Droid.Services
{
    public class LoginService : ILoginService
    {
        private Activity _context;
        private readonly IAzureService _azureServices;

        public LoginService(Activity loginActivity, IAzureService azureServices)
        {
            _context = loginActivity;
            _azureServices = azureServices;
        }
        
        public async Task<MobileServiceUser> LoginAsync()
        {
            return await _azureServices.MobileService.LoginAsync(_context, MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory);
        }
    }
}