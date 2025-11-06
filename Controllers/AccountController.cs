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
        Usuario usuario = BD.levantarUsuario(idUsuario);
        if(idUsuario != -1)
        {
            HttpContext.Session.SetString("usuario", Objeto.ObjectToString(usuario));
            if (!usuario.tipoUsuario)
            {
                return RedirectToAction("irHomePaciente", "Home");
            }
            else
            {
                return RedirectToAction("irHomeTerapeuta", "Home");
            }
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

        Usuario usuario = BD.levantarUsuario(idUsuario);
        HttpContext.Session.SetString("usuario", Objeto.ObjectToString(usuario));
        return RedirectToAction("Login", "Account");
    }
    public IActionResult CerrarSesion()
    {
        HttpContext.Session.Remove("usuario");
        return View("irIndex", "Home");
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