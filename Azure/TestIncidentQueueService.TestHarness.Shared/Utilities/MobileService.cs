using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;

namespace TestIncidentQueueService.TestHarness.Utilities
{
    public static class MobileService
    {
        public static MobileServiceClient Service = new MobileServiceClient(
        "https://testincidentqueue.azure-mobile.net/",
        "putkeyhere"
        );
    }
}