using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CoberturaMedica
    {
        private int _id;
        private string _descripcion;
        private string _comentarios;
        private bool _activo;

        public int Id()
        {
            return this._id;
        }

        public string Descripcion()
        {
            return this._descripcion;
        }
        public string Comentarios()
        {
            return this._comentarios;
        }
        public bool Activo()
        {
            return this._activo;
        }

        public CoberturaMedica(int id, string descripcion, string comentarios, bool activo)
        {
            this._id = id;
            this._descripcion = descripcion;
            this._comentarios = comentarios;
            this._activo = activo;
        }

        public CoberturaMedica()
        {

        }

        public List<CoberturaMedica> GetCoberturas()
        {
            DAL.RepositorioDeCoberturas repo = new DAL.RepositorioDeCoberturas();
            List<CoberturaMedica> coberturas = new List<CoberturaMedica>();
            DataTable table = repo.GetCoberturasMedicas();

            foreach (DataRow row in table.Rows)
            {
                bool esActivo = false;
                if (row["activo"].ToString() == "1")
                {
                    esActivo = true;
                }
                coberturas.Add(new CoberturaMedica(int.Parse(row["id"].ToString()), row["descripcion"].ToString(), row["comentarios"].ToString(), esActivo));
            }
            return coberturas;
        }
    }
}
