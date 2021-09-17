using CartoPrime.Http.Interfaces;
using CartoPrime.Models;
using CartoPrime.Modules.Base;
using CartoPrime.Resources;
using CartoPrime.Services;
using CartoPrime.Views;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CartoPrime.Modules.MyInformations.ViewModels
{
    public class NewsPageViewModel : BaseViewModel<NewsPageViewModel>
    {
        #region Interface
        INewsService _newsService;
        #endregion
        #region Properties
        private List<News> _news;

        public List<News> News
        {
            get { return _news; }
            set
            {
                SetProperty(ref _news, value);
            }
        }
        #endregion
        #region Commands
        public Command<object> ViewNewsCommand { get; set; }
        #endregion
        public NewsPageViewModel(IApiService apiService, INewsService NewsService)
        {
            _newsService = NewsService;
            ViewNewsCommand = new Command<object>(ViewNews);
            BuscarRest();
        }

        #region Metodos
        private async void BuscarRest()
        {
            //pageLoading = new LoadingPopupPage();
            //await App.Current.MainPage.Navigation.PushPopupAsync(pageLoading, true);
            if (IsBusy) return;
            IsBusy = true;
            ShowLoading(AppResources.LOADING);
            News = await _newsService.GetNewsAsync();
            //pageLoading.HidePopup();
            HideLoading();
            IsBusy = false;
            AnalyticsEvents.AnalyticsHandle("NewsPageView", nameof(BuscarRest));
        }
        #endregion

        #region Eventos

        private async void ViewNews(object sender)
        {
            var item = (News)sender;
            AnalyticsEvents.AnalyticsHandle("LeaguesPageView", nameof(ViewNews), JsonConvert.SerializeObject(item));
            await App.Current.MainPage.Navigation.PushModalAsync(new WebViewUrlPage(item.url, "Notícias", "ViewNews"));

        }
        #endregion
    }
}
