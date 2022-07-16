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
    public class RepositorioDeCoberturas
    {

        public DataTable GetCoberturasMedicas()
        {
            DataTable dt = new DataTable();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                using (MySqlCommand cmd = new MySqlCommand("GetCoberturas", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@CustId", customerId);
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public void Create(string nombreCobertura, string comentariosCobertura)
        {
            try
            {
                DataTable dt = new DataTable();
                string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    using (MySqlCommand cmd = new MySqlCommand("CreateCobertura", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@inDescripcionCobertura", nombreCobertura);
                        cmd.Parameters.AddWithValue("@inComentarios", comentariosCobertura);
                        //cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                        //cmd.Parameters.AddWithValue("@Activo", activo);
                        //cmd.Parameters.AddWithValue("@Imagen", imagen);
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

        public void Update(int idCobertura, string nombreCobertura, string comentariosCobertura)
        {
            try
            {
                DataTable dt = new DataTable();
                string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    using (MySqlCommand cmd = new MySqlCommand("UpdateCobertura", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@inIdCobertura", idCobertura);
                        cmd.Parameters.AddWithValue("@inDescripcionCobertura", nombreCobertura);
                        cmd.Parameters.AddWithValue("@inComentarios", comentariosCobertura);
                        //cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                        //cmd.Parameters.AddWithValue("@Activo", activo);
                        //cmd.Parameters.AddWithValue("@Imagen", imagen);
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
