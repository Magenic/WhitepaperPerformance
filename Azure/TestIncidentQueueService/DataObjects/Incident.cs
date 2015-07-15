using System;
using Microsoft.WindowsAzure.Mobile.Service;

namespace TestIncidentQueueService.DataObjects
{
    public class Incident : EntityData
    {
        public string AssignedToId { get; set; }
        public string Subject { get; set; }
        public int Description { get; set; }
        public DateTime DateOpened { get; set; }
        public bool Closed { get; set; }
        public DateTime DateClosed { get; set; }
        public string ImageLink { get; set; }
        public string AudioLink { get; set; }
    }
}