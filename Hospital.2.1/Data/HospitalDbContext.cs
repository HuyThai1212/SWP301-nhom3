using Hospital.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Numerics;

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
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<Medicine> Medicines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.HasKey(e => e.user_id);
                entity.Property(e => e.username).IsRequired();
                entity.Property(e => e.password_hash).IsRequired();
                entity.Property(e => e.role).IsRequired();
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.patient_id);
                entity.HasOne<UserAccount>()
                    .WithMany()
                    .HasForeignKey(p => p.user_id);
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.HasKey(e => e.staff_id);
                entity.HasOne<UserAccount>()
                    .WithMany()
                    .HasForeignKey(s => s.user_id);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.invoice_id);
                entity.HasOne<Patient>()
                    .WithMany()
                    .HasForeignKey(i => i.patient_id);
            });
        }
    }
}