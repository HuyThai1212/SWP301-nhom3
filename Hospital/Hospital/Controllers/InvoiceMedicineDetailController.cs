using Hospital.Data;
using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceMedicineDetailController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public InvoiceMedicineDetailController(HospitalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _context.InvoiceMedicineDetails.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _context.InvoiceMedicineDetails.FindAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InvoiceMedicineDetail detail)
        {
            detail.InvoiceDetailMedicineId = Guid.NewGuid();
            _context.InvoiceMedicineDetails.Add(detail);
            await _context.SaveChangesAsync();
            return Ok(detail);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] InvoiceMedicineDetail detail)
        {
            var existing = await _context.InvoiceMedicineDetails.FindAsync(id);
            if (existing == null) return NotFound();

            existing.InvoiceId = detail.InvoiceId;
            existing.VisitId = detail.VisitId;
            existing.PrescriptionId = detail.PrescriptionId;
            existing.MedicineId = detail.MedicineId;
            existing.Description = detail.Description;
            existing.Quantity = detail.Quantity;
            existing.UnitPrice = detail.UnitPrice;
            existing.TotalAmount = detail.TotalAmount;
            existing.IsBilled = detail.IsBilled;

            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _context.InvoiceMedicineDetails.FindAsync(id);
            if (item == null) return NotFound();

            _context.InvoiceMedicineDetails.Remove(item);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
