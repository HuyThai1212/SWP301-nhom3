using Hospital.Data;
using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public PrescriptionController(HospitalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _context.Prescriptions.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _context.Prescriptions.FindAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Prescription prescription)
        {
            prescription.PrescriptionId = Guid.NewGuid();
            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();
            return Ok(prescription);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Prescription prescription)
        {
            var existing = await _context.Prescriptions.FindAsync(id);
            if (existing == null) return NotFound();

            existing.VisitId = prescription.VisitId;
            existing.CreatedAt = prescription.CreatedAt;
            existing.Note = prescription.Note;

            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _context.Prescriptions.FindAsync(id);
            if (item == null) return NotFound();

            _context.Prescriptions.Remove(item);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
