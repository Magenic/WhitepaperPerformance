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

        internal int MaxOpenIncidents { get; set; }

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

        public int MaxOpenPercent
        {
            get
            {
                if (MaxOpenIncidents == 0)
                {
                    return 0;
                }
                return Convert.ToInt32(((float)TotalOpenIncidents / (float)MaxOpenIncidents) * 100);
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
