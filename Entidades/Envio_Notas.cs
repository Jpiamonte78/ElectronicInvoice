using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Envio_Notas
    {
        public int id_Envio_Nota { get; set; }
        public string Tiponota { get; set; }
        public string Numnota { get; set; }
        public string Codpredio { get; set; }
        public DateTime fecha_envio { get; set; }
        public string codigo_respuesta { get; set; }
        public string mensaje_respuesta { get; set; }
        public string xml_enviado { get; set; }
    }
}
