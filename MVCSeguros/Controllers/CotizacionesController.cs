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


using System.Text;


namespace MVCSeguros.Controllers
{


    [Route("Seguros")]
    public class CotizacionesController : Controller
    {
        private static readonly string apiBasicUri = "https://web.aarco.com.mx/api-examen/api/examen/crear-peticion/";
        

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
        //este es el ultimo que si funcionó:
        //public ActionResult Cotizar(string descripcionId)
        //public string Cotizar(string descripcionId)
        public async Task<IActionResult> Cotizar(string descripcionId)
        //public string Cotizar([FromBody] Descripcion descripcion)
        {
            //string name = form["Name"];
            //string country = form["Country"];
            // string url = "api-examen/api/examen/crear-peticion/";

            //string jsonPost = @"{ ""descripcionId"" : ""bd8e92d6-715b-4551-8ff2-fbdb69a56cc2"" }";
            //Post(url, jsonPost);
           
            string peticionLlave = RequestPeticionLLave(descripcionId);

            //string strCotizaciones = string.Empty;
            Cotizacion cotizacion = new Cotizacion();

            cotizacion = await ObtenerCotizaciones(peticionLlave);
            //se tuvo que sobreexcribir este valor, esta regresando mal del servicio
            cotizacion.PeticionLlave = peticionLlave;
            //while (cotizacion.PeticionFinalizada == false)
            //{
            //    cotizacion = await ObtenerCotizaciones(peticionLlave);
            //}            

            return Json(new { data = cotizacion });
            //return View();
        }


        [HttpPost]
        [Route("Cotizaciones/Cotizar2")]
        // public ActionResult Cotizar(IFormCollection form)
        //este es el ultimo que si funcionó:
        //public ActionResult Cotizar(string descripcionId)
        //public string Cotizar(string descripcionId)
        public async Task<IActionResult> Cotizar2(string peticionLlave, string descripcionId)
        //public string Cotizar([FromBody] Descripcion descripcion)
        {
            //string name = form["Name"];
            //string country = form["Country"];
            // string url = "api-examen/api/examen/crear-peticion/";

            //string jsonPost = @"{ ""descripcionId"" : ""bd8e92d6-715b-4551-8ff2-fbdb69a56cc2"" }";
            //Post(url, jsonPost);
            string peticionLlaveNueva = string.Empty;
            if (peticionLlave == "")
            {
                peticionLlaveNueva = RequestPeticionLLave(descripcionId);
            }
            else
            {
                peticionLlaveNueva = peticionLlave;
            }

            //string strCotizaciones = string.Empty;
            Cotizacion cotizacion = new Cotizacion();

            cotizacion = await ObtenerCotizaciones(peticionLlaveNueva);
            //se tuvo que sobreexcribir este valor, esta regresando mal del servicio
            cotizacion.PeticionLlave = peticionLlave;

            //while (cotizacion.PeticionFinalizada == false)
            //{
            //    cotizacion = await ObtenerCotizaciones(peticionLlave);
            //}            

            return Json(new { data = cotizacion });
            //return View();
        }


        //public static async Task RequestPeticionLLave(string descripcionId)
        public static string RequestPeticionLLave(string descripcionId)
        {
            //DescripcionSoloParaEnviarAPost objDescripcion = new DescripcionSoloParaEnviarAPost();
            //objDescripcion.DescripcionId = descripcionId;

            //string rawJsonFromDb = descripcion.ToJsonArray();

            string jsonPost = @"{ ""descripcionId"" : ""bd8e92d6-715b-4551-8ff2-fbdb69a56cc2"" }";
            //string jsonPost2 = @'{ ""descripcionId"" : ' + '""' + descripcionId + '""' + '}';

            Dictionary<string, string> jsonValues = new Dictionary<string, string>();
            jsonValues.Add("descripcionId", descripcionId);


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiBasicUri);
            //byte[] cred = UTF8Encoding.UTF8.GetBytes("username:password");
            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(cred));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            //HttpContent content = new StringContent(jsonPost, UTF8Encoding.UTF8, "application/json");
            HttpContent content = new StringContent(JsonConvert.SerializeObject(jsonValues), UnicodeEncoding.UTF8, "application/json");

            HttpResponseMessage messge = client.PostAsync(apiBasicUri, content).Result;

            //HttpResponseMessage response = await client.PostAsJsonAsync("api-examen/api/examen/crear-peticion/", objDescripcion);

            //response.EnsureSuccessStatusCode();

            string description = string.Empty;
            if (messge.IsSuccessStatusCode)
            {
                string result = messge.Content.ReadAsStringAsync().Result;
                description = JsonConvert.DeserializeObject<string>(result);
            }

            return description;
        }

        //este no funcionó, es como el de arriba:
        public static async Task Post<T>(string url, T contentValue)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBasicUri);
                var content = new StringContent(JsonConvert.SerializeObject(contentValue), Encoding.UTF8, "application/json");
                var result = await client.PostAsync(url, content);
                result.EnsureSuccessStatusCode();
            }
        }


        public async Task<Cotizacion> ObtenerCotizaciones(string peticionLlave)
        {
            string apiResponse = string.Empty;
            Cotizacion objCotizacion = new Cotizacion();
            using (var httpClient = new HttpClient())
            {
                //string requestQuery = "https://web.aarco.com.mx/api-examen/api/examen/peticion/";


                using (var response = await httpClient.GetAsync("https://web.aarco.com.mx/api-examen/api/examen/peticion/" + peticionLlave))
                //using (var response = await httpClient.GetAsync(requestQuery))
                {
                    //while (apiResponse != string.Empty)
                    //{
                    apiResponse = await response.Content.ReadAsStringAsync();
                    objCotizacion = JsonConvert.DeserializeObject<Cotizacion>(apiResponse);

                    return objCotizacion;
                    //}
                    //return apiResponse;
                    //return Json(new { data = listaDescripciones });
                }

            }

        }

        private class DescripcionSoloParaEnviarAPost
        {
            public string DescripcionId { get; set; }

        }

    }
}