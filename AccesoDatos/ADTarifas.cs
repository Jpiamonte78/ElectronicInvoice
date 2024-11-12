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
                string query = "select ciclo,anio,periodo,uso,estrato,valor,valorl,basicol,suplem,extra1,pleac_r,con_bas,acreal,asreal,extral1,asresi,asgra from BdTariGas";
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
                    tari.mvjm = Convert.ToDecimal(dr["suplem"],culture);
                    tari.dv1 = Convert.ToDecimal(dr["con_bas"],culture);
                    tari.cm = Convert.ToDecimal(dr["acreal"], culture);
                    tari.tm = Convert.ToDecimal(dr["pleac_r"], culture);
                    tari.gm = Convert.ToDecimal(dr["extra1"], culture);
                    tari.poder_c = Convert.ToDecimal(dr["extral1"], culture);
                    tari.pleno_mvjm = Convert.ToDecimal(dr["valorl"], culture) + Convert.ToDecimal(dr["basicol"], culture);
                    tari.neto_mvjm = Convert.ToDecimal(dr["valorl"], culture);
                    tari.subs_contrib = Convert.ToDecimal(dr["asreal"], culture);
                    tari.cons_prom_subs = Convert.ToDecimal(dr["asresi"], culture);
                    tari.factor_correccion = Convert.ToDecimal(dr["asgra"], culture);
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
                    cmd.Parameters.AddWithValue("mvjm", tari.mvjm);
                    cmd.Parameters.AddWithValue("dv1", tari.dv1);
                    cmd.Parameters.AddWithValue("cm", tari.cm);
                    cmd.Parameters.AddWithValue("tm", tari.tm);
                    cmd.Parameters.AddWithValue("gm", tari.gm);
                    cmd.Parameters.AddWithValue("poder_c", tari.poder_c);
                    cmd.Parameters.AddWithValue("pleno_mvjm", tari.pleno_mvjm);
                    cmd.Parameters.AddWithValue("neto_mvjm", tari.neto_mvjm);
                    cmd.Parameters.AddWithValue("subs_contrib", tari.subs_contrib);
                    cmd.Parameters.AddWithValue("cons_prom_subs", tari.cons_prom_subs);
                    cmd.Parameters.AddWithValue("factor_correccion", tari.factor_correccion);
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
        public List<Tarifas> ConsultarTarifas(string ciclo, string periodo,string anio,string uso, string estrato)
        {
            List<Tarifas> ltarifas = new List<Tarifas>();
            using (SqlConnection conn = GetConnDB())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SpTarifas";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "Consultar");
                    cmd.Parameters.AddWithValue("Id_Tarifa", DBNull.Value);
                    cmd.Parameters.AddWithValue("ciclo", ciclo);
                    cmd.Parameters.AddWithValue("anio", anio);
                    cmd.Parameters.AddWithValue("periodo", periodo);
                    cmd.Parameters.AddWithValue("uso", uso);
                    cmd.Parameters.AddWithValue("estrato", estrato);
                    cmd.Parameters.AddWithValue("cargo_fijo", DBNull.Value);
                    cmd.Parameters.AddWithValue("consumo", DBNull.Value);
                    cmd.Parameters.AddWithValue("mvjm", DBNull.Value);
                    cmd.Parameters.AddWithValue("dv1", DBNull.Value);
                    cmd.Parameters.AddWithValue("cm", DBNull.Value);
                    cmd.Parameters.AddWithValue("tm", DBNull.Value);
                    cmd.Parameters.AddWithValue("gm", DBNull.Value);
                    cmd.Parameters.AddWithValue("poder_c", DBNull.Value);
                    cmd.Parameters.AddWithValue("pleno_mvjm", DBNull.Value);
                    cmd.Parameters.AddWithValue("neto_mvjm", DBNull.Value);
                    cmd.Parameters.AddWithValue("subs_contrib", DBNull.Value);
                    cmd.Parameters.AddWithValue("cons_prom_subs", DBNull.Value);
                    cmd.Parameters.AddWithValue("factor_correccion", DBNull.Value);
                    try
                    {
                        Tarifas tari = new Tarifas();
                        var dr = cmd.ExecuteReader();
                        while(dr.Read())
                        {
                            tari = new Tarifas();
                            tari.id_tarifa = Convert.ToInt32(dr["id_tarifa"]);
                            tari.ciclo = dr["ciclo"].ToString();
                            tari.anio = Convert.ToInt32(dr["anio"]);
                            tari.periodo = dr["periodo"].ToString();
                            tari.uso = dr["uso"].ToString();
                            tari.estrato = dr["estrato"].ToString();
                            tari.Cargo_fijo = Convert.ToDecimal(dr["Cargo_fijo"]);
                            tari.Consumo = Convert.ToInt32(dr["Consumo"]);
                            tari.mvjm = Convert.ToDecimal(dr["mvjm"]);
                            tari.dv1 = Convert.ToDecimal(dr["dv1"]);
                            tari.cm = Convert.ToDecimal(dr["cm"]);
                            tari.tm = Convert.ToDecimal(dr["tm"]);
                            tari.gm = Convert.ToDecimal(dr["gm"]);
                            tari.poder_c = Convert.ToDecimal(dr["poder_C"]);
                            tari.pleno_mvjm = Convert.ToDecimal(dr["pleno_mvjm"]);
                            tari.neto_mvjm = Convert.ToDecimal(dr["neto_mvjm"]);
                            tari.subs_contrib = Convert.ToDecimal(dr["subs_contrib"]);
                            tari.cons_prom_subs = Convert.ToDecimal(dr["cons_prom_subs"]);
                            tari.factor_correccion = Convert.ToDecimal(dr["factor_correccion"]);
                            ltarifas.Add(tari);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("ConsultarTarifas: "+ex.Message,ex);
                    }
                }
            }
            return ltarifas;
        }
    }
}
