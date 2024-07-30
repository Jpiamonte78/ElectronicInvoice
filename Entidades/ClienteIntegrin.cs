using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Entidades
{
    
    public class ClienteIntegrin
    {
        public int Id_Cliente_integrin { get; set; }
        public string Codpredio { get; set; }
        public string Identificacion { get; set; }
        public string Nombre_cliente { get; set; }
        public string Apellido1_cliente { get; set; }
        public string Apellido2_Cliente { get; set; }
        public string Direccion_cliente { get; set; }
        public string ciudad_cliente { get; set; }
        public string departamento_cliente { get; set; }
        public string telefono_cliente { get; set; }
        public string email_cliente { get; set; }
        public string ciclo { get; set; }
    }
}
