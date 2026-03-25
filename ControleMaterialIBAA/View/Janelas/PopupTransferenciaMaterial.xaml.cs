using ControleMaterialIBAA.DTO;
using ControleMaterialIBAA.Enums;
using ControleMaterialIBAA.Infra;
using ControleMaterialIBAA.Modelos;
using ControleMaterialIBAA.Servicos;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace ControleMaterialIBAA.View.Janelas
{
    /// <summary>
    /// Lógica interna para PopupTransferenciaMaterial.xaml
    /// </summary>
    public partial class PopupTransferenciaMaterial : Window
    {
        ServicoDepartamentos _servicoDep = new ServicoDepartamentos();
        ServicoSubDepartamentos _servicoSub = new ServicoSubDepartamentos();
        ServicoMateriais _servicoMateriais = new ServicoMateriais();
        ServicoMovimentacoes _servicoMov = new ServicoMovimentacoes();
        private List<ModelosMateriais> _materiais;

        public PopupTransferenciaMaterial(List<ModelosMateriais> materiais)
        {
            InitializeComponent();
            _materiais = materiais;

            //LstMateriais.ItemsSource = materiais;
            //LstMateriais.DisplayMemberPath = "material";

            //AtualizarInformacoesOrigem();

            CarregarDepartamentos();
        }

        //private void LstMateriais_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (LstMateriais.SelectedItem is ModelosMateriais material)
        //    {
        //        TxtDepartamentoOrigem.Text = material.departamento_nome;
        //        TxtSubDepartamentoOrigem.Text = material.subdepartamento_nome;
        //        TxtResponsavelOrigem.Text = material.responsavel;
        //    }
        //}

        private async void CmbDepartamentoDestino_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbDepartamentoDestino.SelectedValue is Guid departamentoId)
            {
                var subdeps = await _servicoSub.ListarPorDepartamentoAsync(departamentoId);

                CmbSubDepartamentoDestino.ItemsSource = subdeps;
            }
        }

        //private void AtualizarInformacoesOrigem()
        //{
        //    var departamentos = _materiais.Select(x => x.departamento_nome).Distinct().ToList();
        //    var subDepartamentos = _materiais.Select(x => x.subdepartamento_nome).Distinct().ToList();
        //    var responsaveis = _materiais.Select(x => x.responsavel).Distinct().ToList();

        //    TxtDepartamentoOrigem.Text =
        //        departamentos.Count == 1 ? departamentos.First() : "Múltiplos departamentos";

        //    TxtSubDepartamentoOrigem.Text =
        //        subDepartamentos.Count == 1 ? subDepartamentos.First() : "Múltiplos subdepartamentos";

        //    TxtResponsavelOrigem.Text =
        //        responsaveis.Count == 1 ? responsaveis.First() : "Múltiplos responsáveis";
        //}

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

        private async void BtnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            if (CmbDepartamentoDestino.SelectedValue == null)
            {
                MessageBox.Show("Selecione o novo departamento.");
                return;
            }

            if (CmbSubDepartamentoDestino.SelectedValue == null)
            {
                MessageBox.Show("Selecione o novo subdepartamento.");
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtResponsavelDestino.Text))
            {
                MessageBox.Show("Informe o novo responsável.");
                return;
            }

            var novoDepartamento = (Guid)CmbDepartamentoDestino.SelectedValue;
            var novoSubDepartamento = (Guid)CmbSubDepartamentoDestino.SelectedValue;
            var novoResponsavel = TxtResponsavelDestino.Text.Trim();

            try
            {
                foreach (var material in _materiais)
                {                    
                    var movimentacao = new ModelosMovimentacoes
                    {
                        id = Guid.NewGuid(),
                        materialId = material.id,
                        departamentoId = novoDepartamento,
                        tipo = TipoMovimentacao.Transferencia,
                        usuario_id = Sessao.UsuarioLogado.Id,
                        dtmovimentacao = DateTime.Now,
                        observacao = TxtObservacao.Text
                    };

                    await _servicoMov.RegistrarMovimentacaoAsync(movimentacao);
                    
                    //await _servicoMateriais.AtualizarAsync(
                    //    material.id,
                    //    ativo
                    //);
                }

                MessageBox.Show("Transferência realizada com sucesso.");

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao realizar transferência:\n{ex.Message}");
            }
        }
    }
}
