using Cirrious.CrossCore;
using Cirrious.CrossCore.IoC;
using Cirrious.MvvmCross.ViewModels;
using Xamarin.IncidentApp.Interfaces;
using Xamarin.IncidentApp.Utilities;
using Xamarin.IncidentApp.ViewModels;

namespace Xamarin.IncidentApp
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
				
            Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<LoginViewModel>());
            RegisterAppStart<LoginViewModel>();
        }
    }
}