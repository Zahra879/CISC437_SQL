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
    public class OrderStatesController : ControllerBase
    {
        private readonly WebRestOracleContext _context;

        public OrderStatesController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/OrderState
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderState>>> GetOrderState()
        {
            return await _context.OrderStates.ToListAsync();
        }

        // GET: api/OrderState/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderState>> GetOrderState(string id)
        {
            var OrderState = await _context.OrderStates.FindAsync(id);

            if (OrderState == null)
            {
                return NotFound();
            }

            return OrderState;
        }

        // PUT: api/OrderState/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderState(string id, OrderState OrderState)
        {
            if (id != OrderState.OrderStateId)
            {
                return BadRequest();
            }

            _context.Entry(OrderState).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderStateExists(id))
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

        // POST: api/OrderState
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderState>> PostOrderState(OrderState OrderState)
        {
            _context.OrderStates.Add(OrderState);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderState", new { id = OrderState.OrderStateId }, OrderState);
        }

        // DELETE: api/OrderState/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderState(string id)
        {
            var OrderState = await _context.OrderStates.FindAsync(id);
            if (OrderState == null)
            {
                return NotFound();
            }

            _context.OrderStates.Remove(OrderState);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderStateExists(string id)
        {
            return _context.OrderStates.Any(e => e.OrderStateId == id);
        }
    }
}