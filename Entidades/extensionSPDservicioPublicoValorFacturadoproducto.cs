using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [Serializable]
    public class extensionSPDservicioPublicoValorFacturadoproducto
    {
        [XmlElement]
        public decimal totalUnidades { get; set; }
        [XmlElement]
        public string unidadMedidaTotal { get; set; }
        [XmlElement]
        public decimal consumoTotal { get; set; }
        [XmlElement]
        public decimal unidadesConsumidas { get; set; }
        [XmlElement]
        public string unidadMedidaConsumida { get; set; }
        [XmlElement]
        public decimal valorConsumoParcial { get; set; }
        [XmlElement]
        public decimal valorUnitario  { get; set; }
        [XmlElement]
        public decimal  cantidadAplicaPrecio { get; set; }
        [XmlElement]
        public string unidadMedida { get; set; }
        [XmlElement]
        public extensionSPDservicioPublicoValorFacturadoProductoDescuento[] descuentos { get; set; }
        [XmlElement]
        public extensionSPDservicioPublicoValorFacturadoProductoCargo[] cargos { get; set; }
        [XmlElement]
        public extensionSPDservicioPublicoValorFacturadoProductoLecturaContador[] lecturaContador { get; set; }
    }
}
