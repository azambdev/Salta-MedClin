using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedClin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //toolStrip1.Anchor = AnchorStyles.Left;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Pacientes frmpacientes = new Pacientes();
            frmpacientes.ShowDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            HistoriaClinica frmHistoriaClinica = new HistoriaClinica();
            frmHistoriaClinica.ShowDialog();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Coberturas frmCobertura = new Coberturas();
            frmCobertura.ShowDialog();
        }
    }
}
