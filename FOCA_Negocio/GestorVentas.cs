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
    public class GestorVentas
    {
        public static void guardarVenta(Venta venta, List<DetalleVenta> listadetallesventa)
        {
            string conexionCadena = ConfigurationManager.ConnectionStrings["FOCAdbstring"].ConnectionString;
            SqlConnection connection = new SqlConnection();
            SqlTransaction transaction = null;
            try
            {
                connection.ConnectionString = conexionCadena;
                connection.Open();
                transaction = connection.BeginTransaction();
                string sql = "INSERT INTO VENTAS (nroFactura, fecha, cliente, monto)  VALUES (@NroFactura, @Fecha, @Cliente, @monto); SELECT @@Identity as ID;";
                SqlCommand comand = new SqlCommand();
                comand.CommandText = sql;
                comand.Connection = connection;
                comand.Transaction = transaction;
                comand.Parameters.AddWithValue("@NroFactura", venta.nroFactura);
                comand.Parameters.AddWithValue("@Fecha", venta.fecha);
                comand.Parameters.AddWithValue("@Cliente", venta.cliente);
                comand.Parameters.AddWithValue("@monto", venta.monto);
             

                int idVenta = Convert.ToInt32(comand.ExecuteScalar());

                //DETALLES INICIO                                                


                foreach (DetalleVenta item in listadetallesventa)
                {
                    string sqlVerStock = "select stock from ARTICULOS where id_Articulo = @idArticulo and disponible = 1";
                    SqlCommand comand4 = new SqlCommand();
                    comand4.CommandText = sqlVerStock;
                    comand4.Transaction = transaction;
                    comand4.Parameters.AddWithValue("@idArticulo", item.articulo);
                    int stockActual = Convert.ToInt32(comand.ExecuteScalar());
                    int restante = stockActual - item.cantidad;
                    if (restante < 0) throw new Exception();

                    string sqlDetalle = "INSERT INTO DETALLE_VENTA VALUES (@NroFactura, @Articulo, @Cantidad, @SubTotal)";
                    SqlCommand comand3 = new SqlCommand();
                    comand3.CommandText = sqlDetalle;
                    comand3.Connection = connection;
                    comand3.Transaction = transaction;
                    comand3.Parameters.AddWithValue("@NroFactura", item.nroFactura);
                    comand3.Parameters.AddWithValue("@Articulo", item.articulo);
                    comand3.Parameters.AddWithValue("@Cantidad", item.cantidad);
                    comand3.Parameters.AddWithValue("@SubTotal", item.subTotal);
                    comand3.ExecuteNonQuery();

                    string sqlupdateStock = "update Articulos set stock = @Restante where id_articulo = @idArticulo";
                    SqlCommand comand5 = new SqlCommand();
                    comand5.CommandText = sqlupdateStock;
                    comand5.Connection = connection;
                    comand5.Transaction = transaction;
                    comand5.Parameters.AddWithValue("@Restante", restante);
                    comand5.Parameters.AddWithValue("@idArticulo", item.articulo);
                    comand5.ExecuteNonQuery();

                }

                //DETALLES FIN       


                //sql = "Insert into AUDITORIA (fecha, descripcion) values (GETDATE(),@descripcion)";
                //SqlCommand comand2 = new SqlCommand();
                //comand2.CommandText = sql;
                //comand2.Connection = connection;
                //comand2.Transaction = transaction;
                //comand2.Parameters.AddWithValue("@descripcion", "Se grabó la reparacion con ID:" + idRepracion.ToString());
                //comand2.ExecuteNonQuery();

                transaction.Commit(); //confirmo los cambios                
            }
            catch (SqlException ex)
            {
                if (connection.State == ConnectionState.Open)
                    transaction.Rollback(); //Vuelvo atras los cambios
                throw new ApplicationException("Error al guardar la venta.");
            }
            catch (Exception nostock)
            {
                if (connection.State == ConnectionState.Open)
                    transaction.Rollback(); //Vuelvo atras los cambios
                throw new ApplicationException("Error al guardar la venta, no hay suficiente stock.");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
    }
    
}
