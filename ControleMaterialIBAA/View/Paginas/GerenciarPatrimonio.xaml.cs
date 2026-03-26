using ControleMaterialIBAA.Enums;
using ControleMaterialIBAA.Helper;
using ControleMaterialIBAA.Infra;
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
        private readonly ServicoPatrimonios _servicoPatrimonios = new ServicoPatrimonios();
        private readonly ServicoMovimentacoes _servicoMovimentacoes = new ServicoMovimentacoes();
        private List<ModelosPatrimonios> _patrimonios;
        private List<ModelosMateriais> _materiais;

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

            _materiais = lista;
            DgMateriais.ItemsSource = lista;
        }

        private async void BtnTranferirMaterial_Click(object sender, RoutedEventArgs e)
        {
            var selecionados = _materiais.Where(x => x.selecionado).ToList();

            if (selecionados.Count == 0)
            {
                MessageBox.Show("Selecione pelo menos um material.");
                return;
            }

            var patrimoniosSelecionados = new List<ModelosPatrimonios>();

            if (_patrimonios == null || !_patrimonios.Any())
            {
                _patrimonios = await _servicoPatrimonios.ListarAsync(); 
            }
            foreach (var mat in selecionados)
            {
                var patrimonio = _patrimonios.FirstOrDefault(p => p.materialId == mat.id && p.ativo);
                patrimoniosSelecionados.Add(patrimonio);
            }

            var popup = new PopupTransferenciaMaterial(selecionados, patrimoniosSelecionados);
            popup.ShowDialog();

            if (!popup.confirmado) {return;}

            var departamentoId = popup.departamentoDestinoId;
            Guid? subDepartamentoId = popup.subDepartamentoDestinoId;
            var responsavel = popup.responsavelDestino;
            var observacao = popup.observacao;
            var quantidade = popup.quant;

            foreach (var material in selecionados)
            {
                if (material.tipoMaterial == TipoMaterial.Permanente)
                {
                    var listaPatrimonios = new List<ModelosPatrimonios>();

                    for (int i = 0; i < quantidade; i++)
                    {
                        listaPatrimonios.Add(new ModelosPatrimonios
                        {
                            id = Guid.NewGuid(),
                            numeroPatrimonial = Convert.ToInt32(GerarCodigos.GerarNumeroPatrimonial()),
                            materialId = material.id,
                            departamentoId = departamentoId,
                            subDepartamentoId = subDepartamentoId,
                            responsavel = responsavel,
                            dtTransferencia = DateTime.Now,
                            ativo = true
                        });
                    }

                    var sucessoPatrimonio = await _servicoPatrimonios.CriarAsync(listaPatrimonios);

                    if (!sucessoPatrimonio)
                    {
                        MessageBox.Show($"Erro ao gerar patrimônio para {material.nome}");
                        return;
                    }
                }

                var movimentacao = new ModelosMovimentacoes
                {
                    id = Guid.NewGuid(),
                    materialId = material.id,
                    departamentoId = departamentoId,
                    subDepartamentoId = subDepartamentoId,
                    quantidade = quantidade,
                    tipo = TipoMovimentacao.Transferencia,
                    usuarioId = Sessao.UsuarioLogado.Id,
                    dtMovimentacao = DateTime.Now,
                    observacao = observacao
                };

                var sucessoMov = await _servicoMovimentacoes.CriarAsync(movimentacao);

                if (!sucessoMov)
                {
                    MessageBox.Show($"Erro ao registrar movimentação para {material.nome}");
                    return;
                }
            }

            MessageBox.Show("Transferência realizada com sucesso!");
            
            foreach (var item in _materiais)
            {
                item.selecionado = false;
            }               

            DgMateriais.Items.Refresh();
        }
    }
}
