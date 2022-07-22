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
    public class RepositorioDeHistoriasClinicas
    {

      

        public void Create(int id, string dni, DateTime fechaConsulta, string motivo, string examenFisico,  string estudios, string tratamiento, string receta)
        {
            try
            {
                DataTable dt = new DataTable();
                string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    using (MySqlCommand cmd = new MySqlCommand("CreateHistoriaClinica", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@inDni", dni);
                        cmd.Parameters.AddWithValue("@inFechaConsulta", fechaConsulta);
                        cmd.Parameters.AddWithValue("@inMotivo", motivo);
                        cmd.Parameters.AddWithValue("@inExamenFisico", examenFisico);                        
                        cmd.Parameters.AddWithValue("@inEstudios", estudios);
                        cmd.Parameters.AddWithValue("@inTratamiento", tratamiento);
                        cmd.Parameters.AddWithValue("@inReceta", receta);                       
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




    }
}
