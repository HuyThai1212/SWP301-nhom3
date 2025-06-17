using Hospital.Data;
using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionDetailController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public PrescriptionDetailController(HospitalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _context.PrescriptionDetails.ToListAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PrescriptionDetail detail)
        {
            _context.PrescriptionDetails.Add(detail);
            await _context.SaveChangesAsync();
            return Ok(detail);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PrescriptionDetail detail)
        {
            var existing = await _context.PrescriptionDetails
                .FirstOrDefaultAsync(d => d.PrescriptionId == detail.PrescriptionId && d.MedicineId == detail.MedicineId);
            if (existing == null) return NotFound();

            existing.Quantity = detail.Quantity;
            existing.Dosage = detail.Dosage;

            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid prescriptionId, [FromQuery] Guid medicineId)
        {
            var detail = await _context.PrescriptionDetails
                .FirstOrDefaultAsync(d => d.PrescriptionId == prescriptionId && d.MedicineId == medicineId);
            if (detail == null) return NotFound();

            _context.PrescriptionDetails.Remove(detail);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
