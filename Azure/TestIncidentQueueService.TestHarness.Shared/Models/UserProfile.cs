using System;
using System.Collections.Generic;
using System.Text;

namespace TestIncidentQueueService.TestHarness.Models
{
    public class UserProfile
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Manager { get; set; }
    }
}
