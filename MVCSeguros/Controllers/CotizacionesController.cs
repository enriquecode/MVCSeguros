using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Net.Http;

using MVCSeguros.Models;
using Newtonsoft.Json;
using System.Web;   
//using System.Web

namespace MVCSeguros.Controllers
{
    [Route("Seguros")]
    public class CotizacionesController : Controller
    {
        [Route("")]
        //[Route("Index")]
        [Route("Cotizaciones")]
        [Route("~/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Cotizaciones/Cotizar")]
        // public ActionResult Cotizar(IFormCollection form)
        public ActionResult Cotizar(string descripcionId)
        {
            //string name = form["Name"];
            //string country = form["Country"];
            return View();
        }

       

    }
}