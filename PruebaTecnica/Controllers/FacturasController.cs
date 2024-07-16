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
    public class FacturasController : ODataController
    {
        private readonly ApplicationDbContext _context;

        public FacturasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IQueryable<Factura> Get()
        {
            return _context.Facturas;
        }

        [EnableQuery]
        public SingleResult<Factura> Get([FromODataUri] int key)
        {
            return SingleResult.Create(_context.Facturas.Where(f => f.Id == key));
        }

        public async Task<IActionResult> Post([FromBody] Factura factura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();

            return Created(factura);
        }

        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<Factura> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var factura = await _context.Facturas.FindAsync(key);
            if (factura == null)
            {
                return NotFound();
            }

            patch.Patch(factura);
            await _context.SaveChangesAsync();

            return Updated(factura);
        }

        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var factura = await _context.Facturas.FindAsync(key);
            if (factura == null)
            {
                return NotFound();
            }

            _context.Facturas.Remove(factura);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
