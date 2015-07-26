using System;

namespace Xamarin.IncidentApp.Models
{
    public class UserStatus
    {
        public UserProfile User { get; set; }
        public int TotalCompleteIncidentsPast30Days { get; set; }
        public Double AvgWaitTimeOfOpenIncidents { get; set; }
        public int TotalOpenIncidents { get; set; }
    }
}
