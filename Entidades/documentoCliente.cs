using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [Serializable]
    public class documentoCliente
    {
        [XmlElement]
        public string idCliente { get; set; }
        [XmlElement]
        public string tipoDocumentoIdCliente { get; set; }
        [XmlElement]
        public string regimenCliente { get; set; }
        [XmlElement]
        public string razonSocialCliente { get; set; }
        [XmlElement]
        public string nombreCliente { get; set; }
        [XmlElement]
        public string apellido1Cliente { get; set; }
        [XmlElement]
        public string apellido2Cliente { get; set; }
        [XmlElement]
        public string tipoPersonaCliente { get; set; }
        [XmlElement]
        public string direccionCliente { get; set; }
        [XmlElement]
        public string distritoCliente { get; set; }
        [XmlElement]
        public string ciudadCliente { get; set; }
        [XmlElement]
        public string departamentoCliente { get; set; }
        [XmlElement]
        public string codigoPostalCliente { get; set; }
        [XmlElement]
        public string paisCliente { get; set; }
        [XmlElement]
        public string telefonoCliente { get; set; }
        [XmlElement]
        public string emailCliente { get; set; }
        [XmlElement]
        public string responsabilidadesRutCliente { get; set; }
        [XmlElement]
        public string tributosCliente { get; set; }
    }
}
