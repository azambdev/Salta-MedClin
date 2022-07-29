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
    public partial class HistoriaDigital : Form
    {
        public HistoriaDigital(string dni, string nombrePaciente)
        {
            InitializeComponent();
            _dni = dni;
            _nombrePaciente = nombrePaciente;
        }

        private string _dni;
        private string _nombrePaciente; 


        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("¿Desea guardar la imagen digitalizada del paciente?", "Confirmar acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                try
                {

                }
                catch (Exception)
                {

                    throw;
                }



            }
            else
            {
                return;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

            try
            {
                // open file dialog   
                OpenFileDialog open = new OpenFileDialog();
                // image filters  
                open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    pictureBoxHistoria.Image = new Bitmap(open.FileName);
                    //txtNombreArchivoFoto.Text = open.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en proceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


        }

        private void HistoriaDigital_Load(object sender, EventArgs e)
        {
            txtDniPaciente.Text = this._dni;
            txtNombreYApellido.Text = this._nombrePaciente;

            txtDniPaciente.Enabled = false;
            txtNombreYApellido.Enabled = false;

        }
    }
}
