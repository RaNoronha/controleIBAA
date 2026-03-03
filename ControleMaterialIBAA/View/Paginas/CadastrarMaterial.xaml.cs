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
            CarregarDepartamentos();
            TxtQtd.TextChanged += CalcularTotal;
            TxtValor.TextChanged += CalcularTotal;
            CmbAquisicao.ItemsSource = Enum.GetValues(typeof(FormaAquisicao));
            CmbTipoMaterial.ItemsSource = Enum.GetValues(typeof(TipoMaterial));
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

        private async void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtMaterial.Text))
            {
                MessageBox.Show("O nome do material é obrigatório.");
                return;
            }

            if (CmbDepartamentos.SelectedValue == null)
            {
                MessageBox.Show("Selecione um departamento.");
                return;
            }

            if (CmbSubDepartamentos.SelectedValue == null)
            {
                MessageBox.Show("Selecione um subdepartamento.");
                return;
            }

            if (!int.TryParse(TxtQtd.Text, out int quantidade))
            {
                MessageBox.Show("Quantidade inválida.");
                return;
            }

            if (!decimal.TryParse(TxtValor.Text, out decimal valor))
            {
                MessageBox.Show("Valor inválido.");
                return;
            }

            Guid departamentoId = (Guid)CmbDepartamentos.SelectedValue;
            Guid subDepartamentoId = (Guid)CmbSubDepartamentos.SelectedValue;

            var listaMateriais = new List<ModelosMateriais>();

            for (int i = 0; i < quantidade; i++)
            {
                var item = new ModelosMateriais
                {
                    id = Guid.NewGuid(),
                    material = TxtMaterial.Text.Trim(),
                    descricao = TxtDescricao.Text.Trim(),
                    marca = TxtMarcaModelo.Text.Trim(),
                    tipoMaterial = (TipoMaterial)CmbTipoMaterial.SelectedItem,
                    numPat = ((TipoMaterial)CmbTipoMaterial.SelectedItem == TipoMaterial.Duravel)?NPAutomatico.Gerar() : null,
                    valorUnitario = valor,                   
                    aquisicao = (FormaAquisicao)CmbAquisicao.SelectedItem,                    
                    responsavel = TxtResponsavel.Text.Trim(),
                    dtVerificacao = DtVerificacao.SelectedDate,
                    departamentoId = departamentoId,
                    subDepartamentoId = subDepartamentoId,
                    ativo = true
                };

                listaMateriais.Add(item);
            }

            var servico = new ServicoMateriais();

            bool sucessoMaterial = await servico.CriarAsync(listaMateriais);           

            if (!sucessoMaterial)
            {
                MessageBox.Show("Erro ao cadastrar material(is).");
                return;
            }
            else
            {                
                foreach (var material in listaMateriais)
                {
                    var mov = new ModelosMovimentacoes
                    {
                        id = Guid.NewGuid(),
                        materialId = material.id,
                        tipo = TipoMovimentacao.Entrada,                        
                        departamentoId = material.departamentoId,
                        dtmovimentacao = DateTime.Now,
                        usuario_id = Sessao.UsuarioLogado!.Id
                    };

                    var servicoMov = new ServicoMovimentacoes();
                    await servicoMov.CriarAsync(mov);
                }  
               
            }

            MessageBox.Show("Material(is) cadastrado(s) com sucesso!");

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
