using CartoPrime.Custom.Styles;
using CartoPrime.Helpers;
using CartoPrime.Models;
using CartoPrime.Modules.Authentication.Views;
using CartoPrime.Modules.Base;
using CartoPrime.Resources;
using MonkeyCache.FileStore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CartoPrime.Modules.Home.ViewModel
{
    public class HomeViewModel : BaseViewModel<HomeViewModel>
    {
        #region Atributes
        public ObservableCollection<TileModel> _tileSource;
        private ObservableCollection<Tile> _tiles;
        private Colors _colors;
        private double _rowHeigth;
        private string _nameTeam;
        private string _statusMercado;
        private string _escudoTeam;
        private string _loginName;

        #endregion

        #region Properties
        public string LoginName
        {
            get { return _loginName; }
            set => SetProperty(ref _loginName, value);
        }
        public string EscudoTeam
        {
            get { return _escudoTeam; }
            set => SetProperty(ref _escudoTeam, value);
        }
        public string NameTeam
        {
            get { return _nameTeam; }
            set => SetProperty(ref _nameTeam, value);
        }
        public string StatusMercado
        {
            get { return _statusMercado; }
            set => SetProperty(ref _statusMercado, value);
        }
        public double RowHeigth
        {
            set
            {
                if (_rowHeigth != value)
                {
                    _rowHeigth = value;
                    OnPropertyChanged("RowHeigth");
                    // SetNewColor();
                }
            }
            get
            {
                return _rowHeigth;
            }
        }
        public ObservableCollection<TileModel> TileSource
        {
            get { return _tileSource; }
            set => SetProperty(ref _tileSource, value);
        }
        public ObservableCollection<Tile> Tiles
        {
            get { return _tiles; }
            set => SetProperty(ref _tiles, value);
        }

        private Tile Favorito { get; set; }
        private Tile Ligas { get; set; }
        private Tile Pontuados { get; set; }
        private Tile MaisEscalados { get; set; }
        private Tile MaisEscaladosPorPosicao { get; set; }
        private Tile Escalar { get; set; }
        private Tile JogosRodada { get; set; }
        private Tile ClassificacaoBraA { get; set; }
        private Tile ClassificacaoBraB { get; set; }
        private Tile DicasMitar { get; set; }
        private Tile Contatos { get; set; }
        private Tile BonsDeBicoA { get; set; }
        private Tile BonsDeBicoB { get; set; }

        private Tile TabelaBonsDeBicoA { get; set; }
        private Tile TabelaBonsDeBicoB { get; set; }


        #endregion

        #region Commands
        public ICommand SelectedTileCommand { get; set; }
        public ICommand LoginCommand { get; set; }

        #endregion

        #region Construtor
        public HomeViewModel()
        {
            Title = "Home";
            _colors = new Colors();
            SelectedTileCommand = new Command<Tile>(async (item) => await TileSelected(item.Url));
            LoginCommand = new Command(async () => await Login());


            MessagingCenter.Subscribe<UserCA>(this, "UpdateMenu", message =>
            {
                try
                {
                    NameTeam = Barrel.Current.Get<string>(key: "NomeTime") != null ? Barrel.Current.Get<string>(key: "NomeTime") : "Cartola Prime FC";
                    EscudoTeam = Barrel.Current.Get<string>(key: "UrlEscudo");
                    StatusMercado = Barrel.Current.Get<string>(key: "StatusMarket");
                    LoginName = NameTeam == "Cartola Prime FC" ? "Faça Login" : "Temporada 2021";
                    if (EscudoTeam == null)
                    {
                        NameTeam = "Cartola Prime FC";
                        EscudoTeam = "loginca.png";
                        StatusMercado = Barrel.Current.Get<string>(key: "StatusMarket");
                        LoginName = "Faça Login";
                    }
                }
                catch (Exception ex)
                {
                    NameTeam = "Cartola Prime FC";
                    EscudoTeam = "loginca.png";
                    StatusMercado = "";
                    LoginName = "Faça Login";
                }

                //SetTeamLog().ConfigureAwait(true);
            });

        }
        #endregion

        #region Methods
        public async Task InitAsync()
        {
            //await BuscarRest().ConfigureAwait(true);
            await CallEstadoInicialTiles().ConfigureAwait(true);
            try
            {
                NameTeam = Barrel.Current.Get<string>(key: "NomeTime") != null ? Barrel.Current.Get<string>(key: "NomeTime") : "Cartola Prime FC";
                EscudoTeam = Barrel.Current.Get<string>(key: "UrlEscudo");
                StatusMercado = Barrel.Current.Get<string>(key: "StatusMarket");
                LoginName = NameTeam == "Cartola Prime FC" ? "Faça Login" : "Ver Time";
            }
            catch (Exception ex)
            {
                NameTeam = "Bem Vindo";
                EscudoTeam = "loginca.png";
                StatusMercado = "";
                LoginName = "Faça Login";
            }
        }
        private async Task TileSelected(object item)
        {
            await base.Navigate(item).ConfigureAwait(true);
        }

        private async Task CallEstadoInicialTiles()
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                try
                {
                    Tiles = new ObservableCollection<Tile>();

                    Favorito = new Tile
                    {
                        Index = 1,
                        Titulo = "Times Favoritos",
                        Descricao = "Seus times favoritos",
                        ExpressaoCentral = "Times Favoritos",
                        ColorTile = (Color)_colors["TileDestac"],
                        Image = "favorito.png",
                        CodeFA = string.Empty,
                        Loading = true,
                        ImageVisible = true,
                        FAVisible = false,
                        Scale = 1,
                        Url = "TeamsPage"
                    };

                    Ligas = new Tile
                    {
                        Index = 2,
                        Titulo = "Minhas Ligas",
                        Descricao = string.Empty,
                        ExpressaoCentral = "Minhas Ligas",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "liga.png",
                        ImageVisible = true,
                        Loading = true,
                        FAVisible = false,
                        Scale = 1,
                        Url = "LeaguesPage"
                    };

                    Pontuados = new Tile
                    {
                        Index = 3,
                        Titulo = "Pontuação",
                        Descricao = "Pontuação",
                        ExpressaoCentral = "Pontuação",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "pontuados.png",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "ScoredPage"
                    };

                    ClassificacaoBraA = new Tile
                    {
                        Index = 4,
                        Titulo = "Classif. Série A",
                        Descricao = "Classif. Série A",
                        SubDescricaoVisible = true,
                        ExpressaoCentral = "Classif. Série A",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "brasil_serie_a.png",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "ClassificationBRAPage"
                    };

                    MaisEscalados = new Tile
                    {
                        Index = 5,
                        Titulo = "Mais Escalados",
                        Descricao = "Mais Escalados",
                        ExpressaoCentral = "Mais Escalados",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "mais_escalados.png",
                        CodeFA = string.Empty,
                        Loading = true,
                        ImageVisible = true,
                        FAVisible = false,
                        Scale = 1,
                        Url = "MostClimbedPage"
                    };

                    MaisEscaladosPorPosicao = new Tile
                    {
                        Index = 6,
                        Titulo = "+Escalados Posiçoes",
                        Descricao = "+Escalados Posiçoes",
                        ExpressaoCentral = "+Escalados Posiçoes",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "mais_escalados_posicao.png",
                        CodeFA = string.Empty,
                        Loading = true,
                        ImageVisible = true,
                        FAVisible = false,
                        Scale = 1,
                        Url = "MostClimbedPositionPage"
                    };

                    Escalar = new Tile
                    {
                        Index = 7,
                        Titulo = "Escalar Time",
                        Descricao = "Escalar Time",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "escalar.png",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "ScalePage"
                    };

                    ClassificacaoBraB = new Tile
                    {
                        Index = 8,
                        Titulo = "Classif. Série B",
                        ExpressaoCentral = "Classif. Série B",
                        Descricao = string.Empty,
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "brasil_serie_b.png",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "ClassifBraBView"
                    };

                    DicasMitar = new Tile
                    {
                        Index = 9,
                        Titulo = "Vamos Mitar?",
                        Descricao = string.Empty,
                        ExpressaoCentral = "Vamos Mitar?",
                        ColorTile = (Color)_colors["TileDestac"],
                        Image = "ic_tips.png",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "AdsPage"
                    };

                    JogosRodada = new Tile
                    {
                        Index = 10,
                        Titulo = "Jogos da Rodada",
                        Descricao = "Jogos da Rodada",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "jogos_rodada.png",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "RoundGamesPage"
                    };



                    BonsDeBicoA = new Tile
                    {
                        Index = 11,
                        Titulo = "Bons de Bico Serie A",
                        Descricao = "Bons de Bico Serie A",
                        ExpressaoCentral = "Bons de Bico Serie A",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "liga_bb.png",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "BonsDeBicoView"
                    };

                    BonsDeBicoB = new Tile
                    {
                        Index = 12,
                        Titulo = "Bons de Bico Serie B",
                        Descricao = "Bons de Bico Serie B",
                        ExpressaoCentral = "Bons de Bico Serie B",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "liga_bb_b.png",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "BonsDeBicoViewB"
                    };

                    TabelaBonsDeBicoA = new Tile
                    {
                        Index = 13,
                        Titulo = "Classif. BB Serie A",
                        Descricao = "Classif. BB Serie A",
                        ExpressaoCentral = "Classif. BB Serie A",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "classif_bb.png",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "ClassifBonsDeBicoViewA"
                    };

                    TabelaBonsDeBicoB = new Tile
                    {
                        Index = 14,
                        Titulo = "Classif. BB Serie B",
                        Descricao = "Classif. BB Serie B",
                        ExpressaoCentral = "Classif. BB Serie B",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "classif_bb_b.png",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "ClassifBonsDeBicoViewB"
                    };
                    Contatos = new Tile
                    {
                        Index = 15,
                        Titulo = "Divulgue Aqui",
                        Descricao = "Divulgue Aqui",
                        ExpressaoCentral = "Divulgue Aqui",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "divulgue.png",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "DivulgueAquiView"
                    };

                    Tiles.Add(Favorito);
                    Tiles.Add(Ligas);
                    Tiles.Add(Escalar);
                    Tiles.Add(MaisEscalados);
                    Tiles.Add(MaisEscaladosPorPosicao);
                    Tiles.Add(Pontuados);
                    Tiles.Add(JogosRodada);
                    Tiles.Add(ClassificacaoBraA);
                    Tiles.Add(ClassificacaoBraB);
                    Tiles.Add(DicasMitar);

                    //var user = Barrel.Current.Get<UserCA>(key: UrlCartola._timeAuth);
                    //string ids = "13924668;364309;942719;961332;19435336;4926245;3299511;4953515;16704764;3348591;5781967;12255141;644367;1484400;397575;12443034;747529;7355350;2774316;18234957;28824572;2760725;5541287;28092158;25554711;6732944;6019592;18010824;13964479;13972492;18193661;9370032;12193947;6114102;7946578;199787;20463017;22031247;";
                    //var vetorId = ids.Split(';').ToList();

                    //if (vetorId.Exists(x => x == user.id.ToString()))
                    //{
                    //    Tiles.Add(BonsDeBicoA);
                    //    Tiles.Add(BonsDeBicoB);
                    //    Tiles.Add(TabelaBonsDeBicoA);
                    //    Tiles.Add(TabelaBonsDeBicoB);
                    //}
                    Tiles.Add(BonsDeBicoA);
                    Tiles.Add(BonsDeBicoB);
                    Tiles.Add(TabelaBonsDeBicoA);
                    Tiles.Add(TabelaBonsDeBicoB);
                    Tiles.Add(Contatos);

                    var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

                    RowHeigth = mainDisplayInfo.Height / 5;
                }
                catch (Exception ex)
                {

                }
                finally
                {

                }
            }).ConfigureAwait(true);

        }

        private async Task Login()
        {
            var user = Barrel.Current.Get<UserCA>(key: UrlCartola._timeAuth);
            if (user == null)
            {
                await App.Current.MainPage.Navigation.PushAsync(new LoginPage()).ConfigureAwait(true);
            }
            else
            {
                await Shell.Current.GoToAsync(Pages.ClimbByAthleteView).ConfigureAwait(true);
            }
        }
        #endregion
    }
}
