using CartoPrime.Helpers;
using CartoPrime.Http;
using CartoPrime.Http.Interfaces;
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
using Xamarin.Forms;

namespace CartoPrime.Modules.Round.ViewModels
{
    public class RoundPlayersPageViewModel : BaseViewModel<RoundPlayersPageViewModel>, IQueryAttributable, INotifyPropertyChanged
    {
        #region Parameters
        public async void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            string paran = HttpUtility.UrlDecode(query["paran"]);
            await Init(paran).ConfigureAwait(true);
        }

        private async Task Init(string paran)
        {
            try
            {
                ShowLoading(AppResources.LOADING);
                Partida = JsonConvert.DeserializeObject<Partida>(paran);
                await SetClubsAsync().ConfigureAwait(true);
                await SetAthleteAsync().ConfigureAwait(true);
                AnalyticsEvents.AnalyticsHandle("RoundPlayersPageView", nameof(Init));
                HideLoading();
            }
            catch (Exception ex)
            {
                AnalyticsEvents.AnalyticsHandle("RoundPlayersPageView", nameof(Init), ex.StackTrace);
                HideLoading();
            }
        }
        #endregion

        #region Properties
        private Partida Partida { get; set; }
        private string _clubeCasaEscodo;
        public string ClubeCasaEscudo
        {
            get { return _clubeCasaEscodo; }
            set { SetProperty(ref _clubeCasaEscodo, value); }
        }
        private string _clubeVisitanteEscudo;
        public string ClubeVisitanteEscudo
        {
            get { return _clubeVisitanteEscudo; }
            set { SetProperty(ref _clubeVisitanteEscudo, value); }
        }
        private string _clubeCasaPosicao;
        public string ClubeCasaPosicao
        {
            get { return _clubeCasaPosicao; }
            set { SetProperty(ref _clubeCasaPosicao, value); }
        }
        private string _clubeVisitantePosicao;
        public string ClubeVisitantePosicao
        {
            get { return _clubeVisitantePosicao; }
            set { SetProperty(ref _clubeVisitantePosicao, value); }
        }
        private string _clubeCasaNome;
        public string ClubeCasaNome
        {
            get { return _clubeCasaNome; }
            set { SetProperty(ref _clubeCasaNome, value); }
        }
        private string _clubeVisitanteNome;
        public string ClubeVisitanteNome
        {
            get { return _clubeVisitanteNome; }
            set { SetProperty(ref _clubeVisitanteNome, value); }
        }
        private string _placarOficialMandante;
        public string PlacarOficialMandante
        {
            get { return _placarOficialMandante; }
            set { SetProperty(ref _placarOficialMandante, value); }
        }
        private string _placarOfficailVisitante;
        public string PlacarOficialVisitante
        {
            get { return _placarOfficailVisitante; }
            set { SetProperty(ref _placarOfficailVisitante, value); }
        }

        private List<AthleteMarket> _athleteMarkets;
        public List<AthleteMarket> AthleteMarkets
        {
            get { return _athleteMarkets; }
            set
            {
                SetProperty(ref _athleteMarkets, value);
            }
        }

        #endregion

        #region Construtor
        public RoundPlayersPageViewModel()
        {

        }
        #endregion

