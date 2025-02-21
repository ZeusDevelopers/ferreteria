﻿using Ferreteria;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PVFP
{
    public partial class FrmInicioSesion : Form
    {
        ClsInicioSesion conexion = new ClsInicioSesion();
        public FrmInicioSesion()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void FrmInicioSesion_Load(object sender, EventArgs e)
        {
            //conexion.ConsultarPath();
            //cls_uno m = new cls_uno();
            //m.Genera();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            sesion();
        }
        public void sesion()
        {
            try
            {

                if (ClsInicioSesion.Usuario == "")
                {
                    conexion.Sesion(txtusuario.Text, txtcontraseña.Text);
                    if (conexion.bandera == false)
                    {
                        txtcontraseña.Text = "";
                        ClsInicioSesion.Usuario = "";
                        MessageBox.Show("Usuario o Contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        conexion.usuario = txtusuario.Text;                        
                        ClsInicioSesion.precio_dolar();
                        if (conexion.puesto == "0")//ADMIN
                        {
                            txtcontraseña.Text = "";
                            FrmMenuAdmin admin = new FrmMenuAdmin();
                            conexion.bandera = false;
                            admin.Show();
                        }
                        else //Trabajador
                        {
                            txtcontraseña.Text = "";
                            FrmPuntoVenta venta = new FrmPuntoVenta(false);
                            conexion.bandera = false;
                            venta.Show();
                        }
                        this.WindowState = FormWindowState.Minimized;
                    }
                }
                else
                {
                    MessageBox.Show("Existe una sesion iniciada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un problema " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtcontraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13)
            {
                sesion();
            }
        }
    }
}
