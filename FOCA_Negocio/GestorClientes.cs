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
    public class GestorClientes
    {
        public static DataTable ObtenerTodos()
        {
            //List<Cliente> clientes = new List<Cliente>();
            string conexionCadena = ConfigurationManager.ConnectionStrings["FOCAdbstring"].ConnectionString;
            DataTable tablaClientes = new DataTable();
            SqlConnection connection = new SqlConnection();
            try
            {
                connection.ConnectionString = conexionCadena;
                connection.Open();
                //string sql = "SELECT id_cliente, nombre, apellido, dni, domicilio, localidad, telefono, fechaNacimiento, preferencial FROM CLIENTES order by apellido";
                string sql = "SELECT (c.apellido + ' ' + c.nombre) AS 'Nombre', c.dni AS 'DNI', c.domicilio AS 'Domicilio', l.descripcion AS 'Localidad', c.telefono AS 'Telefono', c.preferencial AS 'Preferencial' FROM CLIENTES AS c JOIN LOCALIDADES AS l ON (c.localidad = l.id_localidad)";
                SqlCommand comand = new SqlCommand();
                comand.CommandText = sql;
                comand.Connection = connection;
                tablaClientes.Load(comand.ExecuteReader());
                //SqlDataReader dr = comand.ExecuteReader();
                //while (dr.Read())
                //{
                //    clientes.Add(
                //        new Cliente()
                //        {
                //            indexBD = (int)dr["id_cliente"], 
                //            nombre = dr["nombre"].ToString(),
                //             apellido = dr["apellido"].ToString(),
                //             dni =(long)dr["dni"],
                //             domicilio = dr["domicilio"].ToString(),
                //             localidad = (int)dr["localidad"],
                //             telefono = (long)dr["telefono"],
                             
                            
                //            fechaNac = dr["fechaNacimiento"].ToString(),
                //            //intPreferencial = 1

                //        });
                //}
            }
            catch (SqlException ex)
            {
                if (connection.State == ConnectionState.Open)
                    throw new ApplicationException("Error al cargar clientes");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return tablaClientes;
        }
        
        public static void Insertar(Cliente cliente)
        {
            string conexionCadena = ConfigurationManager.ConnectionStrings["FOCAdbstring"].ConnectionString;
            SqlConnection connection = new SqlConnection();
            SqlTransaction transaction = null;
            try
            {
                connection.ConnectionString = conexionCadena;
                connection.Open();
                transaction = connection.BeginTransaction();
                string sql = "INSERT INTO CLIENTES (nombre, apellido, dni, domicilio, localidad, telefono, fechaNacimiento, preferencial)  values (@Nombre, @Apellido, @Dni, @Domicilio, @Localidad, @Telefono, @FechaNacimiento, @Preferencial); SELECT @@Identity as ID";
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
                comand.Parameters.AddWithValue("@Preferencial", cliente.intPreferencial);

                //cmd.ExecuteNonQuery();
                int idCliente = Convert.ToInt32(comand.ExecuteScalar());

                sql = "Insert into AUDITORIA (fecha, descripcion) values (GETDATE(),@descripcion)";
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

        public static void actualizarCliente(Cliente cliente)
        {
            string conexionCadena = ConfigurationManager.ConnectionStrings["FOCAdbstring"].ConnectionString;
            SqlConnection connection = new SqlConnection();
            SqlTransaction transaction = null;
            try
            {
                connection.ConnectionString = conexionCadena;
                connection.Open();
                transaction = connection.BeginTransaction();                
                string sql = "UPDATE CLIENTES SET nombre=@Nombre, apellido=@Apellido, dni=@Dni, domicilio=@Domicilio, localidad=@Localidad, telefono=@Telefono, fechaNacimiento=@FechaNacimiento, preferencial=@Preferencial WHERE id_cliente=@indexBD; SELECT @@Identity as ID";
                SqlCommand comand = new SqlCommand();
                comand.CommandText = sql;
                comand.Connection = connection;
                comand.Transaction = transaction;
                comand.Parameters.AddWithValue("@indexBD", cliente.indexBD);
                comand.Parameters.AddWithValue("@Nombre", cliente.nombre);
                comand.Parameters.AddWithValue("@Apellido", cliente.apellido);
                comand.Parameters.AddWithValue("@Dni", cliente.dni);
                comand.Parameters.AddWithValue("@Domicilio", cliente.domicilio);
                comand.Parameters.AddWithValue("@Localidad", cliente.localidad);
                comand.Parameters.AddWithValue("@Telefono", cliente.telefono);
                comand.Parameters.AddWithValue("@FechaNacimiento", cliente.fechaNac);
                comand.Parameters.AddWithValue("@Preferencial", cliente.intPreferencial);
                                
                int idCliente = Convert.ToInt32(comand.ExecuteScalar());

                sql = "Insert into AUDITORIA (fecha, descripcion) values (GETDATE(),@descripcion)";
                SqlCommand comand2 = new SqlCommand();
                comand2.CommandText = sql;
                comand2.Connection = connection;
                comand2.Transaction = transaction;
                comand2.Parameters.AddWithValue("@descripcion", "Se actualizo el cliente ID:" + idCliente.ToString());
                comand2.ExecuteNonQuery();

                transaction.Commit(); //confirmo los cambios                
            }
            catch (SqlException ex)
            {
                if (connection.State == ConnectionState.Open)
                    transaction.Rollback(); //Vuelvo atras los cambios
                throw new ApplicationException("Error al actualizar el cliente.");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public static void eliminarCliente(int indice)
        {
            string conexionCadena = ConfigurationManager.ConnectionStrings["FOCAdbstring"].ConnectionString;
            SqlConnection connection = new SqlConnection();
            SqlTransaction transaction = null;
            
            try
            {
                connection.ConnectionString = conexionCadena;
                connection.Open();
                transaction = connection.BeginTransaction();
                int? idCliente = null;

                try
                {
                    string sqlBuscaCliente = "SELECT id_cliente FROM CLIENTES WHERE id_cliente = @IdCliente";
                    SqlCommand comand0 = new SqlCommand();
                    comand0.CommandText = sqlBuscaCliente;
                    comand0.Connection = connection;
                    comand0.Transaction = transaction;
                    comand0.Parameters.AddWithValue("@IdCliente", indice);
                    idCliente = Convert.ToInt32(comand0.ExecuteScalar());

                }
                catch
                {
                    if (idCliente == null)
                    {
                        throw new ApplicationException("Error en la busqueda para eliminar un Cliente");
                    }


                }
                
                string sql = "DELETE FROM CLIENTES WHERE id_cliente = @indexBD; SELECT @@Identity as ID";
                SqlCommand comand = new SqlCommand();
                comand.CommandText = sql;
                comand.Connection = connection;
                comand.Transaction = transaction;
                comand.Parameters.AddWithValue("@indexBD", idCliente);

                comand.ExecuteNonQuery();

                string sqlinsertauditoria = "Insert into AUDITORIA (Fecha, descripcion) values (getdate(),@descripcion)";
                SqlCommand comand2 = new SqlCommand();
                comand2.CommandText = sqlinsertauditoria;
                comand2.Connection = connection;
                comand2.Transaction = transaction;
                comand2.Parameters.AddWithValue("@descripcion", "Se elimino el cliente ID:" + idCliente.ToString());
                comand2.ExecuteNonQuery();
                
                transaction.Commit(); //confirmo los cambios
            }
            catch (SqlException ex)
            {
                if (connection.State == ConnectionState.Open)
                    transaction.Rollback(); //Vuelvo atras los cambios
                throw new ApplicationException("Error al eliminar el cliente.");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

    }
}
