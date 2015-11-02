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
    public class GestorReparaciones
    {

        public static List<Estado> ObtenerEstados()
        {
            List<Estado> estados = new List<Estado>();

            string conexionCadena = ConfigurationManager.ConnectionStrings["FOCAdbstring"].ConnectionString;

            SqlConnection connection = new SqlConnection();
            try
            {
                connection.ConnectionString = conexionCadena;
                connection.Open();
                string sql = "SELECT id_estado, descripcion FROM ESTADOS ORDER BY descripcion";
                SqlCommand comand = new SqlCommand();
                comand.CommandText = sql;
                comand.Connection = connection;
                SqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    estados.Add(
                        new Estado()
                        {
                            idEstado = (int)dr["id_estado"],
                            descripcionEstado = dr["descripcion"].ToString()
                        });
                }


            }
            catch (SqlException ex)
            {
                if (connection.State == ConnectionState.Open)
                    throw new ApplicationException("Error al cargar estados.");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return estados;
        }

        public static List<Problema> ObtenerProblemas()
        {
            List<Problema> problemas = new List<Problema>();

            string conexionCadena = ConfigurationManager.ConnectionStrings["FOCAdbstring"].ConnectionString;

            SqlConnection connection = new SqlConnection();
            try
            {
                connection.ConnectionString = conexionCadena;
                connection.Open();
                string sql = "SELECT id_problema, descripcion, duracionEstimada FROM PROBLEMAS ORDER BY descripcion";
                SqlCommand comand = new SqlCommand();
                comand.CommandText = sql;
                comand.Connection = connection;
                SqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    problemas.Add(
                        new Problema()
                        {
                            idProblema = (int)dr["id_problema"],
                            descripcionProblema = dr["descripcion"].ToString(),
                            duracion = float.Parse(dr["duracionEstimada"].ToString())
                        });
                }


            }
            catch (SqlException ex)
            {
                if (connection.State == ConnectionState.Open)
                    throw new ApplicationException("Error al cargar problemas.");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return problemas;
        }

        public static void InsertarReparacion(Reparacion reparacion, List<DetalleReparacion> listaDetalles)
        {
            string conexionCadena = ConfigurationManager.ConnectionStrings["FOCAdbstring"].ConnectionString;
            SqlConnection connection = new SqlConnection();
            SqlTransaction transaction = null;
            try
            {
                connection.ConnectionString = conexionCadena;
                connection.Open();
                transaction = connection.BeginTransaction();
                string sql = "INSERT INTO REPARACIONES (fechaReparacion, fechaDevolucion, descripcion, equipoAReparar, estado, cliente, total)  VALUES (@FechaRep, @FechaDev, @Desc, @Equipo, @Estado, @Cliente, @Total); SELECT @@Identity as ID;";
                SqlCommand comand = new SqlCommand();
                comand.CommandText = sql;
                comand.Connection = connection;
                comand.Transaction = transaction;
                comand.Parameters.AddWithValue("@FechaRep", reparacion.fechaReparacion);
                comand.Parameters.AddWithValue("@FechaDev", reparacion.fechaDevolucion);
                comand.Parameters.AddWithValue("@Desc", reparacion.descripcionReparacion);
                comand.Parameters.AddWithValue("@Equipo", reparacion.equipo);
                comand.Parameters.AddWithValue("@Estado", reparacion.estado);
                comand.Parameters.AddWithValue("@Cliente", reparacion.cliente);
                comand.Parameters.AddWithValue("@Total", reparacion.total);

                int idRepracion = Convert.ToInt32(comand.ExecuteScalar());

                //DETALLES INICIO                                                
                
                
                foreach (DetalleReparacion detallei in listaDetalles)
                {
                    string sqlDetalle = "INSERT INTO DETALLE_REPARACION VALUES (@IDMaestro, @Problema, @SubTotal)";
                    SqlCommand comand3 = new SqlCommand();
                    comand3.CommandText = sqlDetalle;
                    comand3.Connection = connection;
                    comand3.Transaction = transaction;
                    comand3.Parameters.AddWithValue("@IDMaestro", idRepracion);
                    comand3.Parameters.AddWithValue("@Problema", detallei.problema);
                    comand3.Parameters.AddWithValue("@SubTotal", detallei.subTotal);
                    comand3.ExecuteNonQuery();
                }
                
                //DETALLES FIN       
                

                sql = "Insert into AUDITORIA (fecha, descripcion) values (GETDATE(),@descripcion)";
                SqlCommand comand2 = new SqlCommand();
                comand2.CommandText = sql;
                comand2.Connection = connection;
                comand2.Transaction = transaction;
                comand2.Parameters.AddWithValue("@descripcion", "Se grabó la reparacion con ID:" + idRepracion.ToString());
                comand2.ExecuteNonQuery();

                transaction.Commit(); //confirmo los cambios                
            }
            catch (SqlException ex)
            {
                if (connection.State == ConnectionState.Open)
                    transaction.Rollback(); //Vuelvo atras los cambios
                throw new ApplicationException("Error al guardar la reparacion.");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }      


    }
}
