using ControleMaterialIBAA.DTO;
using ControleMaterialIBAA.Servicos;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ControleMaterialIBAA.View.Paginas
{
    public partial class RelatorioMaterial : UserControl
    {
        private readonly ServicoRelatorioEstoque _servicoRelatorio = new();
        private readonly ServicoMateriais _servicoMaterial = new();

        private List<RelatorioPermanenteDTO> _listaCompleta;

        public RelatorioMaterial()
        {
            InitializeComponent();
            CarregarMateriais();
            CarregarDados();
        }

        private async void CarregarMateriais()
        {
            var lista = await _servicoMaterial.ListarAsync();

            CmbMaterial.ItemsSource = lista;
            CmbMaterial.DisplayMemberPath = "nome";
            CmbMaterial.SelectedValuePath = "id";
        }

        private async void CarregarDados()
        {
            try
            {
                _listaCompleta = await _servicoRelatorio.ListarAsync();
                DgRelatorioEstoque.ItemsSource = _listaCompleta;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar relatório: {ex.Message}");
            }
        }

        private async void BtnPesquisar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guid? materialId = null;

                if (CmbMaterial.SelectedValue != null)
                {
                    materialId = (Guid)CmbMaterial.SelectedValue;
                }

                string status = "TODOS";

                if (CmbStatus.SelectedItem is ComboBoxItem item)
                {
                    status = item.Content.ToString();
                }

                var lista = await _servicoRelatorio.ListarAsync(codigo: TxtCodigo.Text?.Trim(), materialId: materialId,status: status);

                DgRelatorioEstoque.ItemsSource = lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro na pesquisa: {ex.Message}");
            }
        }

        private void BtnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            TxtCodigo.Clear();
            CmbMaterial.SelectedIndex = -1;
            CmbStatus.SelectedIndex = 0;
            CarregarDados();
        }

        private void BtnExportar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Exportação em desenvolvimento.");
        }
    }
}