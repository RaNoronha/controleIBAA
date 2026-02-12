using ControleMaterialIBAA.Infra;
using ControleMaterialIBAA.Servicos;
using ControleMaterialIBAA.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ControleMaterialIBAA.View
{
    /// <summary>
    /// Lógica interna para LoginView.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Botão Login
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var usuario = TxtUsuario.Text;
            var senha = TxtSenha.Password;

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(senha))
            {
                MessageBox.Show("Informe usuário e senha.",
                                "Atenção",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            var servico = new ServicoUsuarios();
            var usuarioModel = await servico.LoginAsync(usuario, senha);

            if (usuarioModel == null)
            {
                MessageBox.Show("Usuário ou senha inválidos ou usuário não cadastrado",
                                "Login não autorizado",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }

            var usuarioVM = ViewModelUsuario.FromModel(usuarioModel);
            Sessao.Login(usuarioVM);

            var telaPrincipal = new TelaPrincipal(usuarioVM.Usuario);
            telaPrincipal.Show();
            this.Close();
        }
        #endregion

        #region Botão Cadastrar
        private void BtnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            var telaCadastro = new CadastroUsuario();
          
            this.Hide();
            
            telaCadastro.ShowDialog();
            
            this.Show();
        }
        #endregion
    }
}
