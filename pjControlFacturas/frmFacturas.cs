using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pjControlFacturas
{
    public partial class frmFacturas : Form
    {
        public frmFacturas()
        {
            InitializeComponent();
        }

        private void frmFacturas_Load(object sender, EventArgs e)
        {
            lblFecha.Text = DateTime.Now.ToShortDateString();
            lblMonto.Text = (0).ToString("C");                  //Asi se cambia el monto y la fecha cuando se ingrsa por primera vez

        }

        private void btnNuevaFact_Click(object sender, EventArgs e)
        {
            txtNumFact.Clear();  //Limpiara la caja de texto de factura
            txtUnidades.Clear();
            cboTipo.Text = "(Seleccione el tipo)";
            txtNumFact.Focus();
        }

        private void btnRegistrarItem_Click(object sender, EventArgs e)
        {

            int i;

            //Capturar datos
            int numFact = int.Parse(txtNumFact.Text);
            string licencia = cboTipo.Text;
            int unidades = int.Parse(txtUnidades.Text);

            //Determinar el precio de las licencias
            double precio = 0;

            switch (licencia)
            {
                case "Cobre": precio = 700; break;
                case "Bronce": precio = 900; break;
                case "Silver": precio = 1400; break;
                case "Gold": precio = 2500; break;
            }

            //Calcular el subtotal
            double subtotal = unidades * precio;

            //Enviando informacion a la lista de registro
            ListViewItem fila = new ListViewItem(numFact.ToString());
            fila.SubItems.Add(licencia);
            fila.SubItems.Add(unidades.ToString());
            fila.SubItems.Add(subtotal.ToString("0.00"));
            lvRegistros.Items.Add(fila);

            //Calculando el monto total acumulado
            double mAacumulado = 0;
            i = 0;

            while(i < lvRegistros.Items.Count)
            {
               mAacumulado += double.Parse(lvRegistros.Items[i].SubItems[3].Text);
                i++;
            }
            lblMonto.Text = mAacumulado.ToString("C");

            //Calculando las estadisticas
            int cCobre = 0, cBronce = 0, cSilver = 0, cGold = 0;            //categorias
            double tCobre = 0, tBronce = 0, tSilver = 0, tGold = 0;         //monto

            i = 0;

            do 
            { 
                if(lvRegistros.Items[i].SubItems[1].Text == "Cobre")
                {
                    cCobre += int.Parse(lvRegistros.Items[i].SubItems[2].Text);
                    tCobre += double.Parse(lvRegistros.Items[i].SubItems[3].Text);
                }

                else if(lvRegistros.Items[i].SubItems[1].Text == "Bronce")
                {
                    cBronce += int.Parse(lvRegistros.Items[i].SubItems[2].Text);
                    tBronce += double.Parse(lvRegistros.Items[i].SubItems[3].Text);
                }
                else if(lvRegistros.Items[i].SubItems[1].Text == "Silver")
                {
                    cSilver += int.Parse(lvRegistros.Items[i].SubItems[2].Text);
                    tSilver += double.Parse(lvRegistros.Items[i].SubItems[3].Text);
                }
                else if(lvRegistros.Items[i].SubItems[1].Text == "Gold")
                {
                    cGold += int.Parse(lvRegistros.Items[i].SubItems[2].Text);
                    tGold += double.Parse(lvRegistros.Items[i].SubItems[3].Text);
                }
               i++;

            }while(i < lvRegistros.Items.Count);

            //Imprimiendo estadisticas

            lbEstadisticas.Items.Clear();
            string[] elementosFila = new string[3];
            ListViewItem row;

            elementosFila[0] = "Total Cobre";
            elementosFila[1] = cCobre.ToString();
            elementosFila[2] = tCobre.ToString();   
            row = new ListViewItem(elementosFila);
            lbEstadisticas.Items.Add(row);

            elementosFila[0] = "Total Bronce";
            elementosFila[1] = cBronce.ToString();
            elementosFila[2] = tBronce.ToString();
            row = new ListViewItem(elementosFila);
            lbEstadisticas.Items.Add(row);

            elementosFila[0] = "Total Silver";
            elementosFila[1] = cSilver.ToString();
            elementosFila[2] = tSilver.ToString();
            row = new ListViewItem(elementosFila);
            lbEstadisticas.Items.Add(row);

            elementosFila[0] = "Total Gold";
            elementosFila[1] = cGold.ToString();
            elementosFila[2] = tGold.ToString();
            row = new ListViewItem(elementosFila);
            lbEstadisticas.Items.Add(row);
        }

    }
}
