// ***********************************************************************
// Assembly         : Xamarin.IncidentApp
// Author           : Kevin Ford
// Created          : 07-25-2015
//
// Last Modified By : Ken Ross
// Last Modified On : 07-28-2015
// ***********************************************************************
using System;

/// <summary>
/// The Models namespace.
/// </summary>
namespace Xamarin.IncidentApp.Models
{
    /// <summary>
    /// Class UserStatus.
    /// </summary>
    public class UserStatus
    {
        private int _totalComplete;
        private static Random _random;
        private Double _avgWait;

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public UserProfile User { get; set; }

        /// <summary>
        /// Gets or sets the total complete incidents past30 days.
        /// </summary>
        /// <value>The total complete incidents past30 days.</value>
        public int TotalCompleteIncidentsPast30Days {
            get
            {
                if (_totalComplete == 0)
                {
                    if (_random == null) _random = new Random();
                    _totalComplete = _random.Next(50);                    
                }
                return _totalComplete;
            }
            set { _totalComplete = value; }
        }

        /// <summary>
        /// Gets or sets the average wait time of open incidents.
        /// </summary>
        /// <value>The average wait time of open incidents.</value>
        public Double AvgWaitTimeOfOpenIncidents
        {
            get
            {
                if (_avgWait.Equals(0))
                {
                    if (_random == null) _random = new Random(); 
                    _avgWait = _random.Next(150);
                }
                return _avgWait;
            }
            set { _avgWait = value; }
        }
        /// <summary>
        /// Gets or sets the total open incidents.
        /// </summary>
        /// <value>The total open incidents.</value>
        public int TotalOpenIncidents { get; set; }

        /// <summary>
        /// Gets or sets the maximum completed incidents.
        /// </summary>
        /// <value>The maximum completed incidents.</value>
        internal int MaxCompletedIncidents { get; set; }

        /// <summary>
        /// Gets or sets the maximum wait time.
        /// </summary>
        /// <value>The maximum wait time.</value>
        internal double MaxWaitTime { get; set; }

        /// <summary>
        /// Gets the maximum completed percent.
        /// </summary>
        /// <value>The maximum completed percent.</value>
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

        /// <summary>
        /// Gets the maximum average wait time percent.
        /// </summary>
        /// <value>The maximum average wait time percent.</value>
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
