using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data;
using System.Data.SQLite;
using System.Globalization;

namespace AccesoDatos
{
    public class ADTarifas:ConexionBD
    {
        CultureInfo culture = new CultureInfo("en-US");
        public int Consultar_tarifas(string ruta)
        {
            int reg = 0;
            List<Tarifas> ltarifas = new List<Tarifas>();
            SQLiteDataReader dr;
            SQLiteConnection sqlcon = new SQLiteConnection();
            try
            {
                sqlcon = GetConnIntegrin(ruta);
                string query = "select ciclo,anio,periodo,uso,estrato,valor,valorl from BdTariGas";
                SQLiteCommand cmd = new SQLiteCommand(query,sqlcon);
                dr = cmd.ExecuteReader();
                Tarifas tari = new Tarifas();
                while (dr.Read()) {
                    tari = new Tarifas();
                    tari.ciclo = dr["ciclo"].ToString();
                    tari.anio = Convert.ToInt32(dr["anio"]);
                    tari.periodo = dr["periodo"].ToString();
                    tari.uso = dr["uso"].ToString();
                    tari.estrato = dr["estrato"].ToString();
                    tari.Cargo_fijo = Convert.ToDecimal(dr["valor"],culture);
                    tari.Consumo = Convert.ToDecimal(dr["valorl"],culture);
                    ltarifas.Add(tari);
                }
                reg = Insertar_tarifas(ltarifas);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Consultar_tarifas: "+ex.Message,ex);
            }
            finally
            {
                sqlcon.Close();
            }
            return reg;
        }

        private int Insertar_tarifas(List<Tarifas> ltar)
        {
            int reg = 0;
            SqlConnection conn = GetConnDB();
            foreach(Tarifas tari in ltar)
            {
                using (SqlCommand cmd = new SqlCommand("SpTarifas",conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "Insertar");
                    cmd.Parameters.AddWithValue("Id_Tarifa", DBNull.Value);
                    cmd.Parameters.AddWithValue("ciclo", tari.ciclo);
                    cmd.Parameters.AddWithValue("anio", tari.anio);
                    cmd.Parameters.AddWithValue("periodo", tari.periodo);
                    cmd.Parameters.AddWithValue("uso", tari.uso);
                    cmd.Parameters.AddWithValue("estrato", tari.estrato);
                    cmd.Parameters.AddWithValue("cargo_fijo", tari.Cargo_fijo);
                    cmd.Parameters.AddWithValue("consumo", tari.Consumo);
                    try
                    {
                        reg += cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Insertar_tarifas: " + ex.Message,ex);
                    }
                }
            }
            conn.Close();
            return reg;
        }
    }
}
