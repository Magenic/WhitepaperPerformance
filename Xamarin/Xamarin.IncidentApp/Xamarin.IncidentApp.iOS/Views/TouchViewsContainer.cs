using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.ViewModels;
using Foundation;
using UIKit;

namespace Xamarin.IncidentApp.iOS.Views
{
    public class TouchViewsContainer : MvxTouchViewsContainer
    {
        protected override IMvxTouchView CreateViewOfType(Type viewType, MvxViewModelRequest request)
        {
            MvxTrace.Trace(MvxTraceLevel.Diagnostic, "Create iOS View:" + viewType.Name);
            var storyboard = UIStoryboard.FromName("Main", null);
            var returnValue = storyboard.InstantiateViewController(viewType.Name);

            return (IMvxTouchView)returnValue;
        }
    }
}