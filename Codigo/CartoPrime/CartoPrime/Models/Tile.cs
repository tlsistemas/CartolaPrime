using PropertyChanged;
using System;
using Xamarin.Forms;

namespace CartoPrime.Models
{
    [AddINotifyPropertyChangedInterface]
    public class Tile
    {
        public string Url { get; set; }
        public int Index { get; set; }
        public string Titulo { get; set; }
        public string ExpressaoCentral { get; set; }
        public string SubExpressaoCentral { get; set; }
        public bool SubExpressaoCentralVisible { get; set; }
        public string Descricao { get; set; }
        public string SubDescricao { get; set; }
        public bool SubDescricaoVisible { get; set; }
        public Color ColorTile { get; set; }
        public Color ColorText { get => ColorTile == (Color)CartoPrime.App.Current.Resources["TileBase"] ? Color.Black : Color.White; }
        public Color ColorBorder { get => ColorTile; }
        public string Image { get; set; }
        public bool ImageVisible { get; set; }
        public string CircleImage { get; set; }
        public bool CircleImageVisible { get; set; }
        public string CodeFA { get; set; }
        public bool Loading { get; set; }
        public double Scale { get; set; }

        public bool FAVisible { get; set; }
        public Thickness Padding { get; set; } = new Thickness(0);
    }
}
