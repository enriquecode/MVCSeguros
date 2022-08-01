using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using System.Net.Http;

using MVCSeguros.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;
//using System.Text.Json;


namespace MVCSeguros.Controllers
{
    //[Route("Catalogos")]
    [Route("Seguros")]
    public class CatalogosController : Controller
    {


        [Route("Catalogos")]
        public IActionResult Index()
        {
            return View();
        }

        
        [Route("Catalogos/ObtenerMarcas")]
        //public ContentResult Cotizaciones()
        public async Task<IActionResult> ObtenerMarcas()
        {
            string apiResponse = string.Empty;
            List<Marca> listaMarcas = new List<Marca>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:18933/api/Seguros/Catalogos/ObtenerMarcas"))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                    //apiResponse = response.Result.Content.ToString();
                    listaMarcas = JsonConvert.DeserializeObject<List<Marca>>(apiResponse);

                    //return Json(new { data = listaMarcas }, JsonRequestBehavior.AllowGet);
                    return Json(new { data = listaMarcas });
                }
            }
            //return View(listaMarcas);
            //}
            //return Content("Hello", "text/plain");
        }

        [Route("Catalogos/ObtenerSubMarcas")]       
        public async Task<IActionResult> ObtenerSubMarcas(int MarcaId)
        {
            string apiResponse = string.Empty;
            List<SubMarca> listaSubMarcas = new List<SubMarca>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:18933/api/Seguros/Catalogos/ObtenerSubMarcas/?MarcaId=" + MarcaId.ToString()))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();                   
                    listaSubMarcas = JsonConvert.DeserializeObject<List<SubMarca>>(apiResponse);
                  
                    return Json(new { data = listaSubMarcas });
                }
            }
            
        }

        [Route("Catalogos/ObtenerModelos")]
        public async Task<IActionResult> ObtenerModelos(int SubMarcaId)
        {
            string apiResponse = string.Empty;
            List<Modelo> listaModelos = new List<Modelo>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:18933/api/Seguros/Catalogos/ObtenerModelos/?SubMarcaId=" + SubMarcaId.ToString()))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                    listaModelos = JsonConvert.DeserializeObject<List<Modelo>>(apiResponse);

                    return Json(new { data = listaModelos });
                }

            }
        }

        [Route("Catalogos/ObtenerDescripciones")]
        public async Task<IActionResult> ObtenerDescripciones(int ModeloId)
        {
            string apiResponse = string.Empty;
            List<Descripcion> listaDescripciones = new List<Descripcion>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:18933/api/Seguros/Catalogos/ObtenerDescripciones/?ModeloId=" + ModeloId.ToString()))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                    listaDescripciones = JsonConvert.DeserializeObject<List<Descripcion>>(apiResponse);

                    return Json(new { data = listaDescripciones });
                }

            }

        }

        [Route("Catalogos/ObtenerUbicacionesGeograficas")]
        public async Task<IActionResult> ObtenerUbicacionesGeograficas(string CodigoPostalNumero)
        {
            string apiResponse = string.Empty;
            List<UbicacionGeografica> listaUbicacionesGeograficas = new List<UbicacionGeografica>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:18933/api/Seguros/Catalogos/ObtenerUbicacionesGeograficas/?CodigoPostalNumero=" + CodigoPostalNumero))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                    listaUbicacionesGeograficas = JsonConvert.DeserializeObject<List<UbicacionGeografica>>(apiResponse);

                    return Json(new { data = listaUbicacionesGeograficas });
                }

            }

        }

        [Route("Catalogos/ObtenerDomicilio")]
        public async Task<IActionResult> ObtenerDomicilio(string CodigoPostalNumero)
        {
            string apiResponse = string.Empty;
            List<UbicacionGeografica> listaUbicacionesGeograficas = new List<UbicacionGeografica>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://web.aarco.com.mx/api-examen/api/examen/sepomex/" + CodigoPostalNumero))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                    //listaUbicacionesGeograficas = JsonConvert.DeserializeObject<List<UbicacionGeografica>>(apiResponse);

                    //Object objRespuesta = JsonConvert.DeserializeObject(apiResponse);
                    

                    dynamic objRespuesta = JsonConvert.DeserializeObject(apiResponse);
                    var dynamicObject = JsonConvert.DeserializeObject<dynamic>(apiResponse);

                    dynamic dynamicObject2 = JsonConvert.DeserializeObject<ExpandoObject>(apiResponse);

                    var d = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(apiResponse);

                    foreach (var key in dynamicObject2)
                    {
                    }

                    var jsonDom = JsonConvert.DeserializeObject<JObject>(apiResponse);

                    JObject jObj = new JObject();

                    foreach (var key in jsonDom)
                    {

                        var arrayKey = key.Value.ToArray();

                        //Where(d => d. == "my_key")
                        //JToken x = key.Value.Where(d => d.Value == "h")                           
                        //.Select(v => v)
                        //.FirstOrDefault()
                        //.Value;
                        //string y = ((JValue)x).Value;
                        key.Value.ToList().ForEach(x => {
                            Console.WriteLine(x);
                        });

                        foreach (JProperty x in (JToken)key.Value)
                        { // if 'obj' is a JObject
                            //string name = x.Name;
                            JToken value = x.Value;
                        }

                        foreach (JToken item in key.Value)
                        {
                            Console.WriteLine(item);
                        }
                        foreach (JValue item in key.Value)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    //var jsonDom = System.Text.Json.JsonSerializer.Deserialize<JsonObject>(apiResponse);

                    foreach (var key in d.Keys)
                    {
                       
                        if (!string.IsNullOrEmpty(d[key]))
                        {
                            var value = d[key];

                            foreach (object obj in value)
                            {
                            }
                                foreach (var keyValue in value)
                            {

                            }
                                //se vuelve a serializar y luego se obtiene los keys:
                                var valueSerialized = JsonConvert.SerializeObject(value);

                            var e = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(valueSerialized);

                            foreach (var keyE in e.Keys)
                            {

                                if (!string.IsNullOrEmpty(d[key]))
                                {
                                    var valueE = d[keyE];
                                    //se vuelve a serializar y luego se obtiene los keys:
                                   // var valueSerialized = JsonConvert.SerializeObject(value);

                                    

                                }
                            }

                        }
                    }

                    foreach (var respuesta in objRespuesta)
                    {
                        foreach (var subRespuesta in respuesta)
                        {
                            
                        }
                    }

                    var jsonCompleto = JsonConvert.DeserializeObject<Dictionary<string, string>>(apiResponse);
                    var catalogoJsonString = jsonCompleto.First();

                    var municipioJson = JsonConvert.DeserializeObject(catalogoJsonString.Value);
                    
                    
                    //JObject stuff = JObject.Parse(municipioJson);
                    //foreach (JProperty rate in municipioJson["rates"])
                    //{
                    //}
                    //JArray a = JArray.Parse(municipioJson);

                    var municipioJsonString = JsonConvert.DeserializeObject<Dictionary<string, string>>(catalogoJsonString.Value);

                    foreach (var kv in municipioJsonString)
                    {
                        Console.WriteLine(kv.Key + ":" + kv.Value);
                    }
                    //var municipioResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(dict[0].Value);

                    foreach (var kv in jsonCompleto)
                    {
                        Console.WriteLine(kv.Key + ":" + kv.Value);
                    }

                    var output = JsonConvert.DeserializeObject<dynamic>(apiResponse);
                    //dynamic objRespuestaMunicipio = objRespuesta[0];
                    //foreach (var item in dynJson)
                    //{
                    //}
                    //    string[] tokens = objRespuestaSerializada.Split(",");


                    return Json(new { data = listaUbicacionesGeograficas });
                }

            }

        }
    }
}