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
            if (usuario.tipoUsuario == 1)
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
        if(idUsuario!=-1 && idUsuario !=-2){ 
            Usuario usuario = BD.levantarUsuario(idUsuario); 
            HttpContext.Session.SetString("usuario", Objeto.ObjectToString(usuario)); 
            return RedirectToAction("Login", "Account"); 
        }else if(idUsuario ==-1){ 
            ViewBag.error = "El username ya está en uso. Intente nuevamente."; 
            ViewBag.segundoIntento = true; 
            ViewBag.estaLogeado = false;
            return View("Registro", "Account"); 
        }else{ 
            ViewBag.error = "Ocurrió un error inesperado. Intente nuevamente."; 
            ViewBag.segundoIntento = true; 
            ViewBag.estaLogeado = false;
            return View("Registro", "Account"); 
        }
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
        ViewBag.segundoIntento = false; 

        return View("Registro");
    }
}