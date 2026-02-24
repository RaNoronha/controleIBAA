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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ControleMaterialIBAA.View.Paginas
{
    /// <summary>
    /// Interação lógica para CadastrarSubDepartamento.xam
    /// </summary>
    public partial class CadastrarSubDepartamento : UserControl
    {
        public CadastrarSubDepartamento()
        {
            InitializeComponent();
            CarregarDepartamentos();
        }

        private async void CarregarDepartamentos()
        {
            var servico = new ServicoDepartamentos();
            var lista = await servico.ListarAsync();

            CmbDepartamentos.ItemsSource = lista;
        }

        private async void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (CmbDepartamentos.SelectedValue == null)
            {
                MessageBox.Show("Selecione um departamento.");
                return;
            }

            Guid departamentoId = (Guid)CmbDepartamentos.SelectedValue;

            string nomeSub = TxtSubDepartamento.Text.Trim();

            var sub = new ModelosSubDepartamentos()
            {
                id = Guid.NewGuid(),
                nome = nomeSub,
                departamentoId = departamentoId,
                ativo = true
            };

            var servicoSub = new ServicoSubDepartamentos();
            await servicoSub.CriarAsync(sub);

            MessageBox.Show("Subdepartamento cadastrado com sucesso!");
        }
    }
}
