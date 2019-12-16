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
using System.Windows.Shapes;
using BibliotecaClases;

namespace OnBreakWPF
{
    /// <summary>
    /// Lógica de interacción para ListClientes.xaml
    /// </summary>
    public partial class ListClientes : Window
    {
       
        
       
        public ListClientes()
        {
            InitializeComponent();
            
            ServiceCliente sc = new ServiceCliente();
            sc.llenarData(dgClientes);
        }

        
        

        private void BtnCargarFilas_Click(object sender, RoutedEventArgs e)
        {
            try
            {
              
            }
            catch (Exception d)
            {

                MessageBox.Show(d.Message);
            }

           
        }

        private void TxtRut_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                OnBreakFinalEntities bbdd = new OnBreakFinalEntities();
                Cliente cl = bbdd.Cliente.Where(c => c.RutCliente == txtRut.Text).First<Cliente>();
                txtRut.Text = cl.RutCliente;
            }
            catch (Exception d)
            {

                MessageBox.Show(d.Message);
            }
            
        }
    }
}
