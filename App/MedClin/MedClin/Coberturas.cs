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

        private List<Negocio.CoberturaMedica> _coberturasExistentes = new List<Negocio.CoberturaMedica>();
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtComentariosCobertura.Clear();
            txtNombreCobertura.Clear();
            txtNroCobertura.Clear();
        }

        private void Coberturas_Load(object sender, EventArgs e)
        {
            try
            {
                CargarCoberturas();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en proceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void CargarCoberturas()
        {
            Negocio.CoberturaMedica cobertura = new Negocio.CoberturaMedica();
            List<Negocio.CoberturaMedica> listaCoberturas = cobertura.GetCoberturas();
            _coberturasExistentes = listaCoberturas;
            if (listaCoberturas.Any())
            {
                dataGridViewCoberturas.DataSource = listaCoberturas.OrderBy(x => x.Id()).Select(p => new { Nro = p.Id(), Descripción = p.Descripcion(), Comentarios = p.Comentarios() }).ToList();
                dataGridViewCoberturas.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridViewCoberturas.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                dataGridViewCoberturas.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewCoberturas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            }
        }

        private void dataGridViewCoberturas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridViewCoberturas.Rows.Count == 0)
                {
                    return;
                }
                int rowindex = dataGridViewCoberturas.CurrentCell.RowIndex;
                int columnindex = dataGridViewCoberturas.CurrentCell.ColumnIndex;
                string nroCobertura = dataGridViewCoberturas.Rows[rowindex].Cells[0].Value.ToString();
                txtNroCobertura.Text = nroCobertura;
                string nombreCobertura = (dataGridViewCoberturas.Rows[rowindex].Cells[1].Value.ToString());
                txtNombreCobertura.Text = nombreCobertura;
                string comentariosCobertura = (dataGridViewCoberturas.Rows[rowindex].Cells[2].Value.ToString());
                txtComentariosCobertura.Text = comentariosCobertura;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en proceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (EstaVacio(NroCobertura()))
            {
                if (!FormularioEstaCompleto())
                {
                    MessageBox.Show("Debe completar el nombre de la cobertura", "Validación de operación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_coberturasExistentes.Exists(x => x.Descripcion().Trim().ToUpper() ==NombreCobertura().Trim().ToUpper()))
                {
                    MessageBox.Show("El nombre de cobertura ya existe.", "Validación de operación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                NuevaCobertura();
                LimpiarCampos();
                dataGridViewCoberturas.DataSource = null;
                CargarCoberturas();
                MessageBox.Show("Cobertura creada correctamente", "Resultado de Operación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;            

            }
            else
            {
                ActualizarCobertura();
                LimpiarCampos();
                dataGridViewCoberturas.DataSource = null;
                CargarCoberturas();
                LimpiarCampos();
                MessageBox.Show("Cobertura actualizada correctamente", "Resultado de Operación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }




        }

        private void ActualizarCobertura()
        {
            try
            {
                Negocio.CoberturaMedica cobertura = new Negocio.CoberturaMedica(int.Parse(NroCobertura()),  NombreCobertura(), ComentariosCobertura());
                cobertura.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en proceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void NuevaCobertura()
        {
            try
            {       
            Negocio.CoberturaMedica cobertura = new Negocio.CoberturaMedica(NombreCobertura(), ComentariosCobertura());
            cobertura.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en proceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private string NroCobertura()
        {
            return txtNroCobertura.Text.Trim();
        }

        private string NombreCobertura()
        {
            return txtNombreCobertura.Text.Trim();
        }
        private string ComentariosCobertura()
        {
            return txtComentariosCobertura.Text.Trim();
        }

        private bool EstaVacio(string campo)
        {
            return campo.Length==0;
        }

        private bool FormularioEstaCompleto()
        {
            return !EstaVacio(NombreCobertura());
        }

        private void dataGridViewCoberturas_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewCoberturas.Rows.Count == 0)
                {
                    return;
                }
                int rowindex = dataGridViewCoberturas.CurrentCell.RowIndex;
                int columnindex = dataGridViewCoberturas.CurrentCell.ColumnIndex;
                string nroCobertura = dataGridViewCoberturas.Rows[rowindex].Cells[0].Value.ToString();
                txtNroCobertura.Text = nroCobertura;
                string nombreCobertura = (dataGridViewCoberturas.Rows[rowindex].Cells[1].Value.ToString());
                txtNombreCobertura.Text = nombreCobertura;
                string comentariosCobertura = (dataGridViewCoberturas.Rows[rowindex].Cells[2].Value.ToString());
                txtComentariosCobertura.Text = comentariosCobertura;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en proceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
