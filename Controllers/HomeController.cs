using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using THERA.Models;

namespace THERA.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Comenzar()
    {
        ViewBag.estaLogeado = false;
        return View("Comenzar");
    }

     public IActionResult irIndex()
    {
        ViewBag.estaLogeado = false;
        return View("Index");
    }
    public IActionResult irDiario()
    {
        ViewBag.estaLogeado = true;
        string idUsuario = HttpContext.Session.GetString("idUsuario");
        List<Nota> diario = BD.levantarDiario(int.Parse((idUsuario)));
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
    public IActionResult irAgregarNota(int id)
    {
        ViewBag.estaLogeado = true;
        return View("AgregarNota");
    }
    public IActionResult irRespiraciones()
    {
        ViewBag.estaLogeado = true;
        return View("Respiraciones");
    }
    public IActionResult irAyudaRapida()
    {
        ViewBag.estaLogeado = true;
        return View("AyudaRapida");
    }
    public IActionResult irFrases()
    {
        ViewBag.estaLogeado = true;
        ViewBag.Frases = BD.levantarFrases();
        return View("Frases");
    }
    public IActionResult irAudios()
    {
        ViewBag.estaLogeado = true;
        ViewBag.Audios = BD.levantarAudios();
        return View("Audios");
    }
    public IActionResult irBuscarTerapeuta()
    {
        ViewBag.estaLogeado = true;
        ViewBag.terapeutas = BD.levantarTerapeutas();
        return View ("BuscarTerapeuta");
    }
    public IActionResult irPerfilTerapeuta()
    {
        ViewBag.estaLogeado = true;
        return View ("PerfilTerapeuta");
    }
    public IActionResult irChatTerapeuta(int idChat)
    {
        ViewBag.estaLogeado = true;
        int idUsuario = int.Parse(HttpContext.Session.GetString("idUsuario"));
        Paciente paciente = BD.levantarPaciente(idUsuario);
        ViewBag.idChat = BD.levantarIdChat(1, 1);
        ViewBag.mensajes = BD.levantarMensajes(ViewBag.idChat);

        return View("ChatTerapeuta");
    }
    public IActionResult irHome()
    {
        ViewBag.estaLogeado = true;
        List<Terapeuta> terapeutas = BD.levantarTerapeutas();
        List<int> cantResenas = BD.levantarCantidadResenas();
        Random random = new Random();
        int numRandom1 = random.Next(0, terapeutas.Count);
        int numRandom2 = 0;
        do{
            numRandom2 = random.Next(0, terapeutas.Count);
        }while(numRandom1 == numRandom2);
        List<Terapeuta> terapeutasSeleccionados = new List<Terapeuta>() {terapeutas[numRandom1], terapeutas[numRandom2]};
        List<int> cantResenasSeleccionadas = new List<int>() {cantResenas[numRandom1], cantResenas[numRandom2]};
        ViewBag.terapeutas = terapeutasSeleccionados;
        ViewBag.cantResenas = cantResenasSeleccionadas;
        //ViewBag.notas = BD.levantarDiario();
        return View("Home");
    }
    
    
    public IActionResult irChatBot()
    {
        ViewBag.estaLogeado = true;
        return View("ChatBot");
    }
    public IActionResult enviarMensaje(string mensaje)
    {
        ViewBag.estaLogeado = true;
        int idChat = BD.levantarIdChat(1, 1);
        int idUsuario = int.Parse(HttpContext.Session.GetString("idUsuario"));
        bool tipoUsuario = bool.Parse(HttpContext.Session.GetString("tipoUsuario"));
        BD.enviarMensaje(mensaje, idUsuario, idChat, tipoUsuario);
        ViewBag.mensajes = BD.levantarMensajes(idChat);
        return View("ChatTerapeuta");
    }
}