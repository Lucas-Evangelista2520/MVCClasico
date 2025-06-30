using Microsoft.AspNetCore.Mvc;
using MVCClasico.Models;
using MVCClasico.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MVCClasico.Controllers
{
    public class LoginController : Controller
    {
        private readonly EcommerceDatabaseContext _context;

        public LoginController(EcommerceDatabaseContext context)
        {
            _context = context;
        }

        // GET: /Login/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        // POST: /Login/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Si hay errores de validación, solo mostrar esos
                return View(model);
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
            if (usuario != null)
            {
                // Guardar usuario en sesión
                HttpContext.Session.SetString("UserEmail", usuario.Email);
                HttpContext.Session.SetString("IsAdmin", usuario.IsAdmin ? "true" : "false");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Email o contraseña incorrectos");
            }
            return View(model);
        }

        // GET: /Login/Logout
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

