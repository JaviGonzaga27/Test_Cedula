using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using proyecto_javier.Data;
using proyecto_javier.Models;
using System.Collections.Generic;
using System.Linq;

namespace proyecto_javier.Controllers
{
    public class PostgresController : Controller
    {
        private readonly ClienteSqlDataPostgres _objClientDAL;

        public PostgresController(ClienteSqlDataPostgres objClientDAL)
        {
            _objClientDAL = objClientDAL;
        }

        // GET: Postgres/Index
        public ActionResult Index()
        {
            List<ClientePS> clientes = _objClientDAL.GetAllClientes().ToList();
            return View(clientes);
        }

        // GET: Postgres/Details/5
        public ActionResult Details(int id)
        {
            ClientePS cliente = _objClientDAL.GetClienteData(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // GET: Postgres/Create
        public ActionResult Create(string Pronvicia)
        {
            return View();
        }

        // POST: Postgres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Cedula,Apellidos,Nombres,FechaNacimiento,Mail,Telefono,Direccion,Estado,Saldo")] ClientePS objCliente)
        {
            if (ModelState.IsValid)
            {                
                _objClientDAL.AddCliente(objCliente);
                return RedirectToAction("Index");
            }
            return View(objCliente);
        }

        // GET: Postgres/Edit/5
        public ActionResult Edit(int id)
        {
            ClientePS cliente = _objClientDAL.GetClienteData(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Postgres/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Codigo,Cedula,Apellidos,Nombres,FechaNacimiento,Mail,Telefono,Direccion,Estado,Saldo")] ClientePS objCliente)
        {
            if (id != objCliente.Codigo)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _objClientDAL.UpdateCliente(objCliente);
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("", "No se pudo actualizar el cliente. Inténtelo de nuevo más tarde.");
                }
            }
            return View(objCliente);
        }


        // GET: Postgres/Delete/5
        public ActionResult Delete(int id)
        {
            ClientePS cliente = _objClientDAL.GetClienteData(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }


        // POST: Postgres/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _objClientDAL.DeleteCliente(id);
                return RedirectToAction("Index");
            }
            catch
            {
                // Puedes agregar un mensaje de error aquí para informar al usuario
                ModelState.AddModelError("", "No se pudo eliminar el cliente. Inténtelo de nuevo más tarde.");
                ClientePS cliente = _objClientDAL.GetClienteData(id);
                if (cliente == null)
                {
                    return NotFound();
                }
                return View(cliente);
            }
        }

        [HttpPost]
        public JsonResult GenerarCedula(string provincia)
        {
            if (string.IsNullOrWhiteSpace(provincia) || provincia == "Seleccionar")
            {
                return Json(new { cedula = string.Empty });
            }

            var cedula = _objClientDAL.GenerarCedulaValida(provincia);
            return Json(new { cedula = cedula });
        }
    }
}
