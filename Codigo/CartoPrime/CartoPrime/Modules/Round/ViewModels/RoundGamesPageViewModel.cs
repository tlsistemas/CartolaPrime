using CartoPrime.Helpers;
using CartoPrime.Http;
using CartoPrime.Http.Interfaces;
using CartoPrime.Interfaces;
using CartoPrime.Models;
using CartoPrime.Modules.Base;
using CartoPrime.Modules.Round.Views;
using CartoPrime.Resources;
using CartoPrime.Services;
using CartoPrime.ViewModels;
using CartoPrime.Views;
using Newtonsoft.Json;


using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using Xamarin.Forms;

namespace CartoPrime.Modules.Round.ViewModels
{
    public class RoundGamesPageViewModel : BaseViewModel<RoundGamesPageViewModel>
    {
        #region Properties
        private string _round;
        public string Round
        {
            get { return _round; }
            set { SetProperty(ref _round, value); }
        }
        private Partida _gamesSelected;
        public Partida GamesSelected
        {
            get { return _gamesSelected; }
            set
            {
                SetProperty(ref _gamesSelected, value);
                if (value != null)
                    _ = ViewRound(value).ConfigureAwait(true);
            }
        }

        private List<Partida> _games;
        public List<Partida> Games
        {
            get { return _games; }
            set { SetProperty(ref _games, value); }
        }

        private int CurrentRound { get; set; }
        #endregion

        #region Commands
        public ICommand ViewRoundCommand { get; set; }
        public ICommand PreviousRoundCommand { get; set; }
        public ICommand NextRoundCommand { get; set; }
        #endregion

        #region Construtor
        public RoundGamesPageViewModel()
        {
            Title = "Jogos da Rodada";

            NextRoundCommand = new Command<object>(async (item) => await OnNextRount(item));
            PreviousRoundCommand = new Command<object>(async (item) => await OnPreviousRound(item));
            ViewRoundCommand = new Command<object>(async (item) => await ViewRound(item));

            _ = GetInforAsync().ConfigureAwait(true);
        }
        #endregion

        #region Metodos
        private async Task GetInforAsync(string rodada = null)
        {
            var response = new BaseResponse<Rodada>();
            var partidas = new List<Partida>();
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                ShowLoading(AppResources.LOADING);

                response = await Api.Get<Rodada>(UrlCartola._partidas + "/" + rodada);
                partidas = response.Data.Partidas.OrderBy(x => x.partida_data).ToList();
                var clubes = await _clubService.ListClubs();
                for (int i = 0; i < partidas.Count; i++)
                {
                    var clube_mand = clubes.Find(x => x.id == partidas[i].clube_casa_id);
                    var clube_visit = clubes.Find(x => x.id == partidas[i].clube_visitante_id);
                    partidas[i].clube_casa_escudo = clube_mand._30x30;
                    partidas[i].clube_visitante_escudo = clube_visit._30x30;
                    partidas[i].clube_casa_nome = clube_mand.nome;
                    partidas[i].clube_visitante_nome = clube_visit.nome;
                    partidas[i].local_confronto = (string.Concat(partidas[i].local, " - ", string.Format("{0:f}", partidas[i].partida_data)));
                }
                Games = partidas;
                Round = response.Data.RodadaAtual + "º Rodada";
                CurrentRound = response.Data.RodadaAtual;
                AnalyticsEvents.AnalyticsHandle("RoundGamesPageView", nameof(GetInforAsync));
            }
            catch (Exception ex)
            {
                AnalyticsEvents.AnalyticsHandle("RoundGamesPageView", nameof(GetInforAsync), ex.StackTrace);

            }

            HideLoading();
            IsBusy = false;


        }
        private async Task OnNextRount(object sender)
        {
            var rodada = this.CurrentRound + 1;
            if (rodada <= 38)
            {
                this.CurrentRound = rodada;
                GetInforAsync(rodada.ToString());
            }
        }

        private async Task OnPreviousRound(object sender)
        {
            var rodada = this.CurrentRound - 1;
            if (rodada >= 1)
            {
                this.CurrentRound = rodada;
                GetInforAsync(rodada.ToString());
            }
        }

        private async Task ViewRound(object sender)
        {
            var json = JsonConvert.SerializeObject(GamesSelected);
            GamesSelected = null;
            AnalyticsEvents.AnalyticsHandle("RoundGamesPageView", nameof(ViewRound), json);
            await Shell.Current.GoToAsync($"{Pages.RoundPlayersPage}?paran={json}").ConfigureAwait(true);
        }
        #endregion

        #region GoogleAd
        private void AddGoogleAD()
        {
            try
            {
                Xamarin.Forms.DependencyService.Get<IAdmobInterstitial>().Show();
                //AnalyticsService.RastrearEvento(EventAnalytics.Category_NavigationPage, "MaisEscaladosPage");

                var mostrarProp = int.Parse(App.Current.Properties["click"].ToString());
                mostrarProp += 1;

                if (mostrarProp == 5)
                {
                    mostrarProp = 0;
                    Xamarin.Forms.DependencyService.Get<IAdmobInterstitial>().Show();
                }

                App.Current.Properties["click"] = mostrarProp;
            }
            catch (System.Exception ex)
            {
            }
        }

        #endregion
    }
}