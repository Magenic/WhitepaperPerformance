using System;

namespace Xamarin.IncidentApp.Models
{
    public class UserStatus
    {
        public UserProfile User { get; set; }
        private int _totalComplete;
        private static Random random;
        public int TotalCompleteIncidentsPast30Days {
            get
            {
                if (_totalComplete == 0)
                {
                    if (random == null) random = new Random();
                    _totalComplete = random.Next(50);                    
                }
                return _totalComplete;
            }
            set { _totalComplete = value; }
        }

        private Double _avgWait;
        public Double AvgWaitTimeOfOpenIncidents
        {
            get
            {
                if (_avgWait.Equals(0))
                {
                    if (random == null) random = new Random(); 
                    _avgWait = random.Next(150);
                }
                return _avgWait;
            }
            set { _avgWait = value; }
        }
        public int TotalOpenIncidents { get; set; }

        internal int MaxCompletedIncidents { get; set; }
        internal double MaxWaitTime { get; set; }

        public int MaxCompletedPercent
        {
            get
            {
                if (MaxCompletedIncidents == 0)
                {
                    return 0;
                }
                return Convert.ToInt32(((float)TotalCompleteIncidentsPast30Days / (float)MaxCompletedIncidents) * 100);
            }
        }

        public int MaxAvgWaitTimePercent
        {
            get
            {
                if (MaxWaitTime.Equals(0))
                {
                    return 0;
                }
                return Convert.ToInt32((AvgWaitTimeOfOpenIncidents / MaxWaitTime) * 100);
            }
        }
    }
}
