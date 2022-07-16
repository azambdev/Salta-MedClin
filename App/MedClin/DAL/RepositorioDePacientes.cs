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
    public class RepositorioDePacientes
    {
        public DataTable GetAll()
        {
            try
            {
                DataTable dt = new DataTable();
                string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    using (MySqlCommand cmd = new MySqlCommand("GetPacientes", con))
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

     
        public void Create(string dni, string apellido, string nombre, DateTime fechaNacimiento, int idCobertura, string nroAfiliado, string domicilio, string telefono, string email, string comentarios)
        {
            try
            {
                DataTable dt = new DataTable();
                string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    using (MySqlCommand cmd = new MySqlCommand("CreatePaciente", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@inDni", dni);
                        cmd.Parameters.AddWithValue("@inApellido", apellido);
                        cmd.Parameters.AddWithValue("@inNombre", nombre);
                        cmd.Parameters.AddWithValue("@inFechaNacimiento", fechaNacimiento);
                        cmd.Parameters.AddWithValue("@inIdCobertura", idCobertura);
                        cmd.Parameters.AddWithValue("@inNroAfiliado", nroAfiliado);
                        cmd.Parameters.AddWithValue("@inDomicilio", domicilio);
                        cmd.Parameters.AddWithValue("@inTelefono", telefono);
                        cmd.Parameters.AddWithValue("@inEmail", email);
                        cmd.Parameters.AddWithValue("@inComentarios", comentarios);                       
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

        public void Update(string dni, string apellido, string nombre, DateTime fechaNacimiento, int idCobertura, string nroAfiliado, string domicilio, string telefono, string email, string comentarios)
        {
            try
            {
                DataTable dt = new DataTable();
                string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    using (MySqlCommand cmd = new MySqlCommand("UpdatePaciente", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@inCodigo", codigo);
                        //cmd.Parameters.AddWithValue("@inIdCategoria", idCategoria);
                        //cmd.Parameters.AddWithValue("@inDescripcion", descripcion);
                        //cmd.Parameters.AddWithValue("@inActivo", activo);
                        //cmd.Parameters.AddWithValue("@inImagen", imagen);
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
