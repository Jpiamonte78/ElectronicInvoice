using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EstadoFactura
    {
        public int Id_Estado_Factura { get; set; }
        public int IdEnvio_Factura { get; set; }

        public int Id_Estado_Nota { get; set; }
        public int IdEnvio_Nota { get; set; }


        public string EstadoDian { get; set; }
        public string EstadoEnvioCliente { get; set; }
        public string fechaAlta { get; set; }
        public string fechaEstadoDIAN { get; set; }
        public string fechaEstadoEnvioCliente { get; set; }
        public string fechaFactura { get; set; }
        public string ObservacionesEstadoDIAN { get; set; }
        public string UUID { get; set; }
    }
}
