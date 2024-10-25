using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [Serializable]
    public class extensionSPDservicioPublico
    {
        [XmlElement]
        public string numLinea { get; set; }
        [XmlElement]
        public string indTercero { get; set; }
        [XmlElement]
        public string servicioFacturado { get; set; }
        [XmlElement]
        public string empresa { get; set; }
        [XmlElement]
        public string motivo { get; set; }
        [XmlElement]
        public string numeroContrato { get; set; }
        [XmlElement]
        public string fechaContrato { get; set; }
        [XmlElement]
        public string informacionContrato { get; set; }
        [XmlElement]
        public extensionSPDservicioPublicosuscriptor[] subscriptor { get; set; }
        [XmlElement]
        public extensionSPDservicioPublicovalorFacturado[] valorFacturado { get; set; }
    }
}
