using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Xamarin.IncidentApp.Models
{
    [DataContract]
    public class Incident
    {
        [JsonProperty(PropertyName = "id")]
        [DataMember]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "AssignedToId")]
        [DataMember]
        public string AssignedToId { get; set; }

        [JsonProperty(PropertyName = "Subject")]
        [DataMember]
        public string Subject { get; set; }

        [JsonProperty(PropertyName = "Description")]
        [DataMember]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "DateOpened")]
        [DataMember]
        public DateTime DateOpened { get; set; }

        [JsonProperty(PropertyName = "Closed")]
        [DataMember]
        public bool Closed { get; set; }

        [JsonProperty(PropertyName = "DateClosed")]
        [DataMember]
        public DateTime DateClosed { get; set; }

        [JsonProperty(PropertyName = "ImageLink")]
        [DataMember]
        public string ImageLink { get; set; }

        [JsonProperty(PropertyName = "AudioLink")]
        [DataMember]
        public string AudioLink { get; set; }
    }
}