using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Xamarin.IncidentApp.Models
{
    [DataContract]
    public class IncidentDetail
    {
        [JsonProperty(PropertyName = "id")]
        [DataMember]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "IncidentId")]
        [DataMember]
        public string IncidentId { get; set; }

        [JsonProperty(PropertyName = "DetailEnteredById")]
        [DataMember]
        public string DetailEnteredById { get; set; }


        [JsonProperty(PropertyName = "DetailText")]
        [DataMember]
        public string DetailText { get; set; }

        [JsonProperty(PropertyName = "ImageLink")]
        [DataMember]
        public string ImageLink { get; set; }

        [JsonProperty(PropertyName = "AudioLink")]
        [DataMember]
        public string AudioLink { get; set; }

        [JsonProperty(PropertyName = "DateEntered")]
        [DataMember]
        public DateTime DateEntered { get; set; }
    }
}
