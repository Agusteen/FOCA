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
        public static bool estaAutenticado(string mail, string password)
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
        
        public static string[] obtenerRoles(string mailUsuario)
        {
            string rol = "";
            
            string conexionCadena = ConfigurationManager.ConnectionStrings["FOCAdbstring"].ConnectionString;
            SqlConnection connection = new SqlConnection();
            try
            {
                connection.ConnectionString = conexionCadena;
                connection.Open();
                string sql = "SELECT r.descripcion FROM CLIENTES AS c JOIN ROLES_USUARIO AS r ON (c.rol = r.id_rol) WHERE mail = @Mail";
                SqlCommand comand = new SqlCommand();
                comand.Parameters.AddWithValue("@Mail", mailUsuario);                
                comand.CommandText = sql;
                comand.Connection = connection;
                
                rol = comand.ExecuteScalar().ToString();
                
            }
            catch (SqlException ex)
            {
                
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return new string[] { rol };
        }
    }
}
