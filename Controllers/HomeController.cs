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
            var leituras = _context.Leituras
                .GroupBy(g=>new {g.IdEquipamentos, g.IdLeitores})
                    .Select(s=>new{s.Key.IdLeitores, s.Key.IdEquipamentos, Data = s.Max(g=>g.Data)}).ToList()
                    .Join(_context.Equipamentos,i=> new{id1= i.IdEquipamentos}, j=>new{id1 = j.Id}, (i,j)=>new {i.IdEquipamentos,i.IdLeitores,j.Nome,j.Codigo, i.Data})
                    .Join(_context.Leitores,i=>new{id= i.IdLeitores},j=>new{id= j.Id},(i,j)=> new{ i.Data, i.Nome, j.Localidade }).ToList();
            string retorno = "";
            foreach(var i in leituras){
                retorno += "<tr><td>" + i.Nome + "</td><td>" + i.Localidade + "</td><td>" + i.Data.ToString("dd/MM/yyyy HH:mm") + "</td></tr>";
            }
            ViewBag.lista = retorno;
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

        public IActionResult Leitores()
        {
            List<Leitores> lista = _context.Leitores.ToList();
            string tabela = "";
            foreach (var i in lista){
                tabela += "<tr><td>" + i.Id + "</td><td>" + i.Localidade + "</td></tr>";
            }
            ViewBag.lista = tabela;
            return View();
        }

        public IActionResult Leituras()
        {
             var leituras = _context.Leituras
                    .Join(_context.Equipamentos,i=> new{id1= i.IdEquipamentos}, j=>new{id1 = j.Id}, (i,j)=>new {i.IdEquipamentos,i.IdLeitores,j.Nome,j.Codigo, i.Data})
                    .Join(_context.Leitores,i=>new{id= i.IdLeitores},j=>new{id= j.Id},(i,j)=> new{ i.Data, i.Nome, j.Localidade }).ToList();
            string retorno = "";
            foreach(var i in leituras){
                retorno += "<tr><td>" + i.Nome + "</td><td>" + i.Localidade + "</td><td>" + i.Data.ToString("dd/MM/yyyy HH:mm") + "</td></tr>";
            }
            ViewBag.lista = retorno;
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}