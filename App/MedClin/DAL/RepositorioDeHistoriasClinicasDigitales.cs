using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RepositorioDeHistoriasClinicasDigitales
    {


        public void Create(int id, string dni, Byte[] imageneDigitalizada)
        {
            try
            {
                DataTable dt = new DataTable();
                string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    using (MySqlCommand cmd = new MySqlCommand("CreateHistoriaClinicaDigital", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@inDni", dni);
                        cmd.Parameters.AddWithValue("@inImagen", imageneDigitalizada);
                      
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetHistoriaClinicaDigitalByDni(string dni)
        {
            try
            {
                DataTable dt = new DataTable();
                string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    using (MySqlCommand cmd = new MySqlCommand("GetHistoriaClinicaDigitalByDni", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@inDni", dni);
                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }







    }
}
