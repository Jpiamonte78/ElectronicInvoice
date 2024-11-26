using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace AccesoDatos
{
    public class ADLecturas : ConexionBD
    {
        CultureInfo culture = new CultureInfo("en-US");
        public int Consultar_Lecturas(string ruta)
        {
            int reg = 0;
            List<Lectura> lLecturas = new List<Lectura>();
            SQLiteDataReader dr;
            SQLiteConnection sqlcon = new SQLiteConnection();
            try
            {
                sqlcon = GetConnIntegrin(ruta);
                string query = "select ciclo,anio, periodo,codigo,ifnull(lect_ant,0) as lect_ant,ifnull(lect_act,0) as lect_act,ifnull(consumo,0) as consumo,ifnull(promedio,0) as promedio,fecha_lect,ifnull(consumo1,0) as consumo1,ifnull(consumo2,0) as consumo2,ifnull(consumo3,0) as consumo3,ifnull(consumo4,0) as consumo4,ifnull(consumo5,0) as consumo5,ifnull(consumo6,0) as consumo6 from BdLecturas where fecha_lect is not NULL";
                SQLiteCommand cmd = new SQLiteCommand(query, sqlcon);
                dr = cmd.ExecuteReader();
                Lectura lectura = new Lectura();
                while (dr.Read())
                {
                    string fecha = dr["fecha_lect"].ToString();
                    string formatof = "ddd MMM dd yyyy HH:mm:ss 'GMT'K '(hora estándar de Colombia)'";
                    lectura = new Lectura();
                    lectura.ciclo = dr["ciclo"].ToString();
                    lectura.anio = Convert.ToInt32(dr["anio"]);
                    lectura.periodo = dr["periodo"].ToString();
                    lectura.codigo_p = dr["codigo"].ToString();
                    lectura.lect_anterior = TryParseInt(dr["lect_ant"]);
                    lectura.lect_actual = TryParseInt(dr["lect_act"]);
                    lectura.consumo = TryParseInt(dr["consumo"]);
                    lectura.consumo_promedio = TryParseInt(dr["promedio"]);
                    try
                    {
                        lectura.fecha_lectura = DateTime.ParseExact(fecha, formatof, CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {
                        lectura.fecha_lectura = Convert.ToDateTime(fecha);
                    }
                    lectura.consumo1 = TryParseInt(dr["consumo1"]);
                    lectura.consumo2 = TryParseInt(dr["consumo2"]);
                    lectura.consumo3 = TryParseInt(dr["consumo3"]);
                    lectura.consumo4 = TryParseInt(dr["consumo4"]);
                    lectura.consumo5 = TryParseInt(dr["consumo5"]);
                    lectura.consumo6 = TryParseInt(dr["consumo6"]);
                    lLecturas.Add(lectura);
                }
                reg = InsertarLecturas(lLecturas);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Se ha presentado un error en Consultar_Lecturas " + ex.Message, ex);
            }
            finally
            {
                sqlcon.Close();
            }
            return reg;
        }

        private int InsertarLecturas(List<Lectura> llecturas)
        {
            int reg = 0;
            List<Lectura> lectintegrin = llecturas;
            SqlConnection sqlconn = GetConnDB();
            foreach (Lectura lect in lectintegrin)
            {
                using (SqlCommand cmd = new SqlCommand("SpLecturas", sqlconn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "Insertar");
                    cmd.Parameters.AddWithValue("id_lecturas", DBNull.Value);
                    cmd.Parameters.AddWithValue("ciclo", lect.ciclo);
                    cmd.Parameters.AddWithValue("anio", lect.anio);
                    cmd.Parameters.AddWithValue("periodo", lect.periodo);
                    cmd.Parameters.AddWithValue("codigo_p", lect.codigo_p);
                    cmd.Parameters.AddWithValue("lect_actual", lect.lect_actual);
                    cmd.Parameters.AddWithValue("lect_anterior", lect.lect_anterior);
                    cmd.Parameters.AddWithValue("consumo", lect.consumo);
                    cmd.Parameters.AddWithValue("consumo_promedio", lect.consumo_promedio);
                    cmd.Parameters.AddWithValue("fecha_lectura", lect.fecha_lectura);
                    cmd.Parameters.AddWithValue("consumo1", lect.consumo1);
                    cmd.Parameters.AddWithValue("consumo2", lect.consumo2);
                    cmd.Parameters.AddWithValue("consumo3", lect.consumo3);
                    cmd.Parameters.AddWithValue("consumo4", lect.consumo4);
                    cmd.Parameters.AddWithValue("consumo5", lect.consumo5);
                    cmd.Parameters.AddWithValue("consumo6", lect.consumo6);
                    try
                    {
                        reg += cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Se ha presentado un error en InsertarLecturas: " + ex.Message, ex);
                    }
                }
            }
            sqlconn.Close();

            return reg;
        }

        public Lectura Consultar_lecturas_suscriptor(string codsus, string ciclo, string periodo, int anio)
        {
            Lectura lect = new Lectura();
            using (SqlConnection conn = GetConnDB())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SpLecturas";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "Consultar");
                    cmd.Parameters.AddWithValue("id_lecturas", DBNull.Value);
                    cmd.Parameters.AddWithValue("ciclo", ciclo);
                    cmd.Parameters.AddWithValue("anio", anio);
                    cmd.Parameters.AddWithValue("periodo", periodo);
                    cmd.Parameters.AddWithValue("codigo_p", codsus);
                    cmd.Parameters.AddWithValue("lect_actual", DBNull.Value);
                    cmd.Parameters.AddWithValue("lect_anterior", DBNull.Value);
                    cmd.Parameters.AddWithValue("consumo", DBNull.Value);
                    cmd.Parameters.AddWithValue("consumo_promedio", DBNull.Value);
                    cmd.Parameters.AddWithValue("fecha_lectura", DBNull.Value);
                    cmd.Parameters.AddWithValue("consumo1", DBNull.Value);
                    cmd.Parameters.AddWithValue("consumo2", DBNull.Value);
                    cmd.Parameters.AddWithValue("consumo3", DBNull.Value);
                    cmd.Parameters.AddWithValue("consumo4", DBNull.Value);
                    cmd.Parameters.AddWithValue("consumo5", DBNull.Value);
                    cmd.Parameters.AddWithValue("consumo6", DBNull.Value);
                    try
                    {
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            lect = new Lectura();
                            lect.Id_Lecturas = Convert.ToInt32(dr["id_lecturas"]);
                            lect.ciclo = dr["ciclo"].ToString();
                            lect.periodo = dr["periodo"].ToString();
                            lect.anio = Convert.ToInt16(dr["anio"]);
                            lect.codigo_p = dr["codigo_p"].ToString();
                            lect.lect_actual = Convert.ToDecimal(dr["lect_actual"]);
                            lect.lect_anterior = Convert.ToDecimal(dr["lect_anterior"]);
                            lect.consumo = Convert.ToDecimal(dr["consumo"]);
                            lect.consumo_promedio = Convert.ToDecimal(dr["consumo_promedio"]);
                            lect.fecha_lectura = Convert.ToDateTime(dr["fecha_lectura"]);
                            lect.consumo1 = Convert.ToInt32(dr["consumo1"]);
                            lect.consumo2 = Convert.ToInt32(dr["consumo2"]);
                            lect.consumo3 = Convert.ToInt32(dr["consumo3"]);
                            lect.consumo4 = Convert.ToInt32(dr["consumo4"]);
                            lect.consumo5 = Convert.ToInt32(dr["consumo5"]);
                            lect.consumo6 = Convert.ToInt32(dr["consumo6"]);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Consultar_lecturas_suscriptor: " + ex.Message, ex);
                    }
                }
            }
            return lect;
        }

        private int TryParseInt(object value)
        {
            if (value != null && int.TryParse(value.ToString(), out int result))
            {
                return result;
            }
            return 0; // Valor predeterminado en caso de que el dato no sea numérico o sea nulo }
        }
    }
}
