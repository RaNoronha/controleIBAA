using ControleMaterialIBAA.Config;
using ControleMaterialIBAA.DTO;
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

        private async void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            string nomeDepartamento = TxtDepartamento.Text.Trim().ToLower();            

            if (string.IsNullOrEmpty(nomeDepartamento))
            {
                MessageBox.Show("O nome do Departamento é obrigatório.");
                return;
            }

            var servico = new ServicoDepartamentos();

            var departamento = new DepartamentoDTO
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

            MessageBox.Show("Cadastro realizado com sucesso!");           
        }
    }
}