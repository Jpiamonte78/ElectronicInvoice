using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Envio_Factura
    {
        public int id_Envio_Factura { get; set; }
        
        public string Numfactura { get; set; }
        public string Codpredio { get; set; }
        public string codigo_respuesta { get; set; }
        public string mensaje_respuesta { get; set; }
        public string xml_enviado { get; set; }
        public DateTime fecha_envio { get; set; }
        public DateTime fecha { get; set; }
        public decimal valor_total { get; set; }

    }
}
