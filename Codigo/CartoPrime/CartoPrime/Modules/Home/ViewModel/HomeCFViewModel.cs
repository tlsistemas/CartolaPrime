using CartoPrime.Custom.Styles;
using CartoPrime.Models;
using CartoPrime.Modules.Base;
using CartoPrime.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CartoPrime.Modules.Home.ViewModel
{
    public class HomeCFViewModel : BaseViewModel<HomeCFViewModel>
    {
        #region Atributes
        public ObservableCollection<TileModel> _tileSource;
        private ObservableCollection<Tile> _tiles;
        private Colors _colors;

        #endregion

        #region Properties
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

        public Tile SejaSocio { get; private set; }
        private Tile Favorito { get; set; }
        public Tile AnaliseRodada { get; private set; }
        public Tile Estatisticas { get; private set; }
        public Tile TopsPosicao { get; private set; }
        public Tile ForaCaixa { get; private set; }
        private Tile MaisEscalados { get; set; }
        public Tile Arbitragem { get; private set; }
        public Tile CanlCartoPrime { get; private set; }
        public Tile Nereco { get; private set; }
        public Tile ReiPitado { get; private set; }
        public Tile Top5 { get; private set; }

        private Tile Instagram;

        public Tile SejaMembro { get; private set; }


        #endregion

        #region Commands
        public ICommand SelectedTileCommand { get; set; }

        #endregion

        #region Construtor
        public HomeCFViewModel()
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

        private async Task CallEstadoInicialTiles()
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                try
                {
                    Tiles = new ObservableCollection<Tile>();

                    SejaSocio = new Tile
                    {
                        Index = 1,
                        Titulo = "Seja Sócio",
                        Descricao = "Seus times favoritos",
                        ExpressaoCentral = "Seja Sócio",
                        ColorTile = (Color)_colors["TileDestac"],
                        Image = "seja_socio.png",
                        CodeFA = string.Empty,
                        Loading = true,
                        ImageVisible = true,
                        FAVisible = false,
                        Scale = 1,
                        Url = "PartnerPopUpView"
                    };

                    AnaliseRodada = new Tile
                    {
                        Index = 2,
                        Titulo = "Análise da Rodada",
                        Descricao = string.Empty,
                        ExpressaoCentral = "Análise da Rodada",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "analise.png",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "AnalisePage"
                    };

                    Estatisticas = new Tile
                    {
                        Index = 3,
                        Titulo = "Estatística",
                        Descricao = string.Empty,
                        ExpressaoCentral = "Estatística",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "estatisticas.png",
                        ImageVisible = true,
                        Loading = true,
                        FAVisible = false,
                        Scale = 1,
                        Url = "StatisticsPage"
                    };

                    TopsPosicao = new Tile
                    {
                        Index = 4,
                        Titulo = "Tops por Posisção",
                        Descricao = "Tops por Posisção",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "mais_escalados.png",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "ScoredPage"
                    };

                    ForaCaixa = new Tile
                    {
                        Index = 5,
                        Titulo = "Os Fora da Caixa",
                        Descricao = "Os Fora da Caixa   ",
                        ExpressaoCentral= "Os Fora da Caixa",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "fora_caixa.png",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "ExitBoxPage"
                    };

                    Arbitragem = new Tile
                    {
                        Index = 6,
                        Titulo = "Arbitragem",
                        Descricao = "Arbitragem",
                        SubDescricaoVisible = true,
                        ExpressaoCentral = "Arbitragem",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "arbitragem.jpeg    ",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "ScalePage"
                    };

                    CanlCartoPrime = new Tile
                    {
                        Index = 7,
                        Titulo = "Canal CartoPrime",
                        Descricao = "Canal CartoPrime",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "CartoPrime.jpg",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "RoundGamesPage"
                    };

                    Nereco = new Tile
                    {
                        Index = 8,
                        Titulo = "Parceria Nereco",
                        ExpressaoCentral = "Parceria Nereco",
                        Descricao = string.Empty,
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "nareco.png",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "ClassificationBRAPage"
                    };

                    ReiPitado = new Tile
                    {
                        Index = 9,
                        Titulo = "Rei do Pitaco",
                        Descricao = string.Empty,
                        ExpressaoCentral = "Rei do Pitaco",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "rei_pitaco.jpg",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "ClassificationBRAPage"
                    };

                    Top5 = new Tile
                    {
                        Index = 10,
                        Titulo = "Top 5",
                        Descricao = "Top 5",
                        ExpressaoCentral = "Top 5",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "top5.png",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "NewsPage"
                    };

                    SejaMembro = new Tile
                    {
                        Index = 11,
                        Titulo = "Seja Membro",
                        Descricao = "Seja Membro",
                        ExpressaoCentral = "Seja Membro",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "seja_membro.jpg",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "AdsPage"
                    };

                    Instagram = new Tile
                    {
                        Index = 12,
                        Titulo = "Instagram",
                        Descricao = "Instagram",
                        ExpressaoCentral = "Instagram",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "instagram.jpg",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "ContactsPage"
                    };

                    Tiles.Add(SejaSocio);
                    Tiles.Add(AnaliseRodada);
                    Tiles.Add(Estatisticas);
                    Tiles.Add(TopsPosicao);
                    Tiles.Add(ForaCaixa);
                    Tiles.Add(Arbitragem);
                    Tiles.Add(CanlCartoPrime);
                    Tiles.Add(Nereco);
                    Tiles.Add(ReiPitado);
                    Tiles.Add(Instagram);

                }
                catch (Exception ex)
                {

                }

            }).ConfigureAwait(true);

        }
        #endregion
    }
}
