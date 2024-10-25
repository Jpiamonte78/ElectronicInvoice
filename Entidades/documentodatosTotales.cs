using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Entidades
{
    [Serializable]
    public class documentodatosTotales
    {
        [XmlElement]
        public decimal  subtotal { get; set; }
        [XmlElement]
        public decimal porcDescuentoFinal { get; set; }
        [XmlElement]
        public decimal descuentoFinal { get; set; }
        [XmlElement]
        public decimal totalCargos { get; set; }
        [XmlElement]
        public decimal totalBase { get; set; }
        [XmlElement]
        public decimal totalImpuestos { get; set; }
        [XmlElement]
        public decimal totalGastos { get; set; }
        [XmlElement]
        public decimal totalDocumento { get; set; }
        [XmlElement]
        public decimal totalRetenciones { get; set; }
        [XmlElement]
        public decimal totalAnticipos { get; set; }
        [XmlElement]
        public decimal aPagar { get; set; }
    }
}
