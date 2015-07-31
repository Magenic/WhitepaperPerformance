using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Web.Http;

namespace TestIncidentQueueService.Controllers
{
    public class SasGeneratorController : ApiController
    {
        private const string AppPolicyName = "IncidentQueuePolicy";

        [AuthorizeLevel(AuthorizationLevel.User)]
        public string Get()
        {
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting(Constants.ConfigurationKeys.StorageConnection));
            var blobClient = storageAccount.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference(CloudConfigurationManager.GetSetting(Constants.ConfigurationKeys.ContainerName));
            container.CreateIfNotExists();

            var blobPermissions = new BlobContainerPermissions();

            blobPermissions.SharedAccessPolicies.Add(AppPolicyName, new SharedAccessBlobPolicy()
            {
                // To ensure SAS is valid immediately, don’t set start time.
                // This way, you can avoid failures caused by small clock differences.
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(5),
                Permissions = SharedAccessBlobPermissions.Write |
                                SharedAccessBlobPermissions.Read
            });

            blobPermissions.PublicAccess = BlobContainerPublicAccessType.Blob;

            container.SetPermissions(blobPermissions);

            var sasToken = container.GetSharedAccessSignature(new SharedAccessBlobPolicy(), AppPolicyName);

            return container.Uri + sasToken;
        }
    }
}