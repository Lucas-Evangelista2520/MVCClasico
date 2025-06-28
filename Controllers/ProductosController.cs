using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCClasico.Context;
using MVCClasico.Models;

namespace MVCClasico.Controllers
{
    public class ProductosController : Controller
    {
        private readonly EcommerceDatabaseContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductosController(EcommerceDatabaseContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Productos
        public async Task<IActionResult> Index() =>
            View(await _context.Productos.ToListAsync());

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null) return NotFound();

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create() => View();

        // POST: Productos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto)
        {
            if (producto.ImagenFile == null || producto.ImagenFile.Length == 0)
                ModelState.AddModelError(nameof(producto.ImagenFile), "La imagen es obligatoria.");

            if (!ModelState.IsValid) return View(producto);

            var uploadsDir = Path.Combine(_env.WebRootPath, "public");
            Directory.CreateDirectory(uploadsDir);

            var fileName = $"{Guid.NewGuid():N}{Path.GetExtension(producto.ImagenFile.FileName)}";
            var path = Path.Combine(uploadsDir, fileName);

            await using (var stream = System.IO.File.Create(path))
            {
                await producto.ImagenFile.CopyToAsync(stream);
            }

            producto.imagen = fileName;

            _context.Add(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();

            return View(producto);
        }

        // POST: Productos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Producto producto)
        {
            if (id != producto.Id) return NotFound();

            var productoBd = await _context.Productos
                           .AsNoTracking()
                           .FirstOrDefaultAsync(p => p.Id == id);
            if (productoBd == null) return NotFound();

            if (producto.ImagenFile != null && producto.ImagenFile.Length > 0)
            {
                var uploadsDir = Path.Combine(_env.WebRootPath, "public");
                Directory.CreateDirectory(uploadsDir);

                var fileName = $"{Guid.NewGuid():N}{Path.GetExtension(producto.ImagenFile.FileName)}";
                var path = Path.Combine(uploadsDir, fileName);

                await using (var stream = System.IO.File.Create(path))
                {
                    await producto.ImagenFile.CopyToAsync(stream);
                }

                if (!string.IsNullOrEmpty(productoBd.imagen))
                {
                    var oldPath = Path.Combine(uploadsDir, productoBd.imagen);
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                producto.imagen = fileName;
            }
            else
            {
                producto.imagen = productoBd.imagen;
            }

            if (!ModelState.IsValid) return View(producto);

            try
            {
                _context.Update(producto);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Productos.Any(e => e.Id == id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null) return NotFound();

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
                _context.Productos.Remove(producto);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
