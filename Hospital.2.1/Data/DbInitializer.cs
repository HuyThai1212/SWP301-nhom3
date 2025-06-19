using Hospital.Data;
using Hospital.Models;

public static class DbInitializer
{
    public static void Initialize(HospitalDbContext context)
    {
        context.Database.EnsureCreated();

        if (!context.UserAccount.Any())
        {
            var users = new UserAccount[]
            {
                new UserAccount
                {
                    user_id = Guid.NewGuid(),
                    username = "admin@hospital.com",
                    password_hash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    role = "ADMIN",
                    is_active = true
                },
                new UserAccount
                {
                    user_id = Guid.NewGuid(),
                    username = "pharmacist@hospital.com",
                    password_hash = BCrypt.Net.BCrypt.HashPassword("Pharma123!"),
                    role = "PHARMACIST",
                    is_active = true
                },
                // Thêm các user khác...
            };

            context.UserAccount.AddRange(users);
            context.SaveChanges();
        }
    }
}