using CartoPrime.Helpers;
using CartoPrime.Models;
using CartoPrime.Models.BonsBico;
using CartoPrime.Modules.Base;
using CartoPrime.Resources;
using CartoPrime.Services;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CartoPrime.Modules.BonsBico.ViewModels
{
    public class LigaBonsDeBicoViewBModel : BaseViewModel<LigaBonsDeBicoViewBModel>
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

        #region Construtor
        public LigaBonsDeBicoViewBModel()
        {
            Title = "Confrontos Serie B";
            //eventTracker.SendEvent("ClassificationBRAPage");
            BuscarRest().ConfigureAwait(true);
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
        #endregion

        #region Methods
        private async Task BuscarRest(bool force = false)
        {
            try
            {
                ShowLoading(AppResources.LOADING);
                AnalyticsEvents.AnalyticsHandle("LigaBonsDeBicoViewB", nameof(BuscarRest));

                Jogos = await _bonsDeBicoService.ListConfrontosB(force).ConfigureAwait(true);


                this.Rodada = this.Jogos.FirstOrDefault().Rodada + "º Rodada";
                HideLoading();
            }
            catch (Exception ex)
            {
                HideLoading();
            }

        }
        #endregion
    }
}
