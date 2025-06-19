using Hospital.Data;
using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Controllers
{
    [Route("api/invoice")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public InvoiceController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: api/invoice/user/{username}
        [HttpGet("user/{username}")]
        public async Task<IActionResult> GetInvoicesByUsername(string username)
        {
            var user = await _context.UserAccount.FirstOrDefaultAsync(u => u.username == username && u.is_active);
            if (user == null) return NotFound(new { message = "User not found" });

            var patient = await _context.Patient.FirstOrDefaultAsync(p => p.user_id == user.user_id);
            if (patient == null) return NotFound(new { message = "Patient not found" });

            var invoices = await _context.Invoice
                .Where(i => i.patient_id == patient.patient_id)
                .ToListAsync();

            return Ok(invoices);
        }

        // POST: api/invoice/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceModel model)
        {
            var user = await _context.UserAccount.FirstOrDefaultAsync(u => u.username == model.pharmacist && u.is_active);
            if (user == null) return BadRequest(new { message = "Invalid pharmacist username" });

            var patient = await _context.Patient.FirstOrDefaultAsync(p => p.full_name == model.patient);
            if (patient == null) return BadRequest(new { message = "Patient not found" });

            var invoice = new Invoice
            {
                invoice_id = Guid.NewGuid(),
                patient_id = patient.patient_id,
                amount = model.total,
                status = "PAID",
                issued_date = DateTime.Today
            };

            _context.Invoice.Add(invoice);
            await _context.SaveChangesAsync();

            return Ok(invoice);
        }
    }

    public class CreateInvoiceModel
    {
        public string? pharmacist { get; set; }
        public string? patient { get; set; }
        public string? medicines { get; set; }
        public decimal total { get; set; }
    }
}
