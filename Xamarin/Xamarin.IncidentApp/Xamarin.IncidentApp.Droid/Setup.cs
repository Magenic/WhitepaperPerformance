using System;
using System.Collections.Generic;
using System.Reflection;
using Android.Content;
using Android.Views;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Binding.Bindings.Target.Construction;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.ViewModels;
using Xamarin.IncidentApp.Droid.Bindings;
using Xamarin.IncidentApp.Interfaces;

namespace Xamarin.IncidentApp.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new App();
        }
		
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();
        }

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            registry.RegisterCustomBindingFactory<View>(
                            "ViewWidth",
                            v => new ViewWidthBinging(v));
            base.FillTargetFactories(registry);
        }

        protected override IList<Assembly> AndroidViewAssemblies
        {
            get
            {
                var toReturn = base.AndroidViewAssemblies;
                toReturn.Add(ExecutableAssembly);
                return toReturn;
            }
        }
    }
}