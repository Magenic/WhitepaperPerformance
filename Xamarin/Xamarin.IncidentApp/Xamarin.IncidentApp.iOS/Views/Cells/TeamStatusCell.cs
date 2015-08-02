// ***********************************************************************
// Assembly         : XamarinIncidentAppiOS
// Author           : Ken Ross
// Created          : 07-23-2015
//
// Last Modified By : Ken Ross
// Last Modified On : 07-26-2015
// ***********************************************************************
using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Foundation;
using Xamarin.IncidentApp.Models;

/// <summary>
/// The Cells namespace.
/// </summary>
namespace Xamarin.IncidentApp.iOS.Views.Cells
{
    //[Register("TeamStatusCell")]
    /// <summary>
    /// Class TeamStatusCell.
    /// </summary>
    public partial class TeamStatusCell : MvxTableViewCell
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public static readonly NSString Identifier = new NSString("TeamStatusCell");

        private float _maxCompletedPercentConv;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamStatusCell"/> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
        public TeamStatusCell (IntPtr handle) : base (handle)
		{
			InitializeBindings();
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamStatusCell"/> class.
        /// </summary>
        public TeamStatusCell()
        {
			InitializeBindings();
		}

        /// <summary>
        /// Gets or sets the maximum completed percentage, divided by 100 for easy binding to a progress bar.
        /// </summary>
        /// <value>The maximum completed percent.</value>
        public float MaxCompletedPercentConv
        {
            get { return pvCompletedBar.Progress; }
            set { pvCompletedBar.Progress = value / 100; }
        }

        /// <summary>
        /// Gets or sets the maximum average wait time, divided by 100 for easy binding to a progress bar.
        /// </summary>
        /// <value>The maximum average wait time.</value>
        public float MaxAvgWaitTimeConv
        {
            get { return pvWaitTimeBar.Progress; }
            set { pvWaitTimeBar.Progress = value / 100; }
        }

        /// <summary>
        /// Initializes the bindings.
        /// </summary>
        private void InitializeBindings()
        {
            this.DelayBind(() =>
            {
                this.CreateBinding(lblTeamLead).For(c => c.Text).To((UserStatus property) => property.User.FullName).Apply();
                this.CreateBinding(lblCompleted).For(c => c.Text).To((UserStatus property) => property.MaxCompletedPercent).Apply();
                this.CreateBinding(lblWaitTime).For(c => c.Text).To((UserStatus property) => property.MaxAvgWaitTimePercent).Apply();
                this.CreateBinding().For(c => c.MaxCompletedPercentConv).To((UserStatus property) => property.MaxCompletedPercent).Apply();
                this.CreateBinding().For(c => c.MaxAvgWaitTimeConv).To((UserStatus property) => property.MaxAvgWaitTimePercent).Apply();
            });
        }

    }
}
