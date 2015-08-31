using System.Threading.Tasks;
using Cirrious.CrossCore;
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
        private readonly IAzureService _azureServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginService"/> class.
        /// </summary>
        /// <param name="loginActivity">The login activity.</param>
        /// <param name="azureServices">The Azure Service Container.</param>
        public LoginService(BaseViewController loginActivity, IAzureService azureServices)
        {
            _context = loginActivity;
            _azureServices = azureServices;
        }

        /// <summary>
        /// login as an asynchronous operation.
        /// </summary>
        /// <returns>Task&lt;MobileServiceUser&gt;.</returns>
        public async Task<MobileServiceUser> LoginAsync()
        {
            return await _azureServices.MobileService.LoginAsync(_context, MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory);
        }
    }
}