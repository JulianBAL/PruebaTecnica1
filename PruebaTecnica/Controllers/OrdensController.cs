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
    public class OrdenesController : ODataController
    {
        private readonly ApplicationDbContext _context;

        public OrdenesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IQueryable<Orden> Get()
        {
            return _context.Ordenes;
        }

        [EnableQuery]
        public SingleResult<Orden> Get([FromODataUri] int key)
        {
            return SingleResult.Create(_context.Ordenes.Where(o => o.Id == key));
        }

        public async Task<IActionResult> Post([FromBody] Orden orden)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Ordenes.Add(orden);
            await _context.SaveChangesAsync();

            return Created(orden);
        }

        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<Orden> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orden = await _context.Ordenes.FindAsync(key);
            if (orden == null)
            {
                return NotFound();
            }

            patch.Patch(orden);
            await _context.SaveChangesAsync();

            return Updated(orden);
        }

        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var orden = await _context.Ordenes.FindAsync(key);
            if (orden == null)
            {
                return NotFound();
            }

            _context.Ordenes.Remove(orden);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
