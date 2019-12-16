using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Lógica de interacción para CrudCliente.xaml
    /// </summary>
    public partial class CrudCliente : Window
    {
        int ClienteNoIngresado = 0;
        string Info = "";
        public CrudCliente()
        {
            InitializeComponent();
            VentanaModoInicio();
        }

        private void VentanaModoInicio()
        {
            btnEliminar.IsEnabled = false;
            btnModificar.IsEnabled = false;
            btnRegistrarClientes.IsEnabled = false;
        }

        private void LimpiarControles()
        {
            txtRut.Text = string.Empty;
            txtRazonSocial.Text = string.Empty;
            txtNombreContacto.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            cboxActividadEmpresa.SelectedIndex = -1;
            cboxTipoEmpresa.SelectedIndex = -1; 
        }

        private void BtnBuscarCliente_Click(object sender, RoutedEventArgs e)
        {
            OnBreakFinalEntities bbdd = new OnBreakFinalEntities();
            try
            {

                Cliente cl = bbdd.Cliente.Where(c => c.RutCliente == txtRut.Text).First<Cliente>();


                txtRut.Text = cl.RutCliente;
                txtRazonSocial.Text = cl.RazonSocial;
                txtNombreContacto.Text = cl.NombreContacto;
                txtEmail.Text = cl.MailContacto;
                txtDireccion.Text = cl.Direccion;
                txtTelefono.Text = cl.Telefono;
                cboxActividadEmpresa.Text = cl.IdActividadEmpresa;
                cboxTipoEmpresa.Text = cl.IdTipoEmpresa;

                btnEliminar.IsEnabled = true;
                btnModificar.IsEnabled = true;

            }
            catch (Exception)
            {
                MessageBox.Show("Cliente no encontrado", "Atencion", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

        private void BtnRegistrarClientes_Click(object sender, RoutedEventArgs e)
        {
            ServiceCliente cli = new ServiceCliente();
            Cliente c = new Cliente();

            Info = "Favor ingresar datos Obligatorios:\n";
            ClienteNoIngresado = 0;

            string emailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

            //Validamos que el campo Rut no este vacío y que sea un Rut Valido
            if (txtRut.Text != string.Empty)
            {
                if (ValidarRut(txtRut.Text))
                {c.RutCliente = (txtRut.Text).Replace(".", "").Replace("-", ""); btnRegistrarClientes.IsEnabled = true; }
                else { Info = Info + " - El rut no es valido\n"; }
            } else
            { Info = Info + "- Rut\n"; ClienteNoIngresado = ClienteNoIngresado + 1; }

            //Validamos el campo Razon Social, que no este vacío
            if (txtRazonSocial.Text != string.Empty)
            { c.RazonSocial = txtRazonSocial.Text; }
            else
            { Info = Info + "- Razon Social\n"; ClienteNoIngresado = ClienteNoIngresado + 1; }

            //Validamos el campo Nombre del Contacto, que no este vacío
            if (txtNombreContacto.Text != string.Empty)
            { c.NombreContacto = txtNombreContacto.Text; }
            else
            { Info = Info + "- Nombre del Contacto\n"; ClienteNoIngresado = ClienteNoIngresado + 1; }

            //Validamos el campo Email, que no este vacío y que este escrito correctamente
            if (txtEmail.Text != string.Empty)
            {
                Match resultado = Regex.Match(txtEmail.Text, emailPattern);
                if (resultado.Success)
                { c.MailContacto = txtEmail.Text; }
                else
                { Info = Info + "- Email con errores de Formato\n"; ClienteNoIngresado = ClienteNoIngresado + 1; }
            }
            else
            { Info = Info + "- Email\n"; ClienteNoIngresado = ClienteNoIngresado + 1; }

            //Validamos el campo Direccion, que no este vacío
            if (txtDireccion.Text != string.Empty)
            { c.Direccion = txtDireccion.Text; }
            else
            {
                Info = Info + "- Direccíon\n";
                ClienteNoIngresado = ClienteNoIngresado + 1;
            }

            //Validamos el campo Telefono, que no este vacío
            if (txtTelefono.Text != string.Empty)
            {
                try
                { c.Telefono = txtTelefono.Text; }
                catch (FormatException)
                {
                    Info = Info + "- Telefono: Error de Formato\n"; ClienteNoIngresado = ClienteNoIngresado + 1;
                }
            } else
            { Info = Info + "- Numero de Telefono\n"; ClienteNoIngresado = ClienteNoIngresado + 1;
            }

            //Validamos el campo Actividad, que no se encuentre vacío
            if (cboxActividadEmpresa.Text != string.Empty)
            {
                c.IdActividadEmpresa = cboxActividadEmpresa.Text;
            }
            else
            {
                Info = Info + "- Actividad Empresa\n ";
                ClienteNoIngresado = ClienteNoIngresado + 1;
            }

            //Validamos el campo Tipo Empresa, que no se encuentre vacío
            if (cboxTipoEmpresa.Text != string.Empty)
            {
                c.IdTipoEmpresa = cboxTipoEmpresa.Text;
            }
            else
            {
                Info = Info + "- Tipo de Empresa\n";
                ClienteNoIngresado = ClienteNoIngresado + 1;
            }

            // ************* Chekeamos que la variable "ClienteNoIngresado" sea igual a cero para confirmar registro ***********
            if (ClienteNoIngresado == 0)
            {
                try
                {
                    cli.AgregarEntidad(c);
                    MessageBox.Show("Cliente Registrado con Exito", "Exito", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    LimpiarControles();

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Cliente ya existe en la base de datos");
                }
            }
            else
            {
                //Dentro del MessageBox, metemos la variable Info, que contendra los mensajes capturados de las validaciones de cada text box
                MessageBox.Show(Info, "Alerta", MessageBoxButton.OK, MessageBoxImage.Hand);
            }

        }

        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            ServiceCliente cli = new ServiceCliente();
            Cliente c = new Cliente();


            c.RutCliente = txtRut.Text;
            c.RazonSocial = txtRazonSocial.Text;
            c.NombreContacto = txtNombreContacto.Text;
            c.MailContacto = txtEmail.Text;
            c.Direccion = txtDireccion.Text;
            c.Telefono = txtTelefono.Text;
            c.IdActividadEmpresa = cboxActividadEmpresa.Text;
            c.IdTipoEmpresa = cboxTipoEmpresa.Text;
            try
            {
                cli.ActualizarEntidad(c);
                MessageBox.Show("Cliente modificado con exito");
                LimpiarControles();
                VentanaModoInicio();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            ServiceCliente cli = new ServiceCliente();
            try
            {
                cli.EliminarEntidad(txtRut.Text);
                MessageBox.Show("Cliente eliminado con exito");
                LimpiarControles();
                VentanaModoInicio();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarControles();
            VentanaModoInicio();
        }

        private void BtnListar_Click(object sender, RoutedEventArgs e)
        {
            ListClientes ventana = new ListClientes();
            ventana.Owner = this;
            ventana.ShowDialog();
            VentanaModoInicio();
            LimpiarControles();
            
        }

        public bool ValidarRut(string rut)
        {

            bool validacion = false;
            try
            {
                rut = rut.ToUpper();
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

                char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

                int m = 0, s = 1;
                for (; rutAux != 0; rutAux /= 10)
                {
                    s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
                }
                if (dv == (char)(s != 0 ? s + 47 : 75))
                {
                    validacion = true;
                }
            }
            catch (Exception)
            {
            }
            return validacion;
        }

        private void BtnRestablecer_Click(object sender, RoutedEventArgs e)
        {
            VentanaModoInicio();
            LimpiarControles();
        }

        public void LlenarCampos(Cliente row)
        {

            txtRut.Text = row.RutCliente;
            txtRazonSocial.Text = row.RazonSocial;
            txtNombreContacto.Text = row.NombreContacto;
            txtEmail.Text = row.MailContacto;
            txtDireccion.Text = row.Direccion;
            txtTelefono.Text = row.Telefono;
            cboxActividadEmpresa.Text = row.IdActividadEmpresa;
            cboxTipoEmpresa.Text = row.IdTipoEmpresa;

        }

        private void TxtRut_TextChanged(object sender, TextChangedEventArgs e)
        {
            string rutPattern = @"^ 0 * (\d{ 1,3} (\.?\d{ 3})*)\-? ([\dkK])$";
            Match results = Regex.Match(txtRut.Text, rutPattern);
            if (!results.Success)
            {
                if (ValidarRut(txtRut.Text))
                {
                    btnRegistrarClientes.IsEnabled = true;
                }
                else { btnRegistrarClientes.IsEnabled = false; }
            }
        }
    }
}
