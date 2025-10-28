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
        ViewBag.estaLogeado = true;
        List<Nota> diario = BD.levantarDiario(int.Parse((HttpContext.Session.GetString("idPaciente"))));
        ViewBag.diario = diario;
        return View("Diario");
    }
    public IActionResult irNota(int id)
    {
        ViewBag.estaLogeado = true;
        Nota nota = BD.levantarNota(id);
        ViewBag.nota = nota;
        return View("Nota");
    }
    public IActionResult irRespiraciones()
    {
        ViewBag.estaLogeado = true;
        return View("Respiraciones");
    }
    public IActionResult irFrases()
    {
        ViewBag.estaLogeado = true;
        return View("Frases");
    }
    public IActionResult irAudios()
    {
        ViewBag.estaLogeado = true;
        return View("Audios");
    }
    public IActionResult irBuscarTerapeuta()
    {
        ViewBag.estaLogeado = true;
        return View ("BuscarTerapeuta");
    }
    public IActionResult irPerfilTerapeuta()
    {
        ViewBag.estaLogeado = true;
        return View ("PerfilTerapeuta");
    }
    public IActionResult irChatTerapeuta()
    {
        ViewBag.estaLogeado = true;
        return View("ChatTerapeuta");
    }
    public IActionResult irHome()
    {
        ViewBag.estaLogeado = true;
        return View("Home");
    }
}