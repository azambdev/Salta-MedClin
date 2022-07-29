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
    public partial class HistoriaClinica : Form
    {
        public HistoriaClinica()
        {
            InitializeComponent();
        }

        public HistoriaClinica(Negocio.Paciente paciente)
        {
            InitializeComponent();
            CargarDatosPaciente(paciente);
        }

        private List<Negocio.Paciente> _listaPacientesExistentes = new List<Negocio.Paciente>();
        private List<Negocio.CoberturaMedica> _listaCoberturas = new List<Negocio.CoberturaMedica>();

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void HistoriaClinica_Load(object sender, EventArgs e)
        {

            Negocio.CoberturaMedica cobertura = new Negocio.CoberturaMedica();
            List<Negocio.CoberturaMedica> listaCoberturas = cobertura.GetCoberturas();
            _listaCoberturas = listaCoberturas;

            Negocio.Paciente paciente = new Negocio.Paciente();
            List<Negocio.Paciente> listapacientes = paciente.GetPacientes();
            _listaPacientesExistentes = listapacientes;




        }

        public void CargarDatosPaciente(Negocio.Paciente pacienteConsultado)
        {
            txtDniPaciente.Text = pacienteConsultado.NroDocumento();            
            txtDomicilioPaciente.Text= pacienteConsultado.Domicilio();
            txtNombreYApellido.Text= pacienteConsultado.Nombre()  + " " + pacienteConsultado.Apellido();
            dateTimePickerFN.Value=pacienteConsultado.FechaNacimiento();
            txtEdad.Text = CalcularEdad().ToString();

            bloquearCamposPaciente();
            //txtDni.Enabled = false;
            //txtApellido.Text = pacienteConsultado.Apellido();
            //txtNombre.Text = pacienteConsultado.Nombre();
            //dateTimePickerFN.Value = pacienteConsultado.FechaNacimiento();
            //dropdownCoberturas.SelectedItem = pacienteConsultado.Cobertura().Descripcion();
            //txtNroAfiliado.Text = pacienteConsultado.NroAfiliado();
            //txtDomicilio.Text = pacienteConsultado.Domicilio();
            //txtEmail.Text = pacienteConsultado.Email();
            //txtTelefono.Text = pacienteConsultado.Telefono();
            //txtComentarios.Text = pacienteConsultado.Comentarios();
            //toolStripButtonHistoriaClinica.Visible = true;
        }

        private void bloquearCamposPaciente()
        {
            txtDniPaciente.Enabled = false;
            txtDomicilioPaciente.Enabled = false;
            txtNombreYApellido.Enabled = false;
            dateTimePickerFN.Enabled = false;
            txtEdad.Enabled = false;
        }

        private void HabilitarCamposPaciente()
        {
            txtDniPaciente.Clear();
            txtDomicilioPaciente.Clear();
            txtNombreYApellido.Clear();
            dateTimePickerFN.ResetText();
            txtEdad.Clear();

            txtDniPaciente.Enabled = true;
            txtDomicilioPaciente.Enabled = true;
            txtNombreYApellido.Enabled = true;
            dateTimePickerFN.Enabled = true;            
        }

        public int CalcularEdad()
        {
            try
            {

                int result = DateTime.Compare(dateTimePickerFN.Value, DateTime.Now);
                if (result >= 0)
                {
                    //if (dateTimePickerFN.Value.Date >= DateTime.Now )

                    return 0;
                }

                return DateTime.Today.AddTicks(-dateTimePickerFN.Value.Date.Ticks).Year - 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fecha de nacimiento incorrecta", "Validación de Operación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool baseResult = base.ProcessCmdKey(ref msg, keyData);

            if (txtDniPaciente.Text.Length == 0)
            {
                return false;
            }

            if (keyData == Keys.Tab && txtDniPaciente.Focused)
            {
                Negocio.Paciente pacienteConsultado = _listaPacientesExistentes.Find(x => x.NroDocumento() == txtDniPaciente.Text);
                if (pacienteConsultado != null)
                {
                    CargarDatosPaciente(pacienteConsultado);
                }
                else
                {
                    return true;
                }
            }

            if (keyData == (Keys.Tab | Keys.Shift) && txtDniPaciente.Focused)
            {
                return true;
            }

            return baseResult;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            HabilitarCamposPaciente();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!FormularioEstaCompleto())
                {
                    return;
                }
                if (!txtDniPaciente.Enabled)
                {

                    //if (_listaPacientesExistentes.Exists(x => x.NroDocumento().Trim() == Dni().Trim()))
                    //{
                    //    MessageBox.Show("El DNI ingresado ya existe.", "Validación de operación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}

                    GuardarHistoriaClinica();
                    MessageBox.Show("Historia clínica guardada correctamente", "Resultado de Operación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //txtDni.Enabled = false;
                    //toolStripButtonHistoriaClinica.Visible = true;
                }
                else
                {
                    return;
                    //ActualizarPaciente();
                    //MessageBox.Show("Paciente guardado correctamente", "Resultado de Operación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                return;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en proceso: " + ex.Message, "Resultado de Operación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }










        }

        private void GuardarHistoriaClinica()
        {
            try
            {
                //Negocio.HistoriaClinica historia = new Negocio.HistoriaClinica(0, Dni(), ApellidoPaciente(), NombrePaciente(), FechaNacimiento(), CoberturaAfiliado(), NumeroAfiliado(), Domicilio(), Email(), Telefono(), Comentarios(), true);
                //historia.Create();
                //_listaPacientesExistentes = historia.GetHistoriaClinicaByDni(Dni());
                ////ActualizarGrilladeHistorias

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en proceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private bool FormularioEstaCompleto()
        {
            if (EstaVacio(Dni()))
            {
                MessageBox.Show("Debe completar el DNI del paciente", "Validación de Operación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDniPaciente.Focus();
                return false;
            }

            return true;
        }
        private bool EstaVacio(string campo)
        {
            return campo.Length == 0;
        }

        private string Dni()
        {
            return txtDniPaciente.Text.Trim();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            HistoriaDigital historiaDigital = new HistoriaDigital();
            historiaDigital.ShowDialog();
        }
    }
}
