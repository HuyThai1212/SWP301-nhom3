using Hospital.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateAppointment([FromBody] AppointmentModel model)
        {
            Console.WriteLine($"📝 Appointment: Patient = {model.PatientId}, Doctor = {model.DoctorId}, Time = {model.ScheduledTime}");
            return Ok(new
            {
                message = "Appointment created successfully!",
                received = model
            });
        }
    }
}
