using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SQLite;
using System.Data;
using System.Data.SqlClient;

namespace AccesoDatos
{
    public class ADFacturasT:ConexionBD
    {
        public int Consultar_FacturasTotal(string ruta)
        {
            int reg = 0;
            List<FacturasT> lfacturas = new List<FacturasT>();
            SQLiteDataReader dr;
            SQLiteConnection sqlcon = new SQLiteConnection();
            try
            {
                sqlcon = GetConnIntegrin(ruta);
                string query = "select ciclo,anio, periodo,numfact,codpredio,valor_tota,fecha,codigobarras from BdFacturasT";
                SQLiteCommand cmd = new SQLiteCommand(query, sqlcon);
                dr = cmd.ExecuteReader();
                FacturasT factura = new FacturasT();
                while(dr.Read())
                {
                    string feclim = dr["codigobarras"].ToString().Substring(dr["codigobarras"].ToString().Length-8);
                    factura = new FacturasT();
                    factura.ciclo = dr["ciclo"].ToString();
                    factura.anio = Convert.ToInt32(dr["anio"]);
                    factura.periodo = dr["periodo"].ToString();
                    factura.numfact = dr["numfact"].ToString();
                    factura.codpredio = dr["codpredio"].ToString();
                    factura.valor_total = Convert.ToDecimal(dr["valor_tota"]);
                    factura.fecha = Convert.ToDateTime(dr["fecha"]);
                    factura.fecha_limite = Convert.ToDateTime(feclim.Substring(4,4)+'-'+feclim.Substring(2,2)+'-'+feclim.Substring(0,2));
                    lfacturas.Add(factura);
                }
                reg = InsertarFacturasT(lfacturas);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Se ha presentado un error en Consultar_FacturasTotal "+ ex.Message,ex);
            }
            finally
            {
                sqlcon.Close();
            }
            return reg;
        }

        private int InsertarFacturasT(List<FacturasT> lfacturaT)
        {
            int reg = 0;
            List<FacturasT> factintegrin = lfacturaT;
            SqlConnection sqlconn = GetConnDB();
            foreach(FacturasT fact in factintegrin)
            {
                using (SqlCommand cmd = new SqlCommand("SpFacturasT", sqlconn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "insertar");
                    cmd.Parameters.AddWithValue("id_factura", DBNull.Value);
                    cmd.Parameters.AddWithValue("ciclo", fact.ciclo);
                    cmd.Parameters.AddWithValue("anio", fact.anio);
                    cmd.Parameters.AddWithValue("periodo", fact.periodo);
                    cmd.Parameters.AddWithValue("numfact", fact.numfact);
                    cmd.Parameters.AddWithValue("codpredio", fact.codpredio);
                    cmd.Parameters.AddWithValue("valor_total", fact.valor_total);
                    cmd.Parameters.AddWithValue("fecha", fact.fecha);
                    cmd.Parameters.AddWithValue("fecha_limite", fact.fecha_limite);
                    try
                    {
                        reg += Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Se ha presentado un error en InsertarFacturasT: " + ex.Message, ex);
                    }
                }
            }
            sqlconn.Close();

            return reg;
        }
    }
}
