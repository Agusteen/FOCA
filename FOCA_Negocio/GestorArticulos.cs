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

        public static List<TipoArticulo> obtenerTiposArticulos()
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
                                descripcion = dr["descripcion"].ToString() //should be unique in  BD
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

        public static List<Articulo> obtenerArticulos()
        {
            List<Articulo> listaArticulos = new List<Articulo>();
           
            String conexionCadena = ConfigurationManager.ConnectionStrings["FOCAdbstring"].ConnectionString;
            SqlConnection connection = new SqlConnection();


            try
            {
                connection.ConnectionString = conexionCadena;
                connection.Open();

                string sql = "SELECT a.id_articulo as 'ID Articulo', a.descripcion as 'Descripcion', a.precio as 'Precio', a.stock as 'Stock', a.disponible as 'Disponible',a.tipo as 'TipoID', ta.descripcion as 'Tipo' from ARTICULOS as a JOIN TIPOS_ARTICULO as ta ON (a.tipo = ta.id_tipoArticulo)";
                SqlCommand comand = new SqlCommand();
                comand.CommandText = sql;
                comand.Connection = connection;
                //Llenando un datatable con el resultado de la consulta
                //tablaarticulos.Load(comand.ExecuteReader());

                //En caso de llenar una lista con los articulos;
                SqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    Articulo art = new Articulo();
                    art.indexBD = int.Parse(dr["ID Articulo"].ToString());
                    art.descripcion = dr["Descripcion"].ToString();
                    if (dr["precio"] != DBNull.Value) art.precio = float.Parse(dr["Precio"].ToString());
                    if (dr["stock"] != DBNull.Value) art.stock = int.Parse(dr["Stock"].ToString());
                    art.disponible = Boolean.Parse(dr["Disponible"].ToString()); //interfaz selecciona una opcion por defecto
                    art.tipoArticulo = int.Parse(dr["TipoID"].ToString());  //al momento de la carga se selecciona por lo menos 1 por defecto
                    art.tipoArticuloString = dr["Tipo"].ToString();
                    listaArticulos.Add(art);
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
            return listaArticulos;
            //return tablaarticulos;
        }

        public static Articulo obtenerArticulo(int indexBD)
        {
            Articulo art = new Articulo();
            String conexionCadena = ConfigurationManager.ConnectionStrings["FOCAdbstring"].ConnectionString;
            SqlConnection connection = new SqlConnection();


            try
            {
                connection.ConnectionString = conexionCadena;
                connection.Open();

                string sql = "SELECT a.id_articulo as 'ID Articulo', a.descripcion as 'Descripcion', a.precio as 'Precio', a.stock as 'Stock', a.disponible as 'Disponible',a.tipo as 'TipoID', ta.descripcion as 'Tipo' from ARTICULOS as a JOIN TIPOS_ARTICULO as ta ON (a.tipo = ta.id_tipoArticulo) WHERE a.id_articulo = @indexBD";
                SqlCommand comand = new SqlCommand();
              
                comand.CommandText = sql;
                comand.Connection = connection;
                comand.Parameters.AddWithValue("@indexBD", indexBD);

                SqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    art.indexBD = int.Parse(dr["ID Articulo"].ToString());
                    art.descripcion = dr["Descripcion"].ToString();
                    if (dr["precio"] != DBNull.Value) art.precio = float.Parse(dr["Precio"].ToString());
                    if (dr["stock"] != DBNull.Value) art.stock = int.Parse(dr["Stock"].ToString());
                    art.disponible = Boolean.Parse(dr["Disponible"].ToString()); //interfaz selecciona una opcion por defecto
                    art.tipoArticulo = int.Parse(dr["TipoID"].ToString());  //al momento de la carga se selecciona por lo menos 1 por defecto
                    art.tipoArticuloString = dr["Tipo"].ToString();
                    
                }
                
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al obtener el Articulos.");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();

            }
             

            return art;
        }



        public static void insertarArticulo(Articulo articulo)
        {
            string conexionCadena = ConfigurationManager.ConnectionStrings["FOCAdbstring"].ConnectionString;
            SqlConnection connection = new SqlConnection();
            SqlTransaction transaction = null;
            try
            {
                connection.ConnectionString = conexionCadena;
                connection.Open();
                transaction = connection.BeginTransaction();
                string sql = "INSERT INTO ARTICULOS (descripcion, precio, stock, disponible, tipo) values (@Descripcion, @Precio, @Stock, @Disponible, @tipoArticulo); SELECT @@Identity as ID;";
                SqlCommand comand = new SqlCommand();
                comand.CommandText = sql;
                comand.Connection = connection;
                comand.Transaction = transaction;
                comand.Parameters.AddWithValue("@Descripcion", articulo.descripcion);
                comand.Parameters.AddWithValue("@Precio", articulo.precio);
                comand.Parameters.AddWithValue("@Stock", articulo.stock);
                comand.Parameters.AddWithValue("@Disponible", articulo.intDisponible);
                comand.Parameters.AddWithValue("@tipoArticulo", articulo.tipoArticulo);


                //cmd.ExecuteNonQuery();
                int idArticulo = Convert.ToInt32(comand.ExecuteScalar());

                sql = "Insert into AUDITORIA (Fecha, descripcion) values (getdate(),@descripcion)";
                SqlCommand comand2 = new SqlCommand();
                comand2.CommandText = sql;
                comand2.Connection = connection;
                comand2.Transaction = transaction;
                comand2.Parameters.AddWithValue("@descripcion", "Se grabó el Articulo ID:" + idArticulo.ToString());
                comand2.ExecuteNonQuery();

                transaction.Commit(); //confirmo los cambios

                articulo.indexBD = idArticulo;
            }
            catch (SqlException ex)
            {
                if (connection.State == ConnectionState.Open)
                    transaction.Rollback(); //Vuelvo atras los cambios
                throw new ApplicationException("Error al guardar el Articulo.");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }


        public static void modificarArticulo(Articulo articulo)
        {
            string conexionCadena = ConfigurationManager.ConnectionStrings["FOCAdbstring"].ConnectionString;
            SqlConnection connection = new SqlConnection();
            SqlTransaction transaction = null;
            try
            {
                connection.ConnectionString = conexionCadena;
                connection.Open();
                transaction = connection.BeginTransaction();
                int? idArticulo = null; 

                try
                {
                    string sqlsearchforart = "SELECT id_articulo from ARTICULOS where descripcion = @descripcion";
                    SqlCommand comand0 = new SqlCommand();
                    comand0.CommandText = sqlsearchforart;
                    comand0.Connection = connection;
                    comand0.Transaction = transaction;
                    comand0.Parameters.AddWithValue("@descripcion", articulo.descripcion);
                    idArticulo = Convert.ToInt32(comand0.ExecuteScalar());
                    articulo.indexBD =(int)idArticulo;

                }
                catch
                {
                    if (idArticulo == null)
                    {
                        throw new ApplicationException("Error en la busqueda para actualizacion del Articulo.");
                    }
                       

                }

                string sqlupdate = "Update ARTICULOS set descripcion=@Descripcion, precio=@Precio, stock=@Stock, disponible=@Disponible, tipo=@tipoArticulo where id_articulo = @indexBD";
                SqlCommand comand = new SqlCommand();
                comand.CommandText = sqlupdate;
                comand.Connection = connection;
                comand.Transaction = transaction;
                comand.Parameters.AddWithValue("@indexBD", articulo.indexBD);
                comand.Parameters.AddWithValue("@Descripcion", articulo.descripcion);
                comand.Parameters.AddWithValue("@Precio", articulo.precio);
                comand.Parameters.AddWithValue("@Stock", articulo.stock);
                comand.Parameters.AddWithValue("@Disponible", articulo.intDisponible);
                comand.Parameters.AddWithValue("@tipoArticulo", articulo.tipoArticulo);


                comand.ExecuteNonQuery();
                //idArticulo = Convert.ToInt32(comand.ExecuteScalar());

                string sqlinsertauditoria = "Insert into AUDITORIA (Fecha, descripcion) values (getdate(),@descripcion)";
                SqlCommand comand2 = new SqlCommand();
                comand2.CommandText = sqlinsertauditoria;
                comand2.Connection = connection;
                comand2.Transaction = transaction;
                comand2.Parameters.AddWithValue("@descripcion", "Se actualizó el Articulo ID:" + articulo.indexBD.ToString());
                comand2.ExecuteNonQuery();

                transaction.Commit(); //confirmo los cambios

               
            }
            catch (SqlException ex)
            {
                if (connection.State == ConnectionState.Open)
                    transaction.Rollback(); //Vuelvo atras los cambios
                throw new ApplicationException("Error al guardar el Articulo.");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

        }


        public static void eliminarArticulo(int indice)
        {
            string conexionCadena = ConfigurationManager.ConnectionStrings["FOCAdbstring"].ConnectionString;
            SqlConnection connection = new SqlConnection();
            SqlTransaction transaction = null;
            try
            {
                connection.ConnectionString = conexionCadena;
                connection.Open();
                transaction = connection.BeginTransaction();
                int? idArticulo = null;

                try
                {
                    string sqlsearchforart = "SELECT id_articulo from ARTICULOS where id_articulo = @IdArticulo";
                    SqlCommand comand0 = new SqlCommand();
                    comand0.CommandText = sqlsearchforart;
                    comand0.Connection = connection;
                    comand0.Transaction = transaction;
                    comand0.Parameters.AddWithValue("@IdArticulo", indice);
                    idArticulo = Convert.ToInt32(comand0.ExecuteScalar());

                }
                catch
                {
                    if (idArticulo == null)
                    {
                        throw new ApplicationException("Error en la busqueda para actualizacion del Articulo.");
                    }


                }

                string sqldelete = "DELETE FROM ARTICULOS WHERE id_articulo = @indexBD; SELECT @@Identity as ID";
                SqlCommand comand = new SqlCommand();
                comand.CommandText = sqldelete;
                comand.Connection = connection;
                comand.Transaction = transaction;
                comand.Parameters.AddWithValue("@indexBD", idArticulo);//indice tambien puede ser.


                comand.ExecuteNonQuery();
                //idArticulo = Convert.ToInt32(comand.ExecuteScalar());

                string sqlinsertauditoria = "Insert into AUDITORIA (Fecha, descripcion) values (getdate(),@descripcion)";
                SqlCommand comand2 = new SqlCommand();
                comand2.CommandText = sqlinsertauditoria;
                comand2.Connection = connection;
                comand2.Transaction = transaction;
                comand2.Parameters.AddWithValue("@descripcion", "Se elimino el Articulo ID:" + idArticulo.ToString());
                comand2.ExecuteNonQuery();

                transaction.Commit(); //confirmo los cambios


            }
            catch (SqlException ex)
            {
                if (connection.State == ConnectionState.Open)
                    transaction.Rollback(); //Vuelvo atras los cambios
                throw new ApplicationException("Error al guardar el Articulo.");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }


    }
}
