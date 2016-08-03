using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using William_Santisteban_02_08_2016.Models;
using William_Santisteban_02_08_2016.ViewModels;

namespace William_Santisteban_02_08_2016.Controllers
{
    public class ReglamentoController : Controller
    {
        private ClubContext context = new ClubContext();
        // GET: Reglamento
        public ActionResult Index(DateTime? fecha)
        {
            var listadoReglamento = from l in context.REGLAMENTO
                                    select l;

            if (!String.IsNullOrEmpty(fecha.ToString()))
            {
                var fechaconv = Convert.ToDateTime(fecha).Date;
                listadoReglamento = listadoReglamento.Where(x => DbFunctions.TruncateTime(x.FECHA_CONFECCION) == fechaconv).ToList().AsQueryable();
            }
            else
            {
                listadoReglamento = context.REGLAMENTO.ToList().AsQueryable();
            }

            return View(listadoReglamento);
        }

        //[HttpPost]
        //public ActionResult Index(DateTime fecha)
        //{
        //    var listadoReglamentoxFecha = context.REGLAMENTO.Where(x => x.FECHA_CONFECCION.Date == fecha.Date).ToList();

        //    return View(listadoReglamentoxFecha);
        //}

        [HttpPost]
        public JsonResult BusquedaPorDescripcion(string descripcion)
        {
            descripcion = descripcion.ToUpper();
            
            IEnumerable<REGLAMENTO> reglamento = context.REGLAMENTO.Where(r => r.DESC_REGLAMENTO.ToUpper().Contains(descripcion));
            reglamento.Select(x => new
                {
                    Identificacion = x.ID_REGLAMENTO,
                    Nombre = x.NOMBRE_REGLAMENTO,
                    Descripcion = x.DESC_REGLAMENTO,
                    Estado = x.ESTADO,
                    FechaConf = x.FECHA_CONFECCION,
                    FechaVig = x.FECHA_VIGENCIA
            }).ToList();
            
            return Json(reglamento, JsonRequestBehavior.AllowGet);
        }



        public void EnumEstadoDropDownList()
        {

            var estado = from Estado d in Enum.GetValues(typeof(Estado))
                         select new { IdEstado = (int)d, Name = d.ToString() };

            ViewBag.EstadoReglamento = new SelectList(estado, "IdEstado", "Name");
        }

        [HttpGet]
        public ActionResult Agregar()
        {
            EnumEstadoDropDownList();

            return View();
        }

        [HttpPost]
        public ActionResult Agregar(ReglamentoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var reglamento = new REGLAMENTO
                {
                    NOMBRE_REGLAMENTO = model.NombreReglamento,
                    DESC_REGLAMENTO = model.Descripcion,
                    ESTADO = model.Estado.ToString(),
                    FECHA_CONFECCION = model.FechaConfeccion,
                    FECHA_VIGENCIA = model.FechaVigencia
                };
                context.Entry(reglamento).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            EnumEstadoDropDownList();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var reglamento = context.REGLAMENTO.Find(id);
            if (reglamento == null)
            {
                return HttpNotFound();
            }
            EnumEstadoDropDownList();

            var model = new ReglamentoViewModel()
            {
                Id = reglamento.ID_REGLAMENTO,
                NombreReglamento = reglamento.NOMBRE_REGLAMENTO,
                Descripcion = reglamento.DESC_REGLAMENTO,
                Estado = (Estado)Enum.Parse(typeof(Estado), reglamento.ESTADO),
                FechaConfeccion = reglamento.FECHA_CONFECCION,
                FechaVigencia = reglamento.FECHA_VIGENCIA
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(ReglamentoViewModel editarReglamento)
        {
            var reglamento = context.REGLAMENTO.Find(editarReglamento.Id);

            if (ModelState.IsValid)
            {
                reglamento.NOMBRE_REGLAMENTO = editarReglamento.NombreReglamento;
                reglamento.DESC_REGLAMENTO = editarReglamento.Descripcion;
                reglamento.ESTADO = editarReglamento.Estado.ToString();
                reglamento.FECHA_CONFECCION = editarReglamento.FechaConfeccion;
                reglamento.FECHA_VIGENCIA = editarReglamento.FechaVigencia;

                context.Entry(reglamento).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                
                return RedirectToAction("Index");
            }
            EnumEstadoDropDownList();
            return View();
        }

        
        public ActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var reglamento = context.REGLAMENTO.Find(id);
            if (reglamento == null)
            {
                return HttpNotFound();
            }
            EnumEstadoDropDownList();
            return View(reglamento);
        }


        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarEliminar(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var reglamento = context.REGLAMENTO.Find(id);
                if (reglamento == null)
                {
                    return HttpNotFound();
                }
                
                context.Entry(reglamento).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            EnumEstadoDropDownList();
            return View();
        }

    }
}