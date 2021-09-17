using CartoPrime.Interfaces;
using CartoPrime.Models;
using CartoPrime.Modules.Base;
using CartoPrime.Resources;
using CartoPrime.Services;
using CartoPrime.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CartoPrime.Modules.Classification.ViewModels
{
    public class ClassificationBRAPageViewModel : BaseViewModel<ClassificationBRAPageViewModel>
    {
        #region Properties
        private LoadingPopupPage pageLoading;
        private List<Classificacao> _classificationBra;
        public List<Classificacao> ClassificationBra
        {
            get { return _classificationBra; }
            set { SetProperty(ref _classificationBra, value); }
        }

        #endregion

        #region Construtor
        public ClassificationBRAPageViewModel()
        {
            Title = "Brasileirão 2020";
            //eventTracker.SendEvent("ClassificationBRAPage");
            BuscarRest().ConfigureAwait(true);
        }
        #endregion

        #region Methods
        private async Task BuscarRest()
        {
            try
            {
                ShowLoading(AppResources.LOADING);
                AnalyticsEvents.AnalyticsHandle("ClassificationBRAPage", nameof(BuscarRest));
                var Classifi = await ClassificationBRAService.GetClassication(DateTime.Now.Year).ConfigureAwait(true);
                ClassificationBra = Classifi.classificacao;
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
