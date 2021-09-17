using CartoPrime.Linker;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CartoPrime.Modules.Popups.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [Preserve(AllMembers = true)]
    public partial class GenericMsgAlertView : PopupPage
    {
        private string CorAviso = "#DE3202";
        private string TextoEscuro = "#001C08";
        public bool IconeAviso;

        private string _textoBotaoContinuar;
        public string TextoBotaoContinuar
        {
            get { return _textoBotaoContinuar; }
            set { _textoBotaoContinuar = value; }
        }

        private bool _cancelarIsVisible;
        public bool CancelarIsVisible
        {
            get { return _cancelarIsVisible; }
            set { _cancelarIsVisible = value; }
        }

        public ICommand FecharCommand { get; set; }

        public ICommand OkCommand { get; set; }

        public ICommand CancelarCommand { get; set; }

        public GenericMsgAlertView(string titulo, 
            string mensagem, 
            bool atencao, 
            bool cancelarBtn,
            string icone = null, 
            ICommand continuarCommand = null, 
            ICommand cancelarCommand = null)
        {
            InitializeComponent();

            BindingContext = this;

            CancelarIsVisible = cancelarBtn;

            lblTitulo.Text = titulo;
            lblMensagem.Text = mensagem;

            if (atencao)
            {
                lblIconeAviso.IsVisible = true;
                lblTitulo.TextColor = Color.FromHex(CorAviso);
                lblIcone.TextColor = Color.FromHex(CorAviso);
            }
            else
            {
                lblIconeAviso.IsVisible = false;
                lblTitulo.TextColor = Color.FromHex(TextoEscuro);
                lblIcone.TextColor = Color.FromHex(TextoEscuro);
            }

            if (icone == null)
            {
                lblIcone.IsVisible = false;
            }
            else
            {
                lblIcone.IsVisible = true;
                lblIcone.Text = icone;
            }

            if (continuarCommand is null)
            {
                TextoBotaoContinuar = "OK";
                OkCommand = new Command(async () => { await Ok().ConfigureAwait(true); });
            }
            else
            {
                TextoBotaoContinuar = "CONTINUAR";
                OkCommand = continuarCommand;
            }

            CancelarCommand = cancelarCommand;

            FecharCommand = new Command(async () => { await FecharPopUp().ConfigureAwait(true); });

        }

        private async Task Ok()
        {
            await PopupNavigation.Instance.PopAsync(false).ConfigureAwait(true);
        }

        private async Task FecharPopUp()
        {
            await PopupNavigation.Instance.PopAsync(false).ConfigureAwait(true);
        }
    }
}