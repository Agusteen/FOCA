using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FOCA_Entidades;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace FOCA_Negocio
{
    public class GestorArticulos
    {
       
            public static List<TipoArticulo> ObtenerTiposArticulos()
            {
                List<TipoArticulo> tiposArticulos = new List<TipoArticulo>();

                string conexionCadena = ConfigurationManager.ConnectionStrings["FOCAdbstring"].ConnectionString;

                SqlConnection connection = new SqlConnection();
                try
                {
                    connection.ConnectionString = conexionCadena;
                    connection.Open();
                    string sql = "SELECT id_tipoArticulo, descripcion FROM TIPOS_ARTICULO order by descripcion";
                    SqlCommand comand = new SqlCommand();
                    comand.CommandText = sql;
                    comand.Connection = connection;
                    SqlDataReader dr = comand.ExecuteReader();
                    while (dr.Read())
                    {
                    tiposArticulos.Add(
                            new TipoArticulo()
                            {
                                indexBD = (int)dr["id_tipoArticulo"],
                                descripcion = dr["descripcion"].ToString()
                            });
                    }


                }
                catch (SqlException ex)
                {
                    if (connection.State == ConnectionState.Open)
                        throw new ApplicationException("Error al cargar localidades.");
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
                return tiposArticulos;
            }

     


    }
}
