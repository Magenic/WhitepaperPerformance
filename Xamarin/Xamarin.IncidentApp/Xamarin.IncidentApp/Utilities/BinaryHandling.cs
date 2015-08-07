using System.IO;
using System.Threading.Tasks;

namespace Xamarin.IncidentApp.Utilities
{
    public static class BinaryHandling
    {
        public static async Task<byte[]> LoadBytesFromUrlAsync(string sourceUrl)
        {
            var myWebClient = System.Net.WebRequest.Create(sourceUrl);
            var objResponse = await myWebClient.GetResponseAsync();
            return ReadFully(objResponse.GetResponseStream());
        }

        public static byte[] ReadFully(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}