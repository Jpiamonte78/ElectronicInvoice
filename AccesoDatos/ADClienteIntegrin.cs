using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entidades;
using System.Data.SQLite;


namespace AccesoDatos
{
    public class ADClienteIntegrin:ConexionBD
    {
        private List<ClienteIntegrin> ConsultarClienteIntegrin(string ruta)
        {
            List<ClienteIntegrin> lcliente = new List<ClienteIntegrin>();
            SQLiteDataReader dr;
            SQLiteConnection sqlcon = new SQLiteConnection();
            try
            {
                sqlcon = GetConnIntegrin(ruta);
                string query = "select ciclo,codigo,apellidos,nombres,identificacion,direccion from BdSuscriptores";
                SQLiteCommand cmd = new SQLiteCommand(query, sqlcon);
                dr = cmd.ExecuteReader();
                ClienteIntegrin cliente = new ClienteIntegrin();
                while(dr.Read())
                {
                    cliente = new ClienteIntegrin();
                    cliente.ciclo = dr["ciclo"].ToString();
                    cliente.Codpredio = dr["codigo"].ToString();
                    cliente.Apellido1_cliente = dr["apellidos"].ToString();
                    cliente.Nombre_cliente = dr["nombres"].ToString();
                    cliente.Identificacion = dr["identificacion"].ToString();
                    cliente.Direccion_cliente = dr["direccion"].ToString();
                    lcliente.Add(cliente);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Se ha presentado un error: "+ex.Message,ex);
            }
            finally
            {
                sqlcon.Close();
            }
            return lcliente;
        }
    }
}
