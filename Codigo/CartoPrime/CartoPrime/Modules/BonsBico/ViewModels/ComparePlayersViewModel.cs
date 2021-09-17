using CartoPrime.Helpers;
using CartoPrime.Models;
using CartoPrime.Models.Atletas;
using CartoPrime.Models.BonsBico;
using CartoPrime.Models.Enum;
using CartoPrime.Modules.Base;
using CartoPrime.Resources;
using CartoPrime.Services;
using MonkeyCache.FileStore;
using Plugin.Connectivity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using Xamarin.Forms;

namespace CartoPrime.Modules.BonsBico.ViewModels
{
    public class ComparePlayersViewModel : BaseViewModel<ComparePlayersViewModel>, IQueryAttributable, INotifyPropertyChanged
    {
        #region Parameters
        public async void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            string timeA = HttpUtility.UrlDecode(query["idA"]);
            string timeB = HttpUtility.UrlDecode(query["idB"]);
            await Init(int.Parse(timeA), int.Parse(timeB)).ConfigureAwait(true);
        }
        #endregion

        #region Properties
        private List<ComparePlayersModel> _athletes;
        public List<ComparePlayersModel> Athletes
        {
            get { return _athletes; }
            set
            {
                SetProperty(ref _athletes, value);
            }
        }

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

        private TimeBonsDB _timeA;
        public TimeBonsDB TimeA
        {
            get { return _timeA; }
            set { SetProperty(ref _timeA, value); }
        }
        private TimeBonsDB _timeB;
        public TimeBonsDB TimeB
        {
            get { return _timeB; }
            set { SetProperty(ref _timeB, value); }
        }
        #endregion

        #region Construtor
        public ComparePlayersViewModel()
        {
            Title = "Comparação";
        }
        #endregion

        #region Comands
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await Init(0, 0).ConfigureAwait(true);
                });
            }
        }
        #endregion

        #region Methods
        private async Task Init(int idA, int idB)
        {
            try
            {
                ShowLoading(AppResources.LOADING);

                var Jogos = await _bonsDeBicoService.GetConfrontosAtletas(idA, idB).ConfigureAwait(true);

                Rodada = Jogos.Rodada.ToString();
                TimeA = Jogos.TimeA;
                TimeB = Jogos.TimeB;
                Athletes = new List<ComparePlayersModel>();
                TimeA.Atletas = TimeA.Atletas.OrderBy(x => x.posicao_id).ToList();
                TimeB.Atletas = TimeB.Atletas.OrderBy(x => x.posicao_id).ToList();
                for (int i = 0; i < TimeA.Atletas.Count; i++)
                {
                    var comparar = new ComparePlayersModel();
                    comparar.AtletaA = new Athlete();
                    comparar.AtletaB = new Athlete();
                    comparar.AtletaA.nome = TimeA.Atletas[i].nome;
                    comparar.AtletaA.apelido = TimeA.Atletas[i].apelido;
                    comparar.AtletaA.atleta_id = TimeA.Atletas[i].atleta_id;
                    comparar.AtletaA.capita = TimeA.Atletas[i].capita;
                    comparar.AtletaA.foto = TimeA.Atletas[i].foto;
                    comparar.AtletaA.jogos_num = TimeA.Atletas[i].jogos_num;
                    comparar.AtletaA.media_num = TimeA.Atletas[i].media_num;
                    comparar.AtletaA.min_valorizacao = TimeA.Atletas[i].min_valorizacao;
                    comparar.AtletaA.Pontos = TimeA.Atletas[i].Pontos;
                    comparar.AtletaA.posicao_id = TimeA.Atletas[i].posicao_id;
                    comparar.AtletaA.scout = TimeA.Atletas[i].scout;
                    comparar.AtletaA.status = TimeA.Atletas[i].status;

                    var atletaB = TimeB.Atletas.ElementAt(i);
                    comparar.AtletaB.nome = atletaB.nome;
                    comparar.AtletaB.apelido = atletaB.apelido;
                    comparar.AtletaB.atleta_id = atletaB.atleta_id;
                    comparar.AtletaB.capita = atletaB.capita;
                    comparar.AtletaB.foto = atletaB.foto;
                    comparar.AtletaB.jogos_num = atletaB.jogos_num;
                    comparar.AtletaB.media_num = atletaB.media_num;
                    comparar.AtletaB.min_valorizacao = atletaB.min_valorizacao;
                    comparar.AtletaB.Pontos = atletaB.Pontos;
                    comparar.AtletaB.posicao_id = atletaB.posicao_id;
                    comparar.AtletaB.scout = atletaB.scout;
                    comparar.AtletaB.status = atletaB.status;

                    Athletes.Add(comparar);
                }

                AnalyticsEvents.AnalyticsHandle("ComparePlayersViewModel", nameof(Init));
                this.Rodada = this.Jogos.FirstOrDefault().Rodada + "º Rodada";
                HideLoading();
            }
            catch (Exception ex)
            {
                AnalyticsEvents.AnalyticsHandle("ComparePlayersViewModel", nameof(Init), ex.StackTrace);
                HideLoading();
            }
        }


        #endregion
    }
}