using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using Newtonsoft.Json;

namespace TestIncidentQueueService.Utils
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AuthorizeRole : AuthorizationFilterAttribute
    {
        private bool _isInitialized;
        private bool _isHosted;
        private ApiServices _services;

        // Constants used with ADAL and the Graph REST API for AAD
        //private const string AadInstance = "https://login.windows.net/{0}";
        //private const string GraphResourceId = "https://graph.windows.net/";
        private const string ApiVersion = "?api-version=2013-04-05";

        // App settings pulled from the Mobile Service

        private Dictionary<int, string> groupIds = new Dictionary<int, string>();

        private string _token;

        public AuthorizeRole(Enums.AdRoles role)
        {
            Role = role;
        }

        // private class used to serialize the Graph REST API web response
        private class MembershipResponse
        {
            public bool Value { get; set; }

            public MembershipResponse(bool value)
            {
                Value = value;
            }
        }

        public Enums.AdRoles Role { get; private set; }

        // Generate a local dictionary for the role group ids configured as 
        // Mobile Service app settings
        private void InitGroupIds()
        {
            if (_services == null)
                return;

            if (!groupIds.ContainsKey((int)Enums.AdRoles.Manager))
            {
                groupIds.Add((int)Enums.AdRoles.Manager, Constants.AdIntegration.ManagerAdGroup);
            }
            if (!groupIds.ContainsKey((int)Enums.AdRoles.Worker))
            {
                groupIds.Add((int)Enums.AdRoles.Worker, Constants.AdIntegration.WorkerAdGroup);
            }
        }

        // Use ADAL and the authentication app settings from the Mobile Service to 
        // get an AAD access token
        private void GetAdToken()
        {
            // Try to get the required AAD authentication app settings from the mobile service.  

            var result = AdIntegration.GetAuthenticationToken();

            if (result != null)
                _token = result.AccessToken;
            else
                _services.Log.Error("GetAADToken() : Failed to return a token.");
        }

        // Given an AAD user id, check membership against the group associated with the role.
        private bool CheckMembership(string memberId)
        {
            bool membership = false;
            string url = Constants.AdIntegration.Graph + "/" + Constants.AdIntegration.AdSite + "/isMemberOf" + ApiVersion;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            // Use the Graph REST API to check group membership in the AAD
            try
            {
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", _token);
                using (var sw = new StreamWriter(request.GetRequestStream()))
                {
                    // Request body must have the group id and a member id to check for membership
                    string body = String.Format("\"groupId\":\"{0}\",\"memberId\":\"{1}\"",
                        groupIds[(int)Role], memberId);
                    sw.Write("{" + body + "}");
                }

                var response = request.GetResponse();
                var stream = response.GetResponseStream();
                if (stream != null)
                {
                    var sr = new StreamReader(stream);
                    string json = sr.ReadToEnd();
                    var membershipResponse = JsonConvert.DeserializeObject<MembershipResponse>(json);
                    membership = membershipResponse.Value;                    
                }
            }
            catch (Exception e)
            {
                _services.Log.Error("OnAuthorization() exception : " + e.Message);
            }

            return membership;
        }

        // Called when the user is attempting authorization
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException("actionContext");
            }

            _services = new ApiServices(actionContext.ControllerContext.Configuration);

            // Check whether we are running in a mode where local host access is allowed 
            // through without authentication.
            if (!_isInitialized)
            {
                HttpConfiguration config = actionContext.ControllerContext.Configuration;
                _isHosted = config.GetIsHosted();
                _isInitialized = true;
            }

            // No security when hosted locally
            if (!_isHosted && actionContext.RequestContext.IsLocal)
            {
                _services.Log.Warn("AuthorizeAadRole: Local Hosting.");
                return;
            }

            ApiController controller = actionContext.ControllerContext.Controller as ApiController;
            if (controller == null)
            {
                _services.Log.Error("AuthorizeAadRole: No ApiController.");
            }

            bool isAuthorized = false;
            try
            {
                // Initialize a mapping for the group id to our enumerated type
                InitGroupIds();

                // Retrieve a AAD token from ADAL
                GetAdToken();
                if (_token == null)
                {
                    _services.Log.Error("AuthorizeAadRole: Failed to get an AAD access token.");
                }
                else
                {
                    // Check group membership to see if the user is part of the group that corresponds to the role
                    if (!string.IsNullOrEmpty(groupIds[(int)Role]) && controller != null)
                    {
                        ServiceUser serviceUser = controller.User as ServiceUser;
                        if (serviceUser != null && serviceUser.Level == AuthorizationLevel.User)
                        {
                            var idents = serviceUser.GetIdentitiesAsync().Result;
                            AzureActiveDirectoryCredentials clientAadCredentials =
                                idents.OfType<AzureActiveDirectoryCredentials>().FirstOrDefault();
                            if (clientAadCredentials != null)
                            {
                                isAuthorized = CheckMembership(clientAadCredentials.ObjectId);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _services.Log.Error(e.Message);
            }
            finally
            {
                if (isAuthorized == false)
                {
                    _services.Log.Error("Denying access");

                    actionContext.Response = actionContext.Request
                        .CreateErrorResponse(HttpStatusCode.Forbidden,
                            "User is not logged in or not a member of the required group");
                }
            }
        }
    }
}