using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [Serializable]
    public class documentoLinea
    {
        [XmlElement]
        public uint numLinea { get; set; }
        [XmlElement]
        public string idEstandarReferencia { get; set; }
        [XmlElement]
        public string referenciaItem { get; set; }
        [XmlElement]
        public string descripcionItem { get; set; }
        [XmlElement]
        public string unidadMedida { get; set; }
        [XmlElement]
        public int unidadesLinea { get; set; }
        [XmlElement]
        public decimal precioUnidad { get; set; }
        [XmlElement]
        public decimal subtotalLinea { get; set; }
        [XmlElement]
        public decimal descuentoLinea { get; set; }
        [XmlElement]
        public decimal cargoLinea { get; set; }
        [XmlElement]        
        public decimal totalLinea { get; set; }
        [XmlElement]
        public documentolineaDescuentos descuentos { get; set; }
        [XmlElement]
        public documentolineaCargos cargos { get; set; }
        [XmlElement]
        public documentoLineadatoAdicional[] datosAdicionales { get; set; }
        [XmlElement]
        public documentoLineaimpuestoAdicional[] impuestosAdicionales { get; set; }
    }
}
