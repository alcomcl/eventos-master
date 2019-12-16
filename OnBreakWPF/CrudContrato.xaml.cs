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
    /// Lógica de interacción para CrudContrato.xaml
    /// </summary>
    public partial class CrudContrato : Window
    {
        string mensaje;
        string mensaje2;
        int ClienteNoIngresado = 0;
        double uf = 27500;
        double valorBase = 0;
        double valorTotal = 0;
        double personalBase = 0;
        double personalTotal = 0;
        double asistentes = 0;
        double valorPersonalAdicional = 0;
        double valorAsistentes = 0;

        string estadoVigencia = string.Empty;
        string valorEvento = string.Empty;
        string numeroContrato = string.Empty;
        string razonSocial = string.Empty;
        string rutCliente = string.Empty;
        string nombreCliente = string.Empty;
        string Info = string.Empty;
        string emailCliente = string.Empty;
        string telefonoCliente = string.Empty;
        string fechaTermino = string.Empty;
        int ContratoNoActualizado = 0;

        public CrudContrato()
        {
            InitializeComponent();



            txtPersonal.Text = "0";
            txtAsistentes.Text = "1";
            txtCargaValorBase.IsEnabled = false;
            txtCargaPersoanlBase.IsEnabled = false;

            VentanaContratoModoInicio();

        }

        private void VentanaContratoModoInicio()
        {
            dpFechaInicio.IsEnabled = true;
            dpFechaInicio.SelectedDate = DateTime.Today;
            cboxTipoEvento.IsEnabled = false;
            cboxModalidad.IsEnabled = false;
            txtPersonal.IsEnabled = false;
            txtAsistentes.IsEnabled = false;
            txtObservaciones.IsEnabled = false;
            txtNumeroContra2.IsEnabled = false;
            txtVigenciaContrato.IsEnabled = false;
            txtRutAsociado.IsEnabled = false;
            txtNombreAsociado.IsEnabled = false;
            txtFechaContrato.IsEnabled = false;
            txtRazonSocialAsociada.IsEnabled = false;
            txtValorEventoEscondido.IsEnabled = false;
            btnActualizarContrato.IsEnabled = false;
            btnCrearContrato.IsEnabled = false;
            btnTerminarContrato.IsEnabled = false;
            btnBuscar.IsEnabled = true;
            btnBuscarPorNumero.IsEnabled = true;
            txtValorBase.IsEnabled = false;
            txtFechaTermino.IsEnabled = false;
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            OnBreakFinalEntities bbdd = new OnBreakFinalEntities();
            try
            {
                Cliente cl = bbdd.Cliente.Where(c => c.RutCliente == txtRut.Text).First<Cliente>();

                

                txtRutAsociado.Text = rutCliente;
                txtRazonSocialAsociada.Text = razonSocial;
                txtNombreAsociado.Text = nombreCliente;

                txtRutAsociado.Text = cl.RutCliente;
                txtRazonSocialAsociada.Text = cl.RazonSocial;
                txtNombreAsociado.Text = cl.NombreContacto;

                rutCliente = txtRutAsociado.Text;
                razonSocial = txtRazonSocialAsociada.Text;
                nombreCliente = txtNombreAsociado.Text;


                //Habilitacion de TexBox y Botones
                txtRazonSocialAsociada.IsEnabled = false;
 
                dpFechaInicio.IsEnabled = true;
                cboxTipoEvento.IsEnabled = true;
                cboxModalidad.IsEnabled = true;
                txtPersonal.IsEnabled = true;
                txtAsistentes.IsEnabled = true;
                txtObservaciones.IsEnabled = true;
                btnCrearContrato.IsEnabled = true;
                txtRutAsociado.IsEnabled = false;
                txtNombreAsociado.IsEnabled = false;
                btnBuscar.IsEnabled = false;
                btnBuscarPorNumero.IsEnabled = false;
                txtRut.Text = string.Empty;

            }
            catch (Exception)
            {
                
                MessageBox.Show("Cliente no encontrado", "Alerta", MessageBoxButton.OK, MessageBoxImage.Hand);
                txtRut.Text = string.Empty;
            }
        }

        private void BtnCrearContrato_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                ServiceContrato contrato = new ServiceContrato();
                Contrato c = new Contrato();
                Cliente cl = new Cliente();

                mensaje = "No deben haber campos vacios :\n";

                int ClienteNoIngresado = 0;

                c.Numero = DateTime.Now.ToString("yyyyMMddHHmm");

                numeroContrato = c.Numero;


                mensaje2 = "\nDetalle Contrato: \n" + "* Numero Contrato: " + numeroContrato + "\n* Rut Asociado: " + rutCliente + "\n* Razon Social: " + razonSocial + "\n";


                c.RutCliente = txtRutAsociado.Text;


                #region INGRESO DE LOS DATOS

                if (dpFechaInicio.SelectedDate.ToString() != string.Empty)
                {
                    DateTime.Parse(dpFechaInicio.Text).ToString("MM-dd-yyyy");
                    c.Creacion = DateTime.Parse(dpFechaInicio.Text);
                    mensaje2 = mensaje2 + "* Fecha Inicio: " + dpFechaInicio.Text + "\n";
                }
                else
                { mensaje = mensaje + "- Ingrese la fecha de inicio\n"; ClienteNoIngresado = ClienteNoIngresado + 1; }

                if (cboxTipoEvento.Text != string.Empty)
                {
                    c.TipoEvento = cboxTipoEvento.Text;
                    mensaje2 = mensaje2 + "* Tipo Evento: " + cboxTipoEvento.Text + "\n";
                }
                else
                { mensaje = mensaje + "- Ingrese el tipo de Evento\n"; ClienteNoIngresado = ClienteNoIngresado + 1; }


                if (cboxModalidad.Text != string.Empty)
                {
                    c.ModalidadServicio = cboxModalidad.Text;
                    mensaje2 = mensaje2 + "* Modalidad Servicio: " + cboxModalidad.Text + "\n";
                }
                else
                { mensaje = mensaje + "- Ingrese la Modalidad del Servicio\n"; ClienteNoIngresado = ClienteNoIngresado + 1; }




                if (txtAsistentes.Text != string.Empty)
                {
                    c.Asistentes = int.Parse(txtAsistentes.Text);
                    mensaje2 = mensaje2 + "* Cantidad Asistentes: " + txtAsistentes.Text + "\n";
                }
                else
                { mensaje = mensaje + "- Ingrese la cantidad de asistentes\n"; ClienteNoIngresado = ClienteNoIngresado + 1; }

                if (txtPersonal.Text != string.Empty)
                {
                    c.PersonalAdicional = int.Parse(txtPersonal.Text); mensaje2 = mensaje2 + "* Cantidad Personal Adicional: " + txtPersonal.Text + "\n";
                }
                else
                { mensaje = mensaje + "- Ingrese personal adicional\n"; ClienteNoIngresado = ClienteNoIngresado + 1; }

                if (txtObservaciones.Text != string.Empty)
                {
                    c.Observaciones = txtObservaciones.Text;
                }
                else
                { mensaje = mensaje + "- Ingrese observaciones del Contrato\n"; ClienteNoIngresado = ClienteNoIngresado + 1; }


                #endregion INGRESO DE LOS DATOS

                #region CALCULO VALOR DEL EVENTO

                #region Calculo para evento tipo Matrimonio
                // Calculo para evento tipo Matrimonio, con la 7 modalidades de servicio
                if (cboxTipoEvento.SelectedIndex == 0)
                {
                    if (cboxModalidad.SelectedIndex == 0)
                    {
                        valorBase = 3 * uf; personalBase = 2;

                        if (int.Parse(txtPersonal.Text) == 2)
                        { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                        else
                        {
                            if (double.Parse(txtPersonal.Text) == 3)
                            { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                            else
                            {
                                if (double.Parse(txtPersonal.Text) == 4)
                                {
                                    valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                }
                                else
                                {
                                    if (double.Parse(txtPersonal.Text) > 4)
                                    { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                    else
                                    {
                                        if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                        {
                                            valorPersonalAdicional = 0; personalTotal = personalBase;
                                        }
                                    }

                                }

                            }
                        }
                        if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                        { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                        else
                        {
                            if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                            { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                            else
                            {
                                if (double.Parse(txtAsistentes.Text) > 50)
                                { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                else
                                {
                                    if (double.Parse(txtAsistentes.Text) == 0)
                                    {
                                        valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                    }
                                }
                            }
                        }


                        valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                        c.ValorBase = valorBase;
                        c.ValorTotalContrato = valorTotal;
                    }
                    else
                    {
                        if (cboxModalidad.SelectedIndex == 1)
                        {
                            valorBase = 8 * uf; personalBase = 6;

                            if (int.Parse(txtPersonal.Text) == 2)
                            {
                                valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2;

                            }
                            else
                            {
                                if (double.Parse(txtPersonal.Text) == 3)
                                { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                else
                                {
                                    if (double.Parse(txtPersonal.Text) == 4)
                                    {
                                        valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                    }
                                    else
                                    {
                                        if (double.Parse(txtPersonal.Text) > 4)
                                        { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                        else
                                        {
                                            if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                            {
                                                valorPersonalAdicional = 0; personalTotal = personalBase;
                                            }
                                        }

                                    }

                                }


                            }
                            if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                            { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                            else
                            {
                                if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                else
                                {
                                    if (double.Parse(txtAsistentes.Text) > 50)
                                    { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                    else
                                    {
                                        if (double.Parse(txtAsistentes.Text) == 0)
                                        {
                                            valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                        }
                                    }
                                }
                            }

                            valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                            c.ValorBase = valorBase;
                            c.ValorTotalContrato = valorTotal;
                        }

                        else
                        {
                            if (cboxModalidad.SelectedIndex == 2)
                            {
                                valorBase = 12 * uf; personalBase = 6;

                                if (int.Parse(txtPersonal.Text) == 2)
                                { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                else
                                {
                                    if (double.Parse(txtPersonal.Text) == 3)
                                    { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                    else
                                    {
                                        if (double.Parse(txtPersonal.Text) == 4)
                                        {
                                            valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                        }
                                        else
                                        {
                                            if (double.Parse(txtPersonal.Text) > 4)
                                            { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                {
                                                    valorPersonalAdicional = 0; personalTotal = personalBase;
                                                }
                                            }

                                        }

                                    }
                                }

                                if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                else
                                {
                                    if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                    { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                    else
                                    {
                                        if (double.Parse(txtAsistentes.Text) > 50)
                                        { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                        else
                                        {
                                            if (double.Parse(txtAsistentes.Text) == 0)
                                            {
                                                valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                            }
                                        }
                                    }
                                }

                                valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                c.ValorBase = valorBase;
                                c.ValorTotalContrato = valorTotal;

                            }
                            else
                            {
                                if (cboxModalidad.SelectedIndex == 3)
                                {
                                    valorBase = 25 * uf; personalBase = 10;

                                    if (int.Parse(txtPersonal.Text) == 2)
                                    { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                    else
                                    {
                                        if (double.Parse(txtPersonal.Text) == 3)
                                        { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                        else
                                        {
                                            if (double.Parse(txtPersonal.Text) == 4)
                                            {
                                                valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                            }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) > 4)
                                                { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                    {
                                                        valorPersonalAdicional = 0; personalTotal = personalBase;
                                                    }
                                                }

                                            }

                                        }
                                    }

                                    if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                    { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                    else
                                    {
                                        if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                        { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                        else
                                        {
                                            if (double.Parse(txtAsistentes.Text) > 50)
                                            { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) == 0)
                                                {
                                                    valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                }
                                            }
                                        }
                                    }

                                    valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                    c.ValorBase = valorBase;
                                    c.ValorTotalContrato = valorTotal;

                                }
                                else
                                {
                                    if (cboxModalidad.SelectedIndex == 4)
                                    {
                                        valorBase = 35 * uf; personalBase = 14;

                                        if (int.Parse(txtPersonal.Text) == 2)
                                        { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                        else
                                        {
                                            if (double.Parse(txtPersonal.Text) == 3)
                                            { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) == 4)
                                                {
                                                    valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) > 4)
                                                    { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                        {
                                                            valorPersonalAdicional = 0; personalTotal = personalBase;
                                                        }
                                                    }

                                                }

                                            }
                                        }

                                        if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                        { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                        else
                                        {
                                            if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                            { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) > 50)
                                                { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) == 0)
                                                    {
                                                        valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                    }
                                                }
                                            }
                                        }

                                        valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                        c.ValorBase = valorBase;
                                        c.ValorTotalContrato = valorTotal;

                                    }
                                    else
                                    {
                                        if (cboxModalidad.SelectedIndex == 5)
                                        {
                                            valorBase = 6 * uf; personalBase = 4;

                                            if (int.Parse(txtPersonal.Text) == 2)
                                            { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) == 3)
                                                { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 4)
                                                    {
                                                        valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                    }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) > 4)
                                                        { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                            {
                                                                valorPersonalAdicional = 0; personalTotal = personalBase;
                                                            }
                                                        }

                                                    }

                                                }
                                            }

                                            if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                            { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) > 50)
                                                    { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) == 0)
                                                        {
                                                            valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                        }
                                                    }
                                                }
                                            }

                                            valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                            c.ValorBase = valorBase;
                                            c.ValorTotalContrato = valorTotal;

                                        }
                                        else
                                        {
                                            if (cboxModalidad.SelectedIndex == 6)
                                            {
                                                valorBase = 10 * uf; personalBase = 5;

                                                if (int.Parse(txtPersonal.Text) == 2)
                                                { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 3)
                                                    { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 4)
                                                        {
                                                            valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                        }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) > 4)
                                                            { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                {
                                                                    valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                }
                                                            }

                                                        }

                                                    }
                                                }

                                                if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                    { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) > 50)
                                                        { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) == 0)
                                                            {
                                                                valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                            }
                                                        }
                                                    }
                                                }

                                                valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                c.ValorBase = valorBase;
                                                c.ValorTotalContrato = valorTotal;

                                            }
                                        }
                                    }
                                }

                            }

                        }
                    }
                }
                else
                {
                    #endregion Calculo para evento tipo Matrimonio

                    #region Calculo Tipo de Evento Cumpleaños Adulto
                    //Calculo Tipo de Evento Cumpleaños Adulto, con la 7 modalidades de servicio
                    if (cboxTipoEvento.SelectedIndex == 1)
                    {
                        if (cboxModalidad.SelectedIndex == 0)
                        {
                            valorBase = 3 * uf; personalBase = 2;

                            if (int.Parse(txtPersonal.Text) == 2)
                            { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                            else
                            {
                                if (double.Parse(txtPersonal.Text) == 3)
                                { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                else
                                {
                                    if (double.Parse(txtPersonal.Text) == 4)
                                    {
                                        valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                    }
                                    else
                                    {
                                        if (double.Parse(txtPersonal.Text) > 4)
                                        { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                        else
                                        {
                                            if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                            {
                                                valorPersonalAdicional = 0; personalTotal = personalBase;
                                            }
                                        }

                                    }

                                }
                            }
                            if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                            { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                            else
                            {
                                if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                else
                                {
                                    if (double.Parse(txtAsistentes.Text) > 50)
                                    { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                    else
                                    {
                                        if (double.Parse(txtAsistentes.Text) == 0)
                                        {
                                            valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                        }
                                    }
                                }
                            }


                            valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                            c.ValorBase = valorBase;
                            c.ValorTotalContrato = valorTotal;
                        }
                        else
                        {
                            if (cboxModalidad.SelectedIndex == 1)
                            {
                                valorBase = 8 * uf; personalBase = 6;

                                if (int.Parse(txtPersonal.Text) == 2)
                                {
                                    valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2;

                                }
                                else
                                {
                                    if (double.Parse(txtPersonal.Text) == 3)
                                    { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                    else
                                    {
                                        if (double.Parse(txtPersonal.Text) == 4)
                                        {
                                            valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                        }
                                        else
                                        {
                                            if (double.Parse(txtPersonal.Text) > 4)
                                            { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                {
                                                    valorPersonalAdicional = 0; personalTotal = personalBase;
                                                }
                                            }

                                        }

                                    }


                                }
                                if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                else
                                {
                                    if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                    { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                    else
                                    {
                                        if (double.Parse(txtAsistentes.Text) > 50)
                                        { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                        else
                                        {
                                            if (double.Parse(txtAsistentes.Text) == 0)
                                            {
                                                valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                            }
                                        }
                                    }
                                }

                                valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                c.ValorBase = valorBase;
                                c.ValorTotalContrato = valorTotal;
                            }

                            else
                            {
                                if (cboxModalidad.SelectedIndex == 2)
                                {
                                    valorBase = 12 * uf; personalBase = 6;

                                    if (int.Parse(txtPersonal.Text) == 2)
                                    { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                    else
                                    {
                                        if (double.Parse(txtPersonal.Text) == 3)
                                        { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                        else
                                        {
                                            if (double.Parse(txtPersonal.Text) == 4)
                                            {
                                                valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                            }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) > 4)
                                                { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                    {
                                                        valorPersonalAdicional = 0; personalTotal = personalBase;
                                                    }
                                                }

                                            }

                                        }
                                    }

                                    if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                    { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                    else
                                    {
                                        if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                        { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                        else
                                        {
                                            if (double.Parse(txtAsistentes.Text) > 50)
                                            { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) == 0)
                                                {
                                                    valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                }
                                            }
                                        }
                                    }

                                    valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                    c.ValorBase = valorBase;
                                    c.ValorTotalContrato = valorTotal;

                                }
                                else
                                {
                                    if (cboxModalidad.SelectedIndex == 3)
                                    {
                                        valorBase = 25 * uf; personalBase = 10;

                                        if (int.Parse(txtPersonal.Text) == 2)
                                        { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                        else
                                        {
                                            if (double.Parse(txtPersonal.Text) == 3)
                                            { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) == 4)
                                                {
                                                    valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) > 4)
                                                    { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                        {
                                                            valorPersonalAdicional = 0; personalTotal = personalBase;
                                                        }
                                                    }

                                                }

                                            }
                                        }

                                        if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                        { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                        else
                                        {
                                            if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                            { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) > 50)
                                                { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) == 0)
                                                    {
                                                        valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                    }
                                                }
                                            }
                                        }

                                        valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                        c.ValorBase = valorBase;
                                        c.ValorTotalContrato = valorTotal;

                                    }
                                    else
                                    {
                                        if (cboxModalidad.SelectedIndex == 4)
                                        {
                                            valorBase = 35 * uf; personalBase = 14;

                                            if (int.Parse(txtPersonal.Text) == 2)
                                            { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) == 3)
                                                { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 4)
                                                    {
                                                        valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                    }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) > 4)
                                                        { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                            {
                                                                valorPersonalAdicional = 0; personalTotal = personalBase;
                                                            }
                                                        }

                                                    }

                                                }
                                            }

                                            if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                            { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) > 50)
                                                    { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) == 0)
                                                        {
                                                            valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                        }
                                                    }
                                                }
                                            }

                                            valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                            c.ValorBase = valorBase;
                                            c.ValorTotalContrato = valorTotal;

                                        }
                                        else
                                        {
                                            if (cboxModalidad.SelectedIndex == 5)
                                            {
                                                valorBase = 6 * uf; personalBase = 4;

                                                if (int.Parse(txtPersonal.Text) == 2)
                                                { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 3)
                                                    { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 4)
                                                        {
                                                            valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                        }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) > 4)
                                                            { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                {
                                                                    valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                }
                                                            }

                                                        }

                                                    }
                                                }

                                                if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                    { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) > 50)
                                                        { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) == 0)
                                                            {
                                                                valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                            }
                                                        }
                                                    }
                                                }

                                                valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                c.ValorBase = valorBase;
                                                c.ValorTotalContrato = valorTotal;

                                            }
                                            else
                                            {
                                                if (cboxModalidad.SelectedIndex == 6)
                                                {
                                                    valorBase = 10 * uf; personalBase = 5;

                                                    if (int.Parse(txtPersonal.Text) == 2)
                                                    { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 3)
                                                        { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 4)
                                                            {
                                                                valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                            }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) > 4)
                                                                { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                    {
                                                                        valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                    }
                                                                }

                                                            }

                                                        }
                                                    }

                                                    if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                    { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                        { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) > 50)
                                                            { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) == 0)
                                                                {
                                                                    valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                    c.ValorBase = valorBase;
                                                    c.ValorTotalContrato = valorTotal;

                                                }
                                            }
                                        }
                                    }

                                }

                            }
                        }
                    }
                    else
                    {
                        #endregion Calculo Tipo de Evento Cumpleaños Adulto

                        #region Calculo tipo de evento Cumpleaños Infantil
                        // Calculo tipo de evento Cumpleaños Infantil, con la 7 modalidades de servicio
                        if (cboxTipoEvento.SelectedIndex == 2)
                        {
                            if (cboxModalidad.SelectedIndex == 0)
                            {
                                valorBase = 3 * uf; personalBase = 2;

                                if (int.Parse(txtPersonal.Text) == 2)
                                { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                else
                                {
                                    if (double.Parse(txtPersonal.Text) == 3)
                                    { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                    else
                                    {
                                        if (double.Parse(txtPersonal.Text) == 4)
                                        {
                                            valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                        }
                                        else
                                        {
                                            if (double.Parse(txtPersonal.Text) > 4)
                                            { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                {
                                                    valorPersonalAdicional = 0; personalTotal = personalBase;
                                                }
                                            }

                                        }

                                    }
                                }
                                if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                else
                                {
                                    if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                    { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                    else
                                    {
                                        if (double.Parse(txtAsistentes.Text) > 50)
                                        { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                        else
                                        {
                                            if (double.Parse(txtAsistentes.Text) == 0)
                                            {
                                                valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                            }
                                        }
                                    }
                                }


                                valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                c.ValorBase = valorBase;
                                c.ValorTotalContrato = valorTotal;
                            }
                            else
                            {
                                if (cboxModalidad.SelectedIndex == 1)
                                {
                                    valorBase = 8 * uf; personalBase = 6;

                                    if (int.Parse(txtPersonal.Text) == 2)
                                    {
                                        valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2;

                                    }
                                    else
                                    {
                                        if (double.Parse(txtPersonal.Text) == 3)
                                        { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                        else
                                        {
                                            if (double.Parse(txtPersonal.Text) == 4)
                                            {
                                                valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                            }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) > 4)
                                                { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                    {
                                                        valorPersonalAdicional = 0; personalTotal = personalBase;
                                                    }
                                                }

                                            }

                                        }


                                    }
                                    if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                    { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                    else
                                    {
                                        if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                        { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                        else
                                        {
                                            if (double.Parse(txtAsistentes.Text) > 50)
                                            { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) == 0)
                                                {
                                                    valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                }
                                            }
                                        }
                                    }

                                    valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                    c.ValorBase = valorBase;
                                    c.ValorTotalContrato = valorTotal;
                                }

                                else
                                {
                                    if (cboxModalidad.SelectedIndex == 2)
                                    {
                                        valorBase = 12 * uf; personalBase = 6;

                                        if (int.Parse(txtPersonal.Text) == 2)
                                        { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                        else
                                        {
                                            if (double.Parse(txtPersonal.Text) == 3)
                                            { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) == 4)
                                                {
                                                    valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) > 4)
                                                    { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                        {
                                                            valorPersonalAdicional = 0; personalTotal = personalBase;
                                                        }
                                                    }

                                                }

                                            }
                                        }

                                        if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                        { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                        else
                                        {
                                            if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                            { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) > 50)
                                                { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) == 0)
                                                    {
                                                        valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                    }
                                                }
                                            }
                                        }

                                        valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                        c.ValorBase = valorBase;
                                        c.ValorTotalContrato = valorTotal;

                                    }
                                    else
                                    {
                                        if (cboxModalidad.SelectedIndex == 3)
                                        {
                                            valorBase = 25 * uf; personalBase = 10;

                                            if (int.Parse(txtPersonal.Text) == 2)
                                            { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) == 3)
                                                { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 4)
                                                    {
                                                        valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                    }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) > 4)
                                                        { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                            {
                                                                valorPersonalAdicional = 0; personalTotal = personalBase;
                                                            }
                                                        }

                                                    }

                                                }
                                            }

                                            if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                            { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) > 50)
                                                    { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) == 0)
                                                        {
                                                            valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                        }
                                                    }
                                                }
                                            }

                                            valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                            c.ValorBase = valorBase;
                                            c.ValorTotalContrato = valorTotal;

                                        }
                                        else
                                        {
                                            if (cboxModalidad.SelectedIndex == 4)
                                            {
                                                valorBase = 35 * uf; personalBase = 14;

                                                if (int.Parse(txtPersonal.Text) == 2)
                                                { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 3)
                                                    { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 4)
                                                        {
                                                            valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                        }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) > 4)
                                                            { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                {
                                                                    valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                }
                                                            }

                                                        }

                                                    }
                                                }

                                                if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                    { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) > 50)
                                                        { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) == 0)
                                                            {
                                                                valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                            }
                                                        }
                                                    }
                                                }

                                                valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                c.ValorBase = valorBase;
                                                c.ValorTotalContrato = valorTotal;

                                            }
                                            else
                                            {
                                                if (cboxModalidad.SelectedIndex == 5)
                                                {
                                                    valorBase = 6 * uf; personalBase = 4;

                                                    if (int.Parse(txtPersonal.Text) == 2)
                                                    { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 3)
                                                        { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 4)
                                                            {
                                                                valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                            }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) > 4)
                                                                { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                    {
                                                                        valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                    }
                                                                }

                                                            }

                                                        }
                                                    }

                                                    if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                    { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                        { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) > 50)
                                                            { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) == 0)
                                                                {
                                                                    valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                    c.ValorBase = valorBase;
                                                    c.ValorTotalContrato = valorTotal;

                                                }
                                                else
                                                {
                                                    if (cboxModalidad.SelectedIndex == 6)
                                                    {
                                                        valorBase = 10 * uf; personalBase = 5;

                                                        if (int.Parse(txtPersonal.Text) == 2)
                                                        { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 3)
                                                            { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 4)
                                                                {
                                                                    valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) > 4)
                                                                    { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                        {
                                                                            valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                        }
                                                                    }

                                                                }

                                                            }
                                                        }

                                                        if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                        { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                            { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) > 50)
                                                                { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtAsistentes.Text) == 0)
                                                                    {
                                                                        valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                        c.ValorBase = valorBase;
                                                        c.ValorTotalContrato = valorTotal;

                                                    }
                                                }
                                            }
                                        }

                                    }

                                }
                            }
                        }
                        else
                        {
                            #endregion Calculo tipo de evento Cumpleaños Infantil

                            #region Calculo tipo de evento Evento Empresarial

                            // Calculo tipo de evento Evento Empresarial, con la 7 modalidades de servicio
                            if (cboxTipoEvento.SelectedIndex == 3)
                            {
                                if (cboxModalidad.SelectedIndex == 0)
                                {
                                    valorBase = 3 * uf; personalBase = 2;

                                    if (int.Parse(txtPersonal.Text) == 2)
                                    { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                    else
                                    {
                                        if (double.Parse(txtPersonal.Text) == 3)
                                        { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                        else
                                        {
                                            if (double.Parse(txtPersonal.Text) == 4)
                                            {
                                                valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                            }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) > 4)
                                                { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                    {
                                                        valorPersonalAdicional = 0; personalTotal = personalBase;
                                                    }
                                                }

                                            }

                                        }
                                    }
                                    if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                    { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                    else
                                    {
                                        if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                        { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                        else
                                        {
                                            if (double.Parse(txtAsistentes.Text) > 50)
                                            { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) == 0)
                                                {
                                                    valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                }
                                            }
                                        }
                                    }


                                    valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                    c.ValorBase = valorBase;
                                    c.ValorTotalContrato = valorTotal;
                                }
                                else
                                {
                                    if (cboxModalidad.SelectedIndex == 1)
                                    {
                                        valorBase = 8 * uf; personalBase = 6;

                                        if (int.Parse(txtPersonal.Text) == 2)
                                        {
                                            valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2;

                                        }
                                        else
                                        {
                                            if (double.Parse(txtPersonal.Text) == 3)
                                            { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) == 4)
                                                {
                                                    valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) > 4)
                                                    { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                        {
                                                            valorPersonalAdicional = 0; personalTotal = personalBase;
                                                        }
                                                    }

                                                }

                                            }


                                        }
                                        if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                        { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                        else
                                        {
                                            if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                            { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) > 50)
                                                { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) == 0)
                                                    {
                                                        valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                    }
                                                }
                                            }
                                        }

                                        valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                        c.ValorBase = valorBase;
                                        c.ValorTotalContrato = valorTotal;
                                    }

                                    else
                                    {
                                        if (cboxModalidad.SelectedIndex == 2)
                                        {
                                            valorBase = 12 * uf; personalBase = 6;

                                            if (int.Parse(txtPersonal.Text) == 2)
                                            { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) == 3)
                                                { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 4)
                                                    {
                                                        valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                    }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) > 4)
                                                        { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                            {
                                                                valorPersonalAdicional = 0; personalTotal = personalBase;
                                                            }
                                                        }

                                                    }

                                                }
                                            }

                                            if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                            { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) > 50)
                                                    { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) == 0)
                                                        {
                                                            valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                        }
                                                    }
                                                }
                                            }

                                            valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                            c.ValorBase = valorBase;
                                            c.ValorTotalContrato = valorTotal;

                                        }
                                        else
                                        {
                                            if (cboxModalidad.SelectedIndex == 3)
                                            {
                                                valorBase = 25 * uf; personalBase = 10;

                                                if (int.Parse(txtPersonal.Text) == 2)
                                                { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 3)
                                                    { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 4)
                                                        {
                                                            valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                        }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) > 4)
                                                            { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                {
                                                                    valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                }
                                                            }

                                                        }

                                                    }
                                                }

                                                if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                    { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) > 50)
                                                        { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) == 0)
                                                            {
                                                                valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                            }
                                                        }
                                                    }
                                                }

                                                valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                c.ValorBase = valorBase;
                                                c.ValorTotalContrato = valorTotal;

                                            }
                                            else
                                            {
                                                if (cboxModalidad.SelectedIndex == 4)
                                                {
                                                    valorBase = 35 * uf; personalBase = 14;

                                                    if (int.Parse(txtPersonal.Text) == 2)
                                                    { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 3)
                                                        { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 4)
                                                            {
                                                                valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                            }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) > 4)
                                                                { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                    {
                                                                        valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                    }
                                                                }

                                                            }

                                                        }
                                                    }

                                                    if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                    { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                        { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) > 50)
                                                            { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) == 0)
                                                                {
                                                                    valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                    c.ValorBase = valorBase;
                                                    c.ValorTotalContrato = valorTotal;

                                                }
                                                else
                                                {
                                                    if (cboxModalidad.SelectedIndex == 5)
                                                    {
                                                        valorBase = 6 * uf; personalBase = 4;

                                                        if (int.Parse(txtPersonal.Text) == 2)
                                                        { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 3)
                                                            { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 4)
                                                                {
                                                                    valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) > 4)
                                                                    { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                        {
                                                                            valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                        }
                                                                    }

                                                                }

                                                            }
                                                        }

                                                        if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                        { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                            { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) > 50)
                                                                { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtAsistentes.Text) == 0)
                                                                    {
                                                                        valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                        c.ValorBase = valorBase;
                                                        c.ValorTotalContrato = valorTotal;

                                                    }
                                                    else
                                                    {
                                                        if (cboxModalidad.SelectedIndex == 6)
                                                        {
                                                            valorBase = 10 * uf; personalBase = 5;

                                                            if (int.Parse(txtPersonal.Text) == 2)
                                                            { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 3)
                                                                { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) == 4)
                                                                    {
                                                                        valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtPersonal.Text) > 4)
                                                                        { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                            {
                                                                                valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                            }
                                                                        }

                                                                    }

                                                                }
                                                            }

                                                            if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                            { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                                { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtAsistentes.Text) > 50)
                                                                    { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtAsistentes.Text) == 0)
                                                                        {
                                                                            valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                            valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                            c.ValorBase = valorBase;
                                                            c.ValorTotalContrato = valorTotal;

                                                        }
                                                    }
                                                }
                                            }

                                        }

                                    }
                                }
                            }
                            else
                            {
                                #endregion Calculo tipo de evento Evento Empresarial

                                #region Calculo tipo de evento Desdepida de Soltero
                                //Calculo tipo de evento Desdepida de Soltero, con la 7 modalidades de servicio
                                if (cboxTipoEvento.SelectedIndex == 4)
                                {
                                    if (cboxModalidad.SelectedIndex == 0)
                                    {
                                        valorBase = 3 * uf; personalBase = 2;

                                        if (int.Parse(txtPersonal.Text) == 2)
                                        { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                        else
                                        {
                                            if (double.Parse(txtPersonal.Text) == 3)
                                            { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) == 4)
                                                {
                                                    valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) > 4)
                                                    { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                        {
                                                            valorPersonalAdicional = 0; personalTotal = personalBase;
                                                        }
                                                    }

                                                }

                                            }
                                        }
                                        if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                        { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                        else
                                        {
                                            if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                            { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) > 50)
                                                { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) == 0)
                                                    {
                                                        valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                    }
                                                }
                                            }
                                        }


                                        valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                        c.ValorBase = valorBase;
                                        c.ValorTotalContrato = valorTotal;
                                    }
                                    else
                                    {
                                        if (cboxModalidad.SelectedIndex == 1)
                                        {
                                            valorBase = 8 * uf; personalBase = 6;

                                            if (int.Parse(txtPersonal.Text) == 2)
                                            {
                                                valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2;

                                            }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) == 3)
                                                { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 4)
                                                    {
                                                        valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                    }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) > 4)
                                                        { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                            {
                                                                valorPersonalAdicional = 0; personalTotal = personalBase;
                                                            }
                                                        }

                                                    }

                                                }


                                            }
                                            if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                            { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) > 50)
                                                    { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) == 0)
                                                        {
                                                            valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                        }
                                                    }
                                                }
                                            }

                                            valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                            c.ValorBase = valorBase;
                                            c.ValorTotalContrato = valorTotal;
                                        }

                                        else
                                        {
                                            if (cboxModalidad.SelectedIndex == 2)
                                            {
                                                valorBase = 12 * uf; personalBase = 6;

                                                if (int.Parse(txtPersonal.Text) == 2)
                                                { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 3)
                                                    { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 4)
                                                        {
                                                            valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                        }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) > 4)
                                                            { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                {
                                                                    valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                }
                                                            }

                                                        }

                                                    }
                                                }

                                                if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                    { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) > 50)
                                                        { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) == 0)
                                                            {
                                                                valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                            }
                                                        }
                                                    }
                                                }

                                                valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                c.ValorBase = valorBase;
                                                c.ValorTotalContrato = valorTotal;

                                            }
                                            else
                                            {
                                                if (cboxModalidad.SelectedIndex == 3)
                                                {
                                                    valorBase = 25 * uf; personalBase = 10;

                                                    if (int.Parse(txtPersonal.Text) == 2)
                                                    { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 3)
                                                        { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 4)
                                                            {
                                                                valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                            }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) > 4)
                                                                { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                    {
                                                                        valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                    }
                                                                }

                                                            }

                                                        }
                                                    }

                                                    if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                    { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                        { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) > 50)
                                                            { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) == 0)
                                                                {
                                                                    valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                    c.ValorBase = valorBase;
                                                    c.ValorTotalContrato = valorTotal;

                                                }
                                                else
                                                {
                                                    if (cboxModalidad.SelectedIndex == 4)
                                                    {
                                                        valorBase = 35 * uf; personalBase = 14;

                                                        if (int.Parse(txtPersonal.Text) == 2)
                                                        { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 3)
                                                            { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 4)
                                                                {
                                                                    valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) > 4)
                                                                    { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                        {
                                                                            valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                        }
                                                                    }

                                                                }

                                                            }
                                                        }

                                                        if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                        { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                            { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) > 50)
                                                                { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtAsistentes.Text) == 0)
                                                                    {
                                                                        valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                        c.ValorBase = valorBase;
                                                        c.ValorTotalContrato = valorTotal;

                                                    }
                                                    else
                                                    {
                                                        if (cboxModalidad.SelectedIndex == 5)
                                                        {
                                                            valorBase = 6 * uf; personalBase = 4;

                                                            if (int.Parse(txtPersonal.Text) == 2)
                                                            { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 3)
                                                                { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) == 4)
                                                                    {
                                                                        valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtPersonal.Text) > 4)
                                                                        { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                            {
                                                                                valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                            }
                                                                        }

                                                                    }

                                                                }
                                                            }

                                                            if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                            { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                                { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtAsistentes.Text) > 50)
                                                                    { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtAsistentes.Text) == 0)
                                                                        {
                                                                            valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                            valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                            c.ValorBase = valorBase;
                                                            c.ValorTotalContrato = valorTotal;

                                                        }
                                                        else
                                                        {
                                                            if (cboxModalidad.SelectedIndex == 6)
                                                            {
                                                                valorBase = 10 * uf; personalBase = 5;

                                                                if (int.Parse(txtPersonal.Text) == 2)
                                                                { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) == 3)
                                                                    { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtPersonal.Text) == 4)
                                                                        {
                                                                            valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                        }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtPersonal.Text) > 4)
                                                                            { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                            else
                                                                            {
                                                                                if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                                {
                                                                                    valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                                }
                                                                            }

                                                                        }

                                                                    }
                                                                }

                                                                if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                                { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                                    { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtAsistentes.Text) > 50)
                                                                        { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtAsistentes.Text) == 0)
                                                                            {
                                                                                valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                                valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                                c.ValorBase = valorBase;
                                                                c.ValorTotalContrato = valorTotal;

                                                            }
                                                        }
                                                    }
                                                }

                                            }

                                        }
                                    }
                                }
                                else
                                {
                                    #endregion Calculo tipo de evento Desdepida de Soltero

                                    #region Calculo tipo de evento Evento Religioso
                                    //Calculo tipo de evento Evento Religioso, con la 7 modalidades de servicio
                                    if (cboxTipoEvento.SelectedIndex == 5)
                                    {
                                        if (cboxModalidad.SelectedIndex == 0)
                                        {
                                            valorBase = 3 * uf; personalBase = 2;

                                            if (int.Parse(txtPersonal.Text) == 2)
                                            { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) == 3)
                                                { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 4)
                                                    {
                                                        valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                    }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) > 4)
                                                        { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                            {
                                                                valorPersonalAdicional = 0; personalTotal = personalBase;
                                                            }
                                                        }

                                                    }

                                                }
                                            }
                                            if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                            { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) > 50)
                                                    { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) == 0)
                                                        {
                                                            valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                        }
                                                    }
                                                }
                                            }


                                            valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                            c.ValorBase = valorBase;
                                            c.ValorTotalContrato = valorTotal;
                                        }
                                        else
                                        {
                                            if (cboxModalidad.SelectedIndex == 1)
                                            {
                                                valorBase = 8 * uf; personalBase = 6;

                                                if (int.Parse(txtPersonal.Text) == 2)
                                                {
                                                    valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2;

                                                }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 3)
                                                    { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 4)
                                                        {
                                                            valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                        }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) > 4)
                                                            { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                {
                                                                    valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                }
                                                            }

                                                        }

                                                    }


                                                }
                                                if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                    { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) > 50)
                                                        { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) == 0)
                                                            {
                                                                valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                            }
                                                        }
                                                    }
                                                }

                                                valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                c.ValorBase = valorBase;
                                                c.ValorTotalContrato = valorTotal;
                                            }

                                            else
                                            {
                                                if (cboxModalidad.SelectedIndex == 2)
                                                {
                                                    valorBase = 12 * uf; personalBase = 6;

                                                    if (int.Parse(txtPersonal.Text) == 2)
                                                    { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 3)
                                                        { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 4)
                                                            {
                                                                valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                            }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) > 4)
                                                                { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                    {
                                                                        valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                    }
                                                                }

                                                            }

                                                        }
                                                    }

                                                    if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                    { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                        { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) > 50)
                                                            { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) == 0)
                                                                {
                                                                    valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                    c.ValorBase = valorBase;
                                                    c.ValorTotalContrato = valorTotal;

                                                }
                                                else
                                                {
                                                    if (cboxModalidad.SelectedIndex == 3)
                                                    {
                                                        valorBase = 25 * uf; personalBase = 10;

                                                        if (int.Parse(txtPersonal.Text) == 2)
                                                        { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 3)
                                                            { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 4)
                                                                {
                                                                    valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) > 4)
                                                                    { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                        {
                                                                            valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                        }
                                                                    }

                                                                }

                                                            }
                                                        }

                                                        if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                        { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                            { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) > 50)
                                                                { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtAsistentes.Text) == 0)
                                                                    {
                                                                        valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                        c.ValorBase = valorBase;
                                                        c.ValorTotalContrato = valorTotal;

                                                    }
                                                    else
                                                    {
                                                        if (cboxModalidad.SelectedIndex == 4)
                                                        {
                                                            valorBase = 35 * uf; personalBase = 14;

                                                            if (int.Parse(txtPersonal.Text) == 2)
                                                            { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 3)
                                                                { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) == 4)
                                                                    {
                                                                        valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtPersonal.Text) > 4)
                                                                        { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                            {
                                                                                valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                            }
                                                                        }

                                                                    }

                                                                }
                                                            }

                                                            if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                            { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                                { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtAsistentes.Text) > 50)
                                                                    { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtAsistentes.Text) == 0)
                                                                        {
                                                                            valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                            valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                            c.ValorBase = valorBase;
                                                            c.ValorTotalContrato = valorTotal;

                                                        }
                                                        else
                                                        {
                                                            if (cboxModalidad.SelectedIndex == 5)
                                                            {
                                                                valorBase = 6 * uf; personalBase = 4;

                                                                if (int.Parse(txtPersonal.Text) == 2)
                                                                { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) == 3)
                                                                    { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtPersonal.Text) == 4)
                                                                        {
                                                                            valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                        }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtPersonal.Text) > 4)
                                                                            { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                            else
                                                                            {
                                                                                if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                                {
                                                                                    valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                                }
                                                                            }

                                                                        }

                                                                    }
                                                                }

                                                                if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                                { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                                    { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtAsistentes.Text) > 50)
                                                                        { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtAsistentes.Text) == 0)
                                                                            {
                                                                                valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                                valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                                c.ValorBase = valorBase;
                                                                c.ValorTotalContrato = valorTotal;

                                                            }
                                                            else
                                                            {
                                                                if (cboxModalidad.SelectedIndex == 6)
                                                                {
                                                                    valorBase = 10 * uf; personalBase = 5;

                                                                    if (int.Parse(txtPersonal.Text) == 2)
                                                                    { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtPersonal.Text) == 3)
                                                                        { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtPersonal.Text) == 4)
                                                                            {
                                                                                valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                            }
                                                                            else
                                                                            {
                                                                                if (double.Parse(txtPersonal.Text) > 4)
                                                                                { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                                else
                                                                                {
                                                                                    if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                                    {
                                                                                        valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                                    }
                                                                                }

                                                                            }

                                                                        }
                                                                    }

                                                                    if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                                    { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                                        { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtAsistentes.Text) > 50)
                                                                            { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                            else
                                                                            {
                                                                                if (double.Parse(txtAsistentes.Text) == 0)
                                                                                {
                                                                                    valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                                }
                                                                            }
                                                                        }
                                                                    }

                                                                    valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                                    c.ValorBase = valorBase;
                                                                    c.ValorTotalContrato = valorTotal;

                                                                }
                                                            }
                                                        }
                                                    }

                                                }

                                            }
                                        }
                                    }
                                    else
                                    {
                                        #endregion Calculo tipo de evento Evento Religioso

                                        #region Calculo tipo de evento Baby Shower
                                        //Calculo tipo de evento Baby Shower, con la 7 modalidades de servicio
                                        if (cboxTipoEvento.SelectedIndex == 6)
                                        {
                                            if (cboxModalidad.SelectedIndex == 0)
                                            {
                                                valorBase = 3 * uf; personalBase = 2;

                                                if (int.Parse(txtPersonal.Text) == 2)
                                                { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 3)
                                                    { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 4)
                                                        {
                                                            valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                        }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) > 4)
                                                            { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                {
                                                                    valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                }
                                                            }

                                                        }

                                                    }
                                                }
                                                if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                    { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) > 50)
                                                        { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) == 0)
                                                            {
                                                                valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                            }
                                                        }
                                                    }
                                                }


                                                valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                c.ValorBase = valorBase;
                                                c.ValorTotalContrato = valorTotal;
                                            }
                                            else
                                            {
                                                if (cboxModalidad.SelectedIndex == 1)
                                                {
                                                    valorBase = 8 * uf; personalBase = 6;

                                                    if (int.Parse(txtPersonal.Text) == 2)
                                                    {
                                                        valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2;

                                                    }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 3)
                                                        { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 4)
                                                            {
                                                                valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                            }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) > 4)
                                                                { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                    {
                                                                        valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                    }
                                                                }

                                                            }

                                                        }


                                                    }
                                                    if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                    { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                        { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) > 50)
                                                            { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) == 0)
                                                                {
                                                                    valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                    c.ValorBase = valorBase;
                                                    c.ValorTotalContrato = valorTotal;
                                                }

                                                else
                                                {
                                                    if (cboxModalidad.SelectedIndex == 2)
                                                    {
                                                        valorBase = 12 * uf; personalBase = 6;

                                                        if (int.Parse(txtPersonal.Text) == 2)
                                                        { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 3)
                                                            { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 4)
                                                                {
                                                                    valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) > 4)
                                                                    { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                        {
                                                                            valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                        }
                                                                    }

                                                                }

                                                            }
                                                        }

                                                        if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                        { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                            { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) > 50)
                                                                { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtAsistentes.Text) == 0)
                                                                    {
                                                                        valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                        c.ValorBase = valorBase;
                                                        c.ValorTotalContrato = valorTotal;

                                                    }
                                                    else
                                                    {
                                                        if (cboxModalidad.SelectedIndex == 3)
                                                        {
                                                            valorBase = 25 * uf; personalBase = 10;

                                                            if (int.Parse(txtPersonal.Text) == 2)
                                                            { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 3)
                                                                { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) == 4)
                                                                    {
                                                                        valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtPersonal.Text) > 4)
                                                                        { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                            {
                                                                                valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                            }
                                                                        }

                                                                    }

                                                                }
                                                            }

                                                            if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                            { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                                { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtAsistentes.Text) > 50)
                                                                    { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtAsistentes.Text) == 0)
                                                                        {
                                                                            valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                            valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                            c.ValorBase = valorBase;
                                                            c.ValorTotalContrato = valorTotal;

                                                        }
                                                        else
                                                        {
                                                            if (cboxModalidad.SelectedIndex == 4)
                                                            {
                                                                valorBase = 35 * uf; personalBase = 14;

                                                                if (int.Parse(txtPersonal.Text) == 2)
                                                                { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) == 3)
                                                                    { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtPersonal.Text) == 4)
                                                                        {
                                                                            valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                        }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtPersonal.Text) > 4)
                                                                            { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                            else
                                                                            {
                                                                                if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                                {
                                                                                    valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                                }
                                                                            }

                                                                        }

                                                                    }
                                                                }

                                                                if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                                { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                                    { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtAsistentes.Text) > 50)
                                                                        { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtAsistentes.Text) == 0)
                                                                            {
                                                                                valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                                valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                                c.ValorBase = valorBase;
                                                                c.ValorTotalContrato = valorTotal;

                                                            }
                                                            else
                                                            {
                                                                if (cboxModalidad.SelectedIndex == 5)
                                                                {
                                                                    valorBase = 6 * uf; personalBase = 4;

                                                                    if (int.Parse(txtPersonal.Text) == 2)
                                                                    { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtPersonal.Text) == 3)
                                                                        { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtPersonal.Text) == 4)
                                                                            {
                                                                                valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                            }
                                                                            else
                                                                            {
                                                                                if (double.Parse(txtPersonal.Text) > 4)
                                                                                { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                                else
                                                                                {
                                                                                    if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                                    {
                                                                                        valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                                    }
                                                                                }

                                                                            }

                                                                        }
                                                                    }

                                                                    if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                                    { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                                        { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtAsistentes.Text) > 50)
                                                                            { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                            else
                                                                            {
                                                                                if (double.Parse(txtAsistentes.Text) == 0)
                                                                                {
                                                                                    valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                                }
                                                                            }
                                                                        }
                                                                    }

                                                                    valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                                    c.ValorBase = valorBase;
                                                                    c.ValorTotalContrato = valorTotal;

                                                                }
                                                                else
                                                                {
                                                                    if (cboxModalidad.SelectedIndex == 6)
                                                                    {
                                                                        valorBase = 10 * uf; personalBase = 5;

                                                                        if (int.Parse(txtPersonal.Text) == 2)
                                                                        { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtPersonal.Text) == 3)
                                                                            { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                                            else
                                                                            {
                                                                                if (double.Parse(txtPersonal.Text) == 4)
                                                                                {
                                                                                    valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (double.Parse(txtPersonal.Text) > 4)
                                                                                    { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                                    else
                                                                                    {
                                                                                        if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                                        {
                                                                                            valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                                        }
                                                                                    }

                                                                                }

                                                                            }
                                                                        }

                                                                        if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                                        { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                                            { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                                            else
                                                                            {
                                                                                if (double.Parse(txtAsistentes.Text) > 50)
                                                                                { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                                else
                                                                                {
                                                                                    if (double.Parse(txtAsistentes.Text) == 0)
                                                                                    {
                                                                                        valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                                    }
                                                                                }
                                                                            }
                                                                        }

                                                                        valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                                        c.ValorBase = valorBase;
                                                                        c.ValorTotalContrato = valorTotal;

                                                                    }
                                                                }
                                                            }
                                                        }

                                                    }

                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }

                }
                #endregion Calculo tipo de evento Baby Shower
                  

                #endregion CALCULO VALOR EVENTO




                if (ClienteNoIngresado == 0)
                {
                    c.Termino = "No Definida";
                    c.EstadoContrato = "ACTIVO";
                    contrato.AgregarEntidad(c);
                    estadoVigencia = c.EstadoContrato;

                    MessageBox.Show("Contrato Registrado satisfactoriamente\n" + mensaje2 + "\n* Valor Base Evento: " + valorBase + "\n* Cantidad Total Personal: " + personalTotal + "\n* Valor Total Evento: " + valorTotal, "Exito", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    LimpiarControles();
                    VentanaContratoModoInicio();
                }
                else
                { MessageBox.Show(mensaje, "Alerta", MessageBoxButton.OK, MessageBoxImage.Hand); }
            }

            catch (Exception)
            {

                MessageBox.Show("* Valor minimo para Personal adicional: 0\n* Valor minimo para Cantidad de asistentes: 1", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Hand);

            }

       }

        public void LimpiarControles()
        {
            txtRut.Text = string.Empty;

           

            cboxTipoEvento.SelectedIndex = -1;
            cboxModalidad.SelectedIndex = -1;
            txtObservaciones.Text = string.Empty;
            txtPersonal.Text = string.Empty;
            txtAsistentes.Text = string.Empty;

            txtNombreAsociado.Text = string.Empty;
            dpFechaInicio.SelectedDate = DateTime.Today;
            txtRazonSocialAsociada.Text = string.Empty;
            txtRutAsociado.Text = string.Empty;

            btnBuscar.IsEnabled = false;
            btnBuscarPorNumero.IsEnabled = false;

        }

        private void BtnActualizarContrato_Click(object sender, RoutedEventArgs e)
        {
            Info = "No se permiten campos vacios, favor ingresar datos:\n";
            ContratoNoActualizado = 0;
            ServiceContrato contrato = new ServiceContrato();
            Contrato c = new Contrato();
            Cliente cli = new Cliente();


            if (txtFechaTermino.Text == "No Definida")
            {
                //Bloque Cliente Asociado
                if (txtRutAsociado.Text != string.Empty)
                { c.RutCliente = txtRutAsociado.Text; }
                else
                { Info = Info + "- Rut Cliente\n"; ContratoNoActualizado = ContratoNoActualizado + 1; }

                if (txtRazonSocialAsociada.Text != string.Empty)
                { cli.RazonSocial = txtRazonSocialAsociada.Text; }
                else
                { Info = Info + "- Razon Social\n"; ContratoNoActualizado = ContratoNoActualizado + 1; }

                if (txtNombreAsociado.Text != string.Empty)
                { cli.NombreContacto = txtNombreAsociado.Text; }
                else
                { Info = Info + "- Nombre Cliente\n"; ContratoNoActualizado = ContratoNoActualizado + 1; }

                //Bloque detalle Contrato creado
                if (txtNumeroContra.Text != string.Empty)
                { c.Numero = txtNumeroContra.Text; }
                else
                { Info = Info + "- Numero Contrato\n"; ContratoNoActualizado = ContratoNoActualizado + 1; }

                if (txtVigenciaContrato.Text != string.Empty)
                { c.EstadoContrato = txtVigenciaContrato.Text; }
                else
                { Info = Info + "- Estado Contrato\n"; ContratoNoActualizado = ContratoNoActualizado + 1; }

                if (txtFechaContrato.Text != string.Empty)
                { c.Creacion = DateTime.Parse( txtFechaContrato.Text ); }
                else
                { Info = Info + "- Fecha Contrato\n"; ContratoNoActualizado = ContratoNoActualizado + 1; }

                if (txtValorBase.Text != string.Empty)
                { c.ValorBase = double.Parse(txtValorBase.Text); }
                else
                { Info = Info + "- Valor Base\n"; ContratoNoActualizado = ContratoNoActualizado + 1; }

                if (txtValorEventoEscondido.Text != string.Empty)
                { c.ValorTotalContrato = double.Parse(txtValorEventoEscondido.Text); }
                else
                { Info = Info + "- Valor Total\n"; ContratoNoActualizado = ContratoNoActualizado + 1; }


                //Bloque Caracteristicas del Contrato
                if (cboxTipoEvento.Text != string.Empty)
                { c.TipoEvento = cboxTipoEvento.Text; }
                else
                { Info = Info + "- Tipo Evento\n"; ContratoNoActualizado = ContratoNoActualizado + 1; }


                if (cboxModalidad.Text != string.Empty)
                { c.ModalidadServicio = cboxModalidad.Text; }
                else
                { Info = Info + "- Modalidad Servicio\n"; ContratoNoActualizado = ContratoNoActualizado + 1; }


                if (txtPersonal.Text != string.Empty)
                { c.PersonalAdicional = double.Parse(txtPersonal.Text); }
                else
                { Info = Info + "- Personal Adicional\n"; ContratoNoActualizado = ContratoNoActualizado + 1; }

                if (txtAsistentes.Text != string.Empty)
                { c.Asistentes = double.Parse(txtAsistentes.Text); }
                else
                { Info = Info + "- Cantidad Asistentes\n"; ContratoNoActualizado = ContratoNoActualizado + 1; }

                
                if (dpFechaInicio.Text != string.Empty)
                { c.Creacion = DateTime.Parse( dpFechaInicio.Text ); }
                else
                { Info = Info + "- Fecha Contrato\n"; ContratoNoActualizado = ContratoNoActualizado + 1; }

                if (txtObservaciones.Text != string.Empty)
                { c.Observaciones = txtObservaciones.Text; }
                else
                { Info = Info + "- Observaciones\n"; ContratoNoActualizado = ContratoNoActualizado + 1; }


                #region ACTUALIZACION DEL  VALOR DEL EVENTO

                #region Calculo para evento tipo Matrimonio
                // Calculo para evento tipo Matrimonio, con la 7 modalidades de servicio
                if (cboxTipoEvento.SelectedIndex == 0)
                {
                    if (cboxModalidad.SelectedIndex == 0)
                    {
                        valorBase = 3 * uf; personalBase = 2;

                        if (int.Parse(txtPersonal.Text) == 2)
                        { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                        else
                        {
                            if (double.Parse(txtPersonal.Text) == 3)
                            { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                            else
                            {
                                if (double.Parse(txtPersonal.Text) == 4)
                                {
                                    valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                }
                                else
                                {
                                    if (double.Parse(txtPersonal.Text) > 4)
                                    { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                    else
                                    {
                                        if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                        {
                                            valorPersonalAdicional = 0; personalTotal = personalBase;
                                        }
                                    }

                                }

                            }
                        }
                        if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                        { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                        else
                        {
                            if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                            { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                            else
                            {
                                if (double.Parse(txtAsistentes.Text) > 50)
                                { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                else
                                {
                                    if (double.Parse(txtAsistentes.Text) == 0)
                                    {
                                        valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                    }
                                }
                            }
                        }


                        valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                        c.ValorBase = valorBase;
                        c.ValorTotalContrato = valorTotal;
                    }
                    else
                    {
                        if (cboxModalidad.SelectedIndex == 1)
                        {
                            valorBase = 8 * uf; personalBase = 6;

                            if (int.Parse(txtPersonal.Text) == 2)
                            {
                                valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2;

                            }
                            else
                            {
                                if (double.Parse(txtPersonal.Text) == 3)
                                { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                else
                                {
                                    if (double.Parse(txtPersonal.Text) == 4)
                                    {
                                        valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                    }
                                    else
                                    {
                                        if (double.Parse(txtPersonal.Text) > 4)
                                        { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                        else
                                        {
                                            if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                            {
                                                valorPersonalAdicional = 0; personalTotal = personalBase;
                                            }
                                        }

                                    }

                                }


                            }
                            if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                            { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                            else
                            {
                                if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                else
                                {
                                    if (double.Parse(txtAsistentes.Text) > 50)
                                    { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                    else
                                    {
                                        if (double.Parse(txtAsistentes.Text) == 0)
                                        {
                                            valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                        }
                                    }
                                }
                            }

                            valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                            c.ValorBase = valorBase;
                            c.ValorTotalContrato = valorTotal;
                        }

                        else
                        {
                            if (cboxModalidad.SelectedIndex == 2)
                            {
                                valorBase = 12 * uf; personalBase = 6;

                                if (int.Parse(txtPersonal.Text) == 2)
                                { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                else
                                {
                                    if (double.Parse(txtPersonal.Text) == 3)
                                    { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                    else
                                    {
                                        if (double.Parse(txtPersonal.Text) == 4)
                                        {
                                            valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                        }
                                        else
                                        {
                                            if (double.Parse(txtPersonal.Text) > 4)
                                            { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                {
                                                    valorPersonalAdicional = 0; personalTotal = personalBase;
                                                }
                                            }

                                        }

                                    }
                                }

                                if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                else
                                {
                                    if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                    { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                    else
                                    {
                                        if (double.Parse(txtAsistentes.Text) > 50)
                                        { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                        else
                                        {
                                            if (double.Parse(txtAsistentes.Text) == 0)
                                            {
                                                valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                            }
                                        }
                                    }
                                }

                                valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                c.ValorBase = valorBase;
                                c.ValorTotalContrato = valorTotal;

                            }
                            else
                            {
                                if (cboxModalidad.SelectedIndex == 3)
                                {
                                    valorBase = 25 * uf; personalBase = 10;

                                    if (int.Parse(txtPersonal.Text) == 2)
                                    { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                    else
                                    {
                                        if (double.Parse(txtPersonal.Text) == 3)
                                        { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                        else
                                        {
                                            if (double.Parse(txtPersonal.Text) == 4)
                                            {
                                                valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                            }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) > 4)
                                                { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                    {
                                                        valorPersonalAdicional = 0; personalTotal = personalBase;
                                                    }
                                                }

                                            }

                                        }
                                    }

                                    if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                    { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                    else
                                    {
                                        if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                        { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                        else
                                        {
                                            if (double.Parse(txtAsistentes.Text) > 50)
                                            { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) == 0)
                                                {
                                                    valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                }
                                            }
                                        }
                                    }

                                    valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                    c.ValorBase = valorBase;
                                    c.ValorTotalContrato = valorTotal;

                                }
                                else
                                {
                                    if (cboxModalidad.SelectedIndex == 4)
                                    {
                                        valorBase = 35 * uf; personalBase = 14;

                                        if (int.Parse(txtPersonal.Text) == 2)
                                        { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                        else
                                        {
                                            if (double.Parse(txtPersonal.Text) == 3)
                                            { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) == 4)
                                                {
                                                    valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) > 4)
                                                    { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                        {
                                                            valorPersonalAdicional = 0; personalTotal = personalBase;
                                                        }
                                                    }

                                                }

                                            }
                                        }

                                        if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                        { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                        else
                                        {
                                            if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                            { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) > 50)
                                                { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) == 0)
                                                    {
                                                        valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                    }
                                                }
                                            }
                                        }

                                        valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                        c.ValorBase = valorBase;
                                        c.ValorTotalContrato = valorTotal;

                                    }
                                    else
                                    {
                                        if (cboxModalidad.SelectedIndex == 5)
                                        {
                                            valorBase = 6 * uf; personalBase = 4;

                                            if (int.Parse(txtPersonal.Text) == 2)
                                            { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) == 3)
                                                { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 4)
                                                    {
                                                        valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                    }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) > 4)
                                                        { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                            {
                                                                valorPersonalAdicional = 0; personalTotal = personalBase;
                                                            }
                                                        }

                                                    }

                                                }
                                            }

                                            if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                            { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) > 50)
                                                    { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) == 0)
                                                        {
                                                            valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                        }
                                                    }
                                                }
                                            }

                                            valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                            c.ValorBase = valorBase;
                                            c.ValorTotalContrato = valorTotal;

                                        }
                                        else
                                        {
                                            if (cboxModalidad.SelectedIndex == 6)
                                            {
                                                valorBase = 10 * uf; personalBase = 5;

                                                if (int.Parse(txtPersonal.Text) == 2)
                                                { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 3)
                                                    { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 4)
                                                        {
                                                            valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                        }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) > 4)
                                                            { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                {
                                                                    valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                }
                                                            }

                                                        }

                                                    }
                                                }

                                                if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                    { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) > 50)
                                                        { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) == 0)
                                                            {
                                                                valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                            }
                                                        }
                                                    }
                                                }

                                                valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                c.ValorBase = valorBase;
                                                c.ValorTotalContrato = valorTotal;

                                            }
                                        }
                                    }
                                }

                            }

                        }
                    }
                }
                else
                {
                    #endregion Calculo para evento tipo Matrimonio

                    #region Calculo Tipo de Evento Cumpleaños Adulto
                    //Calculo Tipo de Evento Cumpleaños Adulto, con la 7 modalidades de servicio
                    if (cboxTipoEvento.SelectedIndex == 1)
                    {
                        if (cboxModalidad.SelectedIndex == 0)
                        {
                            valorBase = 3 * uf; personalBase = 2;

                            if (int.Parse(txtPersonal.Text) == 2)
                            { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                            else
                            {
                                if (double.Parse(txtPersonal.Text) == 3)
                                { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                else
                                {
                                    if (double.Parse(txtPersonal.Text) == 4)
                                    {
                                        valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                    }
                                    else
                                    {
                                        if (double.Parse(txtPersonal.Text) > 4)
                                        { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                        else
                                        {
                                            if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                            {
                                                valorPersonalAdicional = 0; personalTotal = personalBase;
                                            }
                                        }

                                    }

                                }
                            }
                            if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                            { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                            else
                            {
                                if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                else
                                {
                                    if (double.Parse(txtAsistentes.Text) > 50)
                                    { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                    else
                                    {
                                        if (double.Parse(txtAsistentes.Text) == 0)
                                        {
                                            valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                        }
                                    }
                                }
                            }


                            valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                            c.ValorBase = valorBase;
                            c.ValorTotalContrato = valorTotal;
                        }
                        else
                        {
                            if (cboxModalidad.SelectedIndex == 1)
                            {
                                valorBase = 8 * uf; personalBase = 6;

                                if (int.Parse(txtPersonal.Text) == 2)
                                {
                                    valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2;

                                }
                                else
                                {
                                    if (double.Parse(txtPersonal.Text) == 3)
                                    { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                    else
                                    {
                                        if (double.Parse(txtPersonal.Text) == 4)
                                        {
                                            valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                        }
                                        else
                                        {
                                            if (double.Parse(txtPersonal.Text) > 4)
                                            { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                {
                                                    valorPersonalAdicional = 0; personalTotal = personalBase;
                                                }
                                            }

                                        }

                                    }


                                }
                                if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                else
                                {
                                    if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                    { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                    else
                                    {
                                        if (double.Parse(txtAsistentes.Text) > 50)
                                        { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                        else
                                        {
                                            if (double.Parse(txtAsistentes.Text) == 0)
                                            {
                                                valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                            }
                                        }
                                    }
                                }

                                valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                c.ValorBase = valorBase;
                                c.ValorTotalContrato = valorTotal;
                            }

                            else
                            {
                                if (cboxModalidad.SelectedIndex == 2)
                                {
                                    valorBase = 12 * uf; personalBase = 6;

                                    if (int.Parse(txtPersonal.Text) == 2)
                                    { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                    else
                                    {
                                        if (double.Parse(txtPersonal.Text) == 3)
                                        { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                        else
                                        {
                                            if (double.Parse(txtPersonal.Text) == 4)
                                            {
                                                valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                            }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) > 4)
                                                { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                    {
                                                        valorPersonalAdicional = 0; personalTotal = personalBase;
                                                    }
                                                }

                                            }

                                        }
                                    }

                                    if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                    { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                    else
                                    {
                                        if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                        { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                        else
                                        {
                                            if (double.Parse(txtAsistentes.Text) > 50)
                                            { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) == 0)
                                                {
                                                    valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                }
                                            }
                                        }
                                    }

                                    valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                    c.ValorBase = valorBase;
                                    c.ValorTotalContrato = valorTotal;

                                }
                                else
                                {
                                    if (cboxModalidad.SelectedIndex == 3)
                                    {
                                        valorBase = 25 * uf; personalBase = 10;

                                        if (int.Parse(txtPersonal.Text) == 2)
                                        { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                        else
                                        {
                                            if (double.Parse(txtPersonal.Text) == 3)
                                            { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) == 4)
                                                {
                                                    valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) > 4)
                                                    { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                        {
                                                            valorPersonalAdicional = 0; personalTotal = personalBase;
                                                        }
                                                    }

                                                }

                                            }
                                        }

                                        if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                        { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                        else
                                        {
                                            if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                            { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) > 50)
                                                { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) == 0)
                                                    {
                                                        valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                    }
                                                }
                                            }
                                        }

                                        valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                        c.ValorBase = valorBase;
                                        c.ValorTotalContrato = valorTotal;

                                    }
                                    else
                                    {
                                        if (cboxModalidad.SelectedIndex == 4)
                                        {
                                            valorBase = 35 * uf; personalBase = 14;

                                            if (int.Parse(txtPersonal.Text) == 2)
                                            { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) == 3)
                                                { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 4)
                                                    {
                                                        valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                    }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) > 4)
                                                        { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                            {
                                                                valorPersonalAdicional = 0; personalTotal = personalBase;
                                                            }
                                                        }

                                                    }

                                                }
                                            }

                                            if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                            { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) > 50)
                                                    { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) == 0)
                                                        {
                                                            valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                        }
                                                    }
                                                }
                                            }

                                            valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                            c.ValorBase = valorBase;
                                            c.ValorTotalContrato = valorTotal;

                                        }
                                        else
                                        {
                                            if (cboxModalidad.SelectedIndex == 5)
                                            {
                                                valorBase = 6 * uf; personalBase = 4;

                                                if (int.Parse(txtPersonal.Text) == 2)
                                                { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 3)
                                                    { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 4)
                                                        {
                                                            valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                        }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) > 4)
                                                            { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                {
                                                                    valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                }
                                                            }

                                                        }

                                                    }
                                                }

                                                if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                    { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) > 50)
                                                        { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) == 0)
                                                            {
                                                                valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                            }
                                                        }
                                                    }
                                                }

                                                valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                c.ValorBase = valorBase;
                                                c.ValorTotalContrato = valorTotal;

                                            }
                                            else
                                            {
                                                if (cboxModalidad.SelectedIndex == 6)
                                                {
                                                    valorBase = 10 * uf; personalBase = 5;

                                                    if (int.Parse(txtPersonal.Text) == 2)
                                                    { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 3)
                                                        { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 4)
                                                            {
                                                                valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                            }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) > 4)
                                                                { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                    {
                                                                        valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                    }
                                                                }

                                                            }

                                                        }
                                                    }

                                                    if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                    { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                        { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) > 50)
                                                            { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) == 0)
                                                                {
                                                                    valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                    c.ValorBase = valorBase;
                                                    c.ValorTotalContrato = valorTotal;

                                                }
                                            }
                                        }
                                    }

                                }

                            }
                        }
                    }
                    else
                    {
                        #endregion Calculo Tipo de Evento Cumpleaños Adulto

                        #region Calculo tipo de evento Cumpleaños Infantil
                        // Calculo tipo de evento Cumpleaños Infantil, con la 7 modalidades de servicio
                        if (cboxTipoEvento.SelectedIndex == 2)
                        {
                            if (cboxModalidad.SelectedIndex == 0)
                            {
                                valorBase = 3 * uf; personalBase = 2;

                                if (int.Parse(txtPersonal.Text) == 2)
                                { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                else
                                {
                                    if (double.Parse(txtPersonal.Text) == 3)
                                    { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                    else
                                    {
                                        if (double.Parse(txtPersonal.Text) == 4)
                                        {
                                            valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                        }
                                        else
                                        {
                                            if (double.Parse(txtPersonal.Text) > 4)
                                            { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                {
                                                    valorPersonalAdicional = 0; personalTotal = personalBase;
                                                }
                                            }

                                        }

                                    }
                                }
                                if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                else
                                {
                                    if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                    { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                    else
                                    {
                                        if (double.Parse(txtAsistentes.Text) > 50)
                                        { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                        else
                                        {
                                            if (double.Parse(txtAsistentes.Text) == 0)
                                            {
                                                valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                            }
                                        }
                                    }
                                }


                                valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                c.ValorBase = valorBase;
                                c.ValorTotalContrato = valorTotal;
                            }
                            else
                            {
                                if (cboxModalidad.SelectedIndex == 1)
                                {
                                    valorBase = 8 * uf; personalBase = 6;

                                    if (int.Parse(txtPersonal.Text) == 2)
                                    {
                                        valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2;

                                    }
                                    else
                                    {
                                        if (double.Parse(txtPersonal.Text) == 3)
                                        { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                        else
                                        {
                                            if (double.Parse(txtPersonal.Text) == 4)
                                            {
                                                valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                            }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) > 4)
                                                { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                    {
                                                        valorPersonalAdicional = 0; personalTotal = personalBase;
                                                    }
                                                }

                                            }

                                        }


                                    }
                                    if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                    { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                    else
                                    {
                                        if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                        { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                        else
                                        {
                                            if (double.Parse(txtAsistentes.Text) > 50)
                                            { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) == 0)
                                                {
                                                    valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                }
                                            }
                                        }
                                    }

                                    valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                    c.ValorBase = valorBase;
                                    c.ValorTotalContrato = valorTotal;
                                }

                                else
                                {
                                    if (cboxModalidad.SelectedIndex == 2)
                                    {
                                        valorBase = 12 * uf; personalBase = 6;

                                        if (int.Parse(txtPersonal.Text) == 2)
                                        { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                        else
                                        {
                                            if (double.Parse(txtPersonal.Text) == 3)
                                            { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) == 4)
                                                {
                                                    valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) > 4)
                                                    { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                        {
                                                            valorPersonalAdicional = 0; personalTotal = personalBase;
                                                        }
                                                    }

                                                }

                                            }
                                        }

                                        if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                        { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                        else
                                        {
                                            if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                            { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) > 50)
                                                { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) == 0)
                                                    {
                                                        valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                    }
                                                }
                                            }
                                        }

                                        valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                        c.ValorBase = valorBase;
                                        c.ValorTotalContrato = valorTotal;

                                    }
                                    else
                                    {
                                        if (cboxModalidad.SelectedIndex == 3)
                                        {
                                            valorBase = 25 * uf; personalBase = 10;

                                            if (int.Parse(txtPersonal.Text) == 2)
                                            { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) == 3)
                                                { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 4)
                                                    {
                                                        valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                    }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) > 4)
                                                        { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                            {
                                                                valorPersonalAdicional = 0; personalTotal = personalBase;
                                                            }
                                                        }

                                                    }

                                                }
                                            }

                                            if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                            { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) > 50)
                                                    { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) == 0)
                                                        {
                                                            valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                        }
                                                    }
                                                }
                                            }

                                            valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                            c.ValorBase = valorBase;
                                            c.ValorTotalContrato = valorTotal;

                                        }
                                        else
                                        {
                                            if (cboxModalidad.SelectedIndex == 4)
                                            {
                                                valorBase = 35 * uf; personalBase = 14;

                                                if (int.Parse(txtPersonal.Text) == 2)
                                                { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 3)
                                                    { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 4)
                                                        {
                                                            valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                        }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) > 4)
                                                            { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                {
                                                                    valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                }
                                                            }

                                                        }

                                                    }
                                                }

                                                if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                    { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) > 50)
                                                        { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) == 0)
                                                            {
                                                                valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                            }
                                                        }
                                                    }
                                                }

                                                valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                c.ValorBase = valorBase;
                                                c.ValorTotalContrato = valorTotal;

                                            }
                                            else
                                            {
                                                if (cboxModalidad.SelectedIndex == 5)
                                                {
                                                    valorBase = 6 * uf; personalBase = 4;

                                                    if (int.Parse(txtPersonal.Text) == 2)
                                                    { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 3)
                                                        { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 4)
                                                            {
                                                                valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                            }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) > 4)
                                                                { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                    {
                                                                        valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                    }
                                                                }

                                                            }

                                                        }
                                                    }

                                                    if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                    { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                        { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) > 50)
                                                            { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) == 0)
                                                                {
                                                                    valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                    c.ValorBase = valorBase;
                                                    c.ValorTotalContrato = valorTotal;

                                                }
                                                else
                                                {
                                                    if (cboxModalidad.SelectedIndex == 6)
                                                    {
                                                        valorBase = 10 * uf; personalBase = 5;

                                                        if (int.Parse(txtPersonal.Text) == 2)
                                                        { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 3)
                                                            { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 4)
                                                                {
                                                                    valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) > 4)
                                                                    { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                        {
                                                                            valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                        }
                                                                    }

                                                                }

                                                            }
                                                        }

                                                        if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                        { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                            { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) > 50)
                                                                { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtAsistentes.Text) == 0)
                                                                    {
                                                                        valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                        c.ValorBase = valorBase;
                                                        c.ValorTotalContrato = valorTotal;

                                                    }
                                                }
                                            }
                                        }

                                    }

                                }
                            }
                        }
                        else
                        {
                            #endregion Calculo tipo de evento Cumpleaños Infantil

                            #region Calculo tipo de evento Evento Empresarial

                            // Calculo tipo de evento Evento Empresarial, con la 7 modalidades de servicio
                            if (cboxTipoEvento.SelectedIndex == 3)
                            {
                                if (cboxModalidad.SelectedIndex == 0)
                                {
                                    valorBase = 3 * uf; personalBase = 2;

                                    if (int.Parse(txtPersonal.Text) == 2)
                                    { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                    else
                                    {
                                        if (double.Parse(txtPersonal.Text) == 3)
                                        { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                        else
                                        {
                                            if (double.Parse(txtPersonal.Text) == 4)
                                            {
                                                valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                            }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) > 4)
                                                { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                    {
                                                        valorPersonalAdicional = 0; personalTotal = personalBase;
                                                    }
                                                }

                                            }

                                        }
                                    }
                                    if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                    { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                    else
                                    {
                                        if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                        { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                        else
                                        {
                                            if (double.Parse(txtAsistentes.Text) > 50)
                                            { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) == 0)
                                                {
                                                    valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                }
                                            }
                                        }
                                    }


                                    valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                    c.ValorBase = valorBase;
                                    c.ValorTotalContrato = valorTotal;
                                }
                                else
                                {
                                    if (cboxModalidad.SelectedIndex == 1)
                                    {
                                        valorBase = 8 * uf; personalBase = 6;

                                        if (int.Parse(txtPersonal.Text) == 2)
                                        {
                                            valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2;

                                        }
                                        else
                                        {
                                            if (double.Parse(txtPersonal.Text) == 3)
                                            { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) == 4)
                                                {
                                                    valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) > 4)
                                                    { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                        {
                                                            valorPersonalAdicional = 0; personalTotal = personalBase;
                                                        }
                                                    }

                                                }

                                            }


                                        }
                                        if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                        { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                        else
                                        {
                                            if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                            { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) > 50)
                                                { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) == 0)
                                                    {
                                                        valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                    }
                                                }
                                            }
                                        }

                                        valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                        c.ValorBase = valorBase;
                                        c.ValorTotalContrato = valorTotal;
                                    }

                                    else
                                    {
                                        if (cboxModalidad.SelectedIndex == 2)
                                        {
                                            valorBase = 12 * uf; personalBase = 6;

                                            if (int.Parse(txtPersonal.Text) == 2)
                                            { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) == 3)
                                                { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 4)
                                                    {
                                                        valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                    }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) > 4)
                                                        { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                            {
                                                                valorPersonalAdicional = 0; personalTotal = personalBase;
                                                            }
                                                        }

                                                    }

                                                }
                                            }

                                            if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                            { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) > 50)
                                                    { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) == 0)
                                                        {
                                                            valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                        }
                                                    }
                                                }
                                            }

                                            valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                            c.ValorBase = valorBase;
                                            c.ValorTotalContrato = valorTotal;

                                        }
                                        else
                                        {
                                            if (cboxModalidad.SelectedIndex == 3)
                                            {
                                                valorBase = 25 * uf; personalBase = 10;

                                                if (int.Parse(txtPersonal.Text) == 2)
                                                { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 3)
                                                    { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 4)
                                                        {
                                                            valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                        }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) > 4)
                                                            { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                {
                                                                    valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                }
                                                            }

                                                        }

                                                    }
                                                }

                                                if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                    { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) > 50)
                                                        { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) == 0)
                                                            {
                                                                valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                            }
                                                        }
                                                    }
                                                }

                                                valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                c.ValorBase = valorBase;
                                                c.ValorTotalContrato = valorTotal;

                                            }
                                            else
                                            {
                                                if (cboxModalidad.SelectedIndex == 4)
                                                {
                                                    valorBase = 35 * uf; personalBase = 14;

                                                    if (int.Parse(txtPersonal.Text) == 2)
                                                    { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 3)
                                                        { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 4)
                                                            {
                                                                valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                            }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) > 4)
                                                                { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                    {
                                                                        valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                    }
                                                                }

                                                            }

                                                        }
                                                    }

                                                    if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                    { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                        { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) > 50)
                                                            { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) == 0)
                                                                {
                                                                    valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                    c.ValorBase = valorBase;
                                                    c.ValorTotalContrato = valorTotal;

                                                }
                                                else
                                                {
                                                    if (cboxModalidad.SelectedIndex == 5)
                                                    {
                                                        valorBase = 6 * uf; personalBase = 4;

                                                        if (int.Parse(txtPersonal.Text) == 2)
                                                        { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 3)
                                                            { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 4)
                                                                {
                                                                    valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) > 4)
                                                                    { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                        {
                                                                            valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                        }
                                                                    }

                                                                }

                                                            }
                                                        }

                                                        if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                        { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                            { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) > 50)
                                                                { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtAsistentes.Text) == 0)
                                                                    {
                                                                        valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                        c.ValorBase = valorBase;
                                                        c.ValorTotalContrato = valorTotal;

                                                    }
                                                    else
                                                    {
                                                        if (cboxModalidad.SelectedIndex == 6)
                                                        {
                                                            valorBase = 10 * uf; personalBase = 5;

                                                            if (int.Parse(txtPersonal.Text) == 2)
                                                            { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 3)
                                                                { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) == 4)
                                                                    {
                                                                        valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtPersonal.Text) > 4)
                                                                        { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                            {
                                                                                valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                            }
                                                                        }

                                                                    }

                                                                }
                                                            }

                                                            if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                            { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                                { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtAsistentes.Text) > 50)
                                                                    { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtAsistentes.Text) == 0)
                                                                        {
                                                                            valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                            valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                            c.ValorBase = valorBase;
                                                            c.ValorTotalContrato = valorTotal;

                                                        }
                                                    }
                                                }
                                            }

                                        }

                                    }
                                }
                            }
                            else
                            {
                                #endregion Calculo tipo de evento Evento Empresarial

                                #region Calculo tipo de evento Desdepida de Soltero
                                //Calculo tipo de evento Desdepida de Soltero, con la 7 modalidades de servicio
                                if (cboxTipoEvento.SelectedIndex == 4)
                                {
                                    if (cboxModalidad.SelectedIndex == 0)
                                    {
                                        valorBase = 3 * uf; personalBase = 2;

                                        if (int.Parse(txtPersonal.Text) == 2)
                                        { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                        else
                                        {
                                            if (double.Parse(txtPersonal.Text) == 3)
                                            { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) == 4)
                                                {
                                                    valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) > 4)
                                                    { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                        {
                                                            valorPersonalAdicional = 0; personalTotal = personalBase;
                                                        }
                                                    }

                                                }

                                            }
                                        }
                                        if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                        { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                        else
                                        {
                                            if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                            { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) > 50)
                                                { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) == 0)
                                                    {
                                                        valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                    }
                                                }
                                            }
                                        }


                                        valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                        c.ValorBase = valorBase;
                                        c.ValorTotalContrato = valorTotal;
                                    }
                                    else
                                    {
                                        if (cboxModalidad.SelectedIndex == 1)
                                        {
                                            valorBase = 8 * uf; personalBase = 6;

                                            if (int.Parse(txtPersonal.Text) == 2)
                                            {
                                                valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2;

                                            }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) == 3)
                                                { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 4)
                                                    {
                                                        valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                    }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) > 4)
                                                        { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                            {
                                                                valorPersonalAdicional = 0; personalTotal = personalBase;
                                                            }
                                                        }

                                                    }

                                                }


                                            }
                                            if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                            { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) > 50)
                                                    { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) == 0)
                                                        {
                                                            valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                        }
                                                    }
                                                }
                                            }

                                            valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                            c.ValorBase = valorBase;
                                            c.ValorTotalContrato = valorTotal;
                                        }

                                        else
                                        {
                                            if (cboxModalidad.SelectedIndex == 2)
                                            {
                                                valorBase = 12 * uf; personalBase = 6;

                                                if (int.Parse(txtPersonal.Text) == 2)
                                                { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 3)
                                                    { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 4)
                                                        {
                                                            valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                        }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) > 4)
                                                            { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                {
                                                                    valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                }
                                                            }

                                                        }

                                                    }
                                                }

                                                if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                    { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) > 50)
                                                        { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) == 0)
                                                            {
                                                                valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                            }
                                                        }
                                                    }
                                                }

                                                valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                c.ValorBase = valorBase;
                                                c.ValorTotalContrato = valorTotal;

                                            }
                                            else
                                            {
                                                if (cboxModalidad.SelectedIndex == 3)
                                                {
                                                    valorBase = 25 * uf; personalBase = 10;

                                                    if (int.Parse(txtPersonal.Text) == 2)
                                                    { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 3)
                                                        { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 4)
                                                            {
                                                                valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                            }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) > 4)
                                                                { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                    {
                                                                        valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                    }
                                                                }

                                                            }

                                                        }
                                                    }

                                                    if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                    { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                        { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) > 50)
                                                            { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) == 0)
                                                                {
                                                                    valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                    c.ValorBase = valorBase;
                                                    c.ValorTotalContrato = valorTotal;

                                                }
                                                else
                                                {
                                                    if (cboxModalidad.SelectedIndex == 4)
                                                    {
                                                        valorBase = 35 * uf; personalBase = 14;

                                                        if (int.Parse(txtPersonal.Text) == 2)
                                                        { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 3)
                                                            { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 4)
                                                                {
                                                                    valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) > 4)
                                                                    { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                        {
                                                                            valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                        }
                                                                    }

                                                                }

                                                            }
                                                        }

                                                        if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                        { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                            { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) > 50)
                                                                { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtAsistentes.Text) == 0)
                                                                    {
                                                                        valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                        c.ValorBase = valorBase;
                                                        c.ValorTotalContrato = valorTotal;

                                                    }
                                                    else
                                                    {
                                                        if (cboxModalidad.SelectedIndex == 5)
                                                        {
                                                            valorBase = 6 * uf; personalBase = 4;

                                                            if (int.Parse(txtPersonal.Text) == 2)
                                                            { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 3)
                                                                { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) == 4)
                                                                    {
                                                                        valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtPersonal.Text) > 4)
                                                                        { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                            {
                                                                                valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                            }
                                                                        }

                                                                    }

                                                                }
                                                            }

                                                            if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                            { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                                { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtAsistentes.Text) > 50)
                                                                    { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtAsistentes.Text) == 0)
                                                                        {
                                                                            valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                            valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                            c.ValorBase = valorBase;
                                                            c.ValorTotalContrato = valorTotal;

                                                        }
                                                        else
                                                        {
                                                            if (cboxModalidad.SelectedIndex == 6)
                                                            {
                                                                valorBase = 10 * uf; personalBase = 5;

                                                                if (int.Parse(txtPersonal.Text) == 2)
                                                                { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) == 3)
                                                                    { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtPersonal.Text) == 4)
                                                                        {
                                                                            valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                        }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtPersonal.Text) > 4)
                                                                            { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                            else
                                                                            {
                                                                                if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                                {
                                                                                    valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                                }
                                                                            }

                                                                        }

                                                                    }
                                                                }

                                                                if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                                { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                                    { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtAsistentes.Text) > 50)
                                                                        { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtAsistentes.Text) == 0)
                                                                            {
                                                                                valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                                valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                                c.ValorBase = valorBase;
                                                                c.ValorTotalContrato = valorTotal;

                                                            }
                                                        }
                                                    }
                                                }

                                            }

                                        }
                                    }
                                }
                                else
                                {
                                    #endregion Calculo tipo de evento Desdepida de Soltero

                                    #region Calculo tipo de evento Evento Religioso
                                    //Calculo tipo de evento Evento Religioso, con la 7 modalidades de servicio
                                    if (cboxTipoEvento.SelectedIndex == 5)
                                    {
                                        if (cboxModalidad.SelectedIndex == 0)
                                        {
                                            valorBase = 3 * uf; personalBase = 2;

                                            if (int.Parse(txtPersonal.Text) == 2)
                                            { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                            else
                                            {
                                                if (double.Parse(txtPersonal.Text) == 3)
                                                { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 4)
                                                    {
                                                        valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                    }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) > 4)
                                                        { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                            {
                                                                valorPersonalAdicional = 0; personalTotal = personalBase;
                                                            }
                                                        }

                                                    }

                                                }
                                            }
                                            if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                            { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                            else
                                            {
                                                if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) > 50)
                                                    { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) == 0)
                                                        {
                                                            valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                        }
                                                    }
                                                }
                                            }


                                            valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                            c.ValorBase = valorBase;
                                            c.ValorTotalContrato = valorTotal;
                                        }
                                        else
                                        {
                                            if (cboxModalidad.SelectedIndex == 1)
                                            {
                                                valorBase = 8 * uf; personalBase = 6;

                                                if (int.Parse(txtPersonal.Text) == 2)
                                                {
                                                    valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2;

                                                }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 3)
                                                    { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 4)
                                                        {
                                                            valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                        }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) > 4)
                                                            { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                {
                                                                    valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                }
                                                            }

                                                        }

                                                    }


                                                }
                                                if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                    { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) > 50)
                                                        { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) == 0)
                                                            {
                                                                valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                            }
                                                        }
                                                    }
                                                }

                                                valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                c.ValorBase = valorBase;
                                                c.ValorTotalContrato = valorTotal;
                                            }

                                            else
                                            {
                                                if (cboxModalidad.SelectedIndex == 2)
                                                {
                                                    valorBase = 12 * uf; personalBase = 6;

                                                    if (int.Parse(txtPersonal.Text) == 2)
                                                    { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 3)
                                                        { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 4)
                                                            {
                                                                valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                            }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) > 4)
                                                                { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                    {
                                                                        valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                    }
                                                                }

                                                            }

                                                        }
                                                    }

                                                    if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                    { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                        { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) > 50)
                                                            { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) == 0)
                                                                {
                                                                    valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                    c.ValorBase = valorBase;
                                                    c.ValorTotalContrato = valorTotal;

                                                }
                                                else
                                                {
                                                    if (cboxModalidad.SelectedIndex == 3)
                                                    {
                                                        valorBase = 25 * uf; personalBase = 10;

                                                        if (int.Parse(txtPersonal.Text) == 2)
                                                        { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 3)
                                                            { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 4)
                                                                {
                                                                    valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) > 4)
                                                                    { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                        {
                                                                            valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                        }
                                                                    }

                                                                }

                                                            }
                                                        }

                                                        if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                        { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                            { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) > 50)
                                                                { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtAsistentes.Text) == 0)
                                                                    {
                                                                        valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                        c.ValorBase = valorBase;
                                                        c.ValorTotalContrato = valorTotal;

                                                    }
                                                    else
                                                    {
                                                        if (cboxModalidad.SelectedIndex == 4)
                                                        {
                                                            valorBase = 35 * uf; personalBase = 14;

                                                            if (int.Parse(txtPersonal.Text) == 2)
                                                            { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 3)
                                                                { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) == 4)
                                                                    {
                                                                        valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtPersonal.Text) > 4)
                                                                        { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                            {
                                                                                valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                            }
                                                                        }

                                                                    }

                                                                }
                                                            }

                                                            if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                            { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                                { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtAsistentes.Text) > 50)
                                                                    { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtAsistentes.Text) == 0)
                                                                        {
                                                                            valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                            valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                            c.ValorBase = valorBase;
                                                            c.ValorTotalContrato = valorTotal;

                                                        }
                                                        else
                                                        {
                                                            if (cboxModalidad.SelectedIndex == 5)
                                                            {
                                                                valorBase = 6 * uf; personalBase = 4;

                                                                if (int.Parse(txtPersonal.Text) == 2)
                                                                { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) == 3)
                                                                    { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtPersonal.Text) == 4)
                                                                        {
                                                                            valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                        }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtPersonal.Text) > 4)
                                                                            { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                            else
                                                                            {
                                                                                if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                                {
                                                                                    valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                                }
                                                                            }

                                                                        }

                                                                    }
                                                                }

                                                                if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                                { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                                    { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtAsistentes.Text) > 50)
                                                                        { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtAsistentes.Text) == 0)
                                                                            {
                                                                                valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                                valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                                c.ValorBase = valorBase;
                                                                c.ValorTotalContrato = valorTotal;

                                                            }
                                                            else
                                                            {
                                                                if (cboxModalidad.SelectedIndex == 6)
                                                                {
                                                                    valorBase = 10 * uf; personalBase = 5;

                                                                    if (int.Parse(txtPersonal.Text) == 2)
                                                                    { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtPersonal.Text) == 3)
                                                                        { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtPersonal.Text) == 4)
                                                                            {
                                                                                valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                            }
                                                                            else
                                                                            {
                                                                                if (double.Parse(txtPersonal.Text) > 4)
                                                                                { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                                else
                                                                                {
                                                                                    if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                                    {
                                                                                        valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                                    }
                                                                                }

                                                                            }

                                                                        }
                                                                    }

                                                                    if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                                    { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                                        { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtAsistentes.Text) > 50)
                                                                            { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                            else
                                                                            {
                                                                                if (double.Parse(txtAsistentes.Text) == 0)
                                                                                {
                                                                                    valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                                }
                                                                            }
                                                                        }
                                                                    }

                                                                    valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                                    c.ValorBase = valorBase;
                                                                    c.ValorTotalContrato = valorTotal;

                                                                }
                                                            }
                                                        }
                                                    }

                                                }

                                            }
                                        }
                                    }
                                    else
                                    {
                                        #endregion Calculo tipo de evento Evento Religioso

                                        #region Calculo tipo de evento Baby Shower
                                        //Calculo tipo de evento Baby Shower, con la 7 modalidades de servicio
                                        if (cboxTipoEvento.SelectedIndex == 6)
                                        {
                                            if (cboxModalidad.SelectedIndex == 0)
                                            {
                                                valorBase = 3 * uf; personalBase = 2;

                                                if (int.Parse(txtPersonal.Text) == 2)
                                                { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                else
                                                {
                                                    if (double.Parse(txtPersonal.Text) == 3)
                                                    { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 4)
                                                        {
                                                            valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                        }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) > 4)
                                                            { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                {
                                                                    valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                }
                                                            }

                                                        }

                                                    }
                                                }
                                                if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                else
                                                {
                                                    if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                    { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) > 50)
                                                        { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) == 0)
                                                            {
                                                                valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                            }
                                                        }
                                                    }
                                                }


                                                valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                c.ValorBase = valorBase;
                                                c.ValorTotalContrato = valorTotal;
                                            }
                                            else
                                            {
                                                if (cboxModalidad.SelectedIndex == 1)
                                                {
                                                    valorBase = 8 * uf; personalBase = 6;

                                                    if (int.Parse(txtPersonal.Text) == 2)
                                                    {
                                                        valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2;

                                                    }
                                                    else
                                                    {
                                                        if (double.Parse(txtPersonal.Text) == 3)
                                                        { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 4)
                                                            {
                                                                valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                            }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) > 4)
                                                                { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                    {
                                                                        valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                    }
                                                                }

                                                            }

                                                        }


                                                    }
                                                    if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                    { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                    else
                                                    {
                                                        if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                        { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) > 50)
                                                            { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) == 0)
                                                                {
                                                                    valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                    c.ValorBase = valorBase;
                                                    c.ValorTotalContrato = valorTotal;
                                                }

                                                else
                                                {
                                                    if (cboxModalidad.SelectedIndex == 2)
                                                    {
                                                        valorBase = 12 * uf; personalBase = 6;

                                                        if (int.Parse(txtPersonal.Text) == 2)
                                                        { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                        else
                                                        {
                                                            if (double.Parse(txtPersonal.Text) == 3)
                                                            { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 4)
                                                                {
                                                                    valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) > 4)
                                                                    { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                        {
                                                                            valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                        }
                                                                    }

                                                                }

                                                            }
                                                        }

                                                        if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                        { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                        else
                                                        {
                                                            if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                            { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) > 50)
                                                                { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtAsistentes.Text) == 0)
                                                                    {
                                                                        valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                        c.ValorBase = valorBase;
                                                        c.ValorTotalContrato = valorTotal;

                                                    }
                                                    else
                                                    {
                                                        if (cboxModalidad.SelectedIndex == 3)
                                                        {
                                                            valorBase = 25 * uf; personalBase = 10;

                                                            if (int.Parse(txtPersonal.Text) == 2)
                                                            { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                            else
                                                            {
                                                                if (double.Parse(txtPersonal.Text) == 3)
                                                                { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) == 4)
                                                                    {
                                                                        valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtPersonal.Text) > 4)
                                                                        { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                            {
                                                                                valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                            }
                                                                        }

                                                                    }

                                                                }
                                                            }

                                                            if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                            { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                            else
                                                            {
                                                                if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                                { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtAsistentes.Text) > 50)
                                                                    { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtAsistentes.Text) == 0)
                                                                        {
                                                                            valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                            valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                            c.ValorBase = valorBase;
                                                            c.ValorTotalContrato = valorTotal;

                                                        }
                                                        else
                                                        {
                                                            if (cboxModalidad.SelectedIndex == 4)
                                                            {
                                                                valorBase = 35 * uf; personalBase = 14;

                                                                if (int.Parse(txtPersonal.Text) == 2)
                                                                { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                                else
                                                                {
                                                                    if (double.Parse(txtPersonal.Text) == 3)
                                                                    { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtPersonal.Text) == 4)
                                                                        {
                                                                            valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                        }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtPersonal.Text) > 4)
                                                                            { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                            else
                                                                            {
                                                                                if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                                {
                                                                                    valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                                }
                                                                            }

                                                                        }

                                                                    }
                                                                }

                                                                if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                                { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                                else
                                                                {
                                                                    if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                                    { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtAsistentes.Text) > 50)
                                                                        { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtAsistentes.Text) == 0)
                                                                            {
                                                                                valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                                valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                                c.ValorBase = valorBase;
                                                                c.ValorTotalContrato = valorTotal;

                                                            }
                                                            else
                                                            {
                                                                if (cboxModalidad.SelectedIndex == 5)
                                                                {
                                                                    valorBase = 6 * uf; personalBase = 4;

                                                                    if (int.Parse(txtPersonal.Text) == 2)
                                                                    { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtPersonal.Text) == 3)
                                                                        { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtPersonal.Text) == 4)
                                                                            {
                                                                                valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                            }
                                                                            else
                                                                            {
                                                                                if (double.Parse(txtPersonal.Text) > 4)
                                                                                { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                                else
                                                                                {
                                                                                    if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                                    {
                                                                                        valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                                    }
                                                                                }

                                                                            }

                                                                        }
                                                                    }

                                                                    if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                                    { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                                    else
                                                                    {
                                                                        if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                                        { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtAsistentes.Text) > 50)
                                                                            { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                            else
                                                                            {
                                                                                if (double.Parse(txtAsistentes.Text) == 0)
                                                                                {
                                                                                    valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                                }
                                                                            }
                                                                        }
                                                                    }

                                                                    valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                                    c.ValorBase = valorBase;
                                                                    c.ValorTotalContrato = valorTotal;

                                                                }
                                                                else
                                                                {
                                                                    if (cboxModalidad.SelectedIndex == 6)
                                                                    {
                                                                        valorBase = 10 * uf; personalBase = 5;

                                                                        if (int.Parse(txtPersonal.Text) == 2)
                                                                        { valorPersonalAdicional = uf * 2; personalTotal = personalBase + 2; }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtPersonal.Text) == 3)
                                                                            { valorPersonalAdicional = uf * 3; personalTotal = personalBase + 2; }
                                                                            else
                                                                            {
                                                                                if (double.Parse(txtPersonal.Text) == 4)
                                                                                {
                                                                                    valorPersonalAdicional = uf * 3.5; personalTotal = personalBase + 4;
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (double.Parse(txtPersonal.Text) > 4)
                                                                                    { valorPersonalAdicional = uf * 3.5 + (double.Parse(txtPersonal.Text) * (0.5 * uf)); personalTotal = 25 + double.Parse(txtPersonal.Text); }
                                                                                    else
                                                                                    {
                                                                                        if (double.Parse(txtPersonal.Text) == 0 && double.Parse(txtPersonal.Text) < 2)
                                                                                        {
                                                                                            valorPersonalAdicional = 0; personalTotal = personalBase;
                                                                                        }
                                                                                    }

                                                                                }

                                                                            }
                                                                        }

                                                                        if (double.Parse(txtAsistentes.Text) >= 1 && double.Parse(txtAsistentes.Text) <= 20)
                                                                        { valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text); }
                                                                        else
                                                                        {
                                                                            if (double.Parse(txtAsistentes.Text) >= 21 && double.Parse(txtAsistentes.Text) <= 50)
                                                                            { valorAsistentes = (uf * 5); asistentes = double.Parse(txtAsistentes.Text); }
                                                                            else
                                                                            {
                                                                                if (double.Parse(txtAsistentes.Text) > 50)
                                                                                { asistentes = double.Parse(txtAsistentes.Text); valorAsistentes = (uf * 5) + (((asistentes - 50) / 20) * (2 * uf)); }
                                                                                else
                                                                                {
                                                                                    if (double.Parse(txtAsistentes.Text) == 0)
                                                                                    {
                                                                                        valorAsistentes = (uf * 3); asistentes = double.Parse(txtAsistentes.Text) + 1;
                                                                                    }
                                                                                }
                                                                            }
                                                                        }

                                                                        valorTotal = valorPersonalAdicional + valorAsistentes + valorBase;
                                                                        c.ValorBase = valorBase;
                                                                        c.ValorTotalContrato = valorTotal;

                                                                    }
                                                                }
                                                            }
                                                        }

                                                    }

                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }

                }
                #endregion Calculo tipo de evento Baby Shower

                #endregion ACTUALIZACION DEL  VALOR DEL EVENTO

                if (ContratoNoActualizado == 0)
                {
                    c.Termino = "No Definida";
                    contrato.ActualizarEntidad(c);
                    
                    MessageBox.Show("Contrato Modificado Correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    LimpiarControles();
                    VentanaContratoModoInicio();
                    txtNumeroContra2.Text = string.Empty;
                    txtVigenciaContrato.Text = string.Empty;
                    txtFechaContrato.Text = string.Empty;
                    txtValorEventoEscondido.Text = string.Empty;
                    txtNumeroContra.Text = string.Empty;
                    txtValorBase.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show(Info, "Alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Este contrato se encuentra inactivo. No se puede modificar", "Alerta", MessageBoxButton.OK, MessageBoxImage.Hand);
            }

        }

        private void BtnBuscarPorNumero_Click(object sender, RoutedEventArgs e)
        {
            OnBreakFinalEntities bbdd = new OnBreakFinalEntities();
            try
            {
                Contrato con = bbdd.Contrato.Where(c => c.Numero == txtNumeroContra.Text).First<Contrato>();
                
                Cliente cli = new Cliente();

                txtRutAsociado.Text = con.RutCliente;
                Cliente cliente = bbdd.Cliente.Where(c => c.RutCliente == txtRutAsociado.Text).First<Cliente>();
                txtRazonSocialAsociada.Text = cliente.RazonSocial;
                txtNombreAsociado.Text = cliente.NombreContacto;
                txtNumeroContra2.Text = con.Numero;
                txtVigenciaContrato.Text = con.EstadoContrato;
                txtFechaContrato.Text = con.Creacion.ToString();
                txtValorEventoEscondido.Text = con.ValorTotalContrato.ToString();
                cboxTipoEvento.Text = con.TipoEvento;
                cboxModalidad.Text = con.ModalidadServicio;
                txtPersonal.Text = con.PersonalAdicional.ToString();
                txtAsistentes.Text = con.Asistentes.ToString();
                dpFechaInicio.Text = con.Creacion.ToString();
                txtObservaciones.Text = con.Observaciones;
                txtFechaTermino.Text = con.Termino;
                txtValorBase.Text = con.ValorBase.ToString();

                if (txtVigenciaContrato.Text == "INACTIVO")
                {
                    txtVigenciaContrato.Foreground = Brushes.Red;
                }
                else
                {
                    txtVigenciaContrato.Foreground = Brushes.Green;
                }

                btnActualizarContrato.IsEnabled = true;
                btnTerminarContrato.IsEnabled = true;
                btnBuscarPorNumero.IsEnabled = false;
                btnBuscar.IsEnabled = false;
                cboxTipoEvento.IsEnabled = true;
                cboxModalidad.IsEnabled = true;
                txtPersonal.IsEnabled = true;
                txtAsistentes.IsEnabled = true;
                dpFechaInicio.IsEnabled = true;
                txtObservaciones.IsEnabled = true;

            }
            catch (Exception)
            {

                MessageBox.Show("Numero de Contrato no existe", "Atencion", MessageBoxButton.OK, MessageBoxImage.Hand);
                txtNumeroContra.Text = string.Empty;
            }
            


        }

        private void BtnTerminarContrato_Click(object sender, RoutedEventArgs e)                              
        {
            try
            {
                ServiceContrato contrato = new ServiceContrato();
                Contrato con = new Contrato();
                Cliente cli = new Cliente();
                if (txtFechaTermino.Text == "No Definida")
                {
                   

                    MessageBoxResult terminar = MessageBox.Show("¿Está seguro que quiere finalizar este contrato?", "Confirmar",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Question);

                    if (terminar == MessageBoxResult.Yes)
                    {
                        con.RutCliente = txtRutAsociado.Text;
                        cli.RazonSocial = txtRazonSocialAsociada.Text;
                        cli.NombreContacto = txtNombreAsociado.Text;
                        con.Numero = txtNumeroContra.Text;
                        con.EstadoContrato = txtVigenciaContrato.Text;
                        con.Creacion = DateTime.Parse(txtFechaContrato.Text);
                        con.ValorBase = double.Parse(txtValorBase.Text);
                        con.ValorTotalContrato = double.Parse(txtValorEventoEscondido.Text);
                        con.TipoEvento = cboxTipoEvento.Text;
                        con.ModalidadServicio = cboxModalidad.Text;
                        con.PersonalAdicional = double.Parse(txtPersonal.Text);
                        con.Asistentes = double.Parse(txtAsistentes.Text);
                        con.Creacion = DateTime.Parse(dpFechaInicio.Text);
                        con.Observaciones = txtObservaciones.Text;


                        estadoVigencia = "INACTIVO";
                        con.EstadoContrato = estadoVigencia;
                        txtVigenciaContrato.Text = con.EstadoContrato;
                        fechaTermino = DateTime.Now.ToString("dd/MM/yyyy HH:mm"); 
                        con.Termino = fechaTermino ;
                        string mensaje = "Detalle Termino Contrato:\n";

                       
                        contrato.ActualizarEntidad(con);
                        MessageBox.Show("Contrato Finalizado\n" + mensaje + "\n" + "* Vigencia Contrato: " + txtVigenciaContrato.Text + "\n* Fecha Termino: " + fechaTermino, "Información", MessageBoxButton.OK, MessageBoxImage.Information);

                        //Limpieza de Controles
                        LimpiarControles();
                        txtNumeroContra2.Text = string.Empty;
                        txtVigenciaContrato.Text = string.Empty;
                        txtFechaContrato.Text = string.Empty;
                        txtValorEventoEscondido.Text = string.Empty;
                        txtNumeroContra.Text = string.Empty;
                        txtValorBase.Text = string.Empty;

                        //Activacion de botones
                        btnBuscar.IsEnabled = true;
                        btnBuscarPorNumero.IsEnabled = true;

                    }
                }
                else
                {
                    MessageBox.Show("No es posible poner termino a este contrato. Se encuentra Inactivo", "Alerta", MessageBoxButton.OK, MessageBoxImage.Hand);
                }

            }
            catch (Exception d)
            {

                MessageBox.Show(d.Message);
            }
        }

        private void BtnBuscarContrato_Click(object sender, RoutedEventArgs e)
        {
            ListContratos ventana = new ListContratos();
            ventana.Owner = this;
            ventana.ShowDialog();
        }

        private void CboxModalidad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cboxModalidad.SelectedIndex == 0)
            {
                txtCargaValorBase.Text = "3 UF";
                
                txtCargaPersoanlBase.Text = "2";
                
            }
            else
            {
                if (cboxModalidad.SelectedIndex == 1)
                {
                    txtCargaValorBase.Text = "8 UF";
                    txtCargaPersoanlBase.Text = "6";
                }
                else
                {
                    if (cboxModalidad.SelectedIndex == 2)
                    {
                        txtCargaValorBase.Text = "12 UF";
                        txtCargaPersoanlBase.Text = "6";
                    }
                    else
                    {
                        if (cboxModalidad.SelectedIndex == 3)
                        {
                            txtCargaValorBase.Text = "25 UF";
                            txtCargaPersoanlBase.Text = "10";
                        }
                        else
                        {
                            if (cboxModalidad.SelectedIndex == 4)
                            {
                                txtCargaValorBase.Text = "35 UF";
                                txtCargaPersoanlBase.Text = "14";
                            }
                            else
                            {
                                if (cboxModalidad.SelectedIndex == 5)
                                {
                                    txtCargaValorBase.Text = "6 UF";
                                    txtCargaPersoanlBase.Text = "4";
                                }
                                else
                                {
                                    if (cboxModalidad.SelectedIndex == 6)
                                    {
                                        txtCargaValorBase.Text = "10 UF";
                                        txtCargaPersoanlBase.Text = "5";
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
