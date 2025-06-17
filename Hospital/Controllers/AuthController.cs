using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using Hospital.Data;
using Hospital.Models;
using Hospital.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly HospitalDbContext _context;
        private static readonly Dictionary<string, string> otpStore = new(); // Email → OTP
        private readonly EmailService _emailService;
        public AuthController(HospitalDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
                return BadRequest(new { message = "Username and password are required." });

            var user = await _context.UserAccount.FirstOrDefaultAsync(u =>
                u.username == model.Username &&
                u.role.ToUpper() == model.Role.ToUpper() &&
                u.is_active == true
            );

            if (user == null)
                return Unauthorized(new { message = "Invalid username, role or account inactive." });

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, user.password_hash);

            if (!isPasswordValid)
                return Unauthorized(new { message = "Incorrect password." });

            user.last_login = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Login successful",
                role = user.role,
                userId = user.user_id
            });
        }

        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOtp([FromBody] EmailRequest model)
        {
            if (string.IsNullOrWhiteSpace(model.Email))
                return BadRequest(new { message = "Email is required." });

            var user = await _context.UserAccount.FirstOrDefaultAsync(u => u.username == model.Email && u.is_active);
            if (user == null)
                return NotFound(new { message = "Email not found or inactive" });

            var otp = new Random().Next(100000, 999999).ToString();
            otpStore[model.Email] = otp;

            try
            {
                await _emailService.SendEmailAsync(
                    toEmail: model.Email,
                    subject: "Your OTP Code",
                    body: $"Your OTP code is: {otp}"
                );

                return Ok(new { message = "OTP sent" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Gửi mail thất bại: " + ex.ToString());
                return StatusCode(500, new { message = "Email failed", detail = ex.Message });
            }

        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Otp) || string.IsNullOrWhiteSpace(model.NewPassword))
                return BadRequest(new { message = "All fields are required." });

            if (!otpStore.TryGetValue(model.Email, out var storedOtp) || storedOtp != model.Otp)
                return BadRequest(new { message = "Invalid or expired OTP." });

            var user = await _context.UserAccount.FirstOrDefaultAsync(u => u.username == model.Email && u.is_active);
            if (user == null)
                return NotFound(new { message = "User not found or inactive." });

            user.password_hash = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            otpStore.Remove(model.Email);

            await _context.SaveChangesAsync();
            return Ok(new { message = "Password reset successful" });
        }
    }


    // --- Request models ---
    public class EmailRequest
    {
        [Required]
        public string Email { get; set; } = string.Empty;
    }

    public class ResetPasswordRequest
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Otp { get; set; } = string.Empty;

        [Required]
        public string NewPassword { get; set; } = string.Empty;
    }

    public class LoginModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;
    }
}

