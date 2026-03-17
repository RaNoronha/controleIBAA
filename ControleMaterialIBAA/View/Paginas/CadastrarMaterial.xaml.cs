using ControleMaterialIBAA.Enums;
using ControleMaterialIBAA.Helper;
using ControleMaterialIBAA.Infra;
using ControleMaterialIBAA.Modelos;
using ControleMaterialIBAA.Servicos;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interação lógica para CadastrarMaterial.xam
    /// </summary>
    public partial class CadastrarMaterial : UserControl
    {
        public CadastrarMaterial()
        {
            InitializeComponent();
            GerarCodigoMaterial();
            TxtQtd.TextChanged += CalcularTotal;
            TxtValor.TextChanged += CalcularTotal;
            CmbAquisicao.ItemsSource = Enum.GetValues(typeof(FormaAquisicao));
            CmbTipoMaterial.ItemsSource = Enum.GetValues(typeof(TipoMaterial));
        }

        private void GerarCodigoMaterial()
        {
            TxtCodigo.Text = GerarCodigos.GerarCodigoMaterial();
        }

        private async void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtMaterial.Text))
            {
                MessageBox.Show("O nome do material é obrigatório.");
                return;
            } 

            if (!decimal.TryParse(TxtValor.Text, out decimal valor))
            {
                MessageBox.Show("Valor inválido.");
                return;
            } 
            var material = new ModelosMateriais
            {
                id = Guid.NewGuid(),
                cod = TxtCodigo.Text,
                nome = TxtMaterial.Text.Trim(),
                descricao = TxtDescricao.Text.Trim(),
                marca = TxtMarcaModelo.Text.Trim(),
                tipoMaterial = (TipoMaterial)CmbTipoMaterial.SelectedItem,
                valorUnitario = valor,
                aquisicao = (FormaAquisicao)CmbAquisicao.SelectedItem,
                dtVerificacao = DtVerificacao.SelectedDate,
                ativo = true
            };

            var servico = new ServicoMateriais();

            bool sucessoMaterial = await servico.CriarAsync(material);           

            if (!sucessoMaterial)
            {
                MessageBox.Show("Erro ao cadastrar material.");
                return;
            }

            MessageBox.Show($"Material cadastrado com sucesso!\n\nCódigo: {TxtCodigo.Text}\nMaterial: {TxtMaterial.Text}");

            GerarCodigoMaterial();

            LimparCampos(this);
        }

        private void CalcularTotal(object sender, TextChangedEventArgs e)
        {
            var cultura = new CultureInfo("pt-BR");

            if (decimal.TryParse(TxtQtd.Text, NumberStyles.Any, cultura, out decimal quantidade) &&
                decimal.TryParse(TxtValor.Text, NumberStyles.Any, cultura, out decimal valorUnitario))
            {
                decimal total = quantidade * valorUnitario;
                TxtValorTotal.Text = total.ToString("N2", cultura);
            }
            else
            {
                TxtValorTotal.Text = "0,00";
            }
        }

        private void LimparCampos(DependencyObject parent)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is TextBox textBox)
                    textBox.Clear();

                else if (child is ComboBox comboBox)
                    comboBox.SelectedIndex = -1;                

                // Recursivo (entra dentro de Grid, StackPanel, etc)
                LimparCampos(child);
            }
        }

    }
}
