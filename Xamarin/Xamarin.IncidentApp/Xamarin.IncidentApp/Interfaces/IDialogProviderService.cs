using System;
using Acr.UserDialogs;

namespace Xamarin.IncidentApp.Interfaces
{
    public interface IDialogProviderService
    {
        IProgressDialog ShowLoading(string message);
        IProgressDialog ShowLoading();
        void ShowAlert(string message);
        void ShowToast(string message);
        void ShowConfirm(string message, string title, string okText, string cancelText, Action<bool> OnConfirm);
        void ShowActionSheet(ActionSheetConfig config);
    }
}
