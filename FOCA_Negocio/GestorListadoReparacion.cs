using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FOCA_Entidades;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using FOCA_Negocio;
namespace FOCA_Negocio
{
    public class GestorListadoReparacion
    {
        public static List<ListadoReparacion> obtenerReparaciones(string contieneFechaDesde, string contieneFechaHasta, string contieneCliente, string contieneEstado)
        {
            List<ListadoReparacion> listadoReparaciones = new List<ListadoReparacion>();

            string conexionCadena = ConfigurationManager.ConnectionStrings["FOCAdbstring"].ConnectionString;

            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = conexionCadena;
                connection.Open();

                string sql = "select  c.nombre as 'Nombre', c.apellido as 'Apellido', c.preferencial as 'Preferencial', r.fechaReparacion as 'Reparacion', r.fechaDevolucion as 'Devolucion', e.descripcion as 'Estado' from REPARACIONES as r JOIN CLIENTES as c ON(r.cliente = c.id_cliente) JOIN ESTADOS as e ON(r.estado = e.id_estado) ";
                SqlCommand comand = new SqlCommand();

                string where = "";
                if (contieneEstado != "")
                {
                    where += " and e.id_estado = @estado";
                    comand.Parameters.AddWithValue("@estado", contieneEstado);
                }
                if (contieneFechaDesde != "" & contieneFechaHasta != "")
                {
                    where += " and r.fechaReparacion BETWEEN @fechaDesde AND @fechaHasta";
                    comand.Parameters.AddWithValue("@fechaDesde", contieneFechaDesde);
                    comand.Parameters.AddWithValue("@fechaHasta", contieneFechaHasta);
                }
                if (contieneCliente != "")
                {
                    where += " and c.id_Cliente = @indexCliente";
                    comand.Parameters.AddWithValue("@indexCliente", contieneCliente);
                }

                if (where != "")
                {
                    where = " where " + where.Substring(5);
                    sql += where;
                }
                //   comand.Parameters.AddWithValue("@Orden", orden); //why

                sql += " ORDER BY c.nombre";
                comand.CommandText = sql;
                comand.Connection = connection;
                //Llenando un datatable con el resultado de la consulta
                //tablaarticulos.Load(comand.ExecuteReader());

                //En caso de llenar una lista con los articulos;
                SqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    ListadoReparacion lr = new ListadoReparacion();
                    lr.apellido = dr["Apellido"].ToString();
                    lr.nombre = dr["Nombre"].ToString();
                    lr.preferencial = Boolean.Parse(dr["Preferencial"].ToString());
                    lr.estado = dr["Estado"].ToString();
                    lr.fechareparacion = DateTime.Parse(dr["Reparacion"].ToString());
                    lr.fechadevolucion = DateTime.Parse(dr["Devolucion"].ToString());

                    listadoReparaciones.Add(lr);
                 
                }



            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al obtener los registros listados reparaciones.");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();

            }
            //Caso de manejar una lista de articulos
            return listadoReparaciones;

        }
    }
}
