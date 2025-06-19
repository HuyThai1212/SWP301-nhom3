using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Models;

public partial class HospitalContext : DbContext
{
    public HospitalContext()
    {
    }

    public HospitalContext(DbContextOptions<HospitalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<ImagingRequest> ImagingRequests { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceConsultDetail> InvoiceConsultDetails { get; set; }

    public virtual DbSet<InvoiceImageDetail> InvoiceImageDetails { get; set; }

    public virtual DbSet<InvoiceLabDetail> InvoiceLabDetails { get; set; }

    public virtual DbSet<InvoiceMedicineDetail> InvoiceMedicineDetails { get; set; }

    public virtual DbSet<LabRequest> LabRequests { get; set; }

    public virtual DbSet<MedicalVisit> MedicalVisits { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<PrescriptionDetail> PrescriptionDetails { get; set; }

    public virtual DbSet<Receptionist> Receptionists { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Technician> Technicians { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=DESKTOP-K77M2S0;database=Hospital;uid=sa;pwd=123;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__A50828FC5D5542D3");

            entity.ToTable("Appointment");

            entity.Property(e => e.AppointmentId)
                .ValueGeneratedNever()
                .HasColumnName("appointment_id");
            entity.Property(e => e.ApprovalStatus)
                .HasMaxLength(50)
                .HasColumnName("approval_status");
            entity.Property(e => e.ApprovedAt)
                .HasColumnType("datetime")
                .HasColumnName("approved_at");
            entity.Property(e => e.ApprovedBy).HasColumnName("approved_by");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedRole)
                .HasMaxLength(50)
                .HasColumnName("created_role");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.ScheduledTime)
                .HasColumnType("datetime")
                .HasColumnName("scheduled_time");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK_Appointment_approved_by_Receptionist");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Appointment_created_by");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK_Appointment_doctor_id_Doctor");

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_Appointment_patient_id_Patient");

            entity.HasOne(d => d.Room).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK_Appointment_room_id_Room");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__D54EE9B4F6B6B2AF");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId)
                .ValueGeneratedNever()
                .HasColumnName("category_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__C2232422848C5B37");

            entity.ToTable("Department");

            entity.Property(e => e.DepartmentId)
                .ValueGeneratedNever()
                .HasColumnName("department_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Doctor__1963DD9C10B4B091");

            entity.ToTable("Doctor");

            entity.Property(e => e.StaffId)
                .ValueGeneratedNever()
                .HasColumnName("staff_id");
            entity.Property(e => e.LicenseNo).HasColumnName("license_no");
            entity.Property(e => e.Specialization).HasColumnName("specialization");

            entity.HasOne(d => d.Staff).WithOne(p => p.Doctor)
                .HasForeignKey<Doctor>(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Doctor_staff_id_Staff");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__7A6B2B8C0E028AEA");

            entity.ToTable("Feedback");

            entity.Property(e => e.FeedbackId)
                .ValueGeneratedNever()
                .HasColumnName("feedback_id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Score).HasColumnName("score");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK_Feedback_doctor_id_Doctor");

