using System;
using System.Threading.Tasks;
using Android.App;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.IncidentApp.Interfaces;

namespace Xamarin.IncidentApp.Droid.Services
{
    public class LoginService : ILoginService
    {
        private Activity _context;
        
        public LoginService(Activity loginActivity)
        {
            _context = loginActivity;
        }
        
        public async Task<MobileServiceUser> LoginAsync()
        {
                return await Utilities.MobileService.Service.LoginAsync(_context,
        MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory);
        }
    }
}