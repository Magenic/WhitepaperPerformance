using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.IncidentApp.iOS.Controllers;
using Xamarin.IncidentApp.Interfaces;
using Xamarin.IncidentApp.Utilities;

namespace Xamarin.IncidentApp.iOS.Services
{
    /// <summary>
    /// Class LoginService.
    /// </summary>
    public class LoginService : ILoginService
    {
        private readonly BaseViewController _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginService"/> class.
        /// </summary>
        /// <param name="loginActivity">The login activity.</param>
        public LoginService(BaseViewController loginActivity)
        {
            _context = loginActivity;
        }

        /// <summary>
        /// login as an asynchronous operation.
        /// </summary>
        /// <returns>Task&lt;MobileServiceUser&gt;.</returns>
        public async Task<MobileServiceUser> LoginAsync()
        {
            return await MobileService.Service.LoginAsync(_context,
                MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory);
        }
    }
}