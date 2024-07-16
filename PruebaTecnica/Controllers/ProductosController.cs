using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Data;
using PruebaTecnica.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnica.Controllers
{
    public class ProductosController : ODataController
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IQueryable<Producto> Get()
        {
            return _context.Productos;
        }

        [EnableQuery]
        public SingleResult<Producto> Get([FromODataUri] int key)
        {
            return SingleResult.Create(_context.Productos.Where(p => p.Id == key));
        }

        public async Task<IActionResult> Post([FromBody] Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return Created(producto);
        }

        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<Producto> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var producto = await _context.Productos.FindAsync(key);
            if (producto == null)
            {
                return NotFound();
            }

            patch.Patch(producto);
            await _context.SaveChangesAsync();

            return Updated(producto);
        }

        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var producto = await _context.Productos.FindAsync(key);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
