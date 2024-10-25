using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [Serializable]
    public class extensionSPDservicioPublicovalorFacturadohistoricoConsumo
    {
        [XmlElement]
        public decimal pagoAnterior { get; set; }
        [XmlElement]
        public extensionSPDservicioPublicovalorFacturadohistoricoConsumoconsumoMensual consumoMensual { get; set; }
        [XmlElement]
        public extensionSPDservicioPublicovalorFacturadohistoricoConsumoconsumoPromedio consumoPromedio { get; set; }
    }
}
