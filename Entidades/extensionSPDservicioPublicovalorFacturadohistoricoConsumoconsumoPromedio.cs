using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [Serializable]
    public class extensionSPDservicioPublicovalorFacturadohistoricoConsumoconsumoPromedio
    {
        [XmlElement]
        public decimal unidadesConsumidas { get; set; }
        [XmlElement]
        public string unidadMedidaConsumo { get; set; }
        [XmlElement]
        public decimal mediaConsumo { get; set; }
        [XmlElement]
        public string unidadMedidaMediaConsumo { get; set; }
    }
}
