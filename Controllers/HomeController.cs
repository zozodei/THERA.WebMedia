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
        Usuario usuario = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("usuario"));
        Paciente paciente = BD.levantarPaciente(usuario.id);
        List<Nota> diario = BD.levantarDiario(paciente.id);
        ViewBag.diario = diario;
        return View("Diario");
    }
    public IActionResult irNota(int id)
    {
        ViewBag.estaLogeado = true;
        Nota nota = BD.levantarNota(id);
        ViewBag.nota = nota;
        ViewBag.disparadoras = BD.levantarDisparadoras();
        return View("Nota");
    }
    public IActionResult irAgregarNota(int id)
    {
        ViewBag.estaLogeado = true;
        ViewBag.disparadoras = BD.levantarDisparadoras();
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
    public IActionResult irChatTerapeuta()
    {
        ViewBag.estaLogeado = true;
        Usuario usuario = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("usuario"));
        ViewBag.idChat = null;
        if (!usuario.tipoUsuario)
        {
            Paciente paciente = BD.levantarPaciente(usuario.id);
            ViewBag.idChat = BD.levantarIdChat(paciente.id, paciente.idTerapeuta);
            ViewBag.mensajes = BD.levantarMensajes(ViewBag.idChat);
            return View("ChatTerapeuta");
        }
            return View("ChatTerapeuta");

        // else
        // {
        //     Terapeuta terapeuta = BD.levantarTerapeuta();
        //     ViewBag.pacientes = BD.levantarPacientes(terapeuta.id);
        //     return View("VerChatsPacientes");
        // }
    }
    public IActionResult irHomePaciente()
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
        return View("HomePaciente");
    }


    public IActionResult irChatBot()
    {
        ViewBag.estaLogeado = true;
        return View("ChatBot");
    }
    [HttpPost]
    public IActionResult enviarMensaje(string mensaje, int idChat)
    {
        ViewBag.estaLogeado = true;
        Usuario usuario = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("usuario"));
        BD.enviarMensaje(mensaje, usuario.id, idChat, usuario.tipoUsuario);
        ViewBag.mensajes = BD.levantarMensajes(idChat);
        ViewBag.idChat = idChat;
        return View("ChatTerapeuta");
    }
    public IActionResult irHomeTerapeuta()
    {
        ViewBag.estaLogeado = true;
        return View("HomeTerapeuta");
    }
    [HttpPost]
    public IActionResult modificarNota(int idNota, string titulo, string descripcion, bool visibleTerapeuta, int idDisparadora)
    {
        ViewBag.estaLogeado = true;
        BD.modificarNota(idNota, titulo, descripcion, visibleTerapeuta, idDisparadora);
        Nota nota = BD.levantarNota(idNota);
        ViewBag.nota = nota;
        ViewBag.disparadoras = BD.levantarDisparadoras();
        return View("Nota");
    }
    [HttpPost]
    public IActionResult AgregarNota(string titulo, string descripcion, bool visibleTerapeuta, int idDisparadora)
    {
        Paciente paciente = BD.levantarPaciente(Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("usuario")).id);
        BD.agregarNota(paciente.id, titulo, descripcion, visibleTerapeuta, idDisparadora);
        return RedirectToAction("irDiario", "Home");
    }
    public IActionResult EliminarNota(int idNota)
    {
        BD.eliminarNota(idNota);
        return RedirectToAction("irDiario", "Home");
    }
}