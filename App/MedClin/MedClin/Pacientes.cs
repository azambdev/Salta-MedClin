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
    public partial class Pacientes : Form
    {
        public Pacientes()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Pacientes_Load(object sender, EventArgs e)
        {
            Negocio.CoberturaMedica cobertura = new Negocio.CoberturaMedica();
            List<Negocio.CoberturaMedica> listaCoberturas = cobertura.GetCoberturas();

            if (listaCoberturas.Any())
            {
                foreach (var item in listaCoberturas)
                {
                    dropdownCoberturas.Items.Add(item.Descripcion());
                }
            }

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {

            
            LimpiarControles();
            

        }


        public int CalcularEdad()
        {           
                return DateTime.Today.AddTicks(-dateTimePickerFN.Value.Date.Ticks).Year - 1;
          

        }

        private void LimpiarControles()
        {
            txtDni.Clear();
            txtApellido.Clear();
            txtNombre.Clear();
            dateTimePickerFN.ResetText();
            txtEdad.Clear();
            dropdownCoberturas.SelectedIndex = -1;
            txtNroAfiliado.Clear();
            txtDomicilio.Clear();
            txtEmail.Clear();
            txtTelefono.Clear();
            txtComentarios.Clear();
            
          
        }

        private void dateTimePickerFN_ValueChanged(object sender, EventArgs e)
        {
            txtEdad.Text = CalcularEdad().ToString();
        }
    }
}
