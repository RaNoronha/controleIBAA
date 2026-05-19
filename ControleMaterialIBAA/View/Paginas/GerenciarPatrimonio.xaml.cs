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
            if (_materiais == null)
            {
                MessageBox.Show("Lista de materiais não carregada.");
                return;
            }

            DgMateriais.CommitEdit(DataGridEditingUnit.Cell, true);
            DgMateriais.CommitEdit(DataGridEditingUnit.Row, true);

            var selecionados = _materiais.Where(x => x.selecionado).ToList();                       

            if (selecionados.Count == 0)
            {
                MessageBox.Show("Selecione pelo menos um material.");
                return;
            }

            if (_patrimonios == null || !_patrimonios.Any())
            {
                _patrimonios = await _servicoPatrimonios.ListarAsync();
            }

            var popup = new PopupTransferenciaMaterial(selecionados, _patrimonios);
            popup.ShowDialog();

            if (!popup.confirmado) return;

            var departamentoId = popup.departamentoDestinoId;
            Guid? subDepartamentoId = popup.subDepartamentoDestinoId;
            var responsavel = popup.responsavelDestino;
            var observacao = popup.observacao;
            var quantidade = popup.quant;

            foreach (var material in selecionados)
            {
                bool isPermanente = material.tipoMaterial == TipoMaterial.Permanente;
                
                if (isPermanente)
                {
                    var patrimoniosSelecionados = popup.patrimoniosSelecionados
                        .Where(p => p.materialId == material.id)
                        .ToList();

                    if (!patrimoniosSelecionados.Any())
                    {
                        MessageBox.Show($"Nenhum patrimônio selecionado para {material.nome}");
                        return;
                    }

                    foreach (var pat in patrimoniosSelecionados)
                    {
                        pat.departamentoId = departamentoId;
                        pat.subDepartamentoId = subDepartamentoId;
                        pat.responsavel = responsavel;
                        pat.dtTransferencia = DateTime.Now;

                        var sucessoPat = await _servicoPatrimonios.AtualizarAsync(pat);

                        if (!sucessoPat)
                        {
                            MessageBox.Show($"Erro ao atualizar patrimônio {pat.numeroPatrimonial}");
                            return;
                        }
                    }

                    quantidade = patrimoniosSelecionados.Count;
                }

                if (!isPermanente)
                {
                    if (quantidade <= 0)
                    {
                        MessageBox.Show($"Quantidade inválida para {material.nome}");
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
        private async void BtnBaixarMaterial_Click(object sender, RoutedEventArgs e)
        {
            if (_materiais == null || !_materiais.Any())
            {
                MessageBox.Show("Nenhum material carregado.");
                return;
            }

            DgMateriais.CommitEdit(DataGridEditingUnit.Cell, true);
            DgMateriais.CommitEdit(DataGridEditingUnit.Row, true);

            var selecionados = _materiais.Where(x => x.selecionado).ToList();
            if (!selecionados.Any())
            {
                MessageBox.Show("Selecione ao menos um material.");
                return;
            }

            var consumoInvalido = selecionados.FirstOrDefault(m => m.tipoMaterial == TipoMaterial.Consumo);
            if (consumoInvalido != null)
            {
                MessageBox.Show($"O material \"{consumoInvalido.nome}\" é do tipo CONSUMO e não permite baixa patrimonial.\n\nApenas materiais PERMANENTES podem ser baixados.",
                                "Operação não permitida", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_patrimonios == null || !_patrimonios.Any())
                _patrimonios = await _servicoPatrimonios.ListarAsync();

            var popup = new PopupBaixaMaterial(selecionados, _patrimonios);
            popup.ShowDialog();

            if (!popup.confirmado || popup.patrimoniosSelecionados == null || !popup.patrimoniosSelecionados.Any())
            {
                if (popup.confirmado) MessageBox.Show("Selecione ao menos um patrimônio.");
                return;
            }

            // 🔹 CONTADORES PARA FEEDBACK CLARO
            int sucessos = 0;
            int falhas = 0;
            var erros = new List<string>();

            foreach (var pat in popup.patrimoniosSelecionados)
            {
                try
                {
                    pat.ativo = false;
                    var sucessoPat = await _servicoPatrimonios.AtualizarAsync(pat);
                    if (!sucessoPat)
                    {
                        falhas++;
                        erros.Add($"❌ Falha ao atualizar {pat.numeroPatrimonial}");
                        continue;
                    }

                    var movimentacao = new ModelosMovimentacoes
                    {
                        id = Guid.NewGuid(),
                        materialId = pat.materialId,
                        departamentoId = pat.departamentoId,
                        subDepartamentoId = pat.subDepartamentoId,
                        quantidade = 1,
                        tipo = popup.tipoBaixa,
                        usuarioId = Sessao.UsuarioLogado.Id,
                        dtMovimentacao = DateTime.Now,
                        observacao = popup.observacao
                    };

                    var sucessoMov = await _servicoMovimentacoes.CriarAsync(movimentacao);
                    if (!sucessoMov)
                    {
                        falhas++;
                        erros.Add($"❌ Falha ao registrar movimentação de {pat.numeroPatrimonial}");
                        continue;
                    }

                    sucessos++;
                }
                catch (Exception ex)
                {
                    falhas++;
                    erros.Add($"💥 Erro inesperado em {pat.numeroPatrimonial}: {ex.Message}");
                }
            }

            // 🔹 FEEDBACK ÚNICO E LIMPO
            if (falhas > 0)
            {
                MessageBox.Show(
                    $"️ Baixa parcial!\n✅ Sucessos: {sucessos}\n Falhas: {falhas}\n\n{string.Join("\n", erros)}",
                    "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show($"✅ Baixa realizada com sucesso!\n{sucessos} patrimônio(s) processado(s).",
                                "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            //  LIMPA SELEÇÃO E ATUALIZA
            foreach (var item in _materiais) item.selecionado = false;
            DgMateriais.Items.Refresh();

            // Opcional: Recarregar patrimônios para refletir a nova situação
            _patrimonios = await _servicoPatrimonios.ListarAsync();
        }
    }
}
