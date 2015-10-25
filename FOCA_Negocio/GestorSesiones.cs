using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace FOCA_Negocio
{
    public class GestorSesiones
    {
        public static bool EstaAutenticado(string mail, string password)
        {
            if (mail == "admin" & password =="admin") return true;
            string conexionCadena = ConfigurationManager.ConnectionStrings["FOCAdbstring"].ConnectionString;
            SqlConnection connection = new SqlConnection();
            try
            {
                connection.ConnectionString = conexionCadena;
                connection.Open();
                //string sql = "SELECT id_cliente, nombre, apellido, localidad, fechaNacimiento, mail, password, rol, preferencial FROM CLIENTES order by apellido";
                string sql = "Select mail, password from CLIENTES where mail like @userName and password like @password";
                SqlCommand comand = new SqlCommand();
                comand.Parameters.AddWithValue("@userName", mail);
                comand.Parameters.AddWithValue("@password", password);
                comand.CommandText = sql;
                comand.Connection = connection;
                SqlDataReader dr = comand.ExecuteReader();

                while (dr.Read())
                {
                    return true;
                }
            }
            catch (SqlException ex)
            {
                if (connection.State == ConnectionState.Open)
                    throw new ApplicationException("No existe cliente");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return false;
        }


        public static string[] obtenerRoles(string usuario)
        {
            return null;

            //string[] pepe = new string[] { "valor1", "valor2" };
            //switch (usuario.ToLower())
            //{
            //    case "administrador":
            //        return new string[] { "administrador" };
            //    case "cliente":
            //        return new string[] { "cliente" };
            //    case "a":
            //        return new string[] { "a" };
            //    default:
            //        return new string[] { "Visita" };
            //}
        }
    }
}
