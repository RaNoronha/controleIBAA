using ControleMaterialIBAA.Config;
using ControleMaterialIBAA.Modelos;
using ControleMaterialIBAA.Servicos;
using System;
using System.Collections.Generic;
using System.Net.Http;
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
using static System.Net.WebRequestMethods;

namespace ControleMaterialIBAA.View.Paginas
{
    /// <summary>
    /// Interação lógica para CadastrarDepartamento.xam
    /// </summary>
    public partial class CadastrarDepartamento : UserControl
    {
        public CadastrarDepartamento()
        {
            InitializeComponent();
        }

        private void ChkCriarSub_Checked(object sender, RoutedEventArgs e)
        {
            TxtSubDepartamento.IsEnabled = true;
            TxtSubDepartamento.Opacity = 1;
        }

        private void ChkCriarSub_Unchecked(object sender, RoutedEventArgs e)
        {
            TxtSubDepartamento.IsEnabled = false;
            TxtSubDepartamento.Opacity = 0.5;
            TxtSubDepartamento.Clear();
        }

        private async void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            string nomeDepartamento = TxtDepartamento.Text.Trim();
            string nomeSub = TxtSubDepartamento.Text.Trim();

            if (string.IsNullOrEmpty(nomeDepartamento))
            {
                MessageBox.Show("O nome do Departamento é obrigatório.");
                return;
            }

            var servico = new ServicoDepartamentos();

            var departamento = new ModelosDepartamentos
            {
                id = Guid.NewGuid(),
                nome = nomeDepartamento,
                ativo = true
            };

            bool sucessoDepartamento = await servico.CriarAsync(departamento);

            if (!sucessoDepartamento)
            {
                MessageBox.Show("Erro ao cadastrar Departamento.");
                return;
            }

            // Se marcou para criar sub
            if (ChkCriarSub.IsChecked == true)
            {
                if (string.IsNullOrEmpty(nomeSub))
                {
                    MessageBox.Show("Informe o nome do SubDepartamento.");
                    return;
                }

                var servicoSub = new ServicoSubDepartamentos();

                var sub = new ModelosSubDepartamentos
                {
                    id = Guid.NewGuid(),
                    nome = nomeSub,
                    departamentoId = departamento.id,
                    ativo = true
                };

                bool sucessoSub = await servicoSub.CriarAsync(sub);

                if (!sucessoSub)
                {
                    MessageBox.Show("Departamento criado, mas erro ao cadastrar SubDepartamento.");
                    return;
                }
            }
            MessageBox.Show("Cadastro realizado com sucesso!");           
        }
    }
}