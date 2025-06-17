using Hospital.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Hospital.Data
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
        : base(options)
        {
        }
        public DbSet<UserAccount> UserAccount { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Patient> Patient { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>().HasKey(u => u.user_id);
            modelBuilder.Entity<Staff>().HasKey(s => s.staff_id);
            modelBuilder.Entity<Patient>().HasKey(p => p.patient_id);
        }
    }
}