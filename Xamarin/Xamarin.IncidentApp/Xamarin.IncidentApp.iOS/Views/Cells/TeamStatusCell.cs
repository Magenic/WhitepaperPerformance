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
        /// Gets or sets the team lead.
        /// </summary>
        /// <value>The team lead.</value>
        public string TeamLead
        {
            get { return lblTeamLead.Text; }
            set { lblTeamLead.Text = value; }
        }

        /// <summary>
        /// Gets or sets the completed incidents bar progress value.
        /// </summary>
        /// <value>The completed bar progress value.</value>
        public float CompletedBarProgress
        {
            get { return pvCompletedBar.Progress; }
            set { pvCompletedBar.Progress = (value/100); }
        }

        /// <summary>
        /// Gets or sets the wait time bar progress value.
        /// </summary>
        /// <value>The wait time bar progress value.</value>
        public float WaitTimeBarProgress
        {
            get { return pvWaitTimeBar.Progress; }
            set { pvWaitTimeBar.Progress = (value / 100); }
        }

        /// <summary>
        /// Gets or sets the completed incidents value as text.
        /// </summary>
        /// <value>The completed text.</value>
        public string CompletedText
        {
            get { return lblCompleted.Text; }
            set { lblCompleted.Text = value; }
        }

        /// <summary>
        /// Gets or sets the wait time value as text.
        /// </summary>
        /// <value>The wait time text.</value>
        public string WaitTimeText
        {
            get { return lblWaitTime.Text; }
            set { lblWaitTime.Text = value; }
        }

        /// <summary>
        /// Initializes the bindings.
        /// </summary>
        private void InitializeBindings()
        {
            this.DelayBind(() =>
            {
                this.CreateBinding().For(cell => cell.TeamLead).To((UserStatus property) => property.User.FullName).Apply();
                this.CreateBinding().For(cell => cell.CompletedText).To((UserStatus property) => property.MaxCompletedPercent).Apply();
                this.CreateBinding().For(cell => cell.WaitTimeText).To((UserStatus property) => property.MaxAvgWaitTimePercent).Apply();
                this.CreateBinding().For(cell => cell.CompletedBarProgress).To((UserStatus property) => property.MaxCompletedPercent).Apply();
                this.CreateBinding().For(cell => cell.WaitTimeBarProgress).To((UserStatus property) => property.MaxAvgWaitTimePercent).Apply();
            });
        }

    }
}