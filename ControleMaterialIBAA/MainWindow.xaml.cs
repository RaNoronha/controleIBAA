using ControleMaterialIBAA.Config;
using ControleMaterialIBAA.Servicos;
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

namespace ControleMaterialIBAA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Teste();
        }
        private async void Teste()
        {
            var service = new ServicoMateriais();
            var lista = await service.ListarAsync();
            MessageBox.Show($"Materiais encontrados: {lista.Count}");
        }
    }
}