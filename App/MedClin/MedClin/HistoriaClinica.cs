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
        private List<Negocio.HistoriaClinica>  _listaHistoriaDePaciente = new List<Negocio.HistoriaClinica>();

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

            btnNuevaConsulta.Visible = false;


        }

        public void CargarDatosPaciente(Negocio.Paciente pacienteConsultado)
        {
            txtDniPaciente.Text = pacienteConsultado.NroDocumento();            
            txtDomicilioPaciente.Text= pacienteConsultado.Domicilio();
            txtNombreYApellido.Text= pacienteConsultado.Nombre()  + " " + pacienteConsultado.Apellido();
            dateTimePickerFN.Value=pacienteConsultado.FechaNacimiento();
            txtEdad.Text = CalcularEdad().ToString();


            Negocio.HistoriaClinica historia = new Negocio.HistoriaClinica();
            _listaHistoriaDePaciente =  historia.GetHistoriasClinicasByDni(pacienteConsultado.NroDocumento());

            if (_listaHistoriaDePaciente.Any())
            {
                ActualizarGrilladeHistorias();
                CargarUltimaVisitaEnFormulario();
                bloquearCamposReceta();
                toolStripButton1.Enabled = false;
                btnNuevaConsulta.Visible = true;
            }
                   



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

        private void CargarUltimaVisitaEnFormulario()
        {
            Negocio.HistoriaClinica historiaClinicaReciente = _listaHistoriaDePaciente.First();

            dateTimePickerFConsulta.Value = historiaClinicaReciente.FechaConsulta();
            txtMotivo.Text = historiaClinicaReciente.Motivo();
            txtExamen.Text = historiaClinicaReciente.ExamenFisico();
            txtEstudios.Text = historiaClinicaReciente.Estudios();
            txtTratamiento.Text= historiaClinicaReciente.Tratamiento();
            txtReceta.Text = historiaClinicaReciente.Receta();

            groupBoxConsulta.Text = "Última visita";
            


        }

        private void bloquearCamposPaciente()
        {
            txtDniPaciente.Enabled = false;
            txtDomicilioPaciente.Enabled = false;
            txtNombreYApellido.Enabled = false;
            dateTimePickerFN.Enabled = false;
            txtEdad.Enabled = false;
        }

        private void bloquearCamposReceta()
        {
            dateTimePickerFConsulta.Enabled = false;
            txtMotivo.ReadOnly = true;
            txtExamen.ReadOnly = true;
            txtEstudios.ReadOnly = true;
            txtTratamiento.ReadOnly = true;
            txtReceta.ReadOnly = true;
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
            HabilitarCamposConsulta();
            groupBoxConsulta.Text = "Consulta";
        }

        private void HabilitarCamposConsulta()
        {
            dateTimePickerFConsulta.Enabled = true;
            txtMotivo.ReadOnly = false;
            txtExamen.ReadOnly = false;
            txtEstudios.ReadOnly = false;
            txtTratamiento.ReadOnly = false;
            txtReceta.ReadOnly = false;


            dateTimePickerFConsulta.Value= DateTime.Now;
            txtMotivo.Clear();
            txtExamen.Clear();
            txtEstudios.Clear();
            txtTratamiento.Clear();
            txtReceta.Clear();

            dataGridHistoriasPaciente.DataSource = null;
            toolStripButton1.Enabled = true;
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
                    bloquearCamposReceta();
                    btnNuevaConsulta.Visible = true;
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

                Negocio.Paciente paciente = new Negocio.Paciente(Dni());


                Negocio.HistoriaClinica historia = new Negocio.HistoriaClinica(0, paciente, FechaConsulta(), Motivo(), ExamenMedico(), Estudios(), Tratamiento(), Receta());
                historia.Create();
                _listaHistoriaDePaciente = historia.GetHistoriasClinicasByDni(Dni());
                ActualizarGrilladeHistorias(); 
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en proceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void ActualizarGrilladeHistorias()
        {
            dataGridHistoriasPaciente.DataSource = null;
            dataGridHistoriasPaciente.DataSource = _listaHistoriaDePaciente.Select(p => new {FechaConsulta= p.FechaConsulta(), Motivo = p.Motivo(), Examen = p.ExamenFisico(), Estudios = p.Estudios(), Tratamiento = p.Tratamiento(), Receta = p.Receta()}).ToList();
            //dataGridHistoriasPaciente.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            //dataGridHistoriasPaciente.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            //dataGridHistoriasPaciente.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dataGridHistoriasPaciente.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;


        }

        private string Motivo()
        {
            return txtMotivo.Text.Trim();
        }

        private string ExamenMedico()
        {
            return txtExamen.Text.Trim();
        }

        private string Estudios()
        {
            return txtEstudios.Text.Trim();
        }

        private string Tratamiento()
        {
            return txtTratamiento.Text.Trim();
        }

        private string Receta()
        {
            return txtReceta.Text.Trim();
        }

        private DateTime FechaConsulta()
        {
            return dateTimePickerFConsulta.Value;
        }

        private bool FormularioEstaCompleto()
        {
            if (EstaVacio(Dni()))
            {
                MessageBox.Show("Debe completar el DNI del paciente", "Validación de Operación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDniPaciente.Focus();
                return false;
            }

            if (EstaVacio(Motivo()))
            {
                MessageBox.Show("Debe completar el motivo de la consulta", "Validación de Operación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDniPaciente.Focus();
                return false;
            }
            if ((Motivo().Length<5))
            {
                MessageBox.Show("Debe completar un motivo de consulta válido", "Validación de Operación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void btnNuevaConsulta_Click(object sender, EventArgs e)
        {


            dateTimePickerFConsulta.Enabled = true;
            txtMotivo.ReadOnly = false;
            txtExamen.ReadOnly = false;
            txtEstudios.ReadOnly = false;
            txtTratamiento.ReadOnly = false;
            txtReceta.ReadOnly = false;


            dateTimePickerFConsulta.Value = DateTime.Now;
            txtMotivo.Clear();
            txtExamen.Clear();
            txtEstudios.Clear();
            txtTratamiento.Clear();
            txtReceta.Clear();

           // dataGridHistoriasPaciente.DataSource = null;
            toolStripButton1.Enabled = true;
            btnNuevaConsulta.Visible = false;
            groupBoxConsulta.Text = "Consulta";


        }

        private void dataGridHistoriasPaciente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (dataGridHistoriasPaciente.Rows.Count == 0)
                {
                    return;
                }
                int rowindex = dataGridHistoriasPaciente.CurrentCell.RowIndex;
                int columnindex = dataGridHistoriasPaciente.CurrentCell.ColumnIndex;
                DateTime fechaConsulta =DateTime.Parse(dataGridHistoriasPaciente.Rows[rowindex].Cells[0].Value.ToString());
                dateTimePickerFConsulta.Value = fechaConsulta;
                string motivoConsulta = (dataGridHistoriasPaciente.Rows[rowindex].Cells[1].Value.ToString());
                txtMotivo.Text = motivoConsulta;

                string examenFisico = (dataGridHistoriasPaciente.Rows[rowindex].Cells[2].Value.ToString());
                txtExamen.Text = examenFisico;


                string estudios = (dataGridHistoriasPaciente.Rows[rowindex].Cells[3].Value.ToString());
                txtEstudios.Text = estudios;

                string tratamiento = (dataGridHistoriasPaciente.Rows[rowindex].Cells[4].Value.ToString());
                txtTratamiento.Text = tratamiento;

                string receta = (dataGridHistoriasPaciente.Rows[rowindex].Cells[5].Value.ToString());
                txtReceta.Text = receta;

                toolStripButton1.Enabled = false;    
                btnNuevaConsulta.Visible = true;
                bloquearCamposReceta();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en proceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



        }
    }
}
