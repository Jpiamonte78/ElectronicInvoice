using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [Serializable]
    public class documento
    {
        [XmlElement]
        public string numeroDocumento { get; set; }
        [XmlElement]
        public string uuidDocumento { get; set; }
        [XmlElement]
        public string tipoDocumento { get; set; }
        [XmlElement]
        public string subtipoDocumento { get; set; }
        [XmlElement]
        public string tipoOperacion { get; set; }
        [XmlElement]
        public string divisa { get; set; }
        [XmlElement]
        public string fechaDocumento { get; set; }
        [XmlElement]
        public string refPedido { get; set; }
        [XmlElement]
        public string unidadOrganizativa { get; set; }
        [XmlElement]
        public string fechaVencimiento { get; set; }
        [XmlElement]
        public string direccionFactura { get; set; }
        [XmlElement]
        public string distritoFactura { get; set; }
        [XmlElement]
        public string ciudadFactura { get; set; }
        [XmlElement]
        public string departamentoFactura { get; set; }
        [XmlElement]
        public string codigoPostalFactura { get; set; }
        [XmlElement]
        public string paisFactura { get; set; }
        [XmlElement]
        public string incoterm { get; set; }
        [XmlElement]
        public string motivoRect { get; set; }
        [XmlElement]
        public string fechaIniFacturacion { get; set; }
        [XmlElement]
        public string fechaFinFacturacion { get; set; }
        [XmlElement]
        public DocumentosReferenciados documentosReferenciados { get; set; }
        [XmlElement]
        public documentosAdjuntos documentosAdjuntos { get; set; }
        [XmlElement]
        public documentoProveedor proveedor { get; set; }
        [XmlElement]
        public documentoCliente cliente { get; set; }
        [XmlElement]
        public documentoemailsEnvio[] emailsEnvio { get; set; }
        [XmlElement]
        public documentoImpuesto[]  impuestos { get; set; }
        [XmlElement]
        public documentoRetencion[] retenciones { get; set; }
        [XmlElement]
        public documentoAnticipo[] anticipos { get; set; }
        [XmlElement]
        public documentolineaDescuentos descuentos { get; set; }
        [XmlElement]
        public documentolineaCargos cargos { get; set; }
        [XmlElement]
        public documentodatosTotales datosTotales { get; set; }
        [XmlElement]
        public documentocondicionesPago condicionesPago { get; set; }
        [XmlElement]
        public documentodatoAdicional[] datosAdicionales { get; set; }
        [XmlElement]
        public documentoLineas lineas { get; set; }
        [XmlElement]
        public documentoExtensionSPD extensionSPD { get; set; }
    }
}
