using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FOCA_Entidades;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace FOCA_Negocio
{
   public class GestorListadoVenta
    {
        public static List<Cliente> ObtenerClientes()
        {
            List<Cliente> clientes = new List<Cliente>();

            string conexionCadena = ConfigurationManager.ConnectionStrings["FOCAdbstring"].ConnectionString;

            SqlConnection connection = new SqlConnection();
            try
            {
                connection.ConnectionString = conexionCadena;
                connection.Open();
                string sql = "SELECT id_cliente ,nombre, apellido FROM CLIENTES order by nombre";
                SqlCommand comand = new SqlCommand();
                comand.CommandText = sql;
                comand.Connection = connection;
                SqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    clientes.Add(
                        new Cliente()
                        {
                            indexBD = (int)dr["id_cliente"],
                            nombre = (string)dr["nombre"],
                            apellido = (string)dr["apellido"]

                           
                        });
                }


            }
            catch (SqlException ex)
            {
                if (connection.State == ConnectionState.Open)
                    throw new ApplicationException("Error al buscar los clientes.");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return clientes;
        }

        public static List<ListadoVenta> obtenerVentas(string contieneMonto, string contieneFecha, string contieneCliente)
        {

            List<ListadoVenta> listadoVenta = new List<ListadoVenta>();

            String conexionCadena = ConfigurationManager.ConnectionStrings["FOCAdbstring"].ConnectionString;
            SqlConnection connection = new SqlConnection();


            try
            {
                connection.ConnectionString = conexionCadena;
                connection.Open();

                string sql = "select c.nombre as 'Nombre', c.apellido as 'Apellido', c.preferencial as 'Preferencial' , v.fecha as 'Fecha', v.monto as 'Monto' FROM VENTAS as v JOIN CLIENTES as c on (v.cliente = c.id_cliente)
                if (!("").Equals(contieneMonto) & !("").Equals(contieneFecha) & !("").Equals(contieneMonto))
                {
                    sql += " where";
                    if (contieneMonto != "")
                    {
                        sql += "v.monto = @monto";
                    }
                        string meter = "where c.id_Cliente = @indexCliente, v.monto = @monto, fecha like @fecha";
                        }
                    

                SqlCommand comand = new SqlCommand();
                //   comand.Parameters.AddWithValue("@Orden", orden); //why
                //comand.Parameters.AddWithValue("@Contiene", contiene);

                comand.CommandText = sql;
                comand.Connection = connection;
                //Llenando un datatable con el resultado de la consulta
                //tablaarticulos.Load(comand.ExecuteReader());

                //En caso de llenar una lista con los articulos;
                SqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    //Articulo art = new Articulo();
                    //art.indexBD = int.Parse(dr["ID Articulo"].ToString());
                    //art.descripcion = dr["Descripcion"].ToString();
                    //if (dr["precio"] != DBNull.Value) art.precio = float.Parse(dr["Precio"].ToString());
                    //if (dr["stock"] != DBNull.Value) art.stock = int.Parse(dr["Stock"].ToString());
                    //art.disponible = Boolean.Parse(dr["Disponible"].ToString()); //interfaz selecciona una opcion por defecto
                    //art.tipoArticulo = int.Parse(dr["TipoID"].ToString());  //al momento de la carga se selecciona por lo menos 1 por defecto
                    //art.tipoArticuloString = dr["Familia"].ToString();
                    //listaArticulos.Add(art);
                }



            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al cargar los Articulos.");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();

            }
            //Caso de manejar una lista de articulos
            return listadoVenta;
            //return tablaarticulos;

        }
    }
}
