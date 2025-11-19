using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
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
        ViewBag.terapeutaLogeado = false;
        return RedirectToAction("irAudios");
    }

     public IActionResult irIndex()
    {
        ViewBag.estaLogeado = false;
        ViewBag.terapeutaLogeado = false;
        return View("Index", "Home");
    }
    public IActionResult irDiario()
    {
        ViewBag.estaLogeado = true;
        ViewBag.terapeutaLogeado = false;
        Usuario usuario = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("usuario"));
        Paciente paciente = BD.levantarPaciente(usuario.id);
        List<Nota> diario = BD.levantarDiario(paciente.id);
        ViewBag.diario = diario;
        return View("Diario");
    }
    public IActionResult irNota(int id)
    {
        ViewBag.estaLogeado = true;
        ViewBag.terapeutaLogeado = false;
        Nota nota = BD.levantarNota(id);
        ViewBag.nota = nota;
        ViewBag.disparadoras = BD.levantarDisparadoras();
        return View("Nota");
    }
    public IActionResult irAgregarNota(int id)
    {
        ViewBag.estaLogeado = true;
        ViewBag.terapeutaLogeado = false;
        ViewBag.disparadoras = BD.levantarDisparadoras();
        return View("AgregarNota");
    }
    public IActionResult irRespiraciones()
    {
        ViewBag.estaLogeado = true;
        ViewBag.terapeutaLogeado = false;
        return View("Respiraciones");
    }
    public IActionResult irAyudaRapida()
    {
        ViewBag.estaLogeado = true;
        ViewBag.terapeutaLogeado = false;
        return View("AyudaRapida");
    }
    public IActionResult irFrases()
    {
        ViewBag.estaLogeado = true;
        ViewBag.terapeutaLogeado = false;
        ViewBag.Frases = BD.levantarFrases();
        return View("Frases");
    }
    public IActionResult irAudios()
    {
        ViewBag.estaLogeado = true;
        ViewBag.terapeutaLogeado = false;
        ViewBag.Audios = BD.levantarAudios();
        return View("Audios");
    }
    public IActionResult irBuscarTerapeuta()
    {
        ViewBag.estaLogeado = true;
        ViewBag.terapeutaLogeado = false;
        ViewBag.terapeutas = BD.levantarTerapeutas();
        List<int> cantResenas = BD.levantarCantidadResenas();
        ViewBag.cantResenas = cantResenas;
        return View ("BuscarTerapeuta");
    }
    public IActionResult irPerfilTerapeutaPublico(int idTerapeuta)
    {
        ViewBag.estaLogeado = true;
        ViewBag.terapeutaLogeado = false;
        Terapeuta terapeuta = BD.levantarTerapeutaConIdTerapeuta(idTerapeuta);
        ViewBag.terapeuta = terapeuta;
        ViewBag.especialidades = BD.levantarEspecialidadesXTerapeuta(idTerapeuta);
        ViewBag.rating = BD.levantarPromedioRatingPorTerapeuta(idTerapeuta);
        ViewBag.resenas = BD.levantarResenasPorTerapeuta(idTerapeuta);
        ViewBag.frasesFav = BD.levantarFrasesFavTerapeuta(idTerapeuta);
        ViewBag.obrasSociales = BD.levantarObrasSocialesXTerapeuta(idTerapeuta);
        return View ("PerfilTerapeutaPublico");
    }
    public IActionResult irChatTerapeuta()
    {
        ViewBag.estaLogeado = true;
        ViewBag.terapeutaLogeado = false;
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
    public IActionResult irChatNuevoTerapeuta(int idTerapeuta)
    {
        ViewBag.estaLogeado = true;
        ViewBag.terapeutaLogeado = false;
        Usuario usuario = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("usuario"));
        Paciente paciente = BD.levantarPaciente(usuario.id);
        ViewBag.idChat = BD.levantarIdChat(paciente.id, paciente.idTerapeuta);
        if(ViewBag.idChat == -1){
            BD.CrearNuevoChat(paciente.id, idTerapeuta);
            ViewBag.mensajes = new List<Mensaje>();
        }else{
            ViewBag.mensajes = BD.levantarMensajes(ViewBag.idChat);
        }
        return View("ChatTerapeuta");
    }
    public IActionResult irHomePaciente()
    {
        Usuario usuario = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("usuario"));
        Paciente paciente = BD.levantarPaciente(usuario.id);
        if(paciente.idTerapeuta != null){
            return RedirectToAction("irHomePacienteConTerapeuta");
        }
        ViewBag.estaLogeado = true;
        ViewBag.terapeutaLogeado = false;
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
        ViewBag.notas = BD.levantarDiario(paciente.id);
        return View("HomePacienteSinTerapeuta");
    }
    public IActionResult irHomePacienteConTerapeuta(){
        Usuario usuario = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("usuario"));
        ViewBag.estaLogeado = true;
        ViewBag.terapeutaLogeado = false;
        Sesión ultimaTarea = BD.levantarUltimaTareaYRespuesta(BD.levantarPaciente(usuario.id).id);
        ViewBag.tarea = ultimaTarea.Tarea;
        ViewBag.respuesta = ultimaTarea.RespuestaPaciente;
        ViewBag.fecha = ultimaTarea.Fecha;
        return View("HomePacienteConTerapeuta", "Home");
    }

    public IActionResult guardarRespuestaPaciente(string Respuesta){
        Usuario usuario = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("usuario"));
        ViewBag.estaLogeado = true;
        ViewBag.terapeutaLogeado = false;
        Paciente paciente = BD.levantarPaciente(usuario.id);
        Sesión sesion = BD.levantarUltimaSesion(paciente.id);
        BD.guardarRespuestaPaciente(Respuesta, sesion.Id);
        return RedirectToAction("irHomePacienteConTerapeuta", "Home");
    }
    public IActionResult irChatBot()
    {
        ViewBag.estaLogeado = true;
        ViewBag.terapeutaLogeado = false;
        return View("ChatBot");
    }
    [HttpPost]
    public IActionResult enviarMensaje(string mensaje, int idChat)
    {
        ViewBag.estaLogeado = true;
        ViewBag.terapeutaLogeado = false;
        Usuario usuario = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("usuario"));
        BD.enviarMensaje(mensaje, usuario.id, idChat, usuario.tipoUsuario);
        ViewBag.mensajes = BD.levantarMensajes(idChat);
        ViewBag.idChat = idChat;
        return View("ChatTerapeuta");
    }
    public IActionResult irHomeTerapeuta()
    {
        ViewBag.terapeutaLogeado = true;
        ViewBag.estaLogeado = false;
        return View("HomeTerapeuta");
    }
    [HttpPost]
    public IActionResult modificarNota(int idNota, string titulo, string descripcion, bool visibleTerapeuta, int idDisparadora)
    {
        ViewBag.estaLogeado = true;
        ViewBag.terapeutaLogeado = false;
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