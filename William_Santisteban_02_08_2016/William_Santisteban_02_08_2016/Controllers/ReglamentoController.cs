using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Index()
        {
            var listadoReglamento = context.REGLAMENTO.ToList();
            return View(listadoReglamento);
        }

        [HttpGet]
        public ActionResult Agregar()
        {
            var estado = from Estado d in Enum.GetValues(typeof(Estado))
                                       select new { IdTipoEstado = (int)d, Name = d.ToString() };

            ViewBag.EstadoReglamento = new SelectList(estado, "IdTipoEstado", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(ReglamentoViewModel model)
        {
            if(ModelState.IsValid)
            {
                var reglamento = new REGLAMENTO
                {
                    NOMBRE_REGLAMENTO = model.NombreReglamento,
                    DESC_REGLAMENTO = model.Descripcion,
                    ESTADO = Convert.ToString(model.Estado),
                    FECHA_CONFECCION = model.FechaConfeccion,
                    FECHA_VIGENCIA = model.FechaVigencia
                };
                context.REGLAMENTO.Add(reglamento);
                context.SaveChanges();
            }

            return View();
        }
    }
}