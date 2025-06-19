namespace HospitalManagement.Models;

public partial class MedicalVisit
{
    public Guid VisitId { get; set; }

    public Guid? AppointmentId { get; set; }

    public Guid? DoctorId { get; set; }

    public Guid? PatientId { get; set; }

    public string? Diagnosis { get; set; }

    public string? Note { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Appointment? Appointment { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual ICollection<ImagingRequest> ImagingRequests { get; set; } = new List<ImagingRequest>();

    public virtual ICollection<InvoiceConsultDetail> InvoiceConsultDetails { get; set; } = new List<InvoiceConsultDetail>();

    public virtual ICollection<InvoiceImageDetail> InvoiceImageDetails { get; set; } = new List<InvoiceImageDetail>();

    public virtual ICollection<InvoiceLabDetail> InvoiceLabDetails { get; set; } = new List<InvoiceLabDetail>();

    public virtual ICollection<InvoiceMedicineDetail> InvoiceMedicineDetails { get; set; } = new List<InvoiceMedicineDetail>();

    public virtual ICollection<LabRequest> LabRequests { get; set; } = new List<LabRequest>();

    public virtual Patient? Patient { get; set; }

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
