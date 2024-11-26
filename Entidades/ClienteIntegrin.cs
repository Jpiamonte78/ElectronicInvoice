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
        public int tipo_identificacion { get; set; }
        public string Identificacion { get; set; }
        public string dv { get; set; }
        public int tipo_persona { get; set; }
        public string Razon_social { get; set; }
        public string Nombre_cliente { get; set; }
        public string Apellido1_cliente { get; set; }
        public string Apellido2_Cliente { get; set; }
        public string Direccion_cliente { get; set; }
        public string ciudad_cliente { get; set; }
        public string departamento_cliente { get; set; }
        public string telefono_cliente { get; set; }
        public string email_cliente { get; set; }
        public string ciclo { get; set; }
        public string uso { get; set; }
        public string estrato { get; set; }
        public string Nmedidor { get; set; }
        public string matricula { get; set; }
        public string zona_postal { get; set; }
        public string resp_rut { get; set; }
        public string tributos { get; set; }
        public bool actualizado { get; set; }
        public string nomciudad { get; set; }
        public string nomdepto { get; set; }
        public List<string> selectedresp { get; set; }
        public List<string> selectedTrib { get; set; }
        public List<TipoDocumento> ltipodocumento { get; set; }

        public List<TipoPersona> lpersona { get; set; }

        public List<MaeDepto> ldeptos { get; set; }
        public List<MaeMuni> lmuni { get; set; }

        public List<Responsabilidades> lresponsabilidades { get; set; }
        public List<Tributos> ltributos { get; set; }

        public string UsoTarifa { get; set; }
        public string EstratoTarifa {  get; set; }
    }
}
