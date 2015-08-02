// ***********************************************************************
// Assembly         : Xamarin.IncidentApp
// Author           : Ken Ross
// Created          : 07-26-2015
//
// Last Modified By : Ken Ross
// Last Modified On : 07-26-2015
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.MvvmCross.Plugins.Network;
using Acr.UserDialogs;
using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.ViewModels;
using Xamarin.IncidentApp.Interfaces;
using Xamarin.IncidentApp.Models;

/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace Xamarin.IncidentApp.ViewModels
{
    /// <summary>
    /// Class DashboardViewModel.
    /// </summary>
    public class DashboardViewModel : BaseViewModel
    {
        private IAzureService _azureService;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardViewModel"/> class.
        /// </summary>
        /// <param name="networkService">The network service.</param>
        /// <param name="userDialogs">The user dialogs.</param>
        /// <param name="mobileService">Service for accessing Azure.</param>
        public DashboardViewModel(INetworkService networkService, IUserDialogs userDialogs, IAzureService mobileService) : base(networkService, userDialogs)
        {
            _azureService = mobileService;
        }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get { return "Team Performance";  }
        }

        /// <summary>
        /// The private _userstatuses collection.
        /// </summary>
        private ObservableCollection<UserStatus> _userStatuses;

        /// <summary>
        /// Gets the user statuses.
        /// </summary>
        /// <value>The user statuses.</value>
        public ObservableCollection<UserStatus> UserStatuses
        {
            get { return _userStatuses; }
            private set
            {
                _userStatuses = value;
                RaisePropertyChanged(() => UserStatuses);
            }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public async void Init()
        {
            await RefreshDashboardAsync();
        }

        /// <summary>
        /// Refresh dashboard as an asynchronous operation.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task RefreshDashboardAsync()
        {
            var service = _azureService.MobileService;
            if (service.CurrentUser != null)
            {
                if (NetworkService.IsConnected)
                {
                    try
                    {
                        UserDialogs.ShowLoading("Retrieving Worker Statistics...");
                        
                        var userStatuses = await service.InvokeApiAsync<IList<UserStatus>>("StatusList", HttpMethod.Get, null);
                        var maxCompleted = MaxCompletedIncidents(userStatuses);
                        var maxOpen = MaxOpenIncidents(userStatuses);
                        var maxWaitTime = MaxWaitTime(userStatuses);
                        foreach (var userStatus in userStatuses)
                        {
                            userStatus.MaxCompletedIncidents = maxCompleted;
                            userStatus.MaxWaitTime = maxWaitTime;
                            userStatus.MaxOpenIncidents = maxOpen;
                        }
                        var collection = new ObservableCollection<UserStatus>(userStatuses);
                        UserStatuses = collection;
                    }
                    finally
                    {
                        UserDialogs.HideLoading();
                    }
                }
            }
        }

        /// <summary>
        /// Command to add a new incident.
        /// </summary>
        private ICommand _addIncidentCommand;

        /// <summary>
        /// Gets the add incident command.
        /// </summary>
        /// <value>The add incident command.</value>
        public ICommand AddIncidentCommand
        {
            get
            {
                return _addIncidentCommand ?? (_addIncidentCommand = new MvxCommand(() =>
                {
                    AddIncident();
                }));
            }
        }

        /// <summary>
        /// Adds the incident.
        /// </summary>
        private void AddIncident()
        {
            ShowViewModel<AddIncidentViewModel>();
        }

        /// <summary>
        /// Command to show the Worker [Incident] Queue.
        /// </summary>
        private ICommand _showWorkerQueueCommand;

        /// <summary>
        /// Gets the show worker queue command.
        /// </summary>
        /// <value>The show worker queue command.</value>
        public ICommand ShowWorkerQueueCommand
        {
            get
            {
                return _showWorkerQueueCommand ?? (_showWorkerQueueCommand = new MvxRelayCommand<UserStatus>(user =>
                {
                    ShowWorkerQueue(user.User.UserId);
                }));
            }
        }

        /// <summary>
        /// Shows the worker queue.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        private void ShowWorkerQueue(string userId)
        {
            ShowViewModel<WorkerQueueViewModel>(userId);
        }

        /// <summary>
        /// Returns the maximum value for average wait times.
        /// </summary>
        /// <param name="userStatuses">The user statuses.</param>
        /// <returns>System.Double.</returns>
        private double MaxWaitTime(IList<UserStatus> userStatuses)
        {
            if (userStatuses == null || userStatuses.Count == 0)
            {
                return 0;
            }
            return userStatuses.Max(u => u.AvgWaitTimeOfOpenIncidents);
        }

        /// <summary>
        /// Returns the maximum value for total completed incidents.
        /// </summary>
        /// <param name="userStatuses">The user statuses.</param>
        /// <returns>System.Int32.</returns>
        private int MaxCompletedIncidents(IList<UserStatus> userStatuses)
        {
            if (userStatuses == null || userStatuses.Count == 0)
            {
                return 0;
            }
            return userStatuses.Max(u => u.TotalCompleteIncidentsPast30Days);
        }

        private int MaxOpenIncidents(IList<UserStatus> userStatuses)
        {
            if (userStatuses == null || userStatuses.Count == 0)
            {
                return 0;
            }
            return userStatuses.Max(u => u.TotalOpenIncidents);
        }

    }
}
