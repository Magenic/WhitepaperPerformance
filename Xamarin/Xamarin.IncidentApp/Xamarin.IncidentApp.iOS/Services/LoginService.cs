using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.IncidentApp.iOS.Controllers;
using Xamarin.IncidentApp.Interfaces;
using Xamarin.IncidentApp.Utilities;

namespace Xamarin.IncidentApp.iOS.Services
{
    public class LoginService : ILoginService
    {
        private BaseViewController _context;

        public LoginService(BaseViewController loginActivity)
        {
            _context = loginActivity;
        }

        public async Task<MobileServiceUser> LoginAsync()
        {
            return await MobileService.Service.LoginAsync(_context,
    MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory);
        }
    }
}