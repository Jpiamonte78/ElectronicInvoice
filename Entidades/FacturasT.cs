using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class FacturasT:ClienteIntegrin
    {
        public int id_factura { get; set; }
        public string Prefijo { get; set; }
        public int anio { get; set; }
        public string periodo { get; set; }
        public string fechainiperiodo { get; set; }
        public string fechafinperiodo { get; set; }   
        public string numfact { get; set; }
        public string codpredio { get; set; }
        public decimal valor_total { get; set; }
        public DateTime fecha { get; set; }
        public DateTime fecha_limite { get; set; }
        public string mensaje { get; set; }


    }
}
