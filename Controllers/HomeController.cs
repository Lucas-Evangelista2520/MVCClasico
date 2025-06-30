using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCClasico.Context;
using MVCClasico.Models;

namespace MVCClasico.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EcommerceDatabaseContext _context;
        public HomeController(ILogger<HomeController> logger, EcommerceDatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        /*  public IActionResult Index()
        {
            return View();
        }*/
        public IActionResult Index()
        {
            var productos = _context.Productos.ToList();
            ViewBag.Productos = productos;
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
