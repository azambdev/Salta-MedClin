using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class HistoriaClinica
    {

        private int _id;
        private Negocio.Paciente _paciente;
        private DateTime _fechaConsulta;
        private string _motivo;
        private string _examenFisico;
        private string _estudios;
        private string _tratamiento;
        private string _receta;

        public int Id()
        {
            return this._id;
        }
        public Negocio.Paciente Paciente()
        {
            return this._paciente;
        }

        public DateTime FechaConsulta()
        {
            return this._fechaConsulta;
        }

        public string Motivo()
        {
            return this._motivo;
        }

        public string ExamenFisico()
        {
            return this._examenFisico;
        }

        public string Estudios()
        {
            return this._estudios;
        }
        public string Tratamiento()
        {
            return this._tratamiento;
        }
        public string Receta()
        {
            return this._receta;
        }

        public HistoriaClinica(int id, Paciente paciente, DateTime fechaConsulta, string motivo, string examenFisico, string estudios, string tratamiento, string receta)
        {
            _id = id;
            _paciente = paciente;
            _fechaConsulta = fechaConsulta;
            _motivo = motivo;
            _examenFisico = examenFisico;
            _estudios = estudios;
            _tratamiento = tratamiento;
            _receta = receta;
        }

        public HistoriaClinica()
        {

        }

        public void Create()
        {
            try
            {
                DAL.RepositorioDeHistoriasClinicas repositorioDeHistorias = new DAL.RepositorioDeHistoriasClinicas();
                repositorioDeHistorias.Create(0,this.Paciente().NroDocumento(), this.FechaConsulta(), this.Motivo(), this.ExamenFisico(), this.Estudios(), this.Tratamiento(), this.Receta());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<HistoriaClinica> GetHistoriasClinicasByDni(string dniPaciente)
        {
            DAL.RepositorioDeHistoriasClinicas repo = new DAL.RepositorioDeHistoriasClinicas();
            List<HistoriaClinica> historias = new List<HistoriaClinica>();
            DataTable table = repo.GetHistoriaClinicaByDni(dniPaciente);


            //List<HistoriaClinica> historias = new List<HistoriaClinica>();
            //HistoriaClinica historia = new HistoriaClinica();
            //historias = historia.GetHistoriasClinicasByDni();

            foreach (DataRow row in table.Rows)
            {
              
              Paciente paciente = new Paciente(row["dni"].ToString());
                               

                historias.Add(new HistoriaClinica(int.Parse(row["id"].ToString()), paciente, DateTime.Parse(row["FechaConsulta"].ToString()), row["Motivo"].ToString(), row["ExamenFisico"].ToString(), row["Estudios"].ToString(), row["Tratamiento"].ToString(), row["Receta"].ToString()));
            }
            return historias;
        }


    }
}
