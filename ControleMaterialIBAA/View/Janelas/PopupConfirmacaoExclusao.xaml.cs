using System.Windows;

namespace ControleMaterialIBAA.View.Janelas
{
    /// <summary>
    /// Lógica interna para PopupConfirmacaoExclusao.xaml
    /// </summary>
    public partial class PopupConfirmacaoExclusao : Window
    {
        public bool Confirmado { get; private set; }

        public PopupConfirmacaoExclusao(string mensagem)
        {
            InitializeComponent();
            TxtMensagem.Text = mensagem;
        }

        private void BtnSim_Click(object sender, RoutedEventArgs e)
        {
            Confirmado = true;
            Close();
        }

        private void BtnNao_Click(object sender, RoutedEventArgs e)
        {
            Confirmado = false;
            Close();
        }

    }
}
