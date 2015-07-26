using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using CoreGraphics;
using Foundation;
using Xamarin.IncidentApp.Models;

namespace Xamarin.IncidentApp.iOS.Views.Cells
{
    //[Register("TeamStatusCell")]
    public partial class TeamStatusCell : MvxTableViewCell
    {
        public static readonly NSString Identifier = new NSString("TeamStatusCell");

        public TeamStatusCell (IntPtr handle) : base (handle)
		{
			InitializeBindings();
		}

        public TeamStatusCell()
        {
			InitializeBindings();
		}

        public string TeamLead
        {
            get { return lblTeamLead.Text; }
            set { lblTeamLead.Text = value; }
        }

        public nfloat CompletedBarWidth
        {
            set { uvCompletedBar.DrawRect(new CGRect(new CGPoint(120, 5), new CGSize(value, 15)), ViewPrintFormatter); }
        }

        public nfloat WaitTimeBarWidth
        {
            set { uvWaitTime.DrawRect(new CGRect(new CGPoint(120, 25), new CGSize(value, 15)), ViewPrintFormatter); }
        }

        public string CompletedText
        {
            get { return lblCompleted.Text; }
            set { lblCompleted.Text = value; }
        }

        public string WaitTimeText
        {
            get { return lblWaitTime.Text; }
            set { lblWaitTime.Text = value; }
        }

        private void InitializeBindings()
        {
            this.DelayBind(() =>
            {
                this.CreateBinding().For(cell => cell.TeamLead).To((UserStatus property) => property.User.FullName).Apply();
                this.CreateBinding().For(cell => cell.CompletedText).To((UserStatus property) => property.TotalCompleteIncidentsPast30Days).Apply();
                this.CreateBinding().For(cell => cell.WaitTimeText).To((UserStatus property) => property.AvgWaitTimeOfOpenIncidents).Apply();
                //this.CreateBinding().For(cell => cell.CompletedBarWidth).To((UserStatus property) => property.TotalCompleteIncidentsPast30Days).Apply();
                //this.CreateBinding().For(cell => cell.WaitTimeBarWidth).To((UserStatus property) => property.AvgWaitTimeOfOpenIncidents).Apply();
            });
        }

    }
}
