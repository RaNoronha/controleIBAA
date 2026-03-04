using ControleMaterialIBAA.Enums;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ControleMaterialIBAA.View.Paginas
{
    /// <summary>
    /// Interação lógica para GerenciarMaterial.xam
    /// </summary>
    public partial class GerenciarMaterial : UserControl
    {
        private readonly ServicoMateriais _servicoMateriais = new ServicoMateriais();
        public GerenciarMaterial()
        {
            InitializeComponent();
            CarregarDepartamentos();
            CarregarTipoMaterial();

        }

        private async void CarregarDepartamentos()
        {
            var lista = await ServicoDepartamentos.ListarAsync();

            // 🔹 inserir opção vazia no início
            lista.Insert(0, new ModelosDepartamentos
            {
                id = Guid.Empty,
                nome = ""
            });

            CmbDepartamentos.ItemsSource = lista;
            CmbDepartamentos.SelectedIndex = 0;
        }

        private void CarregarTipoMaterial()
        {
            var lista = new List<KeyValuePair<int?, string>>();

            // 🔹 Opção vazia
            lista.Add(new KeyValuePair<int?, string>(null, ""));

            // 🔹 Enum
            foreach (TipoMaterial tipo in Enum.GetValues(typeof(TipoMaterial)))
            {
                lista.Add(new KeyValuePair<int?, string>((int)tipo, tipo.ToString()));
            }

            CmbTipoMaterial.ItemsSource = lista;
            CmbTipoMaterial.DisplayMemberPath = "Value";
            CmbTipoMaterial.SelectedValuePath = "Key";

            CmbTipoMaterial.SelectedIndex = 0; // começa vazio
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

        private async void BtnPesquisar_Click(object sender, RoutedEventArgs e)
        {
            
        var numeroPat = TxtNumPat.Text?.Trim();

            var departamentoId = CmbDepartamentos.SelectedValue as Guid?;
            var subDepartamentoId = CmbSubDepartamentos.SelectedValue as Guid?;
            var tipo = CmbTipoMaterial.SelectedItem as TipoMaterial?;

            var lista = await _servicoMateriais.ListarAsync(
                ativos: true,
                numeroPatrimonial: numeroPat,
                departamentoId: departamentoId,
                subDepartamentoId: subDepartamentoId,
                tipo: tipo
            );

            DgMateriais.ItemsSource = lista;
        }
    }
}
