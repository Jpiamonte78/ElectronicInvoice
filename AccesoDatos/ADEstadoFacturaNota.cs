using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AccesoDatos
{
    public class ADEstadoFacturaNota:ConexionBD
    {
        public int InsertarEstadoFactura(EstadoFactura EstadoFactura)
        {
            int result = 0;
            using (var conn = GetConnDB())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SpEstadoFactura";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "Insertar");
                    cmd.Parameters.AddWithValue("Id_Estado_Factura", EstadoFactura.Id_Estado_Factura);
                    cmd.Parameters.AddWithValue("IdEnvio_Factura", EstadoFactura.IdEnvio_Factura);
                    cmd.Parameters.AddWithValue("EstadoDian", EstadoFactura.EstadoDian);
                    cmd.Parameters.AddWithValue("EstadoEnvioCliente", EstadoFactura.EstadoEnvioCliente);
                    cmd.Parameters.AddWithValue("fechaAlta", EstadoFactura.fechaAlta);
                    cmd.Parameters.AddWithValue("fechaEstadoDIAN", EstadoFactura.fechaEstadoDIAN);
                    cmd.Parameters.AddWithValue("fechaEstadoEnvioCliente", EstadoFactura.fechaEstadoEnvioCliente);
                    cmd.Parameters.AddWithValue("fechaFactura", EstadoFactura.fechaFactura);
                    cmd.Parameters.AddWithValue("ObservacionesEstadoDIAN", EstadoFactura.ObservacionesEstadoDIAN);
                    cmd.Parameters.AddWithValue("UUID", EstadoFactura.UUID);
                    try
                    {
                        result = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("InsertarEstadoFactura: "+ex.Message,ex);
                    }
                }
            }
            return result;
        }

        public int InsertarEstadoNota(EstadoFactura EstadoFactura)
        {
            int result = 0;
            using (var conn = GetConnDB())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SpEstadoNota";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("accion", "Insertar");
                    cmd.Parameters.AddWithValue("Id_Estado_Nota", EstadoFactura.Id_Estado_Nota);
                    cmd.Parameters.AddWithValue("IdEnvio_Nota", EstadoFactura.IdEnvio_Nota);
                    cmd.Parameters.AddWithValue("EstadoDian", (string.IsNullOrEmpty(EstadoFactura.EstadoDian))?"":EstadoFactura.EstadoDian);
                    cmd.Parameters.AddWithValue("EstadoEnvioCliente", string.IsNullOrEmpty(EstadoFactura.EstadoEnvioCliente) ? "":EstadoFactura.EstadoEnvioCliente);
                    cmd.Parameters.AddWithValue("fechaAlta",string.IsNullOrEmpty(EstadoFactura.fechaAlta)?"": EstadoFactura.fechaAlta);
                    cmd.Parameters.AddWithValue("fechaEstadoDIAN", string.IsNullOrEmpty(EstadoFactura.fechaEstadoDIAN)?"": EstadoFactura.fechaEstadoDIAN);
                    cmd.Parameters.AddWithValue("fechaEstadoEnvioCliente", string.IsNullOrEmpty(EstadoFactura.fechaEstadoEnvioCliente)?"": EstadoFactura.fechaEstadoEnvioCliente);
                    cmd.Parameters.AddWithValue("fechaFactura", string.IsNullOrEmpty(EstadoFactura.fechaFactura)?"": EstadoFactura.fechaFactura);
                    cmd.Parameters.AddWithValue("ObservacionesEstadoDIAN", string.IsNullOrEmpty(EstadoFactura.ObservacionesEstadoDIAN)?"": EstadoFactura.ObservacionesEstadoDIAN);
                    cmd.Parameters.AddWithValue("UUID",string.IsNullOrEmpty(EstadoFactura.UUID)?"": EstadoFactura.UUID);
                    try
                    {
                        result = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("InsertarEstadoFactura: " + ex.Message, ex);
                    }
                }
            }
            return result;
        }

    }
}
