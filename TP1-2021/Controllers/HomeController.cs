using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text.Json;
using TP1_2021.Entities;
using TP1_2021.Models;

namespace TP1_2021.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into HomeController");
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Hello, this is the index!");
            return View();
        }
        public int problemOne(int a, int b)
        {
            return a - b;
        }
        public IActionResult Problema1()
        {

        return View();            
           
        }
        [HttpPost]
        public string Problema1Result(string num)
        {
            try
            {
                int valor = Convert.ToInt32(num);
                return $"El cuadrado de {valor} es: {valor*valor}"; 

            }
            catch (FormatException fe)
            {
                _logger.LogError(fe.ToString());
                return $"Error: {fe.Message}";
            }
                       
        }
        public IActionResult Problema2()
        {

            return View();

        }
        [HttpPost]
        public string Problema2Result(string num1, string num2)
        {
            decimal valor1, valor2;
           
            if (!Decimal.TryParse(num1, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out valor1) || 
                !Decimal.TryParse(num2, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out valor2))
            {
                return "Error al ingresar los datos";
            }
            else if(valor2==0)
            {
                return "No es posible dividir en cero";
            }
                else
                {
                    try
                    {

                        return $"El resultado de {valor1} dividido en {valor2} es: {Math.Round(valor1 / valor2),4}";

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.ToString());
                        return $"Error al procesar datos /n {ex.Message}";
                    }

                }



        }
        

        public IActionResult Problema4()
        {

            return View();

        }
        [HttpPost]
        public string Problema4Result(string num1, string num2)
        {
            double valor1, valor2;

            if (!Double.TryParse(num1, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out valor1) ||
                !Double.TryParse(num2, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out valor2))
            {
                return "Error al ingresar los datos";
            }
            else if (valor2 == 0)
            {
                return "No es posible dividir en cero";
            }
                else
                {
                    try
                    {

                        return $"El rendimiento es de {Math.Round((valor1 / valor2), 4)} Km/L";

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.ToString());
                        return $"Error al procesar los datos /n {ex.Message}";
                    }
                }
            

            
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public string Problema3()
        {
            string listadoProvincias = "";
            try
            {
                listadoProvincias += getProvincias();
                return listadoProvincias;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return $"Error: {ex.Message}";
            }
        }
        public static string getProvincias()
        {
            var url = $"https://apis.datos.gob.ar/georef/api/provincias?campos=id,nombre";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            string cadena = "";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader != null)
                        {
                            using (StreamReader objReader = new StreamReader(strReader))
                            {
                                string responseBody = objReader.ReadToEnd();
                                provinciasArgentina ListProvincias = JsonSerializer.Deserialize<provinciasArgentina>(responseBody);
                                foreach (provincia prov in ListProvincias.Provincias)
                                {
                                    cadena += $"\t id : {prov.Id}, nombre : {prov.Nombre} \n ";
                                }
                            }
                        }
                    }
                }
                return cadena;

            }
            catch (Exception ex)
            {
                return $"Error {ex.Message}";
            }
            
        }
    }

}
