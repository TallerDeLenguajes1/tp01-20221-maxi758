using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using TP1_2021.Models;

namespace TP1_2021.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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
            try
            {
                double valor1 = Convert.ToDouble(num1);
                double valor2 = Convert.ToDouble(num2);
                return $"El resultado de {valor1} dividido en {valor2} es: {valor1/valor2}";

            }
            catch (FormatException fe)
            {
                return $"Error: {fe.Message}";
            }
            catch (DivideByZeroException ze)
            {
                return $"Error: {ze.Message}";
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
    }
}
