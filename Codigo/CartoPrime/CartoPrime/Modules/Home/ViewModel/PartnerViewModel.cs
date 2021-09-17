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
    class PartnerViewModel : BaseViewModel<PartnerViewModel>
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

        private Tile PlanoAnual { get; set; }
        private Tile Plano1Turno { get; set; }
        private Tile Plano2Turno { get; set; }


        #endregion

        #region Commands
        public ICommand SelectedTileCommand { get; set; }

        #endregion

        #region Construtor
        public PartnerViewModel()
        {
            Title = "Home";
            _colors = new Colors();
            SelectedTileCommand = new Command<Tile>(async (item) => await TileSelected(item.Url));
        }
        #endregion

        #region Methods
        public async Task InitAsync()
        {
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
                    ShowLoading(AppResources.LOADING);
                    Tiles = new ObservableCollection<Tile>();

                    PlanoAnual = new Tile
                    {
                        Index = 1,
                        Titulo = "PLANO DE SÓCIO",
                        Descricao = "PLANO DE SÓCIO TOP 3 ANUAL",
                        ExpressaoCentral = "TOP 3 ANUAL",
                        ColorTile = (Color)_colors["TileDestac"],
                        Image = "seja_socio.png",
                        CodeFA = string.Empty,
                        Loading = true,
                        ImageVisible = true,
                        FAVisible = false,
                        Scale = 1,
                        Url = "PlanoAnualView"
                    };

                    Plano1Turno = new Tile
                    {
                        Index = 2,
                        Titulo = "PLANO DE SÓCIO",
                        Descricao = "PLANO DE SÓCIO TOP 3 1° TURNO",
                        ExpressaoCentral = "TOP 3 1° TURNO",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "seja_socio.png",
                        ImageVisible = true,
                        Loading = true,
                        FAVisible = false,
                        Scale = 1,
                        Url = "Plano1TurnoView"
                    };

                    Plano2Turno = new Tile
                    {
                        Index = 3,
                        Titulo = "PLANO DE SÓCIO",
                        Descricao = "PLANO DE SÓCIO TOP 3 2° TURNO",
                        ExpressaoCentral = "TOP 3 2° TURNO",
                        ColorTile = (Color)_colors["TileBase"],
                        Image = "seja_socio.png",
                        ImageVisible = true,
                        Loading = false,
                        FAVisible = false,
                        Scale = 1,
                        Url = "Plano2TurnoView"
                    };

                    Tiles.Add(PlanoAnual);
                    Tiles.Add(Plano1Turno);
                    Tiles.Add(Plano2Turno);

                    var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

                    RowHeigth = mainDisplayInfo.Height / 5;
                    HideLoading();
                }
                catch (Exception ex)
                {

                }
            }).ConfigureAwait(true);

        }
        #endregion
    }
}
