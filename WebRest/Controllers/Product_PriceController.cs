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
    public class ProductPricesController : ControllerBase
    {
        private readonly WebRestOracleContext _context;

        public ProductPricesController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/ProductPrice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductPrice>>> GetProductPrice()
        {
            return await _context.ProductPrices.ToListAsync();
        }

        // GET: api/ProductPrice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductPrice>> GetProductPrice(string id)
        {
            var ProductPrice = await _context.ProductPrices.FindAsync(id);

            if (ProductPrice == null)
            {
                return NotFound();
            }

            return ProductPrice;
        }

        // PUT: api/ProductPrice/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductPrice(string id, ProductPrice ProductPrice)
        {
            if (id != ProductPrice.ProductPriceId)
            {
                return BadRequest();
            }

            _context.Entry(ProductPrice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductPriceExists(id))
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

        // POST: api/ProductPrice
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductPrice>> PostProductPrice(ProductPrice ProductPrice)
        {
            _context.ProductPrices.Add(ProductPrice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductPrice", new { id = ProductPrice.ProductPriceId }, ProductPrice);
        }

        // DELETE: api/ProductPrice/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductPrice(string id)
        {
            var ProductPrice = await _context.ProductPrices.FindAsync(id);
            if (ProductPrice == null)
            {
                return NotFound();
            }

            _context.ProductPrices.Remove(ProductPrice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductPriceExists(string id)
        {
            return _context.ProductPrices.Any(e => e.ProductPriceId == id);
        }
    }
}
