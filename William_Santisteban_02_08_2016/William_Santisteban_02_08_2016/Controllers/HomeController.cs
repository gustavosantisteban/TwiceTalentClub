using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using William_Santisteban_02_08_2016.Models;

namespace William_Santisteban_02_08_2016.Controllers
{
    public class HomeController : Controller
    {
        private ClubContext context = new ClubContext();
        // GET: Home
        public ActionResult Index()
        {
            var model = context.CLUB.ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult BuscarClub(string busqueda)
        {
            busqueda = busqueda.ToUpper();
            List<CLUB> club = context.CLUB.Where(x => x.DESC_CLUB.ToUpper().Contains(busqueda)).ToList();

            return Json(club, JsonRequestBehavior.AllowGet);
        }
    }
}