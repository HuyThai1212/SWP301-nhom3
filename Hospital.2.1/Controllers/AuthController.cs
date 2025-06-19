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
        private static readonly Dictionary<string, OtpData> otpStore = new(); // Email → OTP
        private readonly EmailService _emailService;
        public AuthController(HospitalDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
                    return BadRequest(new { message = "Username and password are required." });

                // Tìm user không phân biệt hoa thường
                var user = await _context.UserAccount
                    .FirstOrDefaultAsync(u =>
                        u.username.ToLower() == model.Username.ToLower() &&
                        u.is_active);

                if (user == null)
                    return Unauthorized(new { message = "Invalid username or inactive account." });

                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, user.password_hash);

                if (!isPasswordValid)
                    return Unauthorized(new { message = "Incorrect password." });

                user.last_login = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Login successful",
                    role = user.role,
                    userId = user.user_id,
                    username = user.username
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex}");
                return StatusCode(500, new { message = "Internal server error", detail = ex.Message });
            }
        }

        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOtp([FromBody] EmailRequest model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) || !new EmailAddressAttribute().IsValid(model.Email))
                return BadRequest(new { message = "Invalid email format" });
            try
            {
                // Case-insensitive email comparison
                var user = await _context.UserAccount
                    .FirstOrDefaultAsync(u =>
                        u.username.ToLower() == model.Email.ToLower() &&
                        u.is_active);

                if (user == null)
                    return NotFound(new { message = "Email not found or inactive" });

                var otp = new Random().Next(100000, 999999).ToString();

                // Store OTP with expiry (5 minutes)
                otpStore[model.Email] = new OtpData
                {
                    Code = otp,
                    Expiry = DateTime.UtcNow.AddMinutes(5)
                };

                await _emailService.SendEmailAsync(
                    toEmail: model.Email,
                    subject: "Your OTP Code",
                    body: $"Your OTP code is: {otp}\nValid for 5 minutes"
                );

                return Ok(new { message = "OTP sent successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"OTP Error: {ex}");
                return StatusCode(500, new
                {
                    message = "Failed to send OTP",
                    detail = ex.Message
                });
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Email) ||
                   string.IsNullOrWhiteSpace(model.Otp) ||
                   string.IsNullOrWhiteSpace(model.NewPassword))
                {
                    return BadRequest(new { message = "All fields are required." });
                }

                // Kiểm tra OTP
                if (!otpStore.TryGetValue(model.Email, out var storedOtp) ||
                    storedOtp.Code != model.Otp ||
                    storedOtp.Expiry < DateTime.UtcNow)
                {
                    return BadRequest(new { message = "Invalid or expired OTP." });
                }

                var user = await _context.UserAccount
                    .FirstOrDefaultAsync(u => u.username == model.Email && u.is_active);

                if (user == null)
                {
                    return NotFound(new { message = "User not found or inactive." });
                }

                user.password_hash = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
                otpStore.Remove(model.Email);

                await _context.SaveChangesAsync();
                return Ok(new { message = "Password reset successful" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Reset password error: {ex}");
                return StatusCode(500, new { message = "Internal server error" });
            }
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
    }
    public class OtpData
    {
        public string? Code { get; set; }
        public DateTime Expiry { get; set; }
    }
}

