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

            var lista = await _servicoMateriais.ListarAsync(ativos: true,cod: codigo,tipo: tipo);

            DgMateriais.ItemsSource = lista;
        }

        private async void BtnExcluir_Click(object sender, RoutedEventArgs e)
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

            var lista = DgMateriais.Items.Cast<ModelosMateriais>().ToList();

            var selecionados = lista.Where(x => x.selecionado).ToList();

            if (!selecionados.Any())
            {                
                MessageBox.Show("Selecione pelo menos um material para dar baixa.");
                return;
            }

            var nomes = string.Join(", ", selecionados.Select(x => $"{x.nome} ({x.cod})"));

            var popup = new PopupConfirmacaoExclusao(selecionados.Count == 1 ? $"Deseja realmente dar baixa no material:\n{nomes}?"
                    : $"Deseja realmente dar baixa em {selecionados.Count} materiais?\n{nomes}");

            popup.ShowDialog();

            if (!popup.Confirmado)
            {
                return;
            }

            foreach (var item in selecionados)
            {
                await _servicoMateriais.AtualizarAsync(item.id, false);
            }

            MessageBox.Show("Material excluído com sucesso!");

            BtnPesquisar_Click(null, null);
        }

        //private void BtnTransferencia_Click(object sender, RoutedEventArgs e)
        //{
        //    if (DgMateriais.ItemsSource == null)
        //    {
        //        MessageBox.Show("Realize uma pesquisa antes de transferir materiais.");
        //        return;
        //    }

        //    var lista = (List<GerenciaDTO>)DgMateriais.ItemsSource;

        //    var selecionados = lista
        //                        .Where(x => x.Selecionado)
        //                        .ToList();

        //    if (!selecionados.Any())
        //    {
        //        MessageBox.Show("Selecione pelo menos um material para transferir.");
        //        return;
        //    }

        //    var popup = new PopupTransferenciaMaterial(selecionados);

        //    popup.ShowDialog();

        //    BtnPesquisar_Click(null, null);
        //}

        private void BtnHistoricoMaterial_Click(object sender, RoutedEventArgs e)
        {
            var botao = sender as Button;

            var materialId = (Guid)botao.Tag;

            var popup = new PopupHistoricoMaterial(materialId);

            popup.ShowDialog();
        }
    }
}
