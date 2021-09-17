using CartoPrime.Helpers;
using CartoPrime.Models;
using CartoPrime.Models.BonsBico;
using CartoPrime.Modules.Base;
using CartoPrime.Modules.BonsBico.Views;
using CartoPrime.Modules.Round.Views;
using CartoPrime.Resources;
using CartoPrime.Services;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CartoPrime.Modules.BonsBico.ViewModels
{
    public class LigaBonsDeBicoViewModel : BaseViewModel<LigaBonsDeBicoViewModel>
    {
        #region Properties
        private string _rodada;
        public string Rodada
        {
            get { return _rodada; }
            set { SetProperty(ref _rodada, value); }
        }

        private List<BonsDeBico> _jogos;
        public List<BonsDeBico> Jogos
        {
            get { return _jogos; }
            set { SetProperty(ref _jogos, value); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }


        #endregion

        #region Comands
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await BuscarRest(true).ConfigureAwait(true);
                });
            }
        }

        public ICommand SelectedCommand { get; set; }
        #endregion

        #region Construtor
        public LigaBonsDeBicoViewModel()
        {
            Title = "Confrontos Serie A";
            //eventTracker.SendEvent("ClassificationBRAPage");
            SelectedCommand = new Command<BonsDeBico>(async (item) => await ViewPalyers(item));
            BuscarRest().ConfigureAwait(true);
        }
        #endregion

        #region Methods
        private async Task BuscarRest(bool force = false)
        {
            try
            {
                ShowLoading(AppResources.LOADING);
                AnalyticsEvents.AnalyticsHandle("LigaBonsDeBicoView", nameof(BuscarRest));

                Jogos = await _bonsDeBicoService.ListConfrontos(force).ConfigureAwait(true);



                this.Rodada = this.Jogos.FirstOrDefault().Rodada + "º Rodada";
                HideLoading();
            }
            catch (Exception ex)
            {
                HideLoading();
            }

        }

        public async Task ViewPalyers(BonsDeBico jogo)
        {
            int idA = jogo.TimeA.Id;
            int idB = jogo.TimeB.Id;
            AnalyticsEvents.AnalyticsHandle("ComparePlayersView", nameof(ViewPalyers));
            await Shell.Current.GoToAsync($"{Pages.ComparePlayersView}?idA={idA.ToString()}&idB={idB.ToString()}").ConfigureAwait(true);

        }
        #endregion
    }
}
