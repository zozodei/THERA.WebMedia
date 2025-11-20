using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using THERA.Models;

namespace THERA.Controllers;

public class HomeTerapeutaController : Controller
{
    private readonly IWebHostEnvironment _env;

    public HomeTerapeutaController(IWebHostEnvironment env) 
    {
        _env = env;
    }

    public IActionResult irVerPacientes()
    {
        ViewBag.estaLogeado = true;
        ViewBag.terapeutaLogeado = true;
        Usuario usuario = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("usuario"));
        Terapeuta terapeuta = BD.levantarTerapeuta(usuario.id);
        List<Paciente> pacientes = BD.levantarPacientes(terapeuta.id);
        ViewBag.pacientes = pacientes;
        return View("Pacientes", "HomeTerapeuta");
    }
    public IActionResult irDatosSesiones(int idPaciente)
    {
        ViewBag.estaLogeado = true;
        ViewBag.terapeutaLogeado = true;
        Usuario usuario = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("usuario"));
        Terapeuta terapeuta = BD.levantarTerapeuta(usuario.id);
        List<Sesión> sesiones = BD.levantarSesionesXPaciente(idPaciente, terapeuta.id);
        ViewBag.sesiones = sesiones;
        return View("DatosSesiones", "HomeTerapeuta");
    }
    public IActionResult irDatosSesion(int idSesion)
    {
        ViewBag.estaLogeado = true;
        ViewBag.terapeutaLogeado = true;
        Sesión sesion = BD.levantarSesión(idSesion);
        ViewBag.sesion = sesion;
        ViewBag.paciente = BD.levantarPacienteConIdPaciente(sesion.idPaciente);
        return View("DatosSesion", "HomeTerapeuta");
    }
    public IActionResult guardarDatosSesion(string Anotaciones, string Tarea, int idSesion)
    {
        BD.guardarDatosSesion(Anotaciones, Tarea, idSesion);
        return RedirectToAction("DatosSesion", "HomeTerapeuta", new {idSesion = idSesion});
    }
    public IActionResult verDiarioPaciente(int idPaciente)
    {
        ViewBag.estaLogeado = true;
        ViewBag.terapeutaLogeado = true;
        List<Nota> notas = BD.levantarNotasCompartidas(idPaciente);
        ViewBag.paciente = BD.levantarPacienteConIdPaciente(idPaciente);
        ViewBag.diario = notas;
        return View("DiarioPaciente", "HomeTerapeuta");
    }
    public IActionResult irNota(int idNota)
    {
        ViewBag.estaLogeado = true;
        ViewBag.terapeutaLogeado = true;
        Nota nota = BD.levantarNota(idNota);
        ViewBag.nota = nota;
        ViewBag.paciente = BD.levantarPacienteConIdPaciente(nota.idPaciente);
        return View("NotaPaciente", "HomeTerapeuta");
    }
    public IActionResult irDatosPaciente(int idPaciente){
        ViewBag.estaLogeado = true;
        ViewBag.terapeutaLogeado = true;
        Paciente paciente = BD.levantarPacienteConIdPaciente(idPaciente);
        ViewBag.paciente = paciente;
        return View("DatosPaciente");
    }
}
