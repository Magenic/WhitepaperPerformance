using System;

namespace TestIncidentQueueService.TestHarness.Models
{
    public class Incident
    {
        public string Id { get; set; }
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
