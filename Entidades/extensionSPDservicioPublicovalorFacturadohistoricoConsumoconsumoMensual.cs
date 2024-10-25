using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [Serializable]
    public class extensionSPDservicioPublicovalorFacturadohistoricoConsumoconsumoMensual
    {
        [XmlElement]
        public decimal unidadesConsumidas { get; set; }
        [XmlElement]
        public string unidadMedidaConsumo { get; set; }
        [XmlElement]
        public string fechaInicioPeriodo { get; set; }
        [XmlElement]
        public string fechaFinPeriodo { get; set; }
        [XmlElement]
        public decimal diasFacturados { get; set; }
        [XmlElement]
        public string unidadMedidaDiasFacturados { get; set; }
        [XmlElement]
        public decimal valorAPagar { get; set; }
        [XmlElement]
        public string divisa { get; set; }

    }
}
