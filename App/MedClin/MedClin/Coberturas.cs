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
    public partial class Coberturas : Form
    {
        public Coberturas()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            txtComentariosCobertura.Clear();
            txtNombreCobertura.Clear();
            txtNroCobertura.Clear();
        }
    }
}
