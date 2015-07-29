using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure.Mobile.Service.Security;

namespace TestIncidentQueueService.Utils
{
    public static class AdIntegration
    {
        public async static Task<AuthenticationResult> GetAuthenticationTokenAsync()
        {
            ClientCredential clientCred = new ClientCredential(Constants.AdIntegration.ClientId, Constants.AdIntegration.ClientSecret);
            AuthenticationContext authenticationContext = new AuthenticationContext(string.Format(Constants.AdIntegration.Authority, Constants.AdIntegration.AdSite), false);
            return await authenticationContext.AcquireTokenAsync(Constants.AdIntegration.Graph, clientCred);
        }

        public static AuthenticationResult GetAuthenticationToken()
        {
            ClientCredential clientCred = new ClientCredential(Constants.AdIntegration.ClientId, Constants.AdIntegration.ClientSecret);
            AuthenticationContext authenticationContext = new AuthenticationContext(string.Format(Constants.AdIntegration.Authority, Constants.AdIntegration.AdSite), false);
            return authenticationContext.AcquireToken(Constants.AdIntegration.Graph, clientCred);
        }

        public async static Task<AzureActiveDirectoryCredentials> GetCredentialsAsync(IPrincipal user)
        {
            var serviceUser = (ServiceUser)user;
            var identities = await serviceUser.GetIdentitiesAsync();
            return (identities != null && identities.Count > 0) ? (AzureActiveDirectoryCredentials)identities[0] : null;
        }
    }
}