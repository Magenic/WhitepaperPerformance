using Microsoft.WindowsAzure.MobileServices;

namespace Xamarin.IncidentApp.Utilities
{
    public static class MobileService
    {
        public static MobileServiceClient Service = new MobileServiceClient(
            "https://testincidentqueue.azure-mobile.net/",
            "SetKeyHere"
            );
    }
}
