using ControleMaterialIBAA.Enums;
using ControleMaterialIBAA.Modelos;
using ControleMaterialIBAA.Servicos;
using ControleMaterialIBAA.Infra;
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
using ControleMaterialIBAA.DTO;

namespace ControleMaterialIBAA.View.Janelas
{
    /// <summary>
    /// Lógica interna para PopupBaixaMateiral.xaml
    /// </summary>
    public partial class PopupBaixaMaterial : Window
    {
        private List<GerenciaDTO> _materiais;
        ServicoMovimentacoes _servicoMov = new ServicoMovimentacoes();
        ServicoMateriais _servicoMateriais = new ServicoMateriais();

        public PopupBaixaMaterial(List<GerenciaDTO> materiais)
        {
            InitializeComponent();
            _materiais = materiais;
           
            LstMateriais.ItemsSource = materiais;
            LstMateriais.DisplayMemberPath = "material";

            CmbTipoMovimentacao.ItemsSource = new List<TipoMovimentacao>()
            {
                TipoMovimentacao.Baixa,
                TipoMovimentacao.Descarte,
                TipoMovimentacao.Dano
            };
        }
        private async void BtnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            if (CmbTipoMovimentacao.SelectedItem == null)
            {
                MessageBox.Show("Selecione o tipo de baixa.");
                return;
            }

            var tipo = (TipoMovimentacao)CmbTipoMovimentacao.SelectedItem;

            try
            {
                foreach (var material in _materiais)
                {
                    var movimentacao = new ModelosMovimentacoes
                    {
                        id = Guid.NewGuid(),
                        materialId = material.id,
                        departamentoId = material.departamentoId,
                        tipo = tipo,
                        usuario_id = Sessao.UsuarioLogado.Id, // ou Guid fixo se ainda não tiver login
                        dtmovimentacao = DateTime.Now,
                        observacao = TxtObservacao.Text
                    };

                    await _servicoMov.RegistrarMovimentacaoAsync(movimentacao);

                    await _servicoMateriais.InativarAsync(material.id);
                }

                MessageBox.Show("Baixa realizada com sucesso.");

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao realizar baixa:\n{ex.Message}");
            }
        }

    }
}
