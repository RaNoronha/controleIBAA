using ControleMaterialIBAA.Helper;
using ControleMaterialIBAA.Modelos;
using ControleMaterialIBAA.Servicos;
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
    /// Lógica interna para CadastroUsuario.xaml
    /// </summary>
    public partial class CadastroUsuario : Window
    {
        public CadastroUsuario()
        {
            InitializeComponent();
        }        

        private async void BtnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            string nomeUsuario = txtUsuario.Text.Trim();
            string senha = txtSenha.Password;
            string repetirSenha = txtRepetirSenha.Password;

            if (string.IsNullOrEmpty(nomeUsuario) || string.IsNullOrEmpty(senha))
            {
                MessageBox.Show("Usuário e senha são obrigatórios.");
                return;
            }

            if (senha != repetirSenha)
            {
                MessageBox.Show("As senhas não conferem.");
                return;
            }

            string hash = SenhaHelper.GerarHash(senha);

            var usuario = new ModelosUsuarios
            {
                id = Guid.NewGuid(),
                usuario = nomeUsuario,
                hash = hash,
                ativo = true
            };

            var servico = new ServicoUsuarios();
            bool sucesso = await servico.CriarAsync(usuario);

            if (sucesso)
            {
                MessageBox.Show("Usuário cadastrado com sucesso!");
                Close();
            }
            else
            {
                MessageBox.Show("Erro ao cadastrar usuário.");
            }
        }
    }
}
