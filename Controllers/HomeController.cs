using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using projint.Models;

namespace projint.Controllers
{
    public class HomeController : Controller
    {
        LeiturasContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = new LeiturasContext();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Equipamentos()
        {
            List<Equipamento> lista = _context.Equipamentos.ToList();
            string tabela = "";
            foreach (var i in lista){
                tabela += "<tr><td>" + i.Id + "</td><td>" + i.Nome + "</td></tr>";
            }
            ViewBag.lista = tabela;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}