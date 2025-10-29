using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using THERA.Models;

namespace ToDoList.Controllers;

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
        HttpContext.Session.SetString("idUsuario", idUsuario.ToString());
        ViewBag.idUsuario = idUsuario;
        return View("Home");
    }
    public IActionResult Registro(string username, string contraseña, int tipoDeUsuario)
    {
        int idUsuario = BD.Registro(username, contraseña, tipoDeUsuario);
        HttpContext.Session.SetString("idUsuario", idUsuario.ToString());
        return RedirectToAction("Login", "Account");
    }
    public IActionResult CerrarSesion()
    {
        HttpContext.Session.Remove("IdUsuario");
        return View("Index");
    }
    public IActionResult LoginView()
    {
        ViewBag.estaLogeado = false;
        return View("Login");
    }
    public IActionResult RegistroView()
    {
        ViewBag.estaLogeado = false;
        return View("Registro");
    }
}