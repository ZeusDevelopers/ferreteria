﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;  //Referencia mysqldata
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Data;

namespace PVFP
{
    class ClsPerdida
    {

        
        #region MYSQL
     

        public void GuardarPerdida(int perdida_id, int producto_id, string cantidad, string motivo)
        {
            MySqlConnection conexion = ClsInicioSesion.ObtenerConexion();
            MySqlCommand _comando = new MySqlCommand(String.Format("INSERT INTO `Perdida`(`Perdida_ID`, `Producto_ID`, `Cantidad`, `Motivo`) VALUES ('" + 0 + "','" + producto_id + "','" + cantidad + "','" + motivo + "')"), conexion);
            MySqlDataReader _reader = _comando.ExecuteReader();
            conexion.Close();
        }      
        public DataTable CargarPerdida()
        {
            DataTable tb = new DataTable();
            MySqlConnection conexion = ClsInicioSesion.ObtenerConexion();
            MySqlCommand _comando = new MySqlCommand(String.Format("SELECT * FROM perdida"), conexion);
            //MySqlDataReader _reader = _comando.ExecuteReader();
            MySqlDataAdapter ad = new MySqlDataAdapter("select Perdida_ID as 'Perdida',producto.Nombre,Cantidad,Motivo from perdida" +
                " inner join producto on perdida.Producto_ID = producto.Producto_ID ; ", conexion);
            ad.Fill(tb);
            return tb;
        }
        public DataTable productso()
        {
            DataTable tb = new DataTable();
            MySqlConnection conexion = ClsInicioSesion.ObtenerConexion();
            MySqlCommand _comando = new MySqlCommand(String.Format("SELECT * FROM perdida"), conexion);
            //MySqlDataReader _reader = _comando.ExecuteReader();
            MySqlDataAdapter ad = new MySqlDataAdapter("select concat(Producto_ID,' - ' ,Nombre)  as campos from producto;", conexion);
            ad.Fill(tb);
            return tb;
        }

        #endregion

    }
}
