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
    public class TasaCambiosController : ODataController
    {
        private readonly ApplicationDbContext _context;

        public TasaCambiosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IQueryable<TasaCambio> Get()
        {
            return _context.TasaCambios;
        }

        [EnableQuery]
        public SingleResult<TasaCambio> Get([FromODataUri] int key)
        {
            return SingleResult.Create(_context.TasaCambios.Where(tc => tc.Id == key));
        }

        public async Task<IActionResult> Post([FromBody] TasaCambio tasaCambio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TasaCambios.Add(tasaCambio);
            await _context.SaveChangesAsync();

            return Created(tasaCambio);
        }

        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<TasaCambio> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tasaCambio = await _context.TasaCambios.FindAsync(key);
            if (tasaCambio == null)
            {
                return NotFound();
            }

            patch.Patch(tasaCambio);
            await _context.SaveChangesAsync();

            return Updated(tasaCambio);
        }

        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var tasaCambio = await _context.TasaCambios.FindAsync(key);
            if (tasaCambio == null)
            {
                return NotFound();
            }

            _context.TasaCambios.Remove(tasaCambio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
