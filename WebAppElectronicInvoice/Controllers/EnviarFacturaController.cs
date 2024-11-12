using System;
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

namespace WebAppElectronicInvoice.Controllers
{
    public class EnviarFacturaController : Controller
    {

        private readonly string url = ConfigurationManager.AppSettings["Urlinvoway"]; //"https://pruebas.invoway.com/INVOWAYLATAM/services/ConsultaEstadosFacturasWSPort";

        string usuario = ConfigurationManager.AppSettings["UsuarioInvoway"];
        string contraseña = ConfigurationManager.AppSettings["PasswordInvoway"];
        string codsus = "";

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
                    if(fact.Identificacion!=null)
                    {
                        factura.numeroDocumento = fact.Prefijo+fact.numfact;
                        factura.tipoDocumento = "DE";
                        factura.subtipoDocumento = "60";
                        factura.tipoOperacion = "601";
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
                        productos[3] = "30";
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
                                        descuento.porcentaje = Math.Round(((subsidioFSSRI * -1) / det.valor) * 100, 2);
                                        descuento.motivo = "Subsidio FSSRI";
                                        ldescuentos.Add(descuento);
                                    }
                                    else
                                    {
                                        cargo = new documentoLineaCargo();
                                        cargo.@base = det.valor;
                                        cargo.valor = subsidioFSSRI;
                                        cargo.porcentaje = Math.Round((subsidioFSSRI / det.valor) * 100, 2);
                                        cargo.motivo = "Subsidio FSSRI";
                                        lcargos.Add(cargo);
                                    }

                                    descuento = new documentoLineaDescuento();
                                    descuento.@base = det.valor;
                                    descuento.valor = subsidioFECF;
                                    descuento.porcentaje = Math.Round((subsidioFECF / det.valor) * 100, 2);
                                    descuento.motivo = "Subsidio FECF";
                                    ldescuentos.Add(descuento);
                                    linea.unidadMedida = "MTQ";
                                    if (ajuste < 0)
                                    {
                                        descuento = new documentoLineaDescuento();
                                        descuento.@base = det.valor;
                                        descuento.valor = ajuste * -1;
                                        descuento.porcentaje = Math.Round(((ajuste * -1) / det.valor) * 100, 2);
                                        descuento.motivo = "Ajuste";
                                        ldescuentos.Add(descuento);
                                    }
                                    else
                                    {
                                        cargo = new documentoLineaCargo();
                                        cargo.@base = det.valor;
                                        cargo.valor = ajuste;
                                        cargo.porcentaje = Math.Round((ajuste / det.valor) * 100, 2);
                                        cargo.motivo = "Ajuste";
                                        lcargos.Add(cargo);
                                    }
                                    cargos.cargo = lcargos.ToArray();
                                    linea.cargos = cargos;
                                    descuentos.descuento = ldescuentos.ToArray();
                                    linea.descuentoLinea = ldescuentos.Sum(x => x.valor);
                                    linea.cargoLinea = lcargos.Sum(x => x.valor);
                                    linea.descuentos = descuentos;
                                    linea.unidadesLinea = det.cantidad;
                                    linea.precioUnidad = det.valor / det.cantidad;
                                    linea.subtotalLinea = det.valor;
                                    //llenar la información que se registra en la sección de SPD
                                    producto.totalUnidades = det.cantidad;
                                    producto.unidadMedidaTotal = "MTQ";
                                    producto.consumoTotal = det.valor + linea.cargoLinea - linea.descuentoLinea;
                                    producto.unidadesConsumidas = det.cantidad;
                                    producto.unidadMedidaConsumida = "MTQ";
                                    producto.valorConsumoParcial = det.valor;
                                    producto.valorUnitario = det.valor / det.cantidad;
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
                                        carfact.razonCargo = "Subsidio FSSRI";
                                        carfact.valorCargo = subsidioFSSRI;
                                        lcargofac.Add(carfact);
                                    }
                                    dctos = new extensionSPDservicioPublicoValorFacturadoProductoDescuento();
                                    dctos.razonDescuento = "Subsidio FECF";
                                    dctos.valorDto = subsidioFECF;
                                    ldctos.Add(dctos);
                                    if (ajuste < 0)
                                    {
                                        dctos = new extensionSPDservicioPublicoValorFacturadoProductoDescuento();
                                        dctos.razonDescuento = "Ajuste";
                                        dctos.valorDto = ajuste * -1;
                                        ldctos.Add(dctos);
                                    }
                                    else
                                    {
                                        carfact = new extensionSPDservicioPublicoValorFacturadoProductoCargo();
                                        carfact.razonCargo = "Ajuste";
                                        carfact.valorCargo = ajuste;
                                        lcargofac.Add(carfact);
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
                                subtotal += det.valor;
                                linea.totalLinea = linea.subtotalLinea + linea.cargoLinea - linea.descuentoLinea;
                                totaldoc += linea.totalLinea;
                                lLineas.Add(linea);
                                lineas.linea = lLineas.ToArray();
                                factura.lineas = lineas;
                            }
                        }
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
                    else
                    {
                        Envio_Factura envio = new Envio_Factura();
                        envio.Numfactura = fact.numfact;
                        envio.Codpredio = codsus;
                        envio.mensaje_respuesta = "No existe el suscriptor.";
                        envio.codigo_respuesta = "100";
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
            return Json(new { success = success, message = result },JsonRequestBehavior.AllowGet);
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

        private string ConvertirFecha(string fecha,string horas)
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

                if(horas=="horas")
                {
                    outputDate = year.ToString() + "-" + month.ToString().PadLeft(2, '0') + "-" + day.ToString().PadLeft(2, '0')+" "+hour.ToString().PadLeft(2,'0')+":"+minute.ToString().PadLeft(2,'0')+":"+second.ToString().PadLeft(2,'0');
                }
                else
                {
                    outputDate = year.ToString() + "-" + month.ToString().PadLeft(2, '0') + "-" + day.ToString().PadLeft(2, '0');
                }
                
            }
            catch (Exception ex)
            {
                outputDate = "formato no valido: "+ex.Message;
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
    }
}