using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using THERA.Models;
using THERA.WEBMEDIA;

namespace THERA.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewBag.estaLogeado = false;
        return View("Index");
    }
    public IActionResult irDiario()
    {
        List<Nota> notas = BD.levantarNotas(int.Parse((HttpContext.Session.GetString("idPaciente"))));
        ViewBag.notas = notas;
        return View("VerNotas");
    }
}