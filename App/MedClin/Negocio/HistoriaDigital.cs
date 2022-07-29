using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class HistoriaDigital
    {
        private int _id;
        private string _dni;      
        private Byte[] _imagen;

        public int Id()
        { return this._id; }

     
        public string DniPaciente()
        { return this._dni; }

        public Byte[] Imagenes()
        { return this._imagen; }


        public HistoriaDigital()
        {

        }

        public HistoriaDigital(int id, string dni, Byte[] imagen)
        {
            this._id = id;
            this._dni = dni;           
            this._imagen = imagen;
        }



        public List<HistoriaDigital> GetHistoriasClinicasByDni(string dniPaciente)
        {
            DAL.RepositorioDeHistoriasClinicasDigitales repo = new DAL.RepositorioDeHistoriasClinicasDigitales();
            List<HistoriaDigital> historias = new List<HistoriaDigital>();
            DataTable table = repo.GetHistoriaClinicaDigitalByDni(dniPaciente);


            //List<HistoriaClinica> historias = new List<HistoriaClinica>();
            //HistoriaClinica historia = new HistoriaClinica();
            //historias = historia.GetHistoriasClinicasByDni();

            foreach (DataRow row in table.Rows)
            {
                MemoryStream stmBLOBData = new MemoryStream();
                if (row["imagen"].ToString().Length > 0)
                {
                    Byte[] byteBLOBData = new Byte[0];
                    byteBLOBData = (Byte[])(row["imagen"]);
                    stmBLOBData = new MemoryStream(byteBLOBData);
                    // picbx_vwid.Image = Image.FromStream(stmBLOBData);
                }

                historias.Add(new HistoriaDigital(int.Parse(row["id"].ToString()), row["Dni"].ToString(), stmBLOBData.ToArray()));
            }
            return historias;
        }


    }
}
