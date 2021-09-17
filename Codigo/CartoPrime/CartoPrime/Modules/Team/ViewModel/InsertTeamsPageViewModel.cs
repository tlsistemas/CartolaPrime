using CartoPrime.Helpers;
using CartoPrime.Http.Interfaces;
using CartoPrime.Interfaces;
using CartoPrime.Models;
using CartoPrime.Modules.Base;
using CartoPrime.Resources;
using CartoPrime.Services;
using CartoPrime.ViewModels;
using CartoPrime.Views;




using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CartoPrime.Modules.Team.ViewModels
{
    public class InsertTeamsPageViewModel : BaseViewModel<InsertTeamsPageViewModel>
    {
        #region Properties
        private string _search;
        public string Search
        {
            get { return _search; }
            set
            {
                SetProperty(ref _search, value);
            }
        }
        private List<TeamCA> _teams;
        public List<TeamCA> Teams
        {
            get { return _teams; }
            set
            {
                SetProperty(ref _teams, value);
            }
        }
        private bool _visibleList;
        public bool VisibleList
        {
            get { return _visibleList; }
            set
            {
                SetProperty(ref _visibleList, value);
            }
        }
        private TeamCA _selectedItem;
        public TeamCA SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;

                if (_selectedItem == null)
                    return;

                TapCommand.Execute(_selectedItem);

                //SelectedItem = null;
            }
        }
        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }
        #endregion

        #region Commands
        public ICommand SearchCommand { get; set; }
        public ICommand TapCommand { get; set; }
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    await GetAsync();

                    IsRefreshing = false;
                });
            }
        }
        #endregion

        public InsertTeamsPageViewModel()
        {
            Title = "Adicionar";
            SearchCommand = new Command<object>(SearchTime);
            TapCommand = new Command<object>(SelectAsync);
            VisibleList = false;
        }

        #region Eventos
        private async void SearchTime(object sender)
        {
            await GetAsync().ConfigureAwait(true);
        }
        private async void SelectAsync(object sender)
        {
            try
            {
                //pageLoading = new LoadingPopupPage();
                //await App.Current.MainPage.Navigation.PushPopupAsync(pageLoading, true);
                if (IsBusy) return;
                IsBusy = true;
                ShowLoading(AppResources.LOADING);
                var clubes = await _clubService.ListClubs().ConfigureAwait(true);
                var save = await teamService.InsetTeamsFullCAAsync(SelectedItem, clubes).ConfigureAwait(true);


                if (save == 0)
                {
                    Teams = await teamService.GetTeamsCAAsync(Search).ConfigureAwait(true);
                    Xamarin.Forms.DependencyService.Get<IMessage>().LongAlert("Time adicionado.");
                }
                else if (save == 1)
                    Xamarin.Forms.DependencyService.Get<IMessage>().LongAlert("Por favor, tente novamente.");
                else if (save == 2)
                    Xamarin.Forms.DependencyService.Get<IMessage>().LongAlert("Já exiteste esse time na lista.");
                SelectedItem = null;

            }
            catch (Exception)
            {
                Xamarin.Forms.DependencyService.Get<IMessage>().LongAlert("Por favor, tente novamente.");
            }
            //pageLoading.HidePopup();
            HideLoading();
            IsBusy = false;
        }
        #endregion

        #region Metodos
        private async Task GetAsync()
        {
            try
            {
                //pageLoading = new LoadingPopupPage();
                //await App.Current.MainPage.Navigation.PushPopupAsync(pageLoading, true);
                if (IsBusy) return;
                IsBusy = true;
                ShowLoading(AppResources.LOADING);

                Teams = await teamService.GetTeamsCAAsync(Search).ConfigureAwait(true);

                VisibleList = true;
                //pageLoading.HidePopup();
                HideLoading();
                IsBusy = false;
                AnalyticsEvents.AnalyticsHandle("InsertTeamsPageView", nameof(GetAsync));

            }
            catch (Exception ex)
            {
                AnalyticsEvents.AnalyticsHandle("InsertTeamsPageView", nameof(GetAsync), ex.StackTrace);

            }
        }

        #endregion
    }
}
