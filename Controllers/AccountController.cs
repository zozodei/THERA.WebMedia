using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using THERA.Models;
using THERA.WEBMEDIA;

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
        ViewBag.idUsuario = idUsuario;
        return View("Home");
    }
    public IActionResult Registro()
}