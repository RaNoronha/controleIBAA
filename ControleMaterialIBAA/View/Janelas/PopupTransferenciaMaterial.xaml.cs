using ControleMaterialIBAA.Servicos;
using ControleMaterialIBAA.Modelos;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ControleMaterialIBAA.View.Janelas
{
    public partial class PopupTransferenciaMaterial : Window
    {
        ServicoDepartamentos _servicoDep = new ServicoDepartamentos();
        ServicoSubDepartamentos _servicoSub = new ServicoSubDepartamentos();
        ServicoPatrimonios _servicoPatrimonios = new ServicoPatrimonios();
        ServicoMateriais _servicoMateriais = new ServicoMateriais();
        private List<ModelosMateriais> _materiais;
        private List<ModelosPatrimonios> _patrimonios;

        public bool confirmado { get; set; } = false;
        public Guid departamentoDestinoId { get; set; }
        public Guid? subDepartamentoDestinoId { get; set; }
        public string responsavelDestino { get; set; }
        public string observacao { get; set; }
        public int quant { get; set; }

        public PopupTransferenciaMaterial(List<ModelosMateriais> materiais, List<ModelosPatrimonios> patrimonios)
        {
            InitializeComponent();
            _materiais = materiais;
            _patrimonios = patrimonios;
            LstMateriais.ItemsSource = _materiais;

            PreencherOrigem();
            CarregarDepartamentos();
        }

        private async void CarregarDepartamentos()
        {
            try
            {
                var lista = await _servicoDep.ListarAsync();
                CmbDepartamentoDestino.ItemsSource = lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar departamentos:\n{ex.Message}");
            }
        }

        private async Task PreencherOrigem()
        {
            var departamentos = new List<string>();
            var subdeps = new List<string>();
            var responsaveis = new List<string>();

            foreach (var mat in _materiais)
            {
                var origem = await _servicoMateriais.ObterOrigemMaterial(mat);

                departamentos.Add(origem.departamento);
                subdeps.Add(origem.subDepartamento);
                responsaveis.Add(origem.responsavel);
            }

            TxtDepartamentoOrigem.Text = departamentos.Distinct().Count() == 1 ? departamentos.First() : "Múltiplos";

            TxtSubDepartamentoOrigem.Text = subdeps.Distinct().Count() == 1 ? subdeps.First() : "Múltiplos";

            TxtResponsavelOrigem.Text = responsaveis.Distinct().Count() == 1 ? responsaveis.First() : "Múltiplos";
        }

        private async void CmbDepartamentoDestino_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbDepartamentoDestino.SelectedValue is Guid departamentoId)
            {
                var subdeps = await _servicoSub.ListarPorDepartamentoAsync(departamentoId);
                CmbSubDepartamentoDestino.ItemsSource = subdeps;
            }
        }
        
        private void BtnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            if (CmbDepartamentoDestino.SelectedValue == null)
            {
                MessageBox.Show("Selecione o novo departamento.");
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtResponsavelDestino.Text))
            {
                MessageBox.Show("Informe o novo responsável.");
                return;
            }

            if (!int.TryParse(TxtQuantidade.Text, out int quantidade) || quantidade <= 0)
            {
                MessageBox.Show("Quantidade inválida.");
                return;
            }

            departamentoDestinoId = (Guid)CmbDepartamentoDestino.SelectedValue;
            subDepartamentoDestinoId = CmbSubDepartamentoDestino.SelectedValue as Guid?;
            responsavelDestino = TxtResponsavelDestino.Text.Trim();
            observacao = TxtObservacao.Text;
            quant = quantidade;

            confirmado = true;

            Close();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            confirmado = false;
            Close();
        }
    }
}