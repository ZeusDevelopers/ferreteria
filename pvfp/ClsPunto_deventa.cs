﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;  //Referencia mysqldata
using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Collections;


namespace PVFP
{
    class ClsPunto_deventa
    {
        public DataTable VerTodosProductos(int tipo,string param,bool ad)
        {
            //   string opcion = tipo == 2 ? "Ganancia_Mayoreo`+Precio_Costo" : "`Ganancia_Venta`+Precio_Costo ";

            string conca = "";
            if (tipo == 2)
            {
                //Precio_Costo + ((Precio_Costo*Ganancia_Venta)/100)
                //Round(Precio_Costo + ((Precio_Costo*Ganancia_Venta)/100),2) 
                conca = ad ? " Round(Precio_Costo + ((Precio_Costo*Ganancia_Mayoreo)/100),2) " : " Round(Precio_Costo + ((Precio_Costo*Ganancia_Venta)/100),2)  ";
                //conca = ad ? "`Ganancia_Mayoreo`+Precio_Costo" : "`Ganancia_Venta`+Precio_Costo";
            }
            else
            {
                // conca = ad ? "SUM(`Ganancia_Mayoreo`+Precio_Costo)" : "SUM(`Ganancia_Venta`+Precio_Costo)";
                 conca = ad ? " Round(Precio_Costo + ((Precio_Costo*Ganancia_Mayoreo)/100),2) " : " Round(Precio_Costo + ((Precio_Costo*Ganancia_Venta)/100),2)  ";
            }
                string comando = "SELECT `Codigodebarra` as 'Codigo de Barras',`Nombre`," + conca+ " as 'Precio de venta', A_Piso as 'Inventario',UM,producto.Producto_ID,producto.Folio FROM `producto` inner join  almacen on almacen.Producto_ID = producto.Producto_ID ";
            switch (tipo)
            {
                case 0:
                    comando += "where producto.Codigodebarra=@v";
                    break;
                case 1:
                    comando += "where producto.Folio=@v";
                    break;
                case 2:
                    comando += " where producto.Nombre like concat('%',@v,'%') ";
                    break;
                default:
                    break;
            }
            comando += "  and almacen.A_Piso>0 ";
            DataTable tabla = new DataTable();
            MySqlConnection conexion = ClsInicioSesion.ObtenerConexion();
            MySqlCommand _comando = new MySqlCommand(comando, conexion);
            _comando.Parameters.AddWithValue("@v",param);
            MySqlDataAdapter _dataAdapter = new MySqlDataAdapter(_comando);
            _dataAdapter.Fill(tabla);           
            conexion.Close();
            return tabla;
        }
    }
}
