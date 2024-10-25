using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [Serializable]
    public class documentoExtensionSPD
    {
        [XmlElement]
        public string referenciaPago { get; set; }
        [XmlElement]
        public string fechaUltimoPago { get; set; }
        [XmlElement]
        public string estratoPredio { get; set; }
        [XmlElement]
        public string tipoUsoPredio { get; set; }
        [XmlElement]
        public extensionSPDservicioPublico[] servicioPublico { get; set; }

    }
}
