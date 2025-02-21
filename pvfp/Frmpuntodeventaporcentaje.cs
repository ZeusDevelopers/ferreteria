﻿using MySql.Data.MySqlClient;
using PVFP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ferreteria
{
    public partial class Frmpuntodeventaporcentaje : Form
    {
        string id_venta="";
        int tipo=0;
        NumberFormatInfo nfi = new CultureInfo("Es-MX", false).NumberFormat;
        FrmPuntoVenta frm;
        public Frmpuntodeventaporcentaje(string id,int tipo,Form forma)
        {

            InitializeComponent();
            id_venta = id;
            this.tipo = tipo;
            frm = forma as FrmPuntoVenta;
        }
        //Ganancia_Venta,Ganancia_Mayoreo
        private void button1_Click(object sender, EventArgs e)
        {
            frm.porcentaje((cant + a).ToString());
            this.Close();
        }

        private void Frmpuntodeventaporcentaje_Load(object sender, EventArgs e)
        {
            MySqlConnection conexion = ClsInicioSesion.ObtenerConexion();
            //string m = tipo == 1 ? "Ganancia_Venta" : "Ganancia_Mayoreo";
            string m = tipo == 1 ? "Round(((Precio_Costo*Ganancia_Venta)/100),2)" : "Round(((Precio_Costo*Ganancia_Mayoreo)/100),2)   ";
            string comando = "select Precio_Costo, " + m + " as t from producto where Codigodebarra=@id";
            MySqlCommand _comando = new MySqlCommand(comando, conexion);
            _comando.Parameters.AddWithValue("@id", id_venta);
            MySqlDataReader lee = _comando.ExecuteReader();
            
            while (lee.Read())
            {
                cant = double.Parse(lee["Precio_Costo"].ToString());
                ganan = double.Parse(lee["t"].ToString());
            }
            lblporact.Text = ganan.ToString("C",nfi);
            LblPrecioactual.Text = (cant + ganan).ToString("C",nfi);
            Porcentaje.Text = cant.ToString("C",nfi);
            Lblpercen.Text = cant.ToString("C", nfi);
            Lblpreact.Text= (cant + ganan).ToString("C", nfi);
            lee.Close();
            conexion.Close();
        }
        double cant = 0, ganan = 0;
        private void TxtProcentaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar) || Char.IsDigit(e.KeyChar)  || e.KeyChar.Equals('.'))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        double a = 0.0;
        private void TxtProcentaje_TextChanged(object sender, EventArgs e)
        {
            if (TxtProcentaje.Text!="" && !(TxtProcentaje.Text=="." && TxtProcentaje.TextLength==1))
            {
                a = Double.Parse(TxtProcentaje.Text);
                Lblpreact.Text = (a + cant).ToString("C",nfi);
            }            
        }
    }
}
