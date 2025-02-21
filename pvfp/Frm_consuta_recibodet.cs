﻿using Ferreteria;
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

namespace PVFP
{
    public partial class Frm_consuta_recibodet : Form
    {
        int ids,numero=0;
        public Frm_consuta_recibodet(int id)
        {
            InitializeComponent();
            ids = id;           
        }
        Frm_Devolucion frm = null;
        public Frm_consuta_recibodet(int id,int tipo,Form from)
        {
            InitializeComponent();
            ids = id;
            frm = from as Frm_Devolucion;
            numero = tipo;            
        }

        private void cargar()
        {
            DataGridViewButtonColumn Columna_eliminar = new DataGridViewButtonColumn();
            Columna_eliminar.Name = "Eliminar";
            if (dataGridView1.Columns["Eliminar"] == null)
            {
                dataGridView1.Columns.Insert(3, Columna_eliminar);
                dataGridView1.CellClick += DataGridView1_CellClick;
            }
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                item.Cells["Eliminar"].Value = "Remover";
            }
        }
        Cls_devolucion devol = new Cls_devolucion();
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex==3)
            {
                double val_act = Double.Parse(dataGridView1[1, e.RowIndex].Value.ToString());
                if (val_act > 1)
                {

                    DialogResult resul = MessageBox.Show("¿Desea eliminar la venta?" + Environment.NewLine + Environment.NewLine +
                        "(Presione No para fijar cantidad)", "eliminar", MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Information);
                    if (resul.Equals(DialogResult.No))
                    {
                        string r = Clsinputbox.ShowDialog("Cantidad", "Cantidad a eliminar", val_act);
                        double n_r = Double.Parse(r);
                        if (n_r > val_act)
                        {
                            MessageBox.Show("La cantidad es incorrecta", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if(n_r == val_act)
                        {
                            devol.eliminar(ids, e.RowIndex);
                            frm.llenado();
                            this.Close();
                        }
                        else
                        {
                            devol.Eliminar_uno(ids, e.RowIndex,Double.Parse(r));
                            frm.llenado();
                            this.Close();
                        }
                    }
                    else if (resul.Equals(DialogResult.Yes))
                    {
                        devol.eliminar(ids, e.RowIndex);
                        frm.llenado();
                        this.Close();
                    }
                    
                }
                else
                {
                    DialogResult r = MessageBox.Show("Desea eliminar este producto de la venta", "eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (r.Equals(DialogResult.Yes))
                    {
                        devol.eliminar(ids, e.RowIndex);
                        frm.llenado();
                        this.Close();
                    }
                }
            }
        }

        Cls_Consulta cn = new Cls_Consulta();

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        NumberFormatInfo nfi = new CultureInfo("Es-MX", false).NumberFormat;
        private void Frm_consuta_recibodet_Load(object sender, EventArgs e)
        {
            
            //dataGridView1.DataSource = cn.ventas(ids);
            DataTable tb = cn.ventas(ids); //consulta.num_venta(Int32.Parse(TxtVenta.Text));
            DataTable resul = new DataTable();
            foreach (DataColumn item in tb.Columns)
            {
                resul.Columns.Add(item.ColumnName);
            }
            foreach (DataRow item in tb.Rows)
            {
                DataRow dr = resul.NewRow();
                dr[0] = item[0];
                dr[1] = item[1];
                dr[2] = Double.Parse(item[2].ToString()).ToString("C", nfi);
                //dr[3] = Double.Parse(item[3].ToString()).ToString("C", nfi);
                //dr[4] = Double.Parse(item[4].ToString()).ToString("C", nfi);
                resul.Rows.Add(dr);

            }
            dataGridView1.DataSource = resul;
            label1.Text = "Empleado: " + cn.empledado(ids);
            if (numero == 1)
            {
                cargar();
            }           
        }
    }
}
