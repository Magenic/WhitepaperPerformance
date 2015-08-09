using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using TestIncidentQueueService.Models;
using TestIncidentQueueService.Utils;

namespace TestIncidentQueueService.Controllers
{
    [AuthorizeLevel(AuthorizationLevel.User)]
    [RoutePrefix("api/AllUserList")]
    public class AllUserListController : ApiController
    {
        // GET api/WorkerList
        public async Task<IList<UserProfile>> Get()
        {
            var authenticationResult = await AdIntegration.GetAuthenticationTokenAsync();
            var graphConnection = new GraphConnection(authenticationResult.AccessToken);
            var adWorkers = graphConnection.Get<Group>(CloudConfigurationManager.GetSetting(Constants.ConfigurationKeys.WorkerAdGroup));
            var members = graphConnection.GetLinkedObjects(adWorkers, LinkProperty.Members, null, -1);

            var returnValue = new List<UserProfile>();
            foreach (var member in members.Results)
            {
                returnValue.Add(LoadUser(member, graphConnection));
            }

            var adManagers = graphConnection.Get<Group>(CloudConfigurationManager.GetSetting(Constants.ConfigurationKeys.ManagerAdGroup));
            members = graphConnection.GetLinkedObjects(adManagers, LinkProperty.Members, null, -1);
            foreach (var member in members.Results)
            {
                returnValue.Add(LoadUser(member, graphConnection));
            }
            return returnValue;
        }

        private static UserProfile LoadUser(GraphObject member, GraphConnection graphConnection)
        {
            var user = member as User;
            if (user != null && (!user.AccountEnabled.HasValue || user.AccountEnabled.Value))
            {
                var groups = graphConnection.GetLinkedObjects(user, LinkProperty.MemberOf, null, -1);
                return new UserProfile
                {
                    FullName = user.DisplayName,
                    FirstName = user.GivenName,
                    LastName = user.Surname,
                    UserId = user.UserPrincipalName,
                    Manager = groups.Results.ToList()
                            .Any(g => g.ObjectId == CloudConfigurationManager.GetSetting(Constants.ConfigurationKeys.ManagerAdGroup))
                };
            }
            return null;
        }
    }
}
