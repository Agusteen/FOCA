using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FOCA_Entidades;
using System.Data.SqlClient;
using System.Data;

namespace FOCA_Negocio
{
    public class GestorClientes
    {
        public static void Insertar(Cliente cliente)
        {
            //TODO1: Crear bd y poner en el webconfig

            string conexionCadena = "Data Source=maquis;Initial Catalog=Pymes;User ID=avisuales2;Password=avisuales2";
            SqlConnection connection = new SqlConnection();
            SqlTransaction transaction = null;
            try
            {
                connection.ConnectionString = conexionCadena;
                connection.Open();
                transaction = connection.BeginTransaction();
                string sql = "INSERT INTO CLIENTES nombre, apellido, dni, domicilio, localidad, telefono, fechaNacimiento, preferencial  values @Nombre, @Apellido, @Dni, @Domicilio, @Localidad, @Telefono, @FechaNacimiento, @Preferencial; SELECT @@Identity as ID";
                SqlCommand comand = new SqlCommand();
                comand.CommandText = sql;
                comand.Connection = connection;
                comand.Transaction = transaction;
                comand.Parameters.AddWithValue("@Nombre", cliente.nombre);
                comand.Parameters.AddWithValue("@Apellido", cliente.apellido);
                comand.Parameters.AddWithValue("@Dni", cliente.dni);
                comand.Parameters.AddWithValue("@Domicilio", cliente.domicilio);
                comand.Parameters.AddWithValue("@Localidad", cliente.localidad);
                comand.Parameters.AddWithValue("@Telefono", cliente.telefono);
                comand.Parameters.AddWithValue("@FechaNacimiento", cliente.fechaNac);
                comand.Parameters.AddWithValue("@Preferencial", cliente.preferencial);

                //cmd.ExecuteNonQuery();
                int idCliente = Convert.ToInt32(comand.ExecuteScalar());

                sql = "Insert into AUDITORIA (Fecha, descripcion) values (getdate(),@descripcion)";
                SqlCommand comand2 = new SqlCommand();
                comand2.CommandText = sql;
                comand2.Connection = connection;
                comand2.Transaction = transaction;
                comand2.Parameters.AddWithValue("@descripcion", "Se grabó el cliente ID:" + idCliente.ToString());
                comand2.ExecuteNonQuery();

                transaction.Commit(); //confirmo los cambios

                cliente.indexBD = idCliente;
            }
            catch (SqlException ex)
            {
               if (connection.State == ConnectionState.Open)
               transaction.Rollback(); //Vuelvo atras los cambios
                throw new ApplicationException("Error al guardar el cliente.");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

    }
}
