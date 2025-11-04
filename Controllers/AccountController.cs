using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using THERA.Models;

namespace THERA.Controllers;

public class AccountController : Controller
{
    private readonly IWebHostEnvironment _env;

    public AccountController(IWebHostEnvironment env) 
    {
        _env = env;
    }
    public IActionResult Login(string username, string contraseña)
    {
        int idUsuario = BD.Login(username, contraseña);
        if(idUsuario != 0)
        {
            bool tipoUsuario = BD.levantarTipoUsuario(idUsuario);
            HttpContext.Session.SetString("usuario", Objeto.ObjetoATexto<Usuario>(BD.levantarUsuario(idUsuario)));
            HttpContext.Session.SetString("idUsuario", idUsuario.ToString());
            HttpContext.Session.SetString("tipoUsuario", tipoUsuario.ToString());
            ViewBag.idUsuario = idUsuario;
            return RedirectToAction("irHome", "Home");
        }
        else
        {
            ViewBag.estaLogeado = false;
            ViewBag.segundoIntento = true;
            return View("Login");
        }
    }
    public IActionResult Registro(string username, string contraseña, int tipoDeUsuario)
    {
        int idUsuario = BD.Registro(username, contraseña, tipoDeUsuario);

        bool tipoUsuario = BD.levantarTipoUsuario(idUsuario);
        HttpContext.Session.SetString("usuario", Objeto.ObjetoATexto<Usuario>(BD.levantarUsuario(idUsuario)));
        return RedirectToAction("Login", "Account");
    }
    public IActionResult CerrarSesion()
    {
        HttpContext.Session.Remove("usuario");
        return View("Index", "Home");
    }
    public IActionResult LoginView()
    {
        ViewBag.estaLogeado = false;
        ViewBag.segundoIntento = false;
        return View("Login");
    }
    public IActionResult RegistroView()
    {
        ViewBag.estaLogeado = false;
        return View("Registro");
    }
}