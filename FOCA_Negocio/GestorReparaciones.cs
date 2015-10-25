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
    }
}
