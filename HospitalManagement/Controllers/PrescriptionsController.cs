using HospitalManagement.DTOs;
using HospitalManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly HospitalContext _context;

        public PrescriptionsController(HospitalContext context)
        {
            _context = context;
        }

        // GET: api/Prescriptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrescriptionDTO>>> GetPrescriptions()
        {
            var prescriptionsRaw = await _context.Prescriptions
                .Include(p => p.Visit)
                    .ThenInclude(v => v.Doctor)
                        .ThenInclude(d => d.Staff)
                .Include(p => p.Visit)
                    .ThenInclude(v => v.Patient)
                .ToListAsync();

            var prescriptions = prescriptionsRaw.Select(p => new PrescriptionDTO
            {
                PrescriptionId = p.PrescriptionId,
                CreatedAt = p.CreatedAt ?? DateTime.MinValue,
                Note = p.Note ?? "",
                Diagnosis = p.Visit?.Diagnosis ?? "",
                VisitTime = p.Visit?.CreatedAt ?? DateTime.MinValue,
                DoctorName = p.Visit?.Doctor?.Staff?.FullName ?? "N/A",
                PatientName = p.Visit?.Patient?.FullName ?? "N/A",
            }).ToList();

            return Ok(prescriptions);
        }

        // GET: api/Prescriptions/search?query={query}
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<PrescriptionDTO>>> SearchPrescriptions(string query)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                {
                    return await GetPrescriptions();
                }

                var normalizedQuery = query.Trim().ToLower();

                var prescriptions = await _context.Prescriptions
                    .Include(p => p.Visit)
                        .ThenInclude(v => v.Doctor)
                            .ThenInclude(d => d.Staff)
                    .Include(p => p.Visit)
                        .ThenInclude(v => v.Patient)
                    .Where(p =>
                        (p.Visit.Patient.FullName != null && p.Visit.Patient.FullName.ToLower().Contains(normalizedQuery)) ||
                        (p.Visit.Doctor.Staff.FullName != null && p.Visit.Doctor.Staff.FullName.ToLower().Contains(normalizedQuery)) ||
                        (p.Visit.Diagnosis != null && p.Visit.Diagnosis.ToLower().Contains(normalizedQuery)) ||
                        (p.Note != null && p.Note.ToLower().Contains(normalizedQuery))
                    )
                    .OrderByDescending(p => p.CreatedAt)
                    .Select(p => new PrescriptionDTO
                    {
                        PrescriptionId = p.PrescriptionId,
                        CreatedAt = p.CreatedAt ?? DateTime.MinValue,
                        Note = p.Note ?? "",
                        Diagnosis = p.Visit.Diagnosis ?? "",
                        VisitTime = p.Visit.CreatedAt ?? DateTime.MinValue,
                        DoctorName = p.Visit.Doctor.Staff.FullName ?? "N/A",
                        PatientName = p.Visit.Patient.FullName ?? "N/A",
                    })
                    .ToListAsync();

                return Ok(prescriptions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while searching prescriptions");
            }
        }
    }
}
