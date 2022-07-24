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

        private List<Negocio.Paciente> _listaPacientesExistentes = new List<Negocio.Paciente>();
        private List<Negocio.CoberturaMedica> _listaCoberturas = new List<Negocio.CoberturaMedica>();
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Pacientes_Load(object sender, EventArgs e)
        {
            Negocio.CoberturaMedica cobertura = new Negocio.CoberturaMedica();
            List<Negocio.CoberturaMedica> listaCoberturas = cobertura.GetCoberturas();
            _listaCoberturas = listaCoberturas;

            Negocio.Paciente paciente = new Negocio.Paciente();
            List<Negocio.Paciente> listapacientes = paciente.GetPacientes();
            _listaPacientesExistentes = listapacientes;

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
            try
            {
                           
            if (!FormularioEstaCompleto())
            {
                 return;
            }

                if (txtDni.Enabled)
                {

                    if (_listaPacientesExistentes.Exists(x => x.NroDocumento().Trim() == Dni().Trim()))
                    {
                        MessageBox.Show("El DNI ingresado ya existe.", "Validación de operación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    GuardarPaciente();
                    MessageBox.Show("Paciente guardado correctamente", "Resultado de Operación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtDni.Enabled = false;
                    toolStripButtonHistoriaClinica.Visible = true;
                }
                else
                {
                ActualizarPaciente();
                MessageBox.Show("Paciente guardado correctamente", "Resultado de Operación", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
                }
              
                return;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en proceso: " + ex.Message, "Resultado de Operación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           

           
            //LimpiarControles();
            

        }

        private void ActualizarPaciente()
        {
                Negocio.Paciente paciente = new Negocio.Paciente(0, Dni(), ApellidoPaciente(), NombrePaciente(), FechaNacimiento(), CoberturaAfiliado(), NumeroAfiliado(), Domicilio(), Email(), Telefono(), Comentarios(), true);
                paciente.Update();
                _listaPacientesExistentes = paciente.GetPacientes();
        }

        private void GuardarPaciente()
        {
            try
            {        
                Negocio.Paciente paciente = new Negocio.Paciente(0, Dni(),ApellidoPaciente(), NombrePaciente(), FechaNacimiento(),CoberturaAfiliado(), NumeroAfiliado(),Domicilio(),Email(),Telefono(),Comentarios(),true );
                paciente.Create();
                _listaPacientesExistentes = paciente.GetPacientes();
               
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
                txtDni.Focus();
                return false;
            }
            if ((Dni().Length < 5))
            {
                MessageBox.Show("Debe informar un DNI válido", "Validación de Operación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDni.Focus();
                return false;
            }
          
            if (!int.TryParse(Dni(), out _))
            {
                MessageBox.Show("Debe informar un DNI válido, solo números", "Validación de Operación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDni.Focus();
                return false;
            }
                       
            if (EstaVacio(ApellidoPaciente()))
            {
                MessageBox.Show("Debe completar el apellido del paciente", "Validación de Operación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellido.Focus();
                return false;
            }

            if ((ApellidoPaciente().Length < 2))
            {
                MessageBox.Show("Debe informar un apellido válido", "Validación de Operación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellido.Focus();
                return false;
            }

            if (EstaVacio(NombrePaciente()))
            {
                MessageBox.Show("Debe completar el nombre del paciente", "Validación de Operación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if ((NombrePaciente().Length < 2))
            {
                MessageBox.Show("Debe informar un nombre válido", "Validación de Operación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (EstaVacio(FechaNacimiento().ToShortDateString()))
            {
                MessageBox.Show("Debe completar la fecha de nacimiento del paciente", "Validación de Operación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            int result = DateTime.Compare(FechaNacimiento(),DateTime.Parse(DateTime.Now.ToShortDateString()));
            if (result >= 0)
            {
                MessageBox.Show("Fecha de nacimiento incorrecta, debe ser menor a la actual", "Validación de Operación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimePickerFN.Focus();
                return false;
            }

            if (dropdownCoberturas.SelectedIndex==-1)
            {
                MessageBox.Show("Debe especificar una cobertura", "Validación de Operación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dropdownCoberturas.Focus();
                return false;
            }

            return true;
        }

        private bool EstaVacio(string campo)
        {
            return campo.Length == 0;
        }

        public int CalcularEdad()
        {
            try
            {

                int result = DateTime.Compare(FechaNacimiento(), DateTime.Now);
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
            txtDni.Enabled = true;
            toolStripButtonHistoriaClinica.Visible = false;

        }

        private void dateTimePickerFN_ValueChanged(object sender, EventArgs e)
        {
            txtEdad.Text = CalcularEdad().ToString();
        }


        private string Dni()
        {
            return txtDni.Text.Trim();
        }

        private string NombrePaciente()
        {
            return txtNombre.Text.Trim();
        }

        private string ApellidoPaciente()
        {
            return txtApellido.Text.Trim();
        }

        private DateTime FechaNacimiento()
        {
            return dateTimePickerFN.Value.Date;
        }

        private Negocio.CoberturaMedica CoberturaAfiliado()
        {
            return _listaCoberturas.Find(x => x.Descripcion().ToUpper() == dropdownCoberturas.SelectedItem.ToString().ToUpper());
        }

        private string NumeroAfiliado()
        {
            return txtNroAfiliado.Text.Trim();
        }

        private string Domicilio()
        {
            return txtDomicilio.Text.Trim();
        }
        private string Telefono()
        {
            return txtTelefono.Text.Trim();
        }
        private string Email()
        {
            return txtEmail.Text.Trim();
        }
        private string Comentarios()
        {
            return txtComentarios.Text.Trim();
        }

        private void txtDni_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            //if (e.KeyCode == Keys.Tab)
            //{
            //    Negocio.Paciente pacienteConsultado = _listaPacientesExistentes.Find(x => x.NroDocumento() == txtDni.Text);
            //    if (pacienteConsultado != null)
            //    {
            //        CargarDatosPaciente(pacienteConsultado);
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}


        }



        public void CargarPacienteBuscado()
        {


            Negocio.Paciente pacienteConsultado = _listaPacientesExistentes.Find(x => x.NroDocumento() == txtDni.Text);
            if (pacienteConsultado != null)
            {
                CargarDatosPaciente(pacienteConsultado);
            }


        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool baseResult = base.ProcessCmdKey(ref msg, keyData);

            if (txtDni.Text.Length==0)
            {
                return false;
            }

            if (keyData == Keys.Tab && txtDni.Focused)
            {
                Negocio.Paciente pacienteConsultado = _listaPacientesExistentes.Find(x => x.NroDocumento() == txtDni.Text);
                if (pacienteConsultado != null)
                {
                    CargarDatosPaciente(pacienteConsultado);
                }
                else
                {
                    return true;
                }
            }

            if (keyData == (Keys.Tab | Keys.Shift) && txtDni.Focused)
            {              
                return true;
            }

            return baseResult;
        }

        private void txtDni_KeyDown(object sender, KeyEventArgs e)
        {

            //if (e.KeyCode == Keys.Tab)
            //{
            //    Negocio.Paciente pacienteConsultado = _listaPacientesExistentes.Find(x => x.NroDocumento() == txtDni.Text);
            //    if (pacienteConsultado != null)
            //    {
            //        CargarDatosPaciente(pacienteConsultado);
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}


        }

        public void CargarDatosPaciente(Negocio.Paciente pacienteConsultado)
        {
            txtDni.Text = pacienteConsultado.NroDocumento();
            txtDni.Enabled = false;
            txtApellido.Text = pacienteConsultado.Apellido();
            txtNombre.Text = pacienteConsultado.Nombre();
            dateTimePickerFN.Value = pacienteConsultado.FechaNacimiento();
            dropdownCoberturas.SelectedItem = pacienteConsultado.Cobertura().Descripcion();
            txtNroAfiliado.Text = pacienteConsultado.NroAfiliado();
            txtDomicilio.Text = pacienteConsultado.Domicilio();
            txtEmail.Text = pacienteConsultado.Email();
            txtTelefono.Text = pacienteConsultado.Telefono();
            txtComentarios.Text = pacienteConsultado.Comentarios();
            toolStripButtonHistoriaClinica.Visible = true;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            LimpiarControles();
        }

        private void toolStripButtonHistoriaClinica_Click(object sender, EventArgs e)
        {
            Negocio.Paciente paciente = new Negocio.Paciente(0, Dni(), ApellidoPaciente(), NombrePaciente(), FechaNacimiento(), CoberturaAfiliado(), NumeroAfiliado(), Domicilio(), Email(), Telefono(), Comentarios(), true);
            HistoriaClinica frmHistoriaClinica = new HistoriaClinica(paciente);
            frmHistoriaClinica.ShowDialog();


        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            FrmBuscarPaciente formBuscarPaciente = new FrmBuscarPaciente(this);
            formBuscarPaciente.ShowDialog();
        }
    }
}
