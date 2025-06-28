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
using MVCClasico.ViewModels;

namespace MVCClasico.Controllers
{
    
    public class CartsController : Controller
    {
        private readonly EcommerceDatabaseContext _context;

        public CartsController(EcommerceDatabaseContext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            var items = await _context.Carts
                .Include(c => c.Product)
                .Select(c => new CartItem
                {
                    Id = c.Id,
                    ProductId = c.ProductId,
                    ProductName = c.Product.nombre,
                    PrecioUnitario = c.Product.precio,
                    Cantidad = c.Cantidad,
                    
                })
                .ToListAsync();

            return View(items);
        }


        // GET: Carts/Add/5
        public async Task<IActionResult> Add(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
                return NotFound();

            var itemExistente = await _context.Carts.FirstOrDefaultAsync(c => c.ProductId == id);
            if (itemExistente != null)
            {
                itemExistente.Cantidad++;
            }
            else
            {
                var cart = new Cart
                {
                    ProductId = id,
                    Cantidad = 1
                };
                _context.Carts.Add(cart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Carts/Remove/5
        public async Task<IActionResult> Remove(int id)
        {
            var cartItem = await _context.Carts.FindAsync(id);
            if (cartItem != null)
            {
                _context.Carts.Remove(cartItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // GET: Carts/Clear
        public async Task<IActionResult> Clear()
        {
            var allItems = await _context.Carts.ToListAsync();
            if (allItems.Any())
            {
                _context.Carts.RemoveRange(allItems);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }

}
