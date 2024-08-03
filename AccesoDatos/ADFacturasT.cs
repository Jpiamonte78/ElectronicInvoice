using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Microsoft.Data.Sqlite;

namespace AccesoDatos
{
    internal class ADFacturasT:ConexionBD
    {
        private List<FacturasT> Consultar_FacturasTotal(string ruta)
        {
            List<FacturasT> lfacturas = new List<FacturasT>();
            SqliteDataReader dr;
            SqliteConnection sqlcon = new SqliteConnection();
            try
            {
                sqlcon = GetConnIntegrin(ruta);
                string query = "select ciclo,anio, periodo,numfact,codpredio,valor_tota,fecha,codigobarras from BdFacturasT";
                SqliteCommand cmd = new SqliteCommand(query, sqlcon);
                dr = cmd.ExecuteReader();
                FacturasT factura = new FacturasT();
                while(dr.Read())
                {
                    string feclim = dr["codigobarras"].ToString().Substring(0, -8);
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

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Se ha presentado un error en Consultar_FacturasTotal "+ ex.Message,ex);
            }
            finally
            {
                sqlcon.Close();
            }
            return lfacturas;
        }
    }
}
