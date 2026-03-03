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
            int codigo = int.Parse(TxtCod.Text);           

            var sub = new ModelosSubDepartamentos()
            {
                id = Guid.NewGuid(),
                nome = nomeSub,
                departamentoId = departamentoId,
                cod = codigo,
                ativo = true
            };

            var servicoSub = new ServicoSubDepartamentos();

            var existente = await servicoSub.ObterPorCodigoAsync(codigo, departamentoId);

            if (existente != null)
            {
                MessageBox.Show("Já existe um subdepartamento com esse código.");
                return;
            }

            await servicoSub.CriarAsync(sub);

            MessageBox.Show("Subdepartamento cadastrado com sucesso!");
            LimparCampos(this);
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
