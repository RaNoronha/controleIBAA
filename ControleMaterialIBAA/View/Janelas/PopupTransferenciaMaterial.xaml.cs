using ControleMaterialIBAA.Enums;
using ControleMaterialIBAA.Modelos;
using ControleMaterialIBAA.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // 🔥 NOVO
        public List<ModelosPatrimonios> patrimoniosSelecionados { get; set; } = new();

        private bool isPermanente = false;

        public PopupTransferenciaMaterial(List<ModelosMateriais> materiais, List<ModelosPatrimonios> patrimonios)
        {
            InitializeComponent();

            _materiais = materiais;
            _patrimonios = patrimonios;

            LstMateriais.ItemsSource = _materiais;

            isPermanente = _materiais.Any(m => m.tipoMaterial == TipoMaterial.Permanente);

            ConfigurarTela();

            PreencherOrigem();
            CarregarDepartamentos();
        }

        private async void ConfigurarTela()
        {
            if (isPermanente)
            {
                PanelQuantidade.Visibility = Visibility.Collapsed;
                PanelPatrimonios.Visibility = Visibility.Visible;

                var lista = _patrimonios.Where(p => p.ativo && _materiais.Any(m => m.id == p.materialId)).ToList();

                var departamentos = await _servicoDep.ListarAsync();

                foreach (var pat in lista)
                {
                    var dep = departamentos.FirstOrDefault(d => d.id == pat.departamentoId);
                    pat.departamentoNome = dep?.nome ?? "N/A";
                }

                DgPatrimonios.ItemsSource = lista;
            }
            else
            {
                PanelQuantidade.Visibility = Visibility.Visible;
                PanelPatrimonios.Visibility = Visibility.Collapsed;
            }
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

            departamentoDestinoId = (Guid)CmbDepartamentoDestino.SelectedValue;
            subDepartamentoDestinoId = CmbSubDepartamentoDestino.SelectedValue as Guid?;
            responsavelDestino = TxtResponsavelDestino.Text.Trim();
            observacao = TxtObservacao.Text;

            if (isPermanente)
            {
                var lista = DgPatrimonios.ItemsSource as List<ModelosPatrimonios>;

                patrimoniosSelecionados = lista.Where(p => p.selecionado).ToList();

                if (!patrimoniosSelecionados.Any())
                {
                    MessageBox.Show("Selecione pelo menos um patrimônio.");
                    return;
                }

                quant = patrimoniosSelecionados.Count;
            }
            else
            {
                // 🔵 CONSUMO
                if (!int.TryParse(TxtQuantidade.Text, out int quantidade) || quantidade <= 0)
                {
                    MessageBox.Show("Quantidade inválida.");
                    return;
                }

                quant = quantidade;
            }

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