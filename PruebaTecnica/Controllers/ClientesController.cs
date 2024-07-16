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
    public class ClientesController : ODataController
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IQueryable<Cliente> Get()
        {
            return _context.Clientes;
        }

        [EnableQuery]
        public SingleResult<Cliente> Get([FromODataUri] int key)
        {
            return SingleResult.Create(_context.Clientes.Where(c => c.Id == key));
        }

        public async Task<IActionResult> Post([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return Created(cliente);
        }

        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<Cliente> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cliente = await _context.Clientes.FindAsync(key);
            if (cliente == null)
            {
                return NotFound();
            }

            patch.Patch(cliente);
            await _context.SaveChangesAsync();

            return Updated(cliente);
        }

        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var cliente = await _context.Clientes.FindAsync(key);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
