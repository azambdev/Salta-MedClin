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


        //public List<HistoriaClinica> GetHistoriasClinicasByDni(string dniPaciente)
        //{
        //    DAL.RepositorioDeHistoriasClinicas repo = new DAL.RepositorioDeHistoriasClinicas();
        //    List<HistoriaClinica> pacientes = new List<HistoriaClinica>();
        //    DataTable table = repo.GetHistoriasByDni();


        //    List<HistoriaClinica> historias = new List<HistoriaClinica>();
        //    HistoriaClinica historia = new HistoriaClinica();
        //    historias = historia.GetCoberturas();

        //    foreach (DataRow row in table.Rows)
        //    {
        //        bool esActivo = false;
        //        if (row["activo"].ToString() == "1")
        //        {
        //            esActivo = true;
        //        }

        //        CoberturaMedica coberturaPaciente = coberturas.Find(x => x.Id() == int.Parse(row["IdCobertura"].ToString()));

        //        pacientes.Add(new Paciente(int.Parse(row["id"].ToString()), row["dni"].ToString(), row["apellido"].ToString(), row["nombre"].ToString(), DateTime.Parse(row["fechanacimiento"].ToString()), coberturaPaciente, row["numeroafiliado"].ToString(), row["domicilio"].ToString(), row["email"].ToString(), row["telefono"].ToString(), row["comentarios"].ToString(), esActivo));
        //    }
        //    return pacientes;
        //}


    }
}
