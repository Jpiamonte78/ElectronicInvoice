﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppElectronicInvoice.ServiceInvoway;
using AccesoDatos;
using Entidades;
using System.Runtime.InteropServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.ServiceModel;
using WebAppElectronicInvoice.Security;
using System.Configuration;
using System.Dynamic;
using System.Net.Http;
using System.Web.Services.Description;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Net.Sockets;
using System.Net;
using System.Security;
using System.Globalization;

namespace WebAppElectronicInvoice.Controllers
{
    public class EnviarFacturaController : Controller
    {

        private readonly string url = "https://pruebas.invoway.com/INVOWAYLATAM/services/ConsultaEstadosFacturasWSPort";

        string usuario = ConfigurationManager.AppSettings["UsuarioInvoway"];
        string contraseña = ConfigurationManager.AppSettings["PasswordInvoway"];

        // GET: EnviarFactura
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> EnviarFacturas(DateTime fecha)
        {
            string ciclo = "";
            string periodo = "";
            int anio = 0;
            // Configura el cliente
            List<FacturasT> lfacturas = new List<FacturasT>();
            lfacturas = new ADFacturasT().Consultar_Facturas(fecha);
            List<FacturasD> ldetalle = new List<FacturasD>();
            documento factura = new documento();
            if (lfacturas.Any())
            {
                factura = new documento();
                documentoProveedor proveedor = new documentoProveedor();
                proveedor.idProveedor = "830140206";
                documentoCliente docCliente = new documentoCliente();
                foreach (FacturasT fact in lfacturas)
                {
                    ciclo = fact.ciclo;
                    periodo = fact.periodo;
                    anio = fact.anio;
                    Lectura lectura1 = new Lectura();
                    lectura1 = new ADLecturas().Consultar_lecturas_suscriptor(fact.codpredio, ciclo, periodo, anio);

                    docCliente = new documentoCliente();
                    ClienteIntegrin Cliente = new ADClienteIntegrin().ConsultarporCodpredio(fact.codpredio);
                    factura.numeroDocumento = fact.numfact;
                    factura.tipoDocumento = "DE";
                    factura.subtipoDocumento = "60";
                    factura.tipoOperacion = "601";
                    factura.divisa = "COP";
                    factura.fechaDocumento = ConvertirFecha(fact.fecha.ToString());
                    factura.fechaVencimiento = ConvertirFecha(fact.fecha_limite.ToString());
                    factura.direccionFactura = "Carrera 98 A No. 15A-80";
                    factura.distritoFactura = "Fontibon";
                    factura.ciudadFactura = "11001";
                    factura.departamentoFactura = "11";
                    factura.codigoPostalFactura = "110921";
                    factura.paisFactura = "CO";
                    factura.fechaIniFacturacion = ConvertirFecha("01/06/2024 00:00:00");
                    factura.fechaFinFacturacion = ConvertirFecha("30/06/2024 00:00:00");
                    factura.proveedor = proveedor;

                    docCliente.idCliente = Cliente.Identificacion.ToString() + "-" + Cliente.dv.ToString();
                    docCliente.tipoDocumentoIdCliente = Cliente.tipo_identificacion.ToString();
                    docCliente.razonSocialCliente = Cliente.Razon_social;
                    docCliente.nombreCliente = Cliente.Nombre_cliente;
                    docCliente.apellido1Cliente = Cliente.Apellido1_cliente;
                    docCliente.apellido2Cliente = Cliente.Apellido2_Cliente;
                    docCliente.tipoPersonaCliente = Cliente.tipo_persona.ToString();
                    docCliente.direccionCliente = Cliente.Direccion_cliente;
                    docCliente.distritoCliente = Cliente.nomciudad;
                    docCliente.ciudadCliente = Cliente.ciudad_cliente;
                    docCliente.departamentoCliente = Cliente.departamento_cliente;
                    docCliente.codigoPostalCliente = Cliente.zona_postal;
                    docCliente.paisCliente = "CO";
                    docCliente.telefonoCliente = Cliente.telefono_cliente;
                    docCliente.emailCliente = Cliente.email_cliente;
                    docCliente.responsabilidadesRutCliente = Cliente.resp_rut;
                    docCliente.tributosCliente = Cliente.tributos;
                    factura.cliente = docCliente;
                    string[] emails = new string[1];
                    emails[0] = Cliente.email_cliente;
                    factura.emailsEnvio = emails;
                    ldetalle = new ADFacturasD().Consultar_Detalle(fact.ciclo, fact.periodo, fact.anio, fact.codpredio);
                    documentoLinea linea = new documentoLinea();
                    uint i = 0;
                    string[] productos = new string[7];
                    productos[0] = "01";
                    productos[1] = "02";
                    productos[2] = "16";
                    productos[3] = "30";
                    productos[4] = "76";
                    productos[5] = "RX";
                    productos[6] = "SI";
                    decimal subsidioFSSRI = ldetalle.Where(x => x.codigo_c == "96").First().valor * 1;
                    decimal subsidioFECF = ldetalle.Where(x => x.codigo_c == "97").First().valor * -1;
                    decimal ajuste = ldetalle.Where(x => x.codigo_c == "29").First().valor;
                    decimal subtotal = 0;
                    documentoLineaDescuento descuento = new documentoLineaDescuento();
                    documentoLineaCargo cargo = new documentoLineaCargo();
                    List<documentoLineaDescuento> ldescuentos = new List<documentoLineaDescuento>();
                    List<documentoLineaCargo> lcargos = new List<documentoLineaCargo>();
                    List<documentoLinea> lLineas = new List<documentoLinea>();
                    List<documentoExtensionSPDServicioPublicoValorFacturadoProducto> lproductos = new List<documentoExtensionSPDServicioPublicoValorFacturadoProducto>();
                    documentoExtensionSPDServicioPublicoValorFacturadoProducto producto = new documentoExtensionSPDServicioPublicoValorFacturadoProducto();
                    foreach (FacturasD det in ldetalle)
                    {
                        if (productos.Contains(det.codigo_c))
                        {
                            i++;
                            linea = new documentoLinea();
                            linea.numLinea = i;
                            linea.idEstandarReferencia = "999";
                            linea.referenciaItem = det.codigo_c;
                            linea.descripcionItem = det.nombre_c;
                            if (det.codigo_c == "02")
                            {
                                descuento = new documentoLineaDescuento();
                                descuento.@base = det.valor;
                                descuento.valor = subsidioFSSRI;
                                descuento.porcentaje = (subsidioFSSRI / det.valor) * 100;
                                descuento.motivo = "Subsidio FSSRI";
                                ldescuentos.Add(descuento);
                                descuento = new documentoLineaDescuento();
                                descuento.@base = det.valor;
                                descuento.valor = subsidioFECF;
                                descuento.porcentaje = (subsidioFECF / det.valor) * 100;
                                descuento.motivo = "Subsidio FECF";
                                ldescuentos.Add(descuento);
                                linea.unidadMedida = "MTQ";
                                if (ajuste < 0)
                                {
                                    descuento = new documentoLineaDescuento();
                                    descuento.@base = det.valor;
                                    descuento.valor = ajuste * -1;
                                    descuento.porcentaje = ((ajuste * -1) / det.valor) * 100;
                                    descuento.motivo = "Ajuste";
                                    ldescuentos.Add(descuento);
                                    linea.descuentoLinea = subsidioFSSRI + subsidioFECF + (ajuste * -1);
                                }
                                else
                                {
                                    linea.descuentoLinea = subsidioFSSRI + subsidioFECF;
                                    linea.cargoLinea = ajuste * -1;
                                    cargo = new documentoLineaCargo();
                                    cargo.@base = det.valor;
                                    cargo.valor = ajuste * -1;
                                    cargo.porcentaje = ((ajuste * -1) / det.valor) * 100;
                                    cargo.motivo = "Ajuste";
                                    lcargos.Add(cargo);
                                }
                                linea.descuentos = ldescuentos.ToArray();
                                linea.cargos = lcargos.ToArray();
                                linea.unidadesLinea = det.consumo;
                                linea.precioUnidad = det.valor / det.consumo;
                                linea.subtotalLinea = det.valor;

                                //llenar la información que se registra en la sección de SPD
                                producto.totalUnidades = det.consumo;
                                producto.unidadMedidaTotal = "MTQ";
                                producto.consumoTotal = det.valor + linea.cargoLinea - linea.descuentoLinea;
                                producto.unidadesConsumidas = det.consumo;
                                producto.unidadMedidaConsumida = "MTQ";
                                producto.valorConsumoParcial = det.valor;
                                producto.valorUnitario = det.valor / det.consumo;
                                List<documentoExtensionSPDServicioPublicoValorFacturadoProductoDescuentos> ldctos = new List<documentoExtensionSPDServicioPublicoValorFacturadoProductoDescuentos>();
                                List<documentoExtensionSPDServicioPublicoValorFacturadoProductoCargos> lcargofac = new List<documentoExtensionSPDServicioPublicoValorFacturadoProductoCargos>();
                                documentoExtensionSPDServicioPublicoValorFacturadoProductoDescuentos dctos = new documentoExtensionSPDServicioPublicoValorFacturadoProductoDescuentos();
                                dctos.razonDescuento = "Subsidio FSSRI";
                                dctos.valorDto = subsidioFSSRI;
                                ldctos.Add(dctos);
                                dctos = new documentoExtensionSPDServicioPublicoValorFacturadoProductoDescuentos();
                                dctos.razonDescuento = "Subsidio FECF";
                                dctos.valorDto = subsidioFECF;
                                ldctos.Add(dctos);
                                if (ajuste < 0)
                                {
                                    dctos = new documentoExtensionSPDServicioPublicoValorFacturadoProductoDescuentos();
                                    dctos.razonDescuento = "Ajuste";
                                    dctos.valorDto = ajuste;
                                    ldctos.Add(dctos);
                                }
                                else
                                {
                                    documentoExtensionSPDServicioPublicoValorFacturadoProductoCargos carfact = new documentoExtensionSPDServicioPublicoValorFacturadoProductoCargos();
                                    carfact.razonCargo = "Ajuste";
                                    carfact.valorCargo = ajuste;
                                    lcargofac.Add(carfact);
                                }
                                producto.descuentos = ldctos.ToArray();
                                producto.cargos = lcargofac.ToArray();
                            }
                            else
                            {
                                linea.unidadMedida = "94";
                                linea.unidadesLinea = 1;
                                linea.precioUnidad = det.valor;
                                linea.subtotalLinea = det.valor;
                                // se cargan los demás conceptos diferentes a consumos
                                producto.totalUnidades = 1;
                                producto.unidadMedidaTotal = "94";
                                producto.consumoTotal = 1;
                                producto.unidadesConsumidas = 1;
                                producto.unidadMedidaConsumida = "94";
                                producto.valorConsumoParcial = det.valor;
                                producto.valorUnitario = det.valor;

                            }
                            List<documentoExtensionSPDServicioPublicoValorFacturadoProductoLecturaContador> llecturas = new List<documentoExtensionSPDServicioPublicoValorFacturadoProductoLecturaContador>();
                            documentoExtensionSPDServicioPublicoValorFacturadoProductoLecturaContador lectura = new documentoExtensionSPDServicioPublicoValorFacturadoProductoLecturaContador();
                            lectura.datosMedidor = Cliente.Nmedidor;
                            lectura.unidadesLecturaAnterior = lectura1.lect_anterior;
                            lectura.unidadMedidaAnterior = "MTQ";
                            lectura.fechaLecturaActual = ConvertirFecha(lectura1.fecha_lectura.ToString());
                            lectura.unidadesLecturaActual = lectura1.lect_actual;
                            lectura.unidadMedidaActual = "MTQ";
                            llecturas.Add(lectura);
                            producto.lecturaContador = llecturas.ToArray();
                            lproductos.Add(producto);
                            subtotal += det.valor;
                            linea.totalLinea = det.valor + linea.cargoLinea - linea.descuentoLinea;
                            lLineas.Add(linea);
                        }
                        factura.lineas = lLineas.ToArray();
                    }
                    documentoDatosTotales totales = new documentoDatosTotales();
                    totales.subtotal = subtotal;
                    totales.porcDescuentoFinal = 0;
                    totales.descuentoFinal = (ajuste < 0) ? subsidioFECF + subsidioFSSRI + ajuste : subsidioFECF + subsidioFSSRI;
                    totales.totalCargos = (ajuste > 0) ? ajuste : 0;
                    totales.totalBase = subtotal;
                    totales.totalImpuestos = 0;
                    totales.totalGastos = 0;
                    totales.totalDocumento = totales.totalBase + totales.totalImpuestos + totales.totalGastos;
                    totales.totalRetenciones = 0;
                    totales.totalAnticipos = 0;
                    totales.aPagar = totales.totalDocumento - totales.descuentoFinal + totales.totalCargos;
                    factura.datosTotales = totales;
                    List<documentoCondicionPago> lcondiciones = new List<documentoCondicionPago>();
                    documentoCondicionPago condicionpago = new documentoCondicionPago();
                    condicionpago.formaPago = "1";
                    condicionpago.medioPago = "10";
                    lcondiciones.Add(condicionpago);
                    factura.condicionesPago = lcondiciones.ToArray();
                    ServiceInvoway.documentoExtensionSPD extSPD = new documentoExtensionSPD();

                    extSPD.referenciaPago = fact.numfact;
                    extSPD.estratoPredio = Convert.ToInt16(Cliente.estrato).ToString();
                    extSPD.tipoUsoPredio = Cliente.uso;
                    documentoExtensionSPDServicioPublico servicio = new documentoExtensionSPDServicioPublico();
                    servicio.numLinea = "1";
                    servicio.indTercero = "N";
                    servicio.servicioFacturado = "GAS";
                    servicio.empresa = "ENERCER";
                    servicio.motivo = "";
                    servicio.numeroContrato = Cliente.matricula.ToString();
                    List<documentoExtensionSPDServicioPublicoSubscriptor> lsuscriptor = new List<documentoExtensionSPDServicioPublicoSubscriptor>();
                    documentoExtensionSPDServicioPublicoSubscriptor suscriptor = new documentoExtensionSPDServicioPublicoSubscriptor();
                    suscriptor.nombre = Cliente.Nombre_cliente + ' ' + Cliente.Apellido1_cliente + ' ' + Cliente.Apellido2_Cliente + ' ' + Cliente.Razon_social;
                    suscriptor.direccionPostal = Cliente.Direccion_cliente;
                    suscriptor.direccionEntrega = Cliente.Direccion_cliente;
                    suscriptor.ciudad = Cliente.nomciudad;
                    suscriptor.departamento = Cliente.departamento_cliente;
                    suscriptor.pais = "CO";
                    suscriptor.tipoEstrato = Convert.ToInt16(Cliente.estrato).ToString();
                    suscriptor.email = Cliente.email_cliente;
                    lsuscriptor.Add(suscriptor);
                    servicio.subscriptor = lsuscriptor.ToArray();
                    List<documentoExtensionSPDServicioPublicoValorFacturado> lvalfactura = new List<documentoExtensionSPDServicioPublicoValorFacturado>();
                    documentoExtensionSPDServicioPublicoValorFacturado valfactura = new documentoExtensionSPDServicioPublicoValorFacturado();
                    valfactura.ciclo = "1 Mes";
                    valfactura.tipoPeriodicidad = "1";
                    valfactura.producto = lproductos.ToArray();
                    lvalfactura.Add(valfactura);
                    servicio.valorFacturado = lvalfactura.ToArray();
                    List<documentoExtensionSPDServicioPublico> lservicios = new List<documentoExtensionSPDServicioPublico>();
                    lservicios.Add(servicio);
                    extSPD.servicioPublico = lservicios.ToArray();
                    factura.extensionSPD = extSPD;
                }
            }
            try
            {
                var resultado = await EnviarSolicitudSOAP(url, factura);
                return Json(new { success = true, response = resultado });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        public string SerializarEntidadXML(documento factura)
        {
            var xmlSerializer = new XmlSerializer(typeof(documento));
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true,
                Encoding = Encoding.UTF8
            };



            using (var stringWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
                {
                    xmlSerializer.Serialize(xmlWriter, factura, namespaces);
                }
                return stringWriter.ToString();
            }
        }

        public string ConstruirSoapEnvelope(string xmlContenido)
        {
            return $@"
                <soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:impl='http://impl.consultaestados.ws.saaf.delogica.es/'>
                   <soapenv:Header>
                        <wsse:Security soapenv:mustUnderstand=""0"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecuritysecext-
                            1.0.xsd"" xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurityutility-1.0.xsd"">
                            <wsse:UsernameToken wsu:Id=""UsernameToken-2"">
                            <wsse:Username>enercer-int</wsse:Username>
                            <wsse:Password type=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText"">5cMI7c6w6£pv</wsse:Password>
                            </wsse:UsernameToken>
                        </wsse:Security>
                    </soapenv:Header>
                   <soapenv:Body>
                      <impl:entregaFactura>
                         {xmlContenido}
                      </impl:entregaFactura>
                   </soapenv:Body>
                </soapenv:Envelope>";
        }

        public async Task<string> EnviarSolicitudSOAP(string url, documento factura)
        {
            string xmlcontenido = SerializarEntidadXML(factura);
            string soapEnvelope = ConstruirSoapEnvelope(xmlcontenido);
            using (var client = new HttpClient())
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                var content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml");
                var response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        private string ConvertirFecha(string fecha)
        {
            string inputDate = fecha; // Fecha en formato dd/MM/yyyy HH:mm:ss tt
            string outputDate = "";
            try
            {
                int day = Convert.ToDateTime(inputDate).Day;
                int month = Convert.ToDateTime(inputDate).Month;
                int year = Convert.ToDateTime(inputDate).Year;
                int hour = Convert.ToDateTime(inputDate).Hour;
                int minute = Convert.ToDateTime(inputDate).Minute;
                int second = Convert.ToDateTime(inputDate).Second;

                outputDate = year.ToString() + "-" + month.ToString().PadLeft(2, '0') + "-" + day.ToString().PadLeft(2, '0') + " " + hour.ToString().PadLeft(2, '0') + ":" + minute.ToString().PadLeft(2, '0') + ":" + second.ToString().PadLeft(2, '0');
            }
            catch (Exception ex)
            {
                outputDate = "formato no valido: "+ex.Message;
            }
            
            return outputDate;
        }
    }
}