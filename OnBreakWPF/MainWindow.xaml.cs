using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OnBreakWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnAdminClientes_Click(object sender, RoutedEventArgs e)
        {
            CrudCliente ventana = new CrudCliente();
            ventana.Owner = this;
            ventana.ShowDialog();
        }

        private void BtnListadoClientes_Click(object sender, RoutedEventArgs e)
        {
            ListClientes ventana = new ListClientes();
            ventana.Owner = this;
            ventana.ShowDialog();
        }

        private void BtnAdminContratos_Click(object sender, RoutedEventArgs e)
        {
            CrudContrato ventana = new CrudContrato();
            ventana.Owner = this;
            ventana.ShowDialog();
        }

        private void BtnListaContratos_Click(object sender, RoutedEventArgs e)
        {
            ListContratos ventana = new ListContratos();
            ventana.Owner = this;
            ventana.ShowDialog();
        }
    }
}
