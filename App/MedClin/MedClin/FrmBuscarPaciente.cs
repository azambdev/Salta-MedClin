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
    public partial class FrmBuscarPaciente : Form
    {
       private  Pacientes _pacienteFormInstancia;
        private List<Negocio.Paciente> _listaPacientesExistentes = new List<Negocio.Paciente>();

        public FrmBuscarPaciente(Pacientes pacienteForm )
        {
            InitializeComponent();
            this._pacienteFormInstancia = pacienteForm;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            txtApellido.Clear();
            txtNombre.Clear();
            dataGridResultados.DataSource = null;
        }

        private void FrmBuscarPaciente_Load(object sender, EventArgs e)
        {

            Negocio.Paciente paciente = new Negocio.Paciente();
            List<Negocio.Paciente> listapacientes = paciente.GetPacientes();
            _listaPacientesExistentes = listapacientes;

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (EstaVacio(Apellido()) && EstaVacio(Nombre()))
                {
                    return;
                }

                dataGridResultados.DataSource = null;
                dataGridResultados.DataSource = _listaPacientesExistentes.Where(x => x.Apellido().ToUpper().Contains(Apellido().ToUpper()) || x.Nombre().ToUpper().Contains(Nombre().ToUpper())).Select(p => new { Dni = p.NroDocumento(), Apellido = p.Apellido(), Nombre = p.Nombre(), Nacimiento = p.FechaNacimiento(), Telefono = p.Telefono(), Email = p.Email() }).ToList(); 
                 
                }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Validación de Operación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           

        }

        private string Apellido()
        { return txtApellido.Text; }

        private string Nombre()
        { return txtNombre.Text; }

        private bool EstaVacio(string campo)
        {
            return campo.Length == 0;
        }

        private void dataGridResultados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridResultados.Rows.Count == 0)
            {
                return;
            }
        }

        private void dataGridResultados_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridResultados.Rows.Count == 0)
                {
                    return;
                }
                int rowindex = dataGridResultados.CurrentCell.RowIndex;
                int columnindex = dataGridResultados.CurrentCell.ColumnIndex;
              
                string dniPaciente = (dataGridResultados.Rows[rowindex].Cells[0].Value.ToString());

                _pacienteFormInstancia.txtDni.Text = dniPaciente;
                _pacienteFormInstancia.CargarPacienteBuscado();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en proceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