            entity.HasOne(d => d.Patient).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_Feedback_patient_id_Patient");
        });

        modelBuilder.Entity<ImagingRequest>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__ImagingR__DC9AC955D35DEE8B");

            entity.ToTable("ImagingRequest");

            entity.Property(e => e.ImageId)
                .ValueGeneratedNever()
                .HasColumnName("image_id");
            entity.Property(e => e.ImageType)
                .HasMaxLength(50)
                .HasColumnName("image_type");
            entity.Property(e => e.RequestedBy).HasColumnName("requested_by");
            entity.Property(e => e.ResultDescription).HasColumnName("result_description");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.VisitId).HasColumnName("visit_id");

            entity.HasOne(d => d.RequestedByNavigation).WithMany(p => p.ImagingRequests)
                .HasForeignKey(d => d.RequestedBy)
                .HasConstraintName("FK_ImagingRequest_requested_by_Staff");

            entity.HasOne(d => d.Room).WithMany(p => p.ImagingRequests)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK_ImagingRequest_room_id_Room");

            entity.HasOne(d => d.Visit).WithMany(p => p.ImagingRequests)
                .HasForeignKey(d => d.VisitId)
                .HasConstraintName("FK_ImagingRequest_visit_id_MedicalVisit");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoice__F58DFD4951F1D104");

            entity.ToTable("Invoice");

            entity.Property(e => e.InvoiceId)
                .ValueGeneratedNever()
                .HasColumnName("invoice_id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.IssuedDate)
                .HasColumnType("date")
                .HasColumnName("issued_date");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.Patient).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_Invoice_patient_id_Patient");
        });

        modelBuilder.Entity<InvoiceConsultDetail>(entity =>
        {
            entity.HasKey(e => e.InvoiceDetailConsultId).HasName("PK__InvoiceC__A040F87636D2BBF1");

            entity.ToTable("InvoiceConsultDetail");

            entity.Property(e => e.InvoiceDetailConsultId)
                .ValueGeneratedNever()
                .HasColumnName("invoice_detail_consult_id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.IsBilled).HasColumnName("is_billed");
            entity.Property(e => e.VisitId).HasColumnName("visit_id");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceConsultDetails)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("FK_InvoiceConsultDetail_invoice_id_Invoice");

            entity.HasOne(d => d.Visit).WithMany(p => p.InvoiceConsultDetails)
                .HasForeignKey(d => d.VisitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceConsultDetail_visit_id_MedicalVisit");
        });

        modelBuilder.Entity<InvoiceImageDetail>(entity =>
        {
            entity.HasKey(e => e.InvoiceDetailImageId).HasName("PK__InvoiceI__DA48D8D37742CC5E");

            entity.ToTable("InvoiceImageDetail");

            entity.Property(e => e.InvoiceDetailImageId)
                .ValueGeneratedNever()
                .HasColumnName("invoice_detail_image_id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ImageId).HasColumnName("image_id");
            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.IsBilled).HasColumnName("is_billed");
            entity.Property(e => e.VisitId).HasColumnName("visit_id");

            entity.HasOne(d => d.Image).WithMany(p => p.InvoiceImageDetails)
                .HasForeignKey(d => d.ImageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceImageDetail_image_id_ImagingRequest");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceImageDetails)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("FK_InvoiceImageDetail_invoice_id_Invoice");

            entity.HasOne(d => d.Visit).WithMany(p => p.InvoiceImageDetails)
                .HasForeignKey(d => d.VisitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceImageDetail_visit_id_MedicalVisit");
        });

        modelBuilder.Entity<InvoiceLabDetail>(entity =>
        {
            entity.HasKey(e => e.InvoiceDetailLabId).HasName("PK__InvoiceL__33AFC25BB2B270EF");

            entity.ToTable("InvoiceLabDetail");

            entity.Property(e => e.InvoiceDetailLabId)
                .ValueGeneratedNever()
                .HasColumnName("invoice_detail_lab_id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.IsBilled).HasColumnName("is_billed");
            entity.Property(e => e.LabId).HasColumnName("lab_id");
            entity.Property(e => e.VisitId).HasColumnName("visit_id");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceLabDetails)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("FK_InvoiceLabDetail_invoice_id_Invoice");

            entity.HasOne(d => d.Lab).WithMany(p => p.InvoiceLabDetails)
                .HasForeignKey(d => d.LabId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceLabDetail_lab_id_LabRequest");

            entity.HasOne(d => d.Visit).WithMany(p => p.InvoiceLabDetails)
                .HasForeignKey(d => d.VisitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceLabDetail_visit_id_MedicalVisit");
        });

        modelBuilder.Entity<InvoiceMedicineDetail>(entity =>
        {
            entity.HasKey(e => e.InvoiceDetailMedicineId).HasName("PK__InvoiceM__F58F1E3AB1D60300");

            entity.ToTable("InvoiceMedicineDetail");

            entity.Property(e => e.InvoiceDetailMedicineId)
                .ValueGeneratedNever()
                .HasColumnName("invoice_detail_medicine_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.IsBilled).HasColumnName("is_billed");
            entity.Property(e => e.MedicineId).HasColumnName("medicine_id");
            entity.Property(e => e.PrescriptionId).HasColumnName("prescription_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("total_amount");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("unit_price");
            entity.Property(e => e.VisitId).HasColumnName("visit_id");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceMedicineDetails)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("FK_InvoiceMedicineDetail_invoice_id_Invoice");

            entity.HasOne(d => d.Medicine).WithMany(p => p.InvoiceMedicineDetails)
                .HasForeignKey(d => d.MedicineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceMedicineDetail_medicine_id_Medicine");

            entity.HasOne(d => d.Prescription).WithMany(p => p.InvoiceMedicineDetails)
                .HasForeignKey(d => d.PrescriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceMedicineDetail_prescription_id_Prescription");

            entity.HasOne(d => d.Visit).WithMany(p => p.InvoiceMedicineDetails)
                .HasForeignKey(d => d.VisitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceMedicineDetail_visit_id_MedicalVisit");
        });

        modelBuilder.Entity<LabRequest>(entity =>
        {
            entity.HasKey(e => e.LabId).HasName("PK__LabReque__66DE64DBA61BF404");

            entity.ToTable("LabRequest");

            entity.Property(e => e.LabId)
                .ValueGeneratedNever()
                .HasColumnName("lab_id");
            entity.Property(e => e.RequestedBy).HasColumnName("requested_by");
            entity.Property(e => e.Result).HasColumnName("result");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.TestType)
                .HasMaxLength(50)
                .HasColumnName("test_type");
            entity.Property(e => e.VisitId).HasColumnName("visit_id");

            entity.HasOne(d => d.RequestedByNavigation).WithMany(p => p.LabRequests)
                .HasForeignKey(d => d.RequestedBy)
                .HasConstraintName("FK_LabRequest_requested_by_Staff");

            entity.HasOne(d => d.Room).WithMany(p => p.LabRequests)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK_LabRequest_room_id_Room");

            entity.HasOne(d => d.Visit).WithMany(p => p.LabRequests)
                .HasForeignKey(d => d.VisitId)
                .HasConstraintName("FK_LabRequest_visit_id_MedicalVisit");
        });

        modelBuilder.Entity<MedicalVisit>(entity =>
        {
            entity.HasKey(e => e.VisitId).HasName("PK__MedicalV__375A75E1FFB69111");

            entity.ToTable("MedicalVisit");

            entity.Property(e => e.VisitId)
                .ValueGeneratedNever()
                .HasColumnName("visit_id");
            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Diagnosis).HasColumnName("diagnosis");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");

            entity.HasOne(d => d.Appointment).WithMany(p => p.MedicalVisits)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK_MedicalVisit_appointment_id_Appointment");

            entity.HasOne(d => d.Doctor).WithMany(p => p.MedicalVisits)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK_MedicalVisit_doctor_id_Doctor");

            entity.HasOne(d => d.Patient).WithMany(p => p.MedicalVisits)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_MedicalVisit_patient_id_Patient");
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.MedicineId).HasName("PK__Medicine__E7148EBBDD2CBE60");

            entity.ToTable("Medicine");

            entity.Property(e => e.MedicineId)
                .ValueGeneratedNever()
                .HasColumnName("medicine_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Stock).HasColumnName("stock");
            entity.Property(e => e.Unit).HasColumnName("unit");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__Patient__4D5CE4765DFB147F");

            entity.ToTable("Patient");

            entity.Property(e => e.PatientId)
                .ValueGeneratedNever()
                .HasColumnName("patient_id");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("dob");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.FullName).HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .HasColumnName("gender");
            entity.Property(e => e.InsuranceId).HasColumnName("insurance_id");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Patients)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Patient_user_id_UserAccount");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__ED1FC9EAB1E450B0");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId)
                .ValueGeneratedNever()
                .HasColumnName("payment_id");
            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.PaidAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("paid_amount");
            entity.Property(e => e.PaidAt)
                .HasColumnType("datetime")
                .HasColumnName("paid_at");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasColumnName("payment_method");

            entity.HasOne(d => d.Invoice).WithMany(p => p.Payments)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("FK_Payment_invoice_id_Invoice");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("PK__Position__99A0E7A46A306C82");

            entity.ToTable("Position");

            entity.Property(e => e.PositionId)
                .ValueGeneratedNever()
                .HasColumnName("position_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Rank).HasColumnName("rank");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.HasOne(d => d.Category).WithMany(p => p.Positions)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Position_category_id_Category");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.PrescriptionId).HasName("PK__Prescrip__3EE444F803FB14C8");

            entity.ToTable("Prescription");

            entity.Property(e => e.PrescriptionId)
                .ValueGeneratedNever()
                .HasColumnName("prescription_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.VisitId).HasColumnName("visit_id");

            entity.HasOne(d => d.Visit).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.VisitId)
                .HasConstraintName("FK_Prescription_visit_id_MedicalVisit");
        });

        modelBuilder.Entity<PrescriptionDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PrescriptionDetail");

            entity.Property(e => e.Dosage).HasColumnName("dosage");
            entity.Property(e => e.MedicineId).HasColumnName("medicine_id");
            entity.Property(e => e.PrescriptionId).HasColumnName("prescription_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Medicine).WithMany()
                .HasForeignKey(d => d.MedicineId)
                .HasConstraintName("FK_PrescriptionDetail_medicine_id_Medicine");

            entity.HasOne(d => d.Prescription).WithMany()
                .HasForeignKey(d => d.PrescriptionId)
                .HasConstraintName("FK_PrescriptionDetail_prescription_id_Prescription");
        });

        modelBuilder.Entity<Receptionist>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Receptio__1963DD9CA1661429");

            entity.ToTable("Receptionist");

            entity.Property(e => e.StaffId)
                .ValueGeneratedNever()
                .HasColumnName("staff_id");
            entity.Property(e => e.Note).HasColumnName("note");

            entity.HasOne(d => d.Staff).WithOne(p => p.Receptionist)
                .HasForeignKey<Receptionist>(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Receptionist_staff_id_Staff");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Room__19675A8AB2EA532A");

            entity.ToTable("Room");

            entity.Property(e => e.RoomId)
                .ValueGeneratedNever()
                .HasColumnName("room_id");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.RoomName).HasColumnName("room_name");
            entity.Property(e => e.RoomType)
                .HasMaxLength(50)
                .HasColumnName("room_type");

            entity.HasOne(d => d.Department).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Room_department_id_Department");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Staff__1963DD9CDCDA1175");

            entity.Property(e => e.StaffId)
                .ValueGeneratedNever()
                .HasColumnName("staff_id");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("dob");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.FullName).HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.Department).WithMany(p => p.Staff)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Staff_department_id_Department");

            entity.HasOne(d => d.Position).WithMany(p => p.Staff)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK_Staff_position_id_Position");

            entity.HasOne(d => d.StaffNavigation).WithOne(p => p.Staff)
                .HasForeignKey<Staff>(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Staff_staff_id_UserAccount");
        });

        modelBuilder.Entity<Technician>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Technici__1963DD9CCAF1E87F");

            entity.ToTable("Technician");

            entity.Property(e => e.StaffId)
                .ValueGeneratedNever()
                .HasColumnName("staff_id");
            entity.Property(e => e.Qualification).HasColumnName("qualification");
            entity.Property(e => e.TechType)
                .HasMaxLength(50)
                .HasColumnName("tech_type");

            entity.HasOne(d => d.Staff).WithOne(p => p.Technician)
                .HasForeignKey<Technician>(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Technician_Staff");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserAcco__B9BE370F106D9B97");

            entity.ToTable("UserAccount");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.LastLogin)
                .HasColumnType("datetime")
                .HasColumnName("last_login");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Username).HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
