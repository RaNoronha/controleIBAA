using System;
using System.Windows;

namespace ControleMaterialIBAA.View.Janelas
{
    public partial class PopupEstocarMaterial : Window
    {
        public bool confirmado = false;
        public int quantidade;
        public string responsavel;
        public string observacao;

        public PopupEstocarMaterial()
        {
            InitializeComponent();
        }

        private void BtnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtQuantidade.Text) || !int.TryParse(TxtQuantidade.Text, out int qtd) || qtd <= 0)
            {
                MessageBox.Show("Informe uma quantidade válida maior que zero.");
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtResponsavel.Text))
            {
                MessageBox.Show("Informe o responsável.");
                return;
            }

            quantidade = qtd;
            responsavel = TxtResponsavel.Text.Trim();
            observacao = TxtObservacao.Text?.Trim();

            confirmado = true;

            this.Close();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            confirmado = false;
            this.Close();
        }
    }
}