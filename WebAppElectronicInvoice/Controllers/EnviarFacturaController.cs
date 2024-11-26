﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
using System.Diagnostics.Contracts;
using System.Net.Http.Headers;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Kernel.Geom;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Layout.Borders;

namespace WebAppElectronicInvoice.Controllers
{
    public class EnviarFacturaController : Controller
    {

        private readonly string url = ConfigurationManager.AppSettings["Urlinvoway"]; //"https://pruebas.invoway.com/INVOWAYLATAM/services/ConsultaEstadosFacturasWSPort";

        string usuario = ConfigurationManager.AppSettings["UsuarioInvoway"];
        string contraseña = ConfigurationManager.AppSettings["PasswordInvoway"];
        string codsus = "";
        CultureInfo culture = new CultureInfo("es-CO");

        // GET: EnviarFactura
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult FacturasPendientes()
        {
            List<FacturasT> lfacturas = new List<FacturasT>();
            lfacturas = new ADFacturasT().Consultar_Facturas();
            return View(lfacturas);
        }

        [HttpGet]
        public async Task<JsonResult> EnviarFacturas()
        {
            string result = "";
            string ciclo = "";
            string periodo = "";
            int anio = 0;
            bool success = false;
            // Configura el cliente
            List<FacturasT> lfacturas = new List<FacturasT>();
            lfacturas = new ADFacturasT().Consultar_Facturas();
            List<FacturasD> ldetalle = new List<FacturasD>();
            documento factura = new documento();
            if (lfacturas.Any())
            {
                factura = new documento();
                documentoProveedor proveedor = new documentoProveedor();
                proveedor.idProveedor = "830140206-1";
                documentoCliente docCliente = new documentoCliente();
                foreach (FacturasT fact in lfacturas)
                {
                    ciclo = fact.ciclo;
                    periodo = fact.periodo;
                    anio = fact.anio;
                    Lectura lectura1 = new Lectura();
                    lectura1 = new ADLecturas().Consultar_lecturas_suscriptor(fact.codpredio, ciclo, periodo, anio);
                    codsus = fact.codpredio;
                    docCliente = new documentoCliente();
                    if (!string.IsNullOrEmpty(fact.Identificacion))
                    {
                        factura.numeroDocumento = fact.Prefijo + fact.numfact;
                        factura.tipoDocumento = "DE";
                        factura.subtipoDocumento = "60";
                        factura.tipoOperacion = "602"; //Facturación en Sitio
                        factura.divisa = "COP";
                        factura.fechaDocumento = ConvertirFecha(DateTime.Now.ToString(), "horas"); //ConvertirFecha(fact.fecha.ToString());
                        factura.unidadOrganizativa = "DEFAULT";
                        factura.fechaVencimiento = ConvertirFecha(fact.fecha_limite.ToString(), "");
                        factura.direccionFactura = "Calle 12 No. 10-49";
                        factura.distritoFactura = "Garagoa";
                        factura.ciudadFactura = "15299";
                        factura.departamentoFactura = "15";
                        factura.codigoPostalFactura = "152860";
                        factura.paisFactura = "CO";
                        factura.fechaIniFacturacion = fact.fechainiperiodo;
                        factura.fechaFinFacturacion = fact.fechafinperiodo;
                        factura.proveedor = proveedor;
                        if (!string.IsNullOrEmpty(fact.dv.ToString().Trim()))
                            docCliente.idCliente = fact.Identificacion.ToString() + "-" + fact.dv.ToString();
                        else
                            docCliente.idCliente = fact.Identificacion.ToString();
                        docCliente.tipoDocumentoIdCliente = fact.tipo_identificacion.ToString();
                        docCliente.razonSocialCliente = fact.Razon_social;
                        docCliente.nombreCliente = fact.Nombre_cliente;
                        docCliente.apellido1Cliente = fact.Apellido1_cliente;
                        docCliente.apellido2Cliente = fact.Apellido2_Cliente;
                        docCliente.tipoPersonaCliente = fact.tipo_persona.ToString();
                        docCliente.direccionCliente = fact.Direccion_cliente;
                        docCliente.distritoCliente = fact.nomciudad;
                        docCliente.ciudadCliente = fact.ciudad_cliente;
                        docCliente.departamentoCliente = fact.departamento_cliente;
                        docCliente.codigoPostalCliente = fact.zona_postal;
                        docCliente.paisCliente = "CO";
                        docCliente.telefonoCliente = fact.telefono_cliente;
                        docCliente.emailCliente = fact.email_cliente;
                        docCliente.responsabilidadesRutCliente = fact.resp_rut;
                        docCliente.tributosCliente = fact.tributos;
                        factura.cliente = docCliente;
                        List<documentoemailsEnvio> emails = new List<documentoemailsEnvio>();
                        documentoemailsEnvio email = new documentoemailsEnvio();
                        email.email = fact.email_cliente;
                        emails.Add(email);
                        factura.emailsEnvio = emails.ToArray();
                        ldetalle = new ADFacturasD().Consultar_Detalle(fact.ciclo, fact.periodo, fact.anio, fact.codpredio);
                        documentoLinea linea = new documentoLinea();
                        uint i = 0;
                        string[] productos = new string[30];
                        productos[0] = "01";
                        productos[1] = "02";
                        productos[2] = "16";
                        //productos[3] = "30";
                        productos[4] = "76";
                        productos[5] = "RX";
                        productos[6] = "SI";
                        productos[7] = "CG";
                        productos[8] = "CM";
                        productos[9] = "CR";
                        productos[10] = "CV";
                        productos[11] = "DC";
                        productos[12] = "MA";
                        productos[13] = "MC";
                        productos[14] = "MI";
                        productos[15] = "MO";
                        productos[16] = "PA";
                        productos[17] = "PE";
                        productos[18] = "RE";
                        productos[19] = "RQ";
                        productos[20] = "TC";
                        productos[21] = "VA";
                        productos[22] = "VT";
                        productos[23] = "IS";
                        productos[24] = "A2";
                        var resFSSRI = ldetalle.Where(x => x.codigo_c == "96").FirstOrDefault();
                        decimal subsidioFSSRI = 0;
                        if (resFSSRI != null)
                            subsidioFSSRI = resFSSRI.valor;
                        decimal subsidioFECF = 0;
                        var resFECF = ldetalle.Where(x => x.codigo_c == "97").FirstOrDefault();
                        if (resFECF != null)
                            subsidioFECF = resFECF.valor * -1;
                        decimal ajuste = 0;
                        var resAjuste = ldetalle.Where(x => x.codigo_c == "29").FirstOrDefault();
                        if (resAjuste != null)
                            ajuste = resAjuste.valor;
                        decimal subtotal = 0;
                        decimal totaldoc = 0;
                        documentoLineaDescuento descuento = new documentoLineaDescuento();
                        documentoLineaCargo cargo = new documentoLineaCargo();
                        documentoLineas lineas = new documentoLineas();
                        documentolineaDescuentos descuentos = new documentolineaDescuentos();
                        documentolineaCargos cargos = new documentolineaCargos();
                        List<documentoLineaDescuento> ldescuentos = new List<documentoLineaDescuento>();
                        List<documentoLineaCargo> lcargos = new List<documentoLineaCargo>();
                        List<documentoLinea> lLineas = new List<documentoLinea>();
                        List<extensionSPDservicioPublicoValorFacturadoproducto> lproductos = new List<extensionSPDservicioPublicoValorFacturadoproducto>();
                        extensionSPDservicioPublicoValorFacturadoproducto producto = new extensionSPDservicioPublicoValorFacturadoproducto();
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
                                    if (subsidioFSSRI < 0)
                                    {
                                        descuento = new documentoLineaDescuento();
                                        descuento.@base = det.valor;
                                        descuento.valor = subsidioFSSRI * -1;
                                        descuento.porcentaje = Math.Round(((subsidioFSSRI * -1) / det.valor) * 100, 4);
                                        descuento.motivo = "Subsidio FSSRI";
                                        ldescuentos.Add(descuento);
                                    }
                                    else
                                    {
                                        if(subsidioFSSRI>0)
                                        {
                                            cargo = new documentoLineaCargo();
                                            cargo.@base = (det.valor-subsidioFECF);
                                            cargo.valor = subsidioFSSRI;
                                            cargo.porcentaje = Math.Round((subsidioFSSRI / (det.valor-subsidioFECF)) * 100, 4);
                                            cargo.motivo = "Subsidio FSSRI";
                                            lcargos.Add(cargo);
                                        }
                                    }
                                    //se retira el subsidio como descuento porque se está neteando en el consumo.
                                    //descuento = new documentoLineaDescuento();
                                    //descuento.@base = det.valor;
                                    //descuento.valor = subsidioFECF;
                                    //descuento.porcentaje = Math.Round((subsidioFECF / det.valor) * 100, 2);
                                    //descuento.motivo = "Subsidio FECF";
                                    //ldescuentos.Add(descuento);
                                    linea.unidadMedida = "MTQ";
                                    if (ajuste < 0)
                                    {
                                        descuento = new documentoLineaDescuento();
                                        descuento.@base = (det.valor-subsidioFECF);
                                        descuento.valor = ajuste * -1;
                                        descuento.porcentaje = Math.Round(((ajuste * -1) / (det.valor-subsidioFECF)) * 100, 4);
                                        descuento.motivo = "Ajuste";
                                        ldescuentos.Add(descuento);
                                    }
                                    else
                                    {
                                        if(ajuste>0)
                                        {
                                            cargo = new documentoLineaCargo();
                                            cargo.@base = (det.valor-subsidioFECF);
                                            cargo.valor = ajuste;
                                            cargo.porcentaje = Math.Round((ajuste / (det.valor-subsidioFECF)) * 100, 4);
                                            cargo.motivo = "Ajuste";
                                            lcargos.Add(cargo);
                                        }
                                        
                                    }
                                    cargos.cargo = lcargos.ToArray();
                                    linea.cargos = cargos;
                                    linea.cargoLinea = lcargos.Sum(x => x.valor);
                                    descuentos.descuento = ldescuentos.ToArray();
                                    linea.descuentoLinea = ldescuentos.Sum(x => x.valor);
                                    linea.descuentos = descuentos;
                                    linea.unidadesLinea = det.cantidad;
                                    linea.precioUnidad = (det.valor - subsidioFECF) / det.cantidad;
                                    linea.subtotalLinea = (det.valor - subsidioFECF);
                                    //llenar la información que se registra en la sección de SPD
                                    producto.totalUnidades = det.cantidad;
                                    producto.unidadMedidaTotal = "MTQ";
                                    producto.consumoTotal = det.valor + linea.cargoLinea - linea.descuentoLinea - subsidioFECF;
                                    producto.unidadesConsumidas = det.cantidad;
                                    producto.unidadMedidaConsumida = "MTQ";
                                    producto.valorConsumoParcial = det.valor - subsidioFECF;
                                    producto.valorUnitario = (det.valor - subsidioFECF) / det.cantidad;
                                    List<extensionSPDservicioPublicoValorFacturadoProductoDescuento> ldctos = new List<extensionSPDservicioPublicoValorFacturadoProductoDescuento>();
                                    List<extensionSPDservicioPublicoValorFacturadoProductoCargo> lcargofac = new List<extensionSPDservicioPublicoValorFacturadoProductoCargo>();
                                    extensionSPDservicioPublicoValorFacturadoProductoDescuento dctos = new extensionSPDservicioPublicoValorFacturadoProductoDescuento();
                                    extensionSPDservicioPublicoValorFacturadoProductoCargo carfact = new extensionSPDservicioPublicoValorFacturadoProductoCargo();
                                    if (subsidioFSSRI < 0)
                                    {
                                        dctos.razonDescuento = "Subsidio FSSRI";
                                        dctos.valorDto = subsidioFSSRI * -1;
                                        ldctos.Add(dctos);
                                    }
                                    else
                                    {
                                        if(subsidioFSSRI>0)
                                        {
                                            carfact.razonCargo = "Subsidio FSSRI";
                                            carfact.valorCargo = subsidioFSSRI;
                                            lcargofac.Add(carfact);
                                        }
                                    }
                                    //dctos = new extensionSPDservicioPublicoValorFacturadoProductoDescuento();
                                    //dctos.razonDescuento = "Subsidio FECF";
                                    //dctos.valorDto = subsidioFECF;
                                    //ldctos.Add(dctos);
                                    if (ajuste < 0)
                                    {
                                        dctos = new extensionSPDservicioPublicoValorFacturadoProductoDescuento();
                                        dctos.razonDescuento = "Ajuste";
                                        dctos.valorDto = ajuste * -1;
                                        ldctos.Add(dctos);
                                    }
                                    else
                                    {
                                        if (ajuste >0)
                                        {
                                            carfact = new extensionSPDservicioPublicoValorFacturadoProductoCargo();
                                            carfact.razonCargo = "Ajuste";
                                            carfact.valorCargo = ajuste;
                                            lcargofac.Add(carfact);
                                        }
                                    }
                                    producto.cargos = lcargofac.ToArray();
                                    producto.descuentos = ldctos.ToArray();
                                }
                                else
                                {
                                    linea.unidadMedida = "94";
                                    linea.unidadesLinea = 1;
                                    linea.precioUnidad = det.valor;
                                    linea.subtotalLinea = det.valor;
                                    linea.descuentoLinea = 0;
                                    // se cargan los demás conceptos diferentes a consumos
                                    producto.totalUnidades = 1;
                                    producto.unidadMedidaTotal = "94";
                                    producto.consumoTotal = 1;
                                    producto.unidadesConsumidas = 1;
                                    producto.unidadMedidaConsumida = "94";
                                    producto.valorConsumoParcial = det.valor;
                                    producto.valorUnitario = det.valor;
                                }
                                List<extensionSPDservicioPublicoValorFacturadoProductoLecturaContador> llecturas = new List<extensionSPDservicioPublicoValorFacturadoProductoLecturaContador>();
                                extensionSPDservicioPublicoValorFacturadoProductoLecturaContador lectura = new extensionSPDservicioPublicoValorFacturadoProductoLecturaContador();
                                lectura.datosMedidor = fact.Nmedidor;
                                lectura.unidadesLecturaAnterior = lectura1.lect_anterior;
                                lectura.unidadMedidaAnterior = "MTQ";
                                lectura.fechaLecturaActual = ConvertirFecha(lectura1.fecha_lectura.ToString(), "");
                                lectura.unidadesLecturaActual = lectura1.lect_actual;
                                lectura.unidadMedidaActual = "MTQ";
                                llecturas.Add(lectura);
                                producto.lecturaContador = llecturas.ToArray();
                                lproductos.Add(producto);
                                subtotal += (det.codigo_c == "02") ? (det.valor - subsidioFECF) : det.valor;
                                linea.totalLinea = linea.subtotalLinea + linea.cargoLinea - linea.descuentoLinea;
                                totaldoc += linea.totalLinea;
                                lLineas.Add(linea);
                                lineas.linea = lLineas.ToArray();
                                factura.lineas = lineas;
                            }
                            
                        }
                        if(subtotal>0)
                        {
                            documentodatosTotales totales = new documentodatosTotales();
                            totales.subtotal = subtotal;
                            totales.porcDescuentoFinal = 0;
                            totales.descuentoFinal = 0;// (ajuste < 0) ? subsidioFECF + subsidioFSSRI + (ajuste*-1) : subsidioFECF + subsidioFSSRI;
                            totales.totalCargos = 0;// (ajuste > 0) ? ajuste : 0;
                            totales.totalBase = subtotal;
                            totales.totalImpuestos = 0;
                            totales.totalGastos = 0;
                            totales.totalDocumento = totaldoc;
                            totales.totalRetenciones = 0;
                            totales.totalAnticipos = 0;
                            totales.aPagar = totaldoc;
                            factura.datosTotales = totales;
                            documentocondicionesPago condicionesPago = new documentocondicionesPago();
                            documentocondicionPago condicionPago = new documentocondicionPago();
                            condicionPago.formaPago = "1";
                            condicionPago.medioPago = "10";
                            condicionesPago.condicionPago = condicionPago;
                            factura.condicionesPago = condicionesPago;
                            documentoExtensionSPD extSPD = new documentoExtensionSPD();
                            extSPD.referenciaPago = fact.numfact;
                            extSPD.estratoPredio = Convert.ToInt16(fact.estrato).ToString();
                            extSPD.tipoUsoPredio = fact.uso;
                            extensionSPDservicioPublico servicio = new extensionSPDservicioPublico();
                            servicio.numLinea = "1";
                            servicio.indTercero = "N";
                            servicio.servicioFacturado = "GAS";
                            servicio.empresa = "ENERCER";
                            servicio.motivo = "Facturación Servicio Público";
                            servicio.numeroContrato = fact.matricula.ToString();
                            List<extensionSPDservicioPublicosuscriptor> lsuscriptor = new List<extensionSPDservicioPublicosuscriptor>();
                            extensionSPDservicioPublicosuscriptor suscriptor = new extensionSPDservicioPublicosuscriptor();
                            suscriptor.nombre = fact.Nombre_cliente + ' ' + fact.Apellido1_cliente + ' ' + fact.Apellido2_Cliente + ' ' + fact.Razon_social;
                            suscriptor.direccionPostal = fact.Direccion_cliente;
                            suscriptor.direccionEntrega = fact.Direccion_cliente;
                            suscriptor.ciudad = fact.ciudad_cliente;
                            suscriptor.departamento = fact.departamento_cliente;
                            suscriptor.pais = "CO";
                            suscriptor.tipoEstrato = Convert.ToInt16(fact.estrato).ToString();
                            suscriptor.email = fact.email_cliente;
                            lsuscriptor.Add(suscriptor);
                            servicio.subscriptor = lsuscriptor.ToArray();
                            List<extensionSPDservicioPublicovalorFacturado> lvalfactura = new List<extensionSPDservicioPublicovalorFacturado>();
                            extensionSPDservicioPublicovalorFacturado valfactura = new extensionSPDservicioPublicovalorFacturado();
                            valfactura.ciclo = "1";
                            valfactura.tipoPeriodicidad = "1";
                            valfactura.producto = lproductos.ToArray();
                            lvalfactura.Add(valfactura);
                            servicio.valorFacturado = lvalfactura.ToArray();
                            List<extensionSPDservicioPublico> lservicios = new List<extensionSPDservicioPublico>();
                            lservicios.Add(servicio);
                            extSPD.servicioPublico = lservicios.ToArray();
                            factura.extensionSPD = extSPD;
                            //CrearPDF(fact, lectura1, ldetalle);
                            try
                            {
                                var resultado = await EnviarSolicitudSOAPAsync(url, usuario, contraseña, factura);
                                result = GuardarResponse(resultado, factura);
                                success = true;

                            }
                            catch (Exception ex)
                            {
                                result = ex.Message;
                                success = false;
                            }
                        }
                        
                    }
                    else
                    {
                        Envio_Factura envio = new Envio_Factura();
                        envio.Numfactura = fact.Prefijo.Trim() + fact.numfact.Trim();
                        envio.Codpredio = codsus;
                        envio.mensaje_respuesta = "No existe el suscriptor o no tiene la información completa";
                        envio.codigo_respuesta = "INT";
                        envio.xml_enviado = "";
                        try
                        {
                            new ADEnvio_Factura().insertar_respuesta(envio);
                            result = "Documento Enviado";
                        }
                        catch (Exception ex)
                        {
                            result = ex.Message;
                        }
                    }
                }
            }
            else
            {
                result = "No existe factura";
                success = true;
            }
            return Json(new { success = success, message = result }, JsonRequestBehavior.AllowGet);
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
                            <wsse:Username>{usuario}</wsse:Username>
                            <wsse:Password type=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText"">{contraseña}</wsse:Password>
                            </wsse:UsernameToken>
                        </wsse:Security>
                    </soapenv:Header>
                   <soapenv:Body>
                      <impl:entregaFactura>
                            <impl:request>
                                {xmlContenido}
                            </impl:request>
                      </impl:entregaFactura>
                   </soapenv:Body>
                </soapenv:Envelope>";
        }


        public async Task<string> EnviarSolicitudSOAPAsync(string url, string usuario, string contrasena, documento factura)
        {
            string xmlcontenido = SerializarEntidadXML(factura);
            string soapEnvelope = ConstruirSoapEnvelope(xmlcontenido);

            using (var client = new HttpClient())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var authInfo = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{usuario}:{contrasena}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authInfo);

                var content = new StringContent(soapEnvelope, Encoding.UTF8, "application/xml");
                //content.Headers.ContentType = new MediaTypeHeaderValue("application/xop+xml");
                //content.Headers.ContentType.Parameters.Add(new NameValueHeaderValue("type", "\"text/xml\""));
                //content.Headers.Add("SOAPAction", "\"\"");
                try
                {
                    var response = await client.PostAsync(url, content);
                    //response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsStringAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error al consultar el servicio: {e.Message}");
                    return null;
                }
            }
        }

        private string ConvertirFecha(string fecha, string horas)
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

                if (horas == "horas")
                {
                    outputDate = year.ToString() + "-" + month.ToString().PadLeft(2, '0') + "-" + day.ToString().PadLeft(2, '0') + " " + hour.ToString().PadLeft(2, '0') + ":" + minute.ToString().PadLeft(2, '0') + ":" + second.ToString().PadLeft(2, '0');
                }
                else
                {
                    outputDate = year.ToString() + "-" + month.ToString().PadLeft(2, '0') + "-" + day.ToString().PadLeft(2, '0');
                }

            }
            catch (Exception ex)
            {
                outputDate = "formato no valido: " + ex.Message;
            }

            return outputDate;
        }

        private string GuardarResponse(string response, documento factura)
        {
            string result = "";
            string xmlContent = ExtractXmlContent(response);
            XDocument doc = XDocument.Parse(xmlContent);
            XNamespace ns2 = "http://impl.consultaestados.ws.saaf.delogica.es/";
            string mensajerespuesta = doc
                .Descendants(ns2 + "entregaFacturaResponse")
                .Descendants("return")
                .Elements("mensajeRespuesta")
                .FirstOrDefault()?.Value;
            string codigorespuesta = doc
                .Descendants(ns2 + "entregaFacturaResponse")
                .Descendants("return")
                .Elements("codigoRespuesta")
                .FirstOrDefault()?.Value;
            string xmlenviado = SerializarEntidadXML(factura);
            Envio_Factura envio = new Envio_Factura();
            envio.Numfactura = factura.numeroDocumento;
            envio.Codpredio = codsus;
            envio.mensaje_respuesta = mensajerespuesta;
            envio.codigo_respuesta = codigorespuesta;
            envio.xml_enviado = xmlenviado;
            try
            {
                new ADEnvio_Factura().insertar_respuesta(envio);
                result = "Documento Enviado";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;

        }

        static string ExtractXmlContent(string soapResponse)
        {
            int startIndex = soapResponse.IndexOf("<?xml");
            if (startIndex == -1)
            {
                startIndex = soapResponse.IndexOf("<soap:Envelope");
            }
            int endIndex = soapResponse.LastIndexOf("</soap:Envelope>") + "</soap:Envelope>".Length;

            if (startIndex != -1 && endIndex != -1)
            {
                return soapResponse.Substring(startIndex, endIndex - startIndex);
            }

            return string.Empty;
        }

        private string CrearPDF(FacturasT factura, Lectura lecturas, List<FacturasD> detalleF)
        {

            List<Tarifas> tarifa = new ADTarifas().ConsultarTarifas(factura.ciclo, factura.periodo, factura.anio.ToString(), factura.UsoTarifa, factura.EstratoTarifa);
            periodosCiclo periodosC = new ADperiodosCiclo().consultarperiodosCiclo(factura.ciclo, factura.periodo, factura.anio.ToString());

            var resFSSRI = detalleF.Where(x => x.codigo_c == "96").FirstOrDefault();
            decimal subsidioFSSRI = 0;
            if (resFSSRI != null)
                subsidioFSSRI = resFSSRI.valor;
            decimal subsidioFECF = 0;
            var resFECF = detalleF.Where(x => x.codigo_c == "97").FirstOrDefault();
            if (resFECF != null)
                subsidioFECF = resFECF.valor * -1;
            decimal ajuste = 0;
            var resAjuste = detalleF.Where(x => x.codigo_c == "29").FirstOrDefault();
            if (resAjuste != null)
                ajuste = resAjuste.valor;
            int consumo_Fact = 0;
            decimal vrconsumo = 0;
            var consumoFact = detalleF.Where(x => x.codigo_c == "02").FirstOrDefault();
            if (consumoFact != null)
            {
                consumo_Fact = consumoFact.cantidad;
                vrconsumo = consumoFact.valor;
            }
            decimal cargo_fijo = 0;
            var cargofijo = detalleF.Where(x => x.codigo_c == "01").FirstOrDefault();
            if (cargofijo != null)
                cargo_fijo = cargofijo.valor;
            decimal subtotalconsumo = cargo_fijo + (vrconsumo - subsidioFECF) - subsidioFSSRI;

            string filePDF = Server.MapPath("~/Facturas/" + factura.Prefijo + factura.numfact + ".pdf");
            Rectangle pageSize = PageSize.LETTER; // new Rectangle(720f, 1080f);
            try
            {
                PdfWriter pdfWriter = new PdfWriter(filePDF);
                PdfDocument pdfdoc = new PdfDocument(pdfWriter);
                Document doc = new Document(pdfdoc, new PageSize(pageSize));
                doc.SetMargins(14.17f, 28.35f, 14.17f, 28.35f);

                PdfFont boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                PdfFont normalFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                Text TituloIzquierda = new Text("Enercer S.A. E.S.P. \nNit: 830.140.206")
                    .SetFont(boldFont)
                    .SetFontSize(12);
                Text TituloDerecha = new Text("Vigilado \nSUPERINTENDENCIA DE SERVICIOS \nPUBLICOS DOMICILIARIOS")
                    .SetFont(boldFont)
                    .SetFontSize(8);

                iText.Layout.Element.Table titleTable = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(new float[] { 1, 1 })).UseAllAvailableWidth();

                titleTable.AddCell(new Cell().Add(new Paragraph(TituloIzquierda)).SetBorder(Border.NO_BORDER));
                titleTable.AddCell(new Cell().Add(new Paragraph(TituloDerecha).SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER));

                doc.Add(titleTable);

                Paragraph TipoDoc = new Paragraph();
                TipoDoc.SetFont(boldFont)
                    .SetFontSize(10)
                    .SetTextAlignment(TextAlignment.CENTER);
                TipoDoc.Add("DOCUMENTO EQUIVALENTE A LA FACTURA DE VENTA SPD");
                doc.Add(TipoDoc);
                iText.Layout.Element.Table Encabezado = new iText.Layout.Element.Table(3).UseAllAvailableWidth();
                Encabezado.AddHeaderCell(new Cell().Add(new Paragraph("Número").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                Encabezado.AddHeaderCell(new Cell().Add(new Paragraph("Fecha y Hora").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                Encabezado.AddHeaderCell(new Cell().Add(new Paragraph("Periodo").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                Encabezado.AddCell(new Cell().Add(new Paragraph(factura.Prefijo + factura.numfact).SetFontSize(8)));
                Encabezado.AddCell(new Cell().Add(new Paragraph(factura.fecha.ToString()).SetFontSize(8)));
                Encabezado.AddCell(new Cell().Add(new Paragraph(periodosC.nomperiodo).SetFontSize(8)));
                doc.Add(Encabezado);
                Paragraph DatosCliente = new Paragraph();
                DatosCliente.SetFont(boldFont)
                    .SetFontSize(10)
                    .SetTextAlignment(TextAlignment.CENTER);
                DatosCliente.Add("INFORMACIÓN DEL CLIENTE");
                doc.Add(DatosCliente);
                iText.Layout.Element.Table tbdatoscliente = new iText.Layout.Element.Table(4).UseAllAvailableWidth();
                tbdatoscliente.AddCell(new Cell().Add(new Paragraph("Código Suscriptor: ").SetFontSize(8)).SetBorderTop(new SolidBorder(1)));
                tbdatoscliente.AddCell(new Cell().Add(new Paragraph(factura.codpredio).SetFontSize(8)).SetBorderTop(new SolidBorder(1)));
                tbdatoscliente.AddCell(new Cell().Add(new Paragraph("Codigo de Ruta: ").SetFontSize(8)).SetBorderTop(new SolidBorder(1)));
                tbdatoscliente.AddCell(new Cell().Add(new Paragraph("").SetFontSize(8)).SetBorderTop(new SolidBorder(1)));

                tbdatoscliente.AddCell(new Cell().Add(new Paragraph("Cliente: ").SetFontSize(8)).SetBorderTop(new SolidBorder(1)));
                tbdatoscliente.AddCell(new Cell().Add(new Paragraph(factura.Razon_social.Trim() + " " + factura.Nombre_cliente.Trim() + " " + factura.Apellido1_cliente.Trim() + " " + factura.Apellido2_Cliente.Trim()).SetFontSize(8)).SetBorderTop(new SolidBorder(1)));
                tbdatoscliente.AddCell(new Cell().Add(new Paragraph("Nit/CC: ").SetFontSize(8)).SetBorderTop(new SolidBorder(1)));
                tbdatoscliente.AddCell(new Cell().Add(new Paragraph(factura.Identificacion + " " + factura.dv).SetFontSize(8)).SetBorderTop(new SolidBorder(1)));

                tbdatoscliente.AddCell(new Cell().Add(new Paragraph("Estrato: ").SetFontSize(8)).SetBorderTop(new SolidBorder(1)));
                tbdatoscliente.AddCell(new Cell().Add(new Paragraph(factura.estrato).SetFontSize(8)).SetBorderTop(new SolidBorder(1)));
                tbdatoscliente.AddCell(new Cell().Add(new Paragraph("Clase de Uso: ").SetFontSize(8)).SetBorderTop(new SolidBorder(1)));
                tbdatoscliente.AddCell(new Cell().Add(new Paragraph(factura.uso).SetFontSize(8)).SetBorderTop(new SolidBorder(1)));

                tbdatoscliente.AddCell(new Cell().Add(new Paragraph("Dirección del Servicio: ").SetFontSize(8)).SetBorderTop(new SolidBorder(1)));
                tbdatoscliente.AddCell(new Cell().Add(new Paragraph(factura.Direccion_cliente).SetFontSize(8)).SetBorderTop(new SolidBorder(1)));
                tbdatoscliente.AddCell(new Cell().Add(new Paragraph("Barrio: ").SetFontSize(8)).SetBorderTop(new SolidBorder(1)));
                tbdatoscliente.AddCell(new Cell().Add(new Paragraph("").SetFontSize(8)).SetBorderTop(new SolidBorder(1)));

                tbdatoscliente.AddCell(new Cell().Add(new Paragraph("Dirección de Correp.: ").SetFontSize(8)).SetBorderTop(new SolidBorder(1)));
                tbdatoscliente.AddCell(new Cell().Add(new Paragraph("").SetFontSize(8)).SetBorderTop(new SolidBorder(1)));
                tbdatoscliente.AddCell(new Cell().Add(new Paragraph("Ciudad: ").SetFontSize(8)).SetBorderTop(new SolidBorder(1)));
                tbdatoscliente.AddCell(new Cell().Add(new Paragraph(factura.nomciudad).SetFontSize(8)).SetBorderTop(new SolidBorder(1)));

                tbdatoscliente.AddCell(new Cell().Add(new Paragraph("Medidor: ").SetFontSize(8)).SetBorderTop(new SolidBorder(1)).SetBorderBottom(new SolidBorder(1)));
                tbdatoscliente.AddCell(new Cell().Add(new Paragraph(factura.Nmedidor).SetFontSize(8)).SetBorderTop(new SolidBorder(1)).SetBorderBottom(new SolidBorder(1)));
                tbdatoscliente.AddCell(new Cell().Add(new Paragraph("Tipo de Gas: ").SetFontSize(8)).SetBorderTop(new SolidBorder(1)).SetBorderBottom(new SolidBorder(1)));
                tbdatoscliente.AddCell(new Cell().Add(new Paragraph("GN").SetFontSize(8)).SetBorderTop(new SolidBorder(1)).SetBorderBottom(new SolidBorder(1)));

                doc.Add(tbdatoscliente);
                Paragraph Consumo = new Paragraph();
                Consumo.SetFont(boldFont)
                    .SetFontSize(10)
                    .SetTextAlignment(TextAlignment.CENTER);
                Consumo.Add("DETERMINACION DEL CONSUMO");
                doc.Add(Consumo);
                iText.Layout.Element.Table tbconsumo = new iText.Layout.Element.Table(5).UseAllAvailableWidth();
                tbconsumo.AddHeaderCell(new Cell().Add(new Paragraph("Lect. Anterior").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbconsumo.AddHeaderCell(new Cell().Add(new Paragraph("Lect. Actual").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbconsumo.AddHeaderCell(new Cell().Add(new Paragraph("Consumo m3").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbconsumo.AddHeaderCell(new Cell().Add(new Paragraph("Factor Corrección").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbconsumo.AddHeaderCell(new Cell().Add(new Paragraph("Consumo Fact.").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));

                tbconsumo.AddCell(new Cell().Add(new Paragraph(lecturas.lect_anterior.ToString()).SetFont(normalFont).SetFontSize(6).SetTextAlignment(TextAlignment.CENTER)));
                tbconsumo.AddCell(new Cell().Add(new Paragraph(lecturas.lect_actual.ToString()).SetFont(normalFont).SetFontSize(6).SetTextAlignment(TextAlignment.CENTER)));
                tbconsumo.AddCell(new Cell().Add(new Paragraph(lecturas.consumo.ToString()).SetFont(normalFont).SetFontSize(6).SetTextAlignment(TextAlignment.CENTER)));
                tbconsumo.AddCell(new Cell().Add(new Paragraph(tarifa[0].factor_correccion.ToString()).SetFont(normalFont).SetFontSize(6).SetTextAlignment(TextAlignment.CENTER)));
                tbconsumo.AddCell(new Cell().Add(new Paragraph(consumo_Fact.ToString()).SetFont(normalFont).SetFontSize(6).SetTextAlignment(TextAlignment.CENTER)));
                doc.Add(tbconsumo);

                Paragraph Historico = new Paragraph();
                Historico.SetFont(boldFont)
                    .SetFontSize(10)
                    .SetTextAlignment(TextAlignment.CENTER);
                Historico.Add("EVOLUCIÓN DE SU CONSUMO (M3)");
                doc.Add(Historico);
                iText.Layout.Element.Table tbhistorico = new iText.Layout.Element.Table(7).UseAllAvailableWidth();
                tbhistorico.AddHeaderCell(new Cell().Add(new Paragraph("Ant - 6").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbhistorico.AddHeaderCell(new Cell().Add(new Paragraph("Ant - 5").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbhistorico.AddHeaderCell(new Cell().Add(new Paragraph("Ant - 4").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbhistorico.AddHeaderCell(new Cell().Add(new Paragraph("Ant - 3").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbhistorico.AddHeaderCell(new Cell().Add(new Paragraph("Ant - 2").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbhistorico.AddHeaderCell(new Cell().Add(new Paragraph("Ant - 1").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbhistorico.AddHeaderCell(new Cell().Add(new Paragraph("Promedio").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));

                tbhistorico.AddCell(new Cell().Add(new Paragraph(lecturas.consumo6.ToString()).SetFont(normalFont).SetFontSize(6).SetTextAlignment(TextAlignment.CENTER)));
                tbhistorico.AddCell(new Cell().Add(new Paragraph(lecturas.consumo5.ToString()).SetFont(normalFont).SetFontSize(6).SetTextAlignment(TextAlignment.CENTER)));
                tbhistorico.AddCell(new Cell().Add(new Paragraph(lecturas.consumo4.ToString()).SetFont(normalFont).SetFontSize(6).SetTextAlignment(TextAlignment.CENTER)));
                tbhistorico.AddCell(new Cell().Add(new Paragraph(lecturas.consumo3.ToString()).SetFont(normalFont).SetFontSize(6).SetTextAlignment(TextAlignment.CENTER)));
                tbhistorico.AddCell(new Cell().Add(new Paragraph(lecturas.consumo2.ToString()).SetFont(normalFont).SetFontSize(6).SetTextAlignment(TextAlignment.CENTER)));
                tbhistorico.AddCell(new Cell().Add(new Paragraph(lecturas.consumo1.ToString()).SetFont(normalFont).SetFontSize(6).SetTextAlignment(TextAlignment.CENTER)));
                tbhistorico.AddCell(new Cell().Add(new Paragraph(lecturas.consumo_promedio.ToString()).SetFont(normalFont).SetFontSize(6).SetTextAlignment(TextAlignment.CENTER)));

                doc.Add(tbhistorico);
                Paragraph Costos = new Paragraph();
                Costos.SetFont(boldFont)
                    .SetFontSize(10)
                    .SetTextAlignment(TextAlignment.CENTER);
                Costos.Add("COSTO DE PRESTACIÓN DEL SERVICIO");
                doc.Add(Costos);
                Paragraph aviso = new Paragraph();
                aviso.SetFont(normalFont)
                    .SetFontSize(6)
                    .SetTextAlignment(TextAlignment.CENTER);
                aviso.Add("Las tarifas aplicadas estan reguladas por la CREG");
                doc.Add(aviso);
                iText.Layout.Element.Table tbTarifas = new iText.Layout.Element.Table(5).UseAllAvailableWidth();
                tbTarifas.AddCell(new Cell().Add(new Paragraph("Gm").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbTarifas.AddCell(new Cell().Add(new Paragraph("Tm").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbTarifas.AddCell(new Cell().Add(new Paragraph("Dv1").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbTarifas.AddCell(new Cell().Add(new Paragraph("Cm").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbTarifas.AddCell(new Cell().Add(new Paragraph("Sub/Contrib").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));

                tbTarifas.AddCell(new Cell().Add(new Paragraph(tarifa[0].gm.ToString("C", culture)).SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbTarifas.AddCell(new Cell().Add(new Paragraph(tarifa[0].tm.ToString("C", culture)).SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbTarifas.AddCell(new Cell().Add(new Paragraph(tarifa[0].dv1.ToString("C", culture)).SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbTarifas.AddCell(new Cell().Add(new Paragraph(tarifa[0].cm.ToString("C", culture)).SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbTarifas.AddCell(new Cell().Add(new Paragraph(tarifa[0].subs_contrib.ToString()+"%").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));

                tbTarifas.AddCell(new Cell().Add(new Paragraph("Poder C.(PC)").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbTarifas.AddCell(new Cell().Add(new Paragraph("Cons(Kwh)").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbTarifas.AddCell(new Cell().Add(new Paragraph("Val(Kwh)").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                Cell combinedCell = new Cell(1, 2)
                    .Add(new Paragraph("Consumo promedio de subsistencia"))
                    .SetFont(boldFont)
                    .SetFontSize(8)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetBorderTop(new SolidBorder(0.5f))
                    .SetBorderBottom(new SolidBorder(0.5f));
                tbTarifas.AddCell(combinedCell);

                tbTarifas.AddCell(new Cell().Add(new Paragraph(tarifa[0].poder_c.ToString()).SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbTarifas.AddCell(new Cell().Add(new Paragraph("").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbTarifas.AddCell(new Cell().Add(new Paragraph("").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbTarifas.AddCell(new Cell().Add(new Paragraph((tarifa[0].estrato=="01")? "Estrato 1: "+tarifa[0].cons_prom_subs.ToString():"Estrato 1: 0.00").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbTarifas.AddCell(new Cell().Add(new Paragraph((tarifa[0].estrato == "02") ? "Estrato 2: " + tarifa[0].cons_prom_subs.ToString() : "Estrato 2: 0.00").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                doc.Add(tbTarifas);
                Paragraph Liqconsumo = new Paragraph();
                Liqconsumo.SetFont(boldFont)
                    .SetFontSize(10)
                    .SetTextAlignment(TextAlignment.CENTER);
                Liqconsumo.Add("LIQUIDACIÓN DEL CONSUMO");
                doc.Add(Liqconsumo);

                iText.Layout.Element.Table tbliqconsumo = new iText.Layout.Element.Table(6).UseAllAvailableWidth();
                tbliqconsumo.AddHeaderCell(new Cell().Add(new Paragraph("Rango").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbliqconsumo.AddHeaderCell(new Cell().Add(new Paragraph("ConsM3").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbliqconsumo.AddHeaderCell(new Cell().Add(new Paragraph("Pleno Mvjm").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbliqconsumo.AddHeaderCell(new Cell().Add(new Paragraph("Neto Mvjm").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbliqconsumo.AddHeaderCell(new Cell().Add(new Paragraph("Mvjm").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbliqconsumo.AddHeaderCell(new Cell().Add(new Paragraph("Total Consumo").SetFont(boldFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));

                tbliqconsumo.AddCell(new Cell().Add(new Paragraph("1").SetFont(normalFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbliqconsumo.AddCell(new Cell().Add(new Paragraph(consumo_Fact.ToString()).SetFont(normalFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbliqconsumo.AddCell(new Cell().Add(new Paragraph(tarifa[0].pleno_mvjm.ToString("C",culture)).SetFont(normalFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbliqconsumo.AddCell(new Cell().Add(new Paragraph(tarifa[0].neto_mvjm.ToString("C",culture)).SetFont(normalFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbliqconsumo.AddCell(new Cell().Add(new Paragraph(tarifa[0].mvjm.ToString("C",culture)).SetFont(normalFont).SetFontSize(8).SetTextAlignment(TextAlignment.CENTER)));
                tbliqconsumo.AddCell(new Cell().Add(new Paragraph((vrconsumo-subsidioFECF).ToString("C",culture)).SetFont(normalFont).SetFontSize(8).SetTextAlignment(TextAlignment.RIGHT)));

                Cell liqtextcargofijocombined = new Cell(1, 4)
                    .Add(new Paragraph("CARGO FIJO MENSUAL"))
                    .SetFont(boldFont)
                    .SetFontSize(8)
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetBorderTop(new SolidBorder(0.5f))
                    .SetBorderBottom(new SolidBorder(0.5f));
                tbliqconsumo.AddCell(liqtextcargofijocombined);
                Cell liqvrcargofijocombined = new Cell(1, 2)
                    .Add(new Paragraph(cargo_fijo.ToString("C",culture)))
                    .SetFont(boldFont)
                    .SetFontSize(8)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetBorderTop(new SolidBorder(0.5f))
                    .SetBorderBottom(new SolidBorder(0.5f));
                tbliqconsumo.AddCell(liqvrcargofijocombined);
                Cell liqtextsubsidioFSSRIcombined = new Cell(1, 4)
                                    .Add(new Paragraph("VALOR SUB / CON FSSRI"))
                                    .SetFont(boldFont)
                                    .SetFontSize(8)
                                    .SetTextAlignment(TextAlignment.LEFT)
                                    .SetBorderTop(new SolidBorder(0.5f))
                                    .SetBorderBottom(new SolidBorder(0.5f));
                tbliqconsumo.AddCell(liqtextsubsidioFSSRIcombined);
                Cell liqvrsubsidioFSSRIcombined = new Cell(1, 2)
                    .Add(new Paragraph(subsidioFSSRI.ToString("C", culture)))
                    .SetFont(boldFont)
                    .SetFontSize(8)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetBorderTop(new SolidBorder(0.5f))
                    .SetBorderBottom(new SolidBorder(0.5f));
                tbliqconsumo.AddCell(liqvrsubsidioFSSRIcombined);
                Cell liqtextsubtotalcombined = new Cell(1, 4)
                                    .Add(new Paragraph("SUBTOTAL CONSUMO"))
                                    .SetFont(boldFont)
                                    .SetFontSize(8)
                                    .SetTextAlignment(TextAlignment.LEFT)
                                    .SetBorderTop(new SolidBorder(0.5f))
                                    .SetBorderBottom(new SolidBorder(0.5f));
                tbliqconsumo.AddCell(liqtextsubtotalcombined);
                Cell liqvrsubtotalconsumocombined = new Cell(1, 2)
                    .Add(new Paragraph(subtotalconsumo.ToString("C", culture)))
                    .SetFont(boldFont)
                    .SetFontSize(8)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetBorderTop(new SolidBorder(0.5f))
                    .SetBorderBottom(new SolidBorder(0.5f));
                tbliqconsumo.AddCell(liqvrsubtotalconsumocombined);
                Cell liqtextsubsidioFECF = new Cell(1, 4)
                                   .Add(new Paragraph("INFORMATIVO VR SUBSIDIO (-) FNR MUNICIPIO"))
                                   .SetFont(boldFont)
                                   .SetFontSize(8)
                                   .SetTextAlignment(TextAlignment.LEFT)
                                   .SetBorderTop(new SolidBorder(0.5f))
                                   .SetBorderBottom(new SolidBorder(0.5f));
                tbliqconsumo.AddCell(liqtextsubsidioFECF);
                Cell liqvrsubsidioFECFcombined = new Cell(1, 2)
                    .Add(new Paragraph(subsidioFECF.ToString("C", culture)))
                    .SetFont(boldFont)
                    .SetFontSize(8)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetBorderTop(new SolidBorder(0.5f))
                    .SetBorderBottom(new SolidBorder(0.5f));
                tbliqconsumo.AddCell(liqvrsubsidioFECFcombined);
                Cell liqtextperiodosatraso = new Cell(1, 4)
                                   .Add(new Paragraph("PERIODOS DE ATRASO"))
                                   .SetFont(boldFont)
                                   .SetFontSize(8)
                                   .SetTextAlignment(TextAlignment.LEFT)
                                   .SetBorderTop(new SolidBorder(0.5f))
                                   .SetBorderBottom(new SolidBorder(0.5f));
                tbliqconsumo.AddCell(liqtextperiodosatraso);
                Cell liqperiodosatrasocombined = new Cell(1, 2)
                    .Add(new Paragraph(factura.atraso.ToString()))
                    .SetFont(boldFont)
                    .SetFontSize(8)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetBorderTop(new SolidBorder(0.5f))
                    .SetBorderBottom(new SolidBorder(0.5f));
                tbliqconsumo.AddCell(liqperiodosatrasocombined);
                Cell TextOtrosconceptos = new Cell(1, 6)
                                   .Add(new Paragraph("LIQUIDACION OTROS CONCEPTOS"))
                                   .SetFont(boldFont)
                                   .SetFontSize(8)
                                   .SetTextAlignment(TextAlignment.CENTER)
                                   .SetBorderTop(new SolidBorder(0.5f))
                                   .SetBorderBottom(new SolidBorder(0.5f));
                tbliqconsumo.AddCell(TextOtrosconceptos);


                doc.Add(tbliqconsumo);



                doc.Close();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("CrearPDF: "+ex.Message,ex);
            }

            return filePDF;
        }
    }
}