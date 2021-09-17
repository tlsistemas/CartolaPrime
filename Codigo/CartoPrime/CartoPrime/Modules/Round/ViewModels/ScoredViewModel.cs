using CartoPrime.Helpers;
using CartoPrime.Interfaces;
using CartoPrime.Models;
using CartoPrime.Models.Enum;
using CartoPrime.Modules.Base;
using CartoPrime.Resources;
using CartoPrime.Services;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CartoPrime.Modules.Round.ViewModels
{
    public class ScoredViewModel : BaseViewModel<ScoredViewModel>
    {
        #region Properties
        private List<BaseAthletePunctuated> _athleteScored;
        public List<BaseAthletePunctuated> AthleteScoreds
        {
            get { return _athleteScored; }
            set
            {
                SetProperty(ref _athleteScored, value);
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

        private bool _openMarket;
        public bool OpenMarket
        {
            get { return _openMarket; }
            set { SetProperty(ref _openMarket, value); }
        }
        private bool _closeMarket;
        public bool CloseMarket
        {
            get { return _closeMarket; }
            set { SetProperty(ref _closeMarket, value); }
        }
        #endregion

        #region Comands
        public ICommand RefreshButtonCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await UpdateList().ConfigureAwait(true);
                });
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    await UpdateList().ConfigureAwait(true);

                    IsRefreshing = false;
                });
            }
        }
        #endregion

        public ScoredViewModel()
        {
            GetInforsAsync().ConfigureAwait(true);
        }

        private async Task GetInforsAsync()
        {
            try
            {
                //pageLoading = new LoadingPopupPage();
                //await App.Current.MainPage.Navigation.PushPopupAsync(pageLoading, true);
                if (IsBusy) return;
                IsBusy = true;
                ShowLoading(AppResources.LOADING);
                var market = new Market();
                market = Barrel.Current.Get<Market>(key: UrlCartola._mercadoStatus);
                if(market == null)
                {
                    market = await marketService.Information().ConfigureAwait(true);
                    if(market == null)
                    {
                        OpenMarket = true;
                        CloseMarket = false;
                    }
                }
                
                if (market.status_mercado == (int)EMarket.Open)
                {
                    OpenMarket = true;
                    CloseMarket = false;
                }
                else if (market.status_mercado == (int)EMarket.Closed)
                {
                    OpenMarket = false;
                    CloseMarket = true;

                    AthleteScoreds = await athleteService.GetAthleteScoredAsync().ConfigureAwait(true);

                    var partidas = await _clubService.GetConfrontos(market.rodada_atual.ToString()).ConfigureAwait(true);
                    var marketAthle = await marketService.AthletesMarket().ConfigureAwait(true);
                    for (int i = 0; i < AthleteScoreds.Count; i++)
                    {
                        var athle = marketAthle.Find(x => x.atleta_id == AthleteScoreds[i].id_jogador);
                        AthleteScoreds[i].variacao_num = athle.variacao_num == null ? 0 : double.Parse(athle.variacao_num);
                        AthleteScoreds[i].media_num = athle.media_num;
                        AthleteScoreds[i].preco_double = athle.preco_double;//== null ? "0.00" : double.Parse(athle.preco_double);
                        AthleteScoreds[i].min_valorizacao = await athleteService.ScoreToValue(athle.preco_double);
                        AthleteScoreds[i].Partida = partidas.Find(x => x.clube_casa_id == int.Parse(AthleteScoreds[i].clube_id)
                                                                  || x.clube_visitante_id == int.Parse(AthleteScoreds[i].clube_id));
                      
                    }


                }
                else if (market.status_mercado == (int)EMarket.Support)
                {
                    AthleteScoreds = Barrel.Current.Get<List<BaseAthletePunctuated>>(key: UrlCartola._pontuados);
                    Xamarin.Forms.DependencyService.Get<IMessage>().LongAlert("Mercado em manutenção, alguma informações podem não aparecer.");
                }
                    AnalyticsEvents.AnalyticsHandle("ScoredView", nameof(GetInforsAsync));
            }
            catch (Exception ex)
            {
                AnalyticsEvents.AnalyticsHandle("ScoredView", nameof(Init), ex.StackTrace);

                OpenMarket = true;
                CloseMarket = false;
            }
            //pageLoading.HidePopup();
            HideLoading();
            IsBusy = false;
        }

        private async Task UpdateList()
        {
            try
            {
                //pageLoading = new LoadingPopupPage();
                //await App.Current.MainPage.Navigation.PushPopupAsync(pageLoading, true);
                if (IsBusy) return;
                IsBusy = true;
                ShowLoading(AppResources.LOADING);
                var market = Barrel.Current.Get<Market>(key: UrlCartola._mercadoStatus);
                if (market.status_mercado == (int)EMarket.Open)
                {
                    OpenMarket = true;
                    CloseMarket = false;
                }
                else if (market.status_mercado == (int)EMarket.Closed)
                {
                    OpenMarket = false;
                    CloseMarket = true;

                    AthleteScoreds = await athleteService.GetAthleteScoredAsync().ConfigureAwait(true);

                }
                else if (market.status_mercado == (int)EMarket.Support)
                {
                    AthleteScoreds = Barrel.Current.Get<List<BaseAthletePunctuated>>(key: UrlCartola._pontuados);
                    Xamarin.Forms.DependencyService.Get<IMessage>().LongAlert("Mercado em manutenção, alguma informações podem não aparecer.");
                }

                AnalyticsEvents.AnalyticsHandle("ScoredView", nameof(UpdateList));

            }
            catch (Exception ex)
            {
                AnalyticsEvents.AnalyticsHandle("ScoredView", nameof(UpdateList), ex.StackTrace);

            }
            //pageLoading.HidePopup();
            HideLoading();
            IsBusy = false;
        }
    }
}
