using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FOCA_Entidades;
using System.Data;
using System.Data.SqlClient;

namespace FOCA_Negocio
{
    public class GestorLocalidades
    {
        public static List<Localidad> ObtenerTodas()
        {
            List<Localidad> localidades = new List<Localidad>();

            string conexionCadena = "workstation id=FOCAgadgets.mssql.somee.com;packet size=4096;user id=focagadgets_SQLLogin_1;pwd=xby9rtbboe;data source=FOCAgadgets.mssql.somee.com;persist security info=False;initial catalog=FOCAgadgets";
            SqlConnection connection = new SqlConnection();            
            try
            {
                connection.ConnectionString = conexionCadena;
                connection.Open();                
                string sql = "SELECT id_localidad, descripcion FROM LOCALIDADES order by descripcion";
                SqlCommand comand = new SqlCommand();
                comand.CommandText = sql;
                comand.Connection = connection;   
                SqlDataReader dr = comand.ExecuteReader();
                while(dr.Read())
                {
                    localidades.Add(
                        new Localidad()
                        {
                            idLocalidad = (int)dr["id_localidad"],
                            Nombre = dr["descripcion"].ToString()
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
            return localidades;
        }

    }

}


    

