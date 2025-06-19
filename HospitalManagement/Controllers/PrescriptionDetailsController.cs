using HospitalManagement.DTOs;
using HospitalManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionDetailsController : ControllerBase
    {
        private readonly HospitalContext _context;

        public PrescriptionDetailsController(HospitalContext context)
        {
            _context = context;
        }

        // GET: api/PrescriptionDetail/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<PrescriptionDetailDTO>>> GetPrescriptionDetails(Guid id)
        {
            var details = await _context.PrescriptionDetails
                .Where(d => d.PrescriptionId == id)
                .Include(d => d.Medicine)
                .ToListAsync();

            if (details == null || details.Count == 0)
            {
                return NotFound("Không tìm thấy chi tiết đơn thuốc.");
            }

            var detailDTOs = details.Select(d => new PrescriptionDetailDTO
            {
                MedicineId = d.MedicineId ?? Guid.Empty,
                MedicineName = d.Medicine?.Name ?? "Unknown",
                Quantity = d.Quantity ?? 0,
                Dosage = d.Dosage ?? ""
            }).ToList();

            return Ok(detailDTOs);
        }
    }

}
