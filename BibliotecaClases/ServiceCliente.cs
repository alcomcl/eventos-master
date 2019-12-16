using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BibliotecaClases
{
    public class ServiceCliente : ClaseAbstracta<Cliente>
    {
        public override void ActualizarEntidad(Cliente entity)
        {
            Cliente cl = bbdd.Cliente.Where(c => c.RutCliente == entity.RutCliente).First<Cliente>();
            if (cl == null)
            {
                throw new ArgumentException("Cliente no encontrado");
            }
            else
            {
                cl.RutCliente = entity.RutCliente;
                cl.RazonSocial = entity.RazonSocial;
                cl.NombreContacto = entity.NombreContacto;
                cl.MailContacto = entity.MailContacto;
                cl.Direccion = entity.Direccion;
                cl.Telefono = entity.Telefono;
                cl.IdActividadEmpresa = entity.IdActividadEmpresa;
                cl.IdTipoEmpresa = entity.IdTipoEmpresa;

                bbdd.SaveChanges();

            }
        }

        public override void AgregarEntidad(Cliente entity)
        {
            bbdd.Cliente.Add(entity);
            bbdd.SaveChanges();
        }

        public override void EliminarEntidad(object pk)
        {
            Cliente cl = bbdd.Cliente.Where(c => c.RutCliente == pk).First<Cliente>();
            if (cl == null)
            {
                throw new ArgumentException("Cliente no encontrado");
            }
            else
            {
                bbdd.Cliente.Remove(cl);
                bbdd.SaveChanges();
            }
        }

        public override Cliente ObtenerEntidad(object pk)
        {
            return bbdd.Cliente.Where(c => c.RutCliente == pk).First<Cliente>();
        }

        public override List<Cliente> ObtenerEntidades()
        {
            return bbdd.Cliente.ToList<Cliente>();
        }

        public void llenarData(DataGrid data)
        {


            DataTable dt = new DataTable();
            DataColumn RutCliente = new DataColumn("Rut Cliente", typeof(string));
            DataColumn RazonSocial = new DataColumn("Razon Social", typeof(string));
            DataColumn NombreContacto = new DataColumn("Nombre Contacto", typeof(string));
            DataColumn Mail = new DataColumn("Email Contacto", typeof(string));
            DataColumn Direccion = new DataColumn("Direccion", typeof(string));
            DataColumn Telefono = new DataColumn("Telefono", typeof(string));
            DataColumn IdActividaEmpresa = new DataColumn("Actividad Empresa", typeof(string));
            DataColumn IdTipoEmpresa = new DataColumn("Tipo Empresa", typeof(string));

            dt.Columns.Add(RutCliente);
            dt.Columns.Add(RazonSocial);
            dt.Columns.Add(NombreContacto);
            dt.Columns.Add(Mail);
            dt.Columns.Add(Direccion);
            dt.Columns.Add(Telefono);
            dt.Columns.Add(IdActividaEmpresa);
            dt.Columns.Add(IdTipoEmpresa);

            foreach (Cliente c in this.ObtenerEntidades())
            {

                DataRow row = dt.NewRow();

                row[0] = c.RutCliente;
                row[1] = c.RazonSocial;
                row[2] = c.NombreContacto;
                row[3] = c.MailContacto;
                row[4] = c.Direccion;
                row[5] = c.Telefono;
                row[6] = c.IdActividadEmpresa;
                row[7] = c.IdActividadEmpresa;

                dt.Rows.Add(row);
            }
            data.ItemsSource = dt.DefaultView;
            data.IsReadOnly = false;
            data.Items.Refresh();
        }

    }
}
