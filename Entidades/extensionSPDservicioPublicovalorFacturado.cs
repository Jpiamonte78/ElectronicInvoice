using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [Serializable]
    public class extensionSPDservicioPublicovalorFacturado
    {
        [XmlElement]
        public string ciclo { get; set; }
        [XmlElement]
        public string tipoPeriodicidad { get; set; }
        [XmlElement]
        public string infoAdicional { get; set; }
        [XmlElement]
        public extensionSPDservicioPublicoValorFacturadoproducto[] producto { get; set; }
        [XmlElement]
        public extensionSPDservicioPublicovalorFacturadohistoricoConsumo historicoConsumos { get; set; }
    }
}
