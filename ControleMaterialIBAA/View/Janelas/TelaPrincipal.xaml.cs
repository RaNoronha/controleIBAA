using ControleMaterialIBAA.View.Paginas;
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

namespace ControleMaterialIBAA.View.Janelas
{
    /// <summary>
    /// Lógica interna para TelaPrincipal.xaml
    /// </summary>
    public partial class TelaPrincipal : Window
    {
        public TelaPrincipal(string nomeUsuario)
        {
            InitializeComponent();
            TxtBemVindo.Text = $"Bem-vindo, {nomeUsuario}";
        }

        private void CadastrarMaterial(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new CadastrarMaterial();
        }

        private void GerenciarMaterial(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new GerenciarMaterial();
        }

        private void ConsultaResumida(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ConsultaSimplificadaMaterial();
        }

        private void TransferirMaterial(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new GerenciarPatrimonio();
        }

        private void CadastrarDepartamento(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new CadastrarDepartamento();
        }

        private void CadastrarSubDepartamento(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new CadastrarSubDepartamento();
        }

        //private void ConsultaDepartamento(object sender, RoutedEventArgs e)
        //{
        //    MainContent.Content = new ConsultaDepartamento();
        //}

        //private void ExcluirDepartamento(object sender, RoutedEventArgs e)
        //{
        //    MainContent.Content = new ExcluirDepartamento();
        //}
    }
}
