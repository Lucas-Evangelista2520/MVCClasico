using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MVCClasico.Context;
using MVCClasico.Models;

namespace MVCClasico.Controllers
{
    [Authorize]
    public class CartsController : Controller
    {
        private readonly EcommerceDatabaseContext _context;
        public readonly UserManager<ApplicationUser> _userManager;

        public CartsController(EcommerceDatabaseContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);

            var cart = await _context.Carts
                .Include(x => x.Product)
                .Where(x => x.UserId == currentuser.Id)
                .ToListAsync();

            double totalCost = 0;

            foreach (var cartItem in cart)
            {
                totalCost += cartItem.Product.precio;
            }

            ViewBag.TotalCost = totalCost;

            return View(cart);
        }

        public async Task<IActionResult> AddToCart(int productId)
        {

            var currentuser = await _userManager.GetUserAsync(HttpContext.User);

            var product = await _context.Productos.Where(x => x.Id == productId).FirstOrDefaultAsync();

            if (product == null)
            {
                return BadRequest();
            }

            var cart = new Cart { ProductId = productId, UserId = currentuser.Id };

            _context.Add(cart);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Remove(int id)
        {
            var cartItem = await _context.Carts.FindAsync(id);
            if (cartItem == null)
            {
                return BadRequest();
            }
            _context.Carts.Remove(cartItem);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
