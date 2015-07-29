using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Xamarin.IncidentApp.Models
{
    public class Token
    {
        //[AutoIncrement, PrimaryKey]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        public string UserId { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", TokenType, AccessToken.Replace(System.Environment.NewLine, string.Empty));
        }
    }

}
