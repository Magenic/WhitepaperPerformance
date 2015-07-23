using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