        #region Metodos
        public async Task SetAthleteAsync()
        {
            var AtletasStatusCasa = new List<AthleteMarket>();

            var clubes = await base._clubService.ListClubs();

            var lstAtletas = await marketService.AthletesMarket();

            var AtletasCasa = lstAtletas.FindAll(x => x.clube_id == Partida.clube_casa_id);
            var AtletasVist = lstAtletas.FindAll(x => x.clube_id == Partida.clube_visitante_id);

            AtletasStatusCasa = AtletasCasa.FindAll(x => x.status_id == 7);
            var AtletasStatusFora = AtletasVist.FindAll(x => x.status_id == 7);

            var statusMercado = await marketService.Information();

            AtletasStatusCasa.AddRange(AtletasStatusFora);

            for (int i = 0; i < AtletasStatusCasa.Count; i++)
            {
                AtletasStatusCasa[i].escudo_clube = clubes.Find(x => x.id == AtletasStatusCasa[i].clube_id)._30x30;
                AtletasStatusCasa[i].posicao_string = await _positionsService.Get(AtletasStatusCasa[i].posicao_id);
                AtletasStatusCasa[i].img_status = "provavel.png";
                AtletasStatusCasa[i].scouts = OrganizarScouts(AtletasStatusCasa[i].scout);
                AtletasStatusCasa[i].min_valorizacao = await athleteService.ScoreToValue(AtletasStatusCasa[i].preco_double);
                AtletasStatusCasa[i].variacoes = "C$ " + AtletasStatusCasa[i].preco_num + " | " + "VR " + AtletasStatusCasa[i].variacao_num
                     + " | " + "ME " + AtletasStatusCasa[i].media_num + " | " + "JO " + AtletasStatusCasa[i].jogos_num 
                     + " | " + "Min p/ Val. " + AtletasStatusCasa[i].min_valorizacao;




                AtletasStatusCasa[i].pont_color = "Black";
                AtletasStatusCasa[i].scout_tela = "";
                AtletasStatusCasa[i].scout_tela_basico = "";
                AtletasStatusCasa[i].scout_tela_def = "";

                if (statusMercado.status_mercado == 2)
                {
                    var pontos = await athleteService.GetAthleteAsync();
                    var Partidacial = pontos.Find(x => x.id_jogador == AtletasStatusCasa[i].atleta_id);
                    if (Partidacial != null)
                    {
                        AtletasStatusCasa[i].Pontos = string.Format(@"{0:n}", Partidacial.pontuacao_dou);
                        AtletasStatusCasa[i].scout_tela = Partidacial.scout_tela;
                        AtletasStatusCasa[i].scout_tela_def = Partidacial.scout_tela_def;
                        if (Partidacial.pontuacao_dou < 0)
                        {
                            AtletasStatusCasa[i].pont_color = "#b55b49";
                        }
                        else if (Partidacial.pontuacao_dou > 0)
                        {
                            AtletasStatusCasa[i].pont_color = "#7eaf7e";
                        }
                        else
                        {
                            AtletasStatusCasa[i].pont_color = "Gray";
                        }
                    }
                    else
                    {
                        AtletasStatusCasa[i].Pontos = "0.0";
                        AtletasStatusCasa[i].pont_color = "Gray";
                    }
                }
                else
                {
                    AtletasStatusCasa[i].Pontos = string.Format(@"{0:n}", AtletasStatusCasa[i].pontos_num);
                }

            }

            AthleteMarkets = AtletasStatusCasa.OrderBy(x => x.posicao_id).ToList();
        }

        public async Task SetClubsAsync()
        {
            ClubeCasaEscudo = Partida.clube_casa_escudo;
            ClubeVisitanteEscudo = Partida.clube_visitante_escudo;
            ClubeCasaPosicao = Partida.clube_casa_posicao + "º";
            ClubeVisitantePosicao = Partida.clube_visitante_posicao + "º";
            ClubeCasaNome = Partida.clube_casa_nome;
            ClubeVisitanteNome = Partida.clube_visitante_nome;
            PlacarOficialMandante = Partida.placar_oficial_mandante == null ? "" : Partida.placar_oficial_mandante.ToString();
            PlacarOficialVisitante = Partida.placar_oficial_visitante == null ? "" : Partida.placar_oficial_visitante.ToString();


        }

        private string OrganizarScouts(Scout scout)
        {
            string scout_tela = "";
            #region scout Ataque
            if (scout != null)
            {
                if (scout.G > 0)
                {
                    scout_tela += scout.G + "G ";
                }
                if (scout.A > 0)
                {
                    scout_tela += scout.A + "A ";
                }
                if (scout.FT > 0)
                {
                    scout_tela += scout.FT + "FT ";
                }
                if (scout.FD > 0)
                {
                    scout_tela += scout.FD + "FD ";
                }
                if (scout.FF > 0)
                {
                    scout_tela += scout.FF + "FF ";
                }
                if (scout.FS > 0)
                {
                    scout_tela += scout.FS + "FS ";
                }
                if (scout.PE > 0)
                {
                    scout_tela += scout.PE + "PE ";
                }
                if (scout.I > 0)
                {
                    scout_tela += scout.I + "I ";
                }
                if (scout.PP > 0)
                {
                    scout_tela += scout.PP + "PP ";
                }
                if(scout.PI > 0)
                {
                    scout_tela += scout.PI + "PI ";
                }
                if (scout.DS > 0)
                {
                    scout_tela += scout.DS + "DS ";
                }
                #endregion

                #region scout Defesa
                if (scout.RB > 0)
                {
                    scout_tela += scout.RB + "RB ";
                }
                if (scout.FC > 0)
                {
                    scout_tela += scout.FC + "FC ";
                }
                if (scout.GC > 0)
                {
                    scout_tela += scout.GC + "GC ";
                }
                if (scout.CA > 0)
                {
                    scout_tela += scout.CA + "CA ";
                }
                if (scout.CV > 0)
                {
                    scout_tela += scout.CV + "CV ";
                }
                if (scout.SG > 0)
                {
                    scout_tela += scout.SG + "SG ";
                }
                if (scout.DE > 0)
                {
                    scout_tela += scout.DE + "DE ";
                }

                if (scout.DP > 0)
                {
                    scout_tela += scout.DP + "DP ";
                }
                if (scout.GS > 0)
                {
                    scout_tela += scout.GS + "GS";
                }
            }
            #endregion

            return scout_tela;
        }
        #endregion
    }
}
