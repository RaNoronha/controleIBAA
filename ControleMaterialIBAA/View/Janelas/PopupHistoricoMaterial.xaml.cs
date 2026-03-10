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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace ControleMaterialIBAA.View.Janelas
{
    /// <summary>
    /// Lógica interna para PopupHistoricoMaterial.xaml
    /// </summary>
    public partial class PopupHistoricoMaterial : Window
    {
        ServicoMovimentacoes _servico = new ServicoMovimentacoes();
        public PopupHistoricoMaterial(Guid materialId)
        {
            InitializeComponent();
            CarregarHistorico(materialId);
        }

        private async void CarregarHistorico(Guid materialId)
        {
            var lista = await _servico.BuscarHistoricoAsync(materialId);

            DgHistorico.ItemsSource = lista;
        }
    }
}
