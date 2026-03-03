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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ControleMaterialIBAA.View.Paginas
{
    /// <summary>
    /// Interação lógica para GerenciarMaterial.xam
    /// </summary>
    public partial class GerenciarMaterial : UserControl
    {
        public GerenciarMaterial()
        {
            InitializeComponent();
            CarregarDepartamentos();

        }

        private async void CarregarDepartamentos()
        {
            var servico = new ServicoDepartamentos();
            var lista = await servico.ListarAsync();

            CmbDepartamentos.ItemsSource = lista;
            CmbDepartamentos.DisplayMemberPath = "nome";
            CmbDepartamentos.SelectedValuePath = "id";
        }

        private async void CmbDepartamentos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbDepartamentos.SelectedValue == null)
                return;

            Guid departamentoId = (Guid)CmbDepartamentos.SelectedValue;

            var servicoSub = new ServicoSubDepartamentos();
            var listaSub = await servicoSub.ListarPorDepartamentoAsync(departamentoId);

            CmbSubDepartamentos.ItemsSource = listaSub;
        }
    }
}
