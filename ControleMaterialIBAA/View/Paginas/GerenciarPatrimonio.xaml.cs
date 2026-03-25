using ControleMaterialIBAA.Enums;
using ControleMaterialIBAA.Helper;
using ControleMaterialIBAA.Modelos;
using ControleMaterialIBAA.Servicos;
using ControleMaterialIBAA.View.Janelas;
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
    /// Interação lógica para GerenciarPatrimonio.xam
    /// </summary>
    public partial class GerenciarPatrimonio : UserControl
    {
        private readonly ServicoMateriais _servicoMateriais = new ServicoMateriais();

        public GerenciarPatrimonio()
        {
            InitializeComponent();
            CarregarTipoMaterial();
        }

        private void CarregarTipoMaterial()
        {
            var lista = new List<KeyValuePair<int?, string>>();

            lista.Add(new KeyValuePair<int?, string>(null, ""));

            foreach (TipoMaterial tipo in Enum.GetValues(typeof(TipoMaterial)))
            {
                lista.Add(new KeyValuePair<int?, string>((int)tipo, tipo.ToString()));
            }

            CmbTipoMaterial.ItemsSource = lista;
            CmbTipoMaterial.DisplayMemberPath = "Value";
            CmbTipoMaterial.SelectedValuePath = "Key";

            CmbTipoMaterial.SelectedIndex = 0;
        }

        private async void BtnPesquisar_Click(object sender, RoutedEventArgs e)
        {
            var codigo = TxtCod.Text?.Trim();

            TipoMaterial? tipo = null;

            if (CmbTipoMaterial.SelectedValue != null)
            {
                tipo = (TipoMaterial)(int)CmbTipoMaterial.SelectedValue;
            }

            var lista = await _servicoMateriais.ListarAsync(cod:codigo, tipo: tipo);

            DgMateriais.ItemsSource = lista;
        }

        private async void BtnTranferirMaterial_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
