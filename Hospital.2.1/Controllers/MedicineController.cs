using Hospital.Data;
using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public MedicineController(HospitalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _context.Medicines.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _context.Medicines.FindAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Medicine medicine)
        {
            medicine.Medicine_Id = Guid.NewGuid();
            _context.Medicines.Add(medicine);
            await _context.SaveChangesAsync();
            return Ok(medicine);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Medicine medicine)
        {
            var existing = await _context.Medicines.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Name = medicine.Name;
            existing.Unit = medicine.Unit;
            existing.Price = medicine.Price;
            existing.Stock = medicine.Stock;
            existing.Description = medicine.Description;

            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _context.Medicines.FindAsync(id);
            if (item == null) return NotFound();

            _context.Medicines.Remove(item);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string name)
        {
            var listSearch = await _context.Medicines
                .Where(m => m.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();

            return Ok(listSearch);
        }
    }
}
