using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class NotasT : ClienteIntegrin
    {
        public int id_nota { get; set; }
        public string prefijo { get; set; }
        public string fechainiperiodo { get; set; }
        public string fechafinperiodo { get; set; }
        public string anio { get; set; }
        public string periodo { get; set; }
        public string numfact { get; set; }
        public string codpredio { get; set; }
        public decimal valor_mod { get; set; }
        public string Numfactura { get; set; }
        public DateTime? fecha_envio { get; set; }
        public string mensaje { get; set; }
        public string prefijoNota { get; set; }
        public int NumeroNota { get; set; }

    }
}
