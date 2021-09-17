using CartoPrime.Custom.Styles;
using CartoPrime.Models;
using CartoPrime.Modules.Base;
using CartoPrime.Resources;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CartoPrime.Modules.Home.ViewModel
{
    public class HomeCAViewModel : BaseViewModel<HomeCAViewModel>
    {
        #region Atributes
        public ObservableCollection<TileModel> _tileSource;
        private ObservableCollection<Tile> _tiles;
        private Colors _colors;
        private double _rowHeigth;

        #endregion

        #region Properties
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
        private Tile Time { get; set; }
        private Tile Liga { get; set; }
        private Tile Pontuados { get; set; }
        private Tile MaisEscalados { get; set; }
        private Tile Escalar { get; set; }
        private Tile Rodada { get; set; }
        private Tile SerieA { get; set; }
        private Tile SerieB { get; set; }
        private Tile VamosMitar { get; set; }
        private Tile Sobre { get; set; }
        private Tile Noticias { get; set; }


        #endregion

        #region Commands
        public ICommand SelectedTileCommand { get; set; }

        #endregion

        #region Construtor
        public HomeCAViewModel()
        {
            Title = "Home";
            _colors = new Colors();
            SelectedTileCommand = new Command<Tile>(async (item) => await TileSelected(item.Url));

        }
        #endregion

        #region Methods
        public async Task InitAsync()
        {
            //await BuscarRest().ConfigureAwait(true);
            await CallEstadoInicialTiles().ConfigureAwait(true);
        }
        private async Task TileSelected(object item)
        {
            await base.Navigate(item).ConfigureAwait(true);
        }
        private async Task BuscarRest()
        {
            try
            {
                ShowLoading(AppResources.LOADING);
                TileSource = new ObservableCollection<TileModel>()
                {
                    new TileModel(){Title ="Favoritos" ,BgImageSource="favorito.png", Url="TeamsPage"},
                    new TileModel(){Title ="Times" ,BgImageSource="times.png", Url="TeamsPage"},
                    new TileModel(){Title ="Ligas" ,BgImageSource="liga.png", Url = "LeaguesPage"},
                    new TileModel(){Title ="Pontuados" ,BgImageSource="pontuados.png", Url ="ScoredPage"},
                    new TileModel(){Title ="+Escalados" ,BgImageSource="mais_escalados.png", Url="MostClimbedPage"},
                    new TileModel(){Title ="Escalar" ,BgImageSource="escalar.png", Url = "ScalePage"},
                    new TileModel(){Title ="Jogos da Rodada" ,BgImageSource="jogos_rodada.png", Url = "RoundGamesPage"},
                    new TileModel(){Title ="Classif. Serie A" ,BgImageSource="brasil_serie_a.png", Url="ClassificationBRAPage"},
                    new TileModel(){Title ="Classif. Serie B" ,BgImageSource="brasil_serie_b.png"},
                    new TileModel(){Title ="Notícias" ,BgImageSource="news.png", Url ="NewsPage"},
                    new TileModel(){Title ="Vamos Mitar?" ,BgImageSource="ic_tips.png", Url="AdsPage"},
                    new TileModel(){Title ="Sobre" ,BgImageSource="contato.png", Url="ContactsPage"},

               };

                HideLoading();
            }
            catch (Exception ex)
            {
                HideLoading();
            }

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
                        Titulo = "Favoritos",
                        Descricao = "Seus times favoritos",
                        ExpressaoCentral = "Favoritos",
                        ColorTile = (Color)_colors["TileDestac"],
                        Image = "favorito.png",
                        CodeFA = string.Empty,
                        Loading = true,
                        ImageVisible = true,
                        FAVisible = false,
                        Scale = 1,
                        Url = "TeamsPage"
                    };

                    Time = new Tile
                    {
                        Index = 2,
                        Titulo = "Times",
                        Descricao = string.Empty,
                        ExpressaoCentral = "Times",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "times.png",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "TeamsPage"
                    };

                    Liga = new Tile
                    {
                        Index = 3,
                        Titulo = "Ligas",
                        Descricao = string.Empty,
                        ExpressaoCentral = "Ligas",
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
                        Index = 4,
                        Titulo = "Pontuados",
                        Descricao = "Jogadores pontuados",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "pontuados.png",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "ScoredPage"
                    };

                    MaisEscalados = new Tile
                    {
                        Index = 5,
                        Titulo = "+Escalados",
                        Descricao = "Mais escalados",
                        ExpressaoCentral = "Mais escalados",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "mais_escalados.png",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "MostClimbedPage"
                    };

                    Escalar = new Tile
                    {
                        Index = 6,
                        Titulo = "Escalar",
                        Descricao = "Escalar",
                        SubDescricaoVisible = true,
                        ExpressaoCentral = "Escalar",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "escalar.png",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "ScalePage"
                    };

                    Rodada = new Tile
                    {
                        Index = 7,
                        Titulo = "Rodada",
                        Descricao = "Jogos da Rodada",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "jogos_rodada.png",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "RoundGamesPage"
                    };

                    SerieA = new Tile
                    {
                        Index = 8,
                        Titulo = "Serie A",
                        ExpressaoCentral = "Tabela",
                        Descricao = string.Empty,
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "brasil_serie_a.png",
                        ImageVisible=true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "ClassificationBRAPage"
                    };

                    SerieB = new Tile
                    {
                        Index = 9,
                        Titulo = "Serie B",
                        Descricao = string.Empty,
                        ExpressaoCentral = "Tabela Serie B",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "brasil_serie_b.png",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "ClassificationBRAPage"
                    };

                    Noticias = new Tile
                    {
                        Index = 10,
                        Titulo = "Noticias",
                        Descricao = "Noticias",
                        ExpressaoCentral = "Ultímas do Cartola",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "news.png",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "NewsPage"
                    };

                    VamosMitar = new Tile
                    {
                        Index = 11,
                        Titulo = "Dicas",
                        Descricao = "Vamos Mitar?",
                        ExpressaoCentral = "Dicas do Cartola",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "ic_tips.png",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "AdsPage"
                    };

                    Sobre = new Tile
                    {
                        Index = 12,
                        Titulo = "Contatos",
                        Descricao = "Contatos",
                        ExpressaoCentral = "Contatos",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "contato.png",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "ContactsPage"
                    };

                    Tiles.Add(Favorito);
                    Tiles.Add(Time);
                    Tiles.Add(Liga);
                    Tiles.Add(Rodada);
                    Tiles.Add(Pontuados);
                    Tiles.Add(MaisEscalados);
                    Tiles.Add(SerieA);
                    Tiles.Add(SerieB);
                    Tiles.Add(Escalar);
                    //Tiles.Add(Noticias);
                    //Tiles.Add(VamosMitar);
                    //Tiles.Add(Sobre);

                    var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

                    RowHeigth = mainDisplayInfo.Height/5;
                }
                catch (Exception ex)
                {

                }
            }).ConfigureAwait(true);

        }
        #endregion
    }
}
