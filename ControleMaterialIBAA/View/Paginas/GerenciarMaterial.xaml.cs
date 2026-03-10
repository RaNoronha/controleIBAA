using ControleMaterialIBAA.DTO;
using ControleMaterialIBAA.Enums;
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
            var servico = new ServicoDepartamentos();
            var lista = await servico.ListarAsync();

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

            Guid? departamentoId = null;
            Guid? subDepartamentoId = null;

            if (CmbDepartamentos.SelectedValue is Guid dep && dep != Guid.Empty)
                departamentoId = dep;

            if (CmbSubDepartamentos.SelectedValue is Guid sub && sub != Guid.Empty)
                subDepartamentoId = sub;

            TipoMaterial? tipo = null;

            if (CmbTipoMaterial.SelectedValue != null)
                tipo = (TipoMaterial)CmbTipoMaterial.SelectedValue;

            var lista = await _servicoMateriais.ListarAsync(
                ativos: true,
                numeroPatrimonial: numeroPat,
                departamentoId: departamentoId,
                subDepartamentoId: subDepartamentoId,
                tipo: tipo
            );

            DgMateriais.ItemsSource = lista;
        }

        private void BtnBaixa_Click(object sender, RoutedEventArgs e)
        {
            if (DgMateriais.ItemsSource == null)
            {
                MessageBox.Show(
                    "Realize uma pesquisa antes de tentar dar baixa.",
                    "Atenção",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            var selecionados = ((List<GerenciaDTO>)DgMateriais.ItemsSource)
                                .Where(x => x.Selecionado)
                                .ToList();

            if (!selecionados.Any())
            {
                MessageBox.Show("Selecione pelo menos um material para dar baixa.");
                return;
            }

            var popup = new PopupBaixaMaterial(selecionados);

            popup.ShowDialog();
           
            BtnPesquisar_Click(null, null);
        }

        private void BtnTransferencia_Click(object sender, RoutedEventArgs e)
        {
            if (DgMateriais.ItemsSource == null)
            {
                MessageBox.Show("Realize uma pesquisa antes de transferir materiais.");
                return;
            }

            var lista = (List<GerenciaDTO>)DgMateriais.ItemsSource;

            var selecionados = lista
                                .Where(x => x.Selecionado)
                                .ToList();

            if (!selecionados.Any())
            {
                MessageBox.Show("Selecione pelo menos um material para transferir.");
                return;
            }

            var popup = new PopupTransferenciaMaterial(selecionados);

            popup.ShowDialog();

            BtnPesquisar_Click(null, null);
        }

        private void BtnHistoricoMaterial_Click(object sender, RoutedEventArgs e)
        {
            var botao = sender as Button;

            var materialId = (Guid)botao.Tag;

            var popup = new PopupHistoricoMaterial(materialId);

            popup.ShowDialog();
        }
    }
}
