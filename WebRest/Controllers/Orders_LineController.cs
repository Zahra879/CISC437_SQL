using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRestEF.EF.Data;
using WebRestEF.EF.Models;

namespace WebRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersLinesController : ControllerBase
    {
        private readonly WebRestOracleContext _context;

        public OrdersLinesController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/OrdersLine
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdersLine>>> GetOrdersLine()
        {
            return await _context.OrdersLines.ToListAsync();
        }

        // GET: api/OrdersLine/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdersLine>> GetOrdersLine(string id)
        {
            var OrdersLine = await _context.OrdersLines.FindAsync(id);

            if (OrdersLine == null)
            {
                return NotFound();
            }

            return OrdersLine;
        }

        // PUT: api/OrdersLine/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrdersLine(string id, OrdersLine OrdersLine)
        {
            if (id != OrdersLine.OrdersLineId)
            {
                return BadRequest();
            }

            _context.Entry(OrdersLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersLineExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OrdersLine
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrdersLine>> PostProduct_Price(OrdersLine OrdersLine)
        {
            _context.OrdersLines.Add(OrdersLine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrdersLine", new { id = OrdersLine.OrdersLineId }, OrdersLine);
        }

        // DELETE: api/OrdersLine/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdersLine(string id)
        {
            var OrdersLine = await _context.OrdersLines.FindAsync(id);
            if (OrdersLine == null)
            {
                return NotFound();
            }

            _context.OrdersLines.Remove(OrdersLine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdersLineExists(string id)
        {
            return _context.OrdersLines.Any(e => e.OrdersLineId == id);
        }
    }
}
