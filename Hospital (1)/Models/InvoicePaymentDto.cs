namespace Hospital.Models
{
    public class InvoicePaymentDto
    {
        public string CustomerName { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public string InvoiceStatus { get; set; }
        public DateTime InvoiceDate { get; set; }
    }
}
