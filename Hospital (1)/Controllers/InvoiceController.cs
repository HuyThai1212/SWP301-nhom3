using Hospital.Data;
using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public InvoiceController(HospitalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _context.Invoices.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _context.Invoices.FindAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Invoice invoice)
        {
            invoice.InvoiceId = Guid.NewGuid();
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
            return Ok(invoice);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Invoice invoice)
        {
            var existing = await _context.Invoices.FindAsync(id);
            if (existing == null) return NotFound();

            existing.PatientId = invoice.PatientId;
            existing.Amount = invoice.Amount;
            existing.Status = invoice.Status;
            existing.IssuedDate = invoice.IssuedDate;

            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _context.Invoices.FindAsync(id);
            if (item == null) return NotFound();

            _context.Invoices.Remove(item);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
