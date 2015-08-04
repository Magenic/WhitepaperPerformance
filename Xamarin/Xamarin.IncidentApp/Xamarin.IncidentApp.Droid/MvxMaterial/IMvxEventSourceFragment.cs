using System;

using Android.App;
using Android.OS;
using Cirrious.CrossCore.Core;

namespace Xamarin.IncidentApp.Droid.MvxMaterial
{
    public interface IMvxEventSourceFragment : IMvxDisposeSource
    {
        //Created sate
        event EventHandler<MvxValueEventArgs<Activity>> AttachCalled;
        event EventHandler<MvxValueEventArgs<Bundle>> CreateWillBeCalled;
        event EventHandler<MvxValueEventArgs<Bundle>> CreateCalled;
        event EventHandler<MvxValueEventArgs<MvxCreateViewParameters>> CreateViewCalled;

        //Started state
        event EventHandler StartCalled;
        //Resumed state
        event EventHandler ResumeCalled;
        //Paused state
        event EventHandler PauseCalled;
        //Stopped state
        event EventHandler StopCalled;

        //Destroyed state
        event EventHandler DestroyViewCalled;
        event EventHandler DestroyCalled;
        event EventHandler DetachCalled;

        event EventHandler<MvxValueEventArgs<Bundle>> SaveInstanceStateCalled;
    }
}