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
        private List<Byte[]> _imagenes;

        public int Id()
        { return this._id; }

     
        public string DniPaciente()
        { return this._dni; }

        public List<Byte[]> Imagenes()
        { return this._imagenes; }


        public HistoriaDigital()
        {

        }

        public HistoriaDigital(int id, string dni, List<Byte[]> imagenes)
        {
            this._id = id;
            this._dni = dni;           
            this._imagenes = imagenes;
        }

    }
}
