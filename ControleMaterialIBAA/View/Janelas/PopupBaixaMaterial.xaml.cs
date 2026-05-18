using ControleMaterialIBAA.Enums;
using ControleMaterialIBAA.Modelos;
using ControleMaterialIBAA.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ControleMaterialIBAA.View.Janelas
{
    public partial class PopupBaixaMaterial : Window
    {
        private readonly ServicoPatrimonios _servicoPatrimonios = new ServicoPatrimonios();
        ServicoDepartamentos _servicoDep = new ServicoDepartamentos();
        ServicoSubDepartamentos _servicoSubDep = new ServicoSubDepartamentos();
        private List<ModelosMateriais> _materiais;
        private List<ModelosPatrimonios> _patrimonios;
        public bool confirmado { get; set; }
        public TipoMovimentacao tipoBaixa { get; set; }
        public string observacao { get; set; }
        public List<ModelosPatrimonios> patrimoniosSelecionados { get; set; } = new();

        public PopupBaixaMaterial(List<ModelosMateriais> materiais,  List<ModelosPatrimonios> patrimonios)
        {
            InitializeComponent();

            _materiais = materiais;
            _patrimonios = patrimonios;

            LstMateriais.ItemsSource = _materiais;

            CarregarPatrimonios();
        }

        #region CARREGAR PATRIMÔNIOS

        private async void CarregarPatrimonios()
        {
            var materiaisIds = _materiais.Where(m => m != null).Select(m => m.id).ToHashSet();

            var lista = _patrimonios.Where(p => p != null && p.ativo && materiaisIds.Contains(p.materialId)). ToList();
            
            var departamentos = await _servicoDep.ListarAsync();
            var subDepartamento = await _servicoSubDep.ListarAsync();

            foreach (var patrimonio in lista)
            {
                var dept = departamentos.FirstOrDefault(d => d.id == patrimonio.departamentoId);
                patrimonio.departamentoNome = dept?.nome ?? "N/A";

                var subDept = subDepartamento.FirstOrDefault(d => d.id == patrimonio.subDepartamentoId);
                patrimonio.subDepartamentoNome = subDept?.nome ?? "N/A";

            }

            DgPatrimonios.ItemsSource = lista;
        }

        #endregion

        #region CONFIRMAR

        private void BtnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            var comboItem = CmbTipoBaixa.SelectedItem as ComboBoxItem;

            if (comboItem == null)
            {
                MessageBox.Show("Selecione o tipo de baixa.");
                return;
            }

            tipoBaixa = (TipoMovimentacao)int.Parse(comboItem.Tag.ToString());

            observacao = TxtObservacao.Text?.Trim();

            var lista = DgPatrimonios.ItemsSource as List<ModelosPatrimonios>;

            if (lista == null)
            {
                MessageBox.Show("Nenhum patrimônio carregado.");
                return;
            }

            patrimoniosSelecionados = lista.Where(p => p.selecionado).ToList();

            if (!patrimoniosSelecionados.Any())
            {
                MessageBox.Show("Selecione pelo menos um patrimônio.");
                return;
            }

            confirmado = true;
            Close();
        }

        #endregion

        #region CANCELAR

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            confirmado = false;
            Close();
        }

        #endregion
    }
}