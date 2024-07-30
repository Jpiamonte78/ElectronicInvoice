using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class cliente
    {
        public int id_cliente { get; set; }
        public int tipo_identificacion { get; set; }
	    public int identificacion { get; set; }
	    public int dv { get; set; }
        public string razon_social { get; set; }
        public string nombres { get; set; }
        public string apellido_1 { get; set; }
        public string apellido_2 { get; set; }
        public int tipo_persona { get; set; }
        public string direccion { get; set; }
        public string departamento { get; set; }
        public string ciudad { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string zona_postal { get; set; }
    }
}
