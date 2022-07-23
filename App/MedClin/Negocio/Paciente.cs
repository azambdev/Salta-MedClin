using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Paciente
    {
        private int _id;
        private string _nroDocumento;
        private string _apellido;
        private string _nombre;
        private DateTime _fechaNacimiento;
        private CoberturaMedica _coberturaMedica;
        private string _numeroAfiliado;
        private string _domicilio;
        private string _email;
        private string _telefono;
        private string _comentarios;
        private bool _activo;
       
        public int Id()
        {
            return this._id;
        }
        public string NroDocumento()
        {
            return this._nroDocumento;
        }

        public string Apellido()
        {
            return this._apellido;
        }

        public string Nombre()
        {
            return this._nombre;
        }
        public DateTime FechaNacimiento()
        {
            return this._fechaNacimiento;
        }

        public CoberturaMedica Cobertura()
        {
            return this._coberturaMedica;
        }

        public string NroAfiliado()
        {
            return this._numeroAfiliado;
        }
        public string Domicilio()
        {
            return this._domicilio;
        }
        public string Email()
        {
            return this._email;
        }
        public string Telefono()
        {
            return this._telefono;
        }
        public string Comentarios()
        {
            return this._comentarios;
        }

        public Paciente()
        {

        }

        public Paciente(int id, string nroDocumento, string apellido, string nombre, DateTime fechaNacimiento, CoberturaMedica coberturaMedica, string numeroAfiliado, string domicilio, string email, string telefono, string comentarios, bool activo)
        {
            _id = id;
            _nroDocumento = nroDocumento;
            _apellido = apellido;
            _nombre = nombre;
            _fechaNacimiento = fechaNacimiento;
            _coberturaMedica = coberturaMedica;
            _numeroAfiliado = numeroAfiliado;
            _domicilio = domicilio;
            _email = email;
            _telefono = telefono;
            _comentarios = comentarios;
            _activo = activo;
        }

        public Paciente(string dni)
        {
            this._nroDocumento = dni;
        }

        public void Create()
        {
            try
            {
                DAL.RepositorioDePacientes repositorioDePacientes = new DAL.RepositorioDePacientes();
                repositorioDePacientes.Create(this.NroDocumento(),this.Apellido(), this.Nombre(),this.FechaNacimiento(), this.Cobertura().Id(), this.NroAfiliado(),this.Domicilio(),this.Telefono(), this.Email(), this.Comentarios());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Paciente> GetPacientes()
        {
            DAL.RepositorioDePacientes repo = new DAL.RepositorioDePacientes();
            List<Paciente> pacientes = new List<Paciente>();
            DataTable table = repo.GetAll();

           
            List<CoberturaMedica> coberturas = new List<CoberturaMedica>();          
            CoberturaMedica cobertura = new CoberturaMedica();
            coberturas = cobertura.GetCoberturas();

            foreach (DataRow row in table.Rows)
            {
                bool esActivo = false;
                if (row["activo"].ToString() == "1")
                {
                    esActivo = true;
                }

                CoberturaMedica coberturaPaciente = coberturas.Find(x => x.Id() == int.Parse(row["IdCobertura"].ToString()));

                pacientes.Add(new Paciente(int.Parse(row["id"].ToString()), row["dni"].ToString(), row["apellido"].ToString(), row["nombre"].ToString(),DateTime.Parse( row["fechanacimiento"].ToString()), coberturaPaciente, row["numeroafiliado"].ToString(), row["domicilio"].ToString(), row["email"].ToString(), row["telefono"].ToString(), row["comentarios"].ToString(), esActivo));
            }
            return pacientes;
        }

        public void Update()
        {
            try
            {
                DAL.RepositorioDePacientes repositorioDePacientes = new DAL.RepositorioDePacientes();
                repositorioDePacientes.Update(this.NroDocumento(), this.Apellido(), this.Nombre(), this.FechaNacimiento(), this.Cobertura().Id(), this.NroAfiliado(), this.Domicilio(), this.Telefono(), this.Email(), this.Comentarios());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
