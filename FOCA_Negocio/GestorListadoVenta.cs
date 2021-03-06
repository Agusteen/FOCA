﻿using System;
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
   public class GestorListadoVenta
    {
        public static List<Cliente> ObtenerClientes()
        {
            List<Cliente> clientes = new List<Cliente>();

            string conexionCadena = ConfigurationManager.ConnectionStrings["FOCAdbstring"].ConnectionString;

            SqlConnection connection = new SqlConnection();
            try
            {
                connection.ConnectionString = conexionCadena;
                connection.Open();
                string sql = "SELECT id_cliente ,nombre, apellido FROM CLIENTES order by nombre";
                SqlCommand comand = new SqlCommand();
                comand.CommandText = sql;
                comand.Connection = connection;
                SqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    clientes.Add(
                        new Cliente()
                        {
                            indexBD = (int)dr["id_cliente"],
                            nombre = (string)dr["nombre"],
                            apellido = (string)dr["apellido"]

                           
                        });
                }


            }
            catch (SqlException ex)
            {
                if (connection.State == ConnectionState.Open)
                    throw new ApplicationException("Error al buscar los clientes.");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return clientes;
        }//se puede borrar

        public static List<ListadoVenta> obtenerVentas(string contieneMonto, string contieneFechaDesde, string contieneFechaHasta, string contieneCliente, string orden)
        {

            List<ListadoVenta> listadoVenta = new List<ListadoVenta>();

            String conexionCadena = ConfigurationManager.ConnectionStrings["FOCAdbstring"].ConnectionString;
            SqlConnection connection = new SqlConnection();


            try
            {
                connection.ConnectionString = conexionCadena;
                connection.Open();

                string sql = "select v.id_Venta as 'idVenta', c.nombre as 'Nombre', c.apellido as 'Apellido', c.preferencial as 'Preferencial' , v.fecha as 'Fecha', v.monto as 'Monto' FROM VENTAS as v JOIN CLIENTES as c on (v.cliente = c.id_cliente) ";
                SqlCommand comand = new SqlCommand();

                string where = "";
                if (contieneMonto != "")
                {
                    where += " and v.monto <= @monto";
                    comand.Parameters.AddWithValue("@monto", contieneMonto);
                }
                if (contieneFechaDesde != "" & contieneFechaHasta != "")
                {
                    where += " and v.fecha BETWEEN @fechaDesde AND @fechaHasta";
                    comand.Parameters.AddWithValue("@fechaDesde", contieneFechaDesde);
                    comand.Parameters.AddWithValue("@fechaHasta", contieneFechaHasta);
                }
                if (contieneCliente != "")
                {
                    where += " and c.id_Cliente = @indexCliente";
                    comand.Parameters.AddWithValue("@indexCliente", contieneCliente);
                }

                if (where != "")
                {
                    where = " where " + where.Substring(5);
                    sql += where;
                }
                //   comand.Parameters.AddWithValue("@Orden", orden); //why

                sql += " ORDER BY "+ orden;
                comand.CommandText = sql;
                comand.Connection = connection;
                //Llenando un datatable con el resultado de la consulta
                //tablaarticulos.Load(comand.ExecuteReader());

                //En caso de llenar una lista con los articulos;
                SqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    ListadoVenta lv = new ListadoVenta();
                    lv.idVenta= int.Parse(dr["idVenta"].ToString());
                    lv.nombre = dr["Nombre"].ToString();
                    lv.apellido = dr["Apellido"].ToString();
                    lv.preferencial = Boolean.Parse(dr["Preferencial"].ToString());
                    lv.fecha = DateTime.Parse(dr["Fecha"].ToString());
                    lv.monto = Decimal.Parse(dr["Monto"].ToString());

                    listadoVenta.Add(lv);
                }



            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al obtener los registros listados ventas.");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();

            }
            //Caso de manejar una lista de articulos
            return listadoVenta;
          

        }

        public static DataTable obtenerDetalles(int idVentas)
        {
            string conexionCadena = ConfigurationManager.ConnectionStrings["FOCAdbstring"].ConnectionString;

            SqlConnection connection = new SqlConnection();
            DataTable dt = new DataTable();
            try
            {
                connection.ConnectionString = conexionCadena;
                connection.Open();
                string sql = "select * from ventas as v join detalle_venta as d ON (v.id_venta = d.nroFactura) ";
                SqlCommand comand = new SqlCommand();
                comand.CommandText = sql;
                comand.Connection = connection;
                dt.Load(comand.ExecuteReader());



            }
            catch (SqlException ex)
            {
                if (connection.State == ConnectionState.Open)
                    throw new ApplicationException("Error al buscar detalles.");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return dt;
        }
    }
}
