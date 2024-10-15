using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccesoDatos;
using Entidades;

namespace WebAppElectronicInvoice.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Cliente()
        {
            List<ClienteIntegrin> lclientespendientes = new List<ClienteIntegrin>();
            lclientespendientes = new ADClienteIntegrin().ConsultarPendientes();
            return View(lclientespendientes);
        }
        [HttpGet]
        public ActionResult EditCliente(int id)
        {
            List<TipoPersona> ltipopersona= new List<TipoPersona>();
            ltipopersona.Add(new TipoPersona() { codigo = 1, Descripcion = "Persona Juridica y asimiladas" });
            ltipopersona.Add(new TipoPersona() { codigo = 2, Descripcion = "Persona Natural y asimiladas" });
            cliente cliente = new cliente();
            cliente = new ADClienteIntegrin().ConsultarUnCliente(id);
            cliente.id_cliente_integrin = id;
            cliente.ltipodocumento = new ADTipoDocumento().tipoDocumentos();
            cliente.lpersona = ltipopersona;
            cliente.ldeptos = new MaeDepto_MuniAD().Consultar_Deptos();
            cliente.lmuni = new MaeDepto_MuniAD().Consultar_Municipios(cliente.departamento);
            cliente.ltributos = new ADParametrosGenerales().consultarTributos();
            cliente.lresponsabilidades = new ADParametrosGenerales().Consultar_Responsabilidades();
            cliente.selectedresp = cliente.resp_rut.Split(',').ToList();
            cliente.selectedTrib = (!string.IsNullOrEmpty(cliente.tributos))?cliente.tributos.Split(',').ToList():new List<string>() {""};
            return View(cliente);
        }

        [HttpPost]
        public ActionResult EditCliente(cliente Cliente)
        {
            Cliente.resp_rut = string.Join(",", Cliente.selectedresp);
            Cliente.tributos = (Cliente.selectedTrib != null) ? string.Join(",", Cliente.selectedTrib):"";
            try
            {
                new AdCliente().insertar_cliente(Cliente);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            return RedirectToAction("Cliente");
        }
        public JsonResult Consultar_Municipio(string codDepto)
        {
            List<MaeMuni> mpios = new MaeDepto_MuniAD().Consultar_Municipios(codDepto);
            return Json(mpios, JsonRequestBehavior.AllowGet);
        }
    }
}