using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data;
using System.Data.SqlClient;

namespace AccesoDatos
{
    public class MaeDepto_MuniAD:ConexionBD
    {
        public List<MaeDepto> Consultar_Deptos()
        {
            List<MaeDepto> ldeptos = new List<MaeDepto>();
            using (SqlConnection conn = GetConnDB())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SpMaeDepto";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "Consultar_Todos");
                    cmd.Parameters.AddWithValue("CodDepto", DBNull.Value);
                    try
                    {
                        MaeDepto depto = new MaeDepto();
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            depto = new MaeDepto();
                            depto.CodDepto = dr["CodDepto"].ToString();
                            depto.Nombre = dr["Nombre"].ToString();
                            ldeptos.Add(depto);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Consultar_Deptos: "+ex.Message,ex);
                    }
                }
            }
            return ldeptos;
        }

        public List<MaeMuni> Consultar_Municipios(string CodDepto)
        {
            List<MaeMuni> lmuni = new List<MaeMuni>();
            using (SqlConnection conn = GetConnDB())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SpMaeMuni";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "Consultar_por_Depto");
                    cmd.Parameters.AddWithValue("CodDepto", CodDepto);
                    cmd.Parameters.AddWithValue("CodMuni", DBNull.Value);
                    try
                    {
                        MaeMuni muni = new MaeMuni();
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            muni = new MaeMuni();
                            muni.CodDepto = dr["CodDepto"].ToString();
                            muni.Nombre = dr["Nombre"].ToString();
                            muni.CodMuni = dr["CodMuni"].ToString() ;
                            lmuni.Add(muni);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Consultar_Municipios: " + ex.Message, ex);
                    }
                }
            }
            return lmuni;
        }


    }
}
