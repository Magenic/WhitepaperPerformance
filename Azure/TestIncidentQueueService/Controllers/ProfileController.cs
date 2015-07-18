using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using TestIncidentQueueService.Models;

namespace TestIncidentQueueService.Controllers
{
    [AuthorizeLevel(AuthorizationLevel.User)]
    [RoutePrefix("api/Profile")]
    public class ProfileController : ApiController
    {
        // GET api/Profile
        public async Task<UserProfile> Get()
        {
            var authenticationResult = await Utils.AdIntegration.GetAuthenticationTokenAsync();
            var credentials = await Utils.AdIntegration.GetCredentialsAsync(User);
            var graphConnection = new GraphConnection(authenticationResult.AccessToken);
            var adUser = graphConnection.Get<User>(credentials.ObjectId);
            var groups = graphConnection.GetLinkedObjects(adUser, LinkProperty.MemberOf, null, -1);

            var returnValue = new UserProfile
            {
                FullName = adUser.DisplayName,
                FirstName = adUser.GivenName,
                LastName = adUser.Surname,
                UserId = adUser.UserPrincipalName,
                Manager = groups.Results.ToList().Any(g => g.ObjectId == Constants.AdIntegration.ManagerAdGroup)
            };
            return returnValue;
        }
    }
}
