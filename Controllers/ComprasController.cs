using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCClasico.Context;
using MVCClasico.Models;
using MVCClasico.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MVCClasico.Controllers
{
    public class ComprasController : Controller
    {
        private readonly EcommerceDatabaseContext _context;

        public ComprasController(EcommerceDatabaseContext context)
        {
            _context = context;
        }

        // GET: Compras/FinalizarCompra
        public async Task<IActionResult> FinalizarCompra()
        {
            // Obtener los items del carrito
            var itemsCarrito = await _context.Carts
                .Include(c => c.Product)
                .ToListAsync();

            var cartItems = itemsCarrito.Select(c => new CartItem
            {
                Id = c.Id,
                ProductId = c.ProductId,
                ProductName = c.Product.nombre,
                PrecioUnitario = c.Product.precio,
                Cantidad = c.Cantidad
            }).ToList();

            var totalCarrito = cartItems.Sum(item => item.Subtotal);

            var compraViewModel = new CompraViewModel
            {
                ItemsCarrito = cartItems,
                TotalCarrito = totalCarrito
            };

            return View(compraViewModel);
        }

        // POST: Compras/FinalizarCompra
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinalizarCompra(CompraViewModel compraViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // calcumos total del carrito con LINQ
                    var itemsCarrito = await _context.Carts
                        .Include(c => c.Product)
                        .ToListAsync();

                    var totalCarrito = itemsCarrito.Sum(item => item.Product.precio * item.Cantidad);

                    var compraFinalizada = new CompraFinalizada
                    {
                        FechaCompra = DateTime.Now,
                        PrecioTotal = totalCarrito,
                        Email = compraViewModel.Email,
                        Direccion = compraViewModel.Direccion,
                        CodigoPostal = compraViewModel.CodigoPostal,
                        MetodoPago = compraViewModel.MetodoPago
                    };

                    _context.ComprasFinalizadas.Add(compraFinalizada);
                    await _context.SaveChangesAsync();

                    // creamos los detalles de compra
                    foreach (var item in itemsCarrito)
                    {
                        var detalleCompra = new DetalleCompra
                        {
                            CompraFinalizadaId = compraFinalizada.Id,
                            ProductoId = item.ProductId,
                            Cantidad = item.Cantidad,
                            Precio = item.Product.precio
                        };

                        _context.DetallesCompra.Add(detalleCompra);
                    }

                    // limpiamos el carrito una vez confirmada la compra
                    _context.Carts.RemoveRange(itemsCarrito);
                    await _context.SaveChangesAsync();

                    TempData["MensajeExito"] = "¡Compra realizada con éxito! Tu número de compra es: " + compraFinalizada.Id;
                    return RedirectToAction("ConfirmacionCompra", new { id = compraFinalizada.Id });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al procesar la compra: " + ex.Message);
                }
            }

            // si llegara a haber un error, recarga los items del carrito
            var cartItems = await _context.Carts
                .Include(c => c.Product)
                .ToListAsync();

            compraViewModel.ItemsCarrito = cartItems.Select(c => new CartItem
            {
                Id = c.Id,
                ProductId = c.ProductId,
                ProductName = c.Product.nombre,
                PrecioUnitario = c.Product.precio,
                Cantidad = c.Cantidad
            }).ToList();

            compraViewModel.TotalCarrito = compraViewModel.ItemsCarrito.Sum(item => item.Subtotal);

            return View(compraViewModel);
        }

        // GET: Compras/ConfirmacionCompra/5
        public async Task<IActionResult> ConfirmacionCompra(int id)
        {
            var compra = await _context.ComprasFinalizadas
                .Include(c => c.DetallesCompra)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }
    }
} 