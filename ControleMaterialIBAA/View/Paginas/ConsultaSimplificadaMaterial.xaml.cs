using ControleMaterialIBAA.Enums;
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
    /// Interação lógica para ConsultaSimplificadaMaterial.xam
    /// </summary>
    public partial class ConsultaSimplificadaMaterial : UserControl
    {
        private readonly ServicoMateriais _servicoMateriais = new ServicoMateriais();
        public ConsultaSimplificadaMaterial()
        {
            InitializeComponent();
            CarregarDepartamentos();
            CarregarTipoMaterial();
        }

        private async void CarregarDepartamentos()
        {
            var servico = new ServicoDepartamentos();
            var lista = await servico.ListarAsync();

            // 🔹 inserir opção vazia no início
            lista.Insert(0, new ModelosDepartamentos
            {
                id = Guid.Empty,
                nome = ""
            });

            CmbDepartamentos.ItemsSource = lista;
            CmbDepartamentos.SelectedIndex = 0;
        }

        private void CarregarTipoMaterial()
        {
            var lista = new List<KeyValuePair<int?, string>>();

            // 🔹 Opção vazia
            lista.Add(new KeyValuePair<int?, string>(null, ""));

            // 🔹 Enum
            foreach (TipoMaterial tipo in Enum.GetValues(typeof(TipoMaterial)))
            {
                lista.Add(new KeyValuePair<int?, string>((int)tipo, tipo.ToString()));
            }

            CmbTipoMaterial.ItemsSource = lista;
            CmbTipoMaterial.DisplayMemberPath = "Value";
            CmbTipoMaterial.SelectedValuePath = "Key";

            CmbTipoMaterial.SelectedIndex = 0; // começa vazio
        }

        private async void BtnPesquisar_Click(object sender, RoutedEventArgs e)
        {

            var material = TxtMaterial.Text?.Trim();
            TipoMaterial? tipo = null;
            Guid? departamentoId = null;           

            if (CmbDepartamentos.SelectedValue is Guid dep && dep != Guid.Empty)
            {
                departamentoId = dep;
            }           

            if (CmbTipoMaterial.SelectedValue != null)
            {
                tipo = (TipoMaterial)CmbTipoMaterial.SelectedValue;
            }
                

            var lista = await _servicoMateriais.ConsultaResumidaAsync(               
                material: material,
                departamentoId: departamentoId,                
                tipo: tipo
            );

            DgMateriais.ItemsSource = lista;
        }
    }
}
