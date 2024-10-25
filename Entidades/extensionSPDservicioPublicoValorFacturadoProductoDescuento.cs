using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [Serializable]
    public class extensionSPDservicioPublicoValorFacturadoProductoDescuento
    {
        [XmlElement]
        public string razonDescuento { get; set; }
        [XmlElement]
        public decimal valorDto { get; set; }
        [XmlElement]
        public extensionSPDservicioPublico[] servicioPublico { get; set; }
    }
}
