using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FOCA_Entidades;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace FOCA_Negocio
{
    public class GestorRoles
    {
        public static List<Rol> ObtenerTodas()
        {
            List<Rol> roles = new List<Rol>();

            string conexionCadena = ConfigurationManager.ConnectionStrings["FOCAdbstring"].ConnectionString;

            SqlConnection connection = new SqlConnection();
            try
            {
                connection.ConnectionString = conexionCadena;
                connection.Open();
                string sql = "SELECT id_rol, descripcion FROM ROLES_USUARIO order by descripcion";
                SqlCommand comand = new SqlCommand();
                comand.CommandText = sql;
                comand.Connection = connection;
                SqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    roles.Add(
                        new Rol()
                        {
                            idRol = (int)dr["id_rol"],
                            descripcionRol = dr["descripcion"].ToString()
                        });
                }


            }
            catch (SqlException ex)
            {
                if (connection.State == ConnectionState.Open)
                    throw new ApplicationException("Error al cargar roles.");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return roles;
        }
    }
}
