using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class HistoriaDigital
    {
        private int _id;
        private string _dni;
        private string _nombreYApellidoPaciente;
        private List<Byte[]> _imagenes;
            
        public int Id()
        { return this._id; }

        public string NnombreYApellidoPaciente()    
        { return this._nombreYApellidoPaciente; }

        public string DniPaciente()
        { return this._dni; }

        public List<Byte[]> Imagenes()
            { return this._imagenes; }


        public HistoriaDigital()
        {

        }

        public HistoriaDigital(int id, string dni,string nombreYApellido, List<Byte[]> imagenes  )
        {
            this._id = id;
            this._dni = dni;
            this._nombreYApellidoPaciente = nombreYApellido;
            this._imagenes = imagenes;
        }

    }
}
