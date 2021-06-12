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
    public class LeituraController : Controller
    {
        LeiturasContext _context;
        private readonly ILogger<HomeController> _logger;

        public LeituraController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = new LeiturasContext();
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult incluirLeitura(string leitor, string etiqueta){
            if(!string.IsNullOrWhiteSpace(leitor) && !string.IsNullOrWhiteSpace(etiqueta)){
                var _equipamento = _context.Equipamentos.Where(w=>w.Codigo == etiqueta);
                var _leitor = _context.Leitores.Where(w=>w.Endereco == leitor);
                if(_equipamento != null && _leitor!= null){
                    Leitura leitura = new Leitura();
                    leitura.IdEquipamentos = _equipamento.First().Id;
                    leitura.IdLeitores = _leitor.First().Id;
                    _context.Leituras.Add(leitura);
                    _context.SaveChangesAsync();
                    ViewBag.retorno = "Movimentação gravada";
                    
                }
                else 
                ViewBag.retorno = "Equipamento/Leitor não cadastrado!";
            }
            else
                ViewBag.retorno = "Dados incompletos";
                return View();
        }
    }
}