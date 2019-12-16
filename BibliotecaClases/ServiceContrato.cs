using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BibliotecaClases
{
    public class ServiceContrato : ClaseAbstracta<Contrato>
    {
        public override void ActualizarEntidad(Contrato entity)
        {
            Contrato con = bbdd.Contrato.Where(c => c.Numero == entity.Numero).First<Contrato>();
            if (con == null)
            {
                throw new ArgumentException("Contrato no encontrado");
            }
            else
            {
                con.Numero = entity.Numero;
                con.Creacion = entity.Creacion;
                con.Termino = entity.Termino;
                con.RutCliente = entity.RutCliente;
                con.TipoEvento = entity.TipoEvento;
                con.Asistentes = entity.Asistentes;
                con.PersonalAdicional = entity.PersonalAdicional;
                con.EstadoContrato = entity.EstadoContrato;
                con.ValorTotalContrato = entity.ValorTotalContrato;
                con.Observaciones = entity.Observaciones;

                bbdd.SaveChanges();

            }
        }

        public override void AgregarEntidad(Contrato entity)
        {
            bbdd.Contrato.Add(entity);
            bbdd.SaveChanges();
        }

        public override void EliminarEntidad(object pk)
        {
            throw new NotImplementedException();
        }

        public override Contrato ObtenerEntidad(object pk)
        {
            return bbdd.Contrato.Where(c => c.RutCliente == pk).First<Contrato>();
        }

        public override List<Contrato> ObtenerEntidades()
        {
            return bbdd.Contrato.ToList<Contrato>(); 
        }

        public void llenarData(DataGrid data)
        {


            DataTable dt = new DataTable();
            DataColumn Numero = new DataColumn("Numero Contrato", typeof(string));
            DataColumn Creacion = new DataColumn("Fecha Inicio", typeof(string));
            DataColumn Termino = new DataColumn("Fecha Termino", typeof(string));
            DataColumn RutCliente = new DataColumn("Rut Cliente", typeof(string));
            DataColumn TipoEvento = new DataColumn("Tipo Evento", typeof(string));
            DataColumn ModalidadServicio = new DataColumn("Modalidad Servicio", typeof(string));
            DataColumn Asistentes = new DataColumn("Cant Asistentes", typeof(string));
            DataColumn ValorBase = new DataColumn("Valor Base", typeof(string));
            DataColumn PersonalAdicional = new DataColumn("Personal Adicional", typeof(string));
            DataColumn EstadoContrato = new DataColumn("Estado Contrato", typeof(string));
            DataColumn ValorTotalContrato = new DataColumn("Valor Total", typeof(string));
            DataColumn Observaciones = new DataColumn("Observaciones", typeof(string));


            dt.Columns.Add(Numero);
            dt.Columns.Add(Creacion);
            dt.Columns.Add(Termino);
            dt.Columns.Add(RutCliente);
            dt.Columns.Add(TipoEvento);
            dt.Columns.Add(ModalidadServicio);
            dt.Columns.Add(Asistentes);
            dt.Columns.Add(ValorBase);
            dt.Columns.Add(PersonalAdicional);
            dt.Columns.Add(EstadoContrato);
            dt.Columns.Add(ValorTotalContrato);
            dt.Columns.Add(Observaciones);

            foreach (Contrato c in this.ObtenerEntidades())
            {

                DataRow row = dt.NewRow();

                row[0] = c.Numero;
                row[1] = c.Creacion;
                row[2] = c.Termino;
                row[3] = c.RutCliente;
                row[4] = c.TipoEvento;
                row[5] = c.ModalidadServicio;
                row[6] = c.Asistentes;
                row[7] = c.ValorBase;
                row[8] = c.PersonalAdicional;
                row[9] = c.EstadoContrato;
                row[10] = c.ValorTotalContrato;
                row[11] = c.Observaciones;

                dt.Rows.Add(row);
            }
            data.ItemsSource = dt.DefaultView;
            data.IsReadOnly = false;
            data.Items.Refresh();
        }

    }
}
