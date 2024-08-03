using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Entidades;

namespace AccesoDatos
{
    internal class ADFacturasD:ConexionBD
    {
        private List<FacturasD> Consultar_detalles(string ruta)
        {
            List<FacturasD> ldetalle = new List<FacturasD>();
            SqliteDataReader dr;
            SqliteConnection con = new SqliteConnection();
            try
            {
                con = GetConnIntegrin(ruta);
                string query = "select a.ciclo,a.anio,a.periodo,a.numfac,a.codpredio,a.codigo_c,b.nombre_c,a.valor_c from BdFacturasD a inner join BdConceptos b on a.codigo_c=b.codigo_c";
                SqliteCommand cmd = new SqliteCommand(query, con);
                dr = cmd.ExecuteReader();
                FacturasD detalle = new FacturasD();
                while (dr.Read())
                {
                    detalle = new FacturasD();
                    detalle.ciclo = dr["ciclo"].ToString();
                    detalle.anio = Convert.ToInt32(dr["anio"]);
                    detalle.periodo = dr["periodo"].ToString();
                    detalle.numfac = dr["numfac"].ToString();
                    detalle.codpredio = dr["codpredio"].ToString();
                    detalle.codigo_c = dr["codigo_c"].ToString();
                    detalle.nombre_c = dr["nombre_c"].ToString();
                    detalle.valor = Convert.ToDecimal(dr["valor_c"]);
                    ldetalle.Add(detalle);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Se ha presentado error en Consultar_detalles: " + ex.Message, ex);
            }
            finally
            {
                con.Close();
            }
            return ldetalle;
        }
    }
}
