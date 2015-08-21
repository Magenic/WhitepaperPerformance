using System;
using Microsoft.WindowsAzure.Mobile.Service;

namespace TestIncidentQueueService.DataObjects
{
    public class IncidentDetail : EntityData
    {
        public string IncidentId { get; set; }
        public string DetailEnteredById { get; set; }
        public string DetailText { get; set; }
        public string ImageLink { get; set; }
        public string AudioLink { get; set; }
        public DateTime DateEntered { get; set; }
    }
}