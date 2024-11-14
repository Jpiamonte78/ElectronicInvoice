using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccesoDatos;
using Entidades;
using WebAppElectronicInvoice.Models;

namespace WebAppElectronicInvoice.Controllers
{
    public class ClienteController : Controller
    {
        [HttpGet]
        public ActionResult Clienteintegrin()
        {
            var pendientes = new ADClienteIntegrin().ConsultarPendientes();
            var clientes = new ADClienteIntegrin().ConsultarTodos();

            var model = new MyViewModel { Clientes = clientes, Pendientes = pendientes };
            return View(model);
        }
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
            ClienteIntegrin cliente = new ClienteIntegrin();
            cliente = new ADClienteIntegrin().ConsultarUnCliente(id);
            cliente.Id_Cliente_integrin = id;
            cliente.ltipodocumento = new ADTipoDocumento().tipoDocumentos();
            cliente.lpersona = ltipopersona;
            cliente.ldeptos = new MaeDepto_MuniAD().Consultar_Deptos();
            cliente.lmuni = new MaeDepto_MuniAD().Consultar_Municipios(cliente.departamento_cliente);
            cliente.ltributos = new ADParametrosGenerales().consultarTributos();
            cliente.lresponsabilidades = new ADParametrosGenerales().Consultar_Responsabilidades();
            cliente.selectedresp = cliente.resp_rut.Split(',').ToList();
            cliente.selectedTrib = (!string.IsNullOrEmpty(cliente.tributos))?cliente.tributos.Split(',').ToList():new List<string>() {""};
            return View(cliente);
        }

        [HttpPost]
        public ActionResult EditCliente(ClienteIntegrin Cliente)
        {
            Cliente.resp_rut = string.Join(",", Cliente.selectedresp);
            Cliente.tributos = (Cliente.selectedTrib != null) ? string.Join(",", Cliente.selectedTrib):"";
            try
            {
                new ADClienteIntegrin().Actualizar_cliente(Cliente);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            return RedirectToAction("Clienteintegrin");
        }
        public JsonResult Consultar_Municipio(string codDepto)
        {
            List<MaeMuni> mpios = new MaeDepto_MuniAD().Consultar_Municipios(codDepto);
            return Json(mpios, JsonRequestBehavior.AllowGet);
        }
    }
}